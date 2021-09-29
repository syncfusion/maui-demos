using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Graphics;
using System.Reflection;
using CoreEventArgs = SampleBrowser.Maui.Core.SelectedIndexChangedEventArgs;

namespace SampleBrowser.Maui.Core
{
#nullable disable
    public partial class SamplePage : ContentPage
    {
        #region Fields

        public Type SampleType;

        ObservableCollection<SampleModel> topListViewItemsSource, bottomListViewItemsSource;

        int topScrollIndex, bottomScrollIndex;

        string sampleName, controlName;

        Type previousSample;

        SampleView sampleView;

        #endregion

        #region Properties

        public ObservableCollection<SampleModel> Samples { get; set; }

        public Dictionary<string, ObservableCollection<SampleModel>> SamplesGroupCollection { get; set; }

        public Dictionary<string, ObservableCollection<SampleModel>> SubSamplesGroupCollection { get; set; }

        private object selectedItem = null;

        /// <summary>
        /// Gets or sets selected
        /// </summary>
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }

            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        #endregion

        #region Constructor

        public SamplePage(object samples, string cntrlName, SampleModel sample, string title = null)
        {
            InitializeComponent();

            Title = string.IsNullOrEmpty(title) ? cntrlName : title;
            controlName = cntrlName;

            topListViewItemsSource = new ObservableCollection<SampleModel>();
            bottomListViewItemsSource = new ObservableCollection<SampleModel>();
            SamplesGroupCollection = new Dictionary<string, ObservableCollection<SampleModel>>();
            Samples = samples as ObservableCollection<SampleModel>;

            GenerateItemsSource(sample);
            LoadSample();
            NavigationPage.SetBackButtonTitle(this, "Back");
        }

        #endregion

        #region Methods

        protected override void OnAppearing()
        {
            if (this.sampleView != null)
            {
                this.sampleView.OnAppearing();
            }
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        void GenerateItemsSource(SampleModel sample)
        {
            List<string> categories = Samples.Select(X => X.Category).Distinct().ToList();

            foreach (string category in categories!)
            {
                var samplesGroup = Samples.Where(X => X.Category == category).ToList();
                ObservableCollection<SampleModel> samplesList = new ObservableCollection<SampleModel>();
                foreach (var item in samplesGroup!)
                    samplesList.Add(item);
                SamplesGroupCollection.Add(category!, samplesList);
            }

            if (SamplesGroupCollection.Count > 1)
            {
                if (SamplesGroupCollection.ContainsKey("None"))
                    topListViewItemsSource = SamplesGroupCollection["None"];

                for (int i = 0; i < categories.Count; i++)
                {
                    string category = categories[i]!;

                    if (category == "None") continue;

                    if (SamplesGroupCollection[category].Contains(sample))
                    {
                        bottomScrollIndex = SamplesGroupCollection[category].IndexOf(sample);
                        topScrollIndex = i;
                    }

                    topListViewItemsSource?.Insert(i, new SampleModel() { Title = category, Control = controlName });
                }
            }
            else
                topListViewItemsSource = Samples;

            if (topListViewItemsSource!.Contains(sample))
                topScrollIndex = topListViewItemsSource.IndexOf(sample);

            TabBar.ItemsSource = topListViewItemsSource;
        }

        void LoadSample()
        {
            if (topListViewItemsSource != null)
            {
                var topSampleModel = topListViewItemsSource[topScrollIndex];
                sampleName = topSampleModel.Name;

                if (sampleName == null)
                {
                    string category = topSampleModel.Title;
                    bottomListViewItemsSource = SamplesGroupCollection?[category!];
                    var bottomSampleModel = bottomListViewItemsSource?[bottomScrollIndex];
                    sampleName = bottomSampleModel?.Name;
                    bottomSampleListView.ItemsSource = bottomListViewItemsSource;
                    bottomSampleListView.SelectedItem = bottomSampleModel;
                    bottomSampleModel.BackgroundColor = Color.FromArgb("EAEAEA");
                }

                CreateInstance(sampleName);

                if (topListViewItemsSource.Count <= 1)
                {
                    mainGrid.Children.Remove(TabBar);
                    topSampleLVRow.Height = 0;
                }

                if (bottomListViewItemsSource?.Count > 0 && bottomListViewItemsSource != null)
                {
                    bottomBoxView.Height = 7;
                    bottomSampleLVRow.Height = 38;
                }
            }
        }

        public static Type GetAssembliesType(string assemblyName, string sampleName, string controlName)
        {
            Assembly assm = Assembly.Load(assemblyName + ", Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            return assm.GetType(assemblyName + "." + controlName + "." + sampleName);
        }

        internal void CreateInstance(string sampleName)
        {
            if (!SampleBrowser.IsIndividualSB)
                SampleType = GetAssembliesType("SampleBrowser.Maui", sampleName, controlName);
            else
                SampleType = GetAssembliesType("SampleBrowser." + controlName, sampleName, string.Empty);

            if (SampleType != null)
            {
                previousSample = SampleType;
                SampleLoaded?.Invoke(null, new SampleLoadedEventArgs(SampleType!.FullName!));
                sampleView = Activator.CreateInstance(SampleType!) as SampleView;
                Microsoft.Maui.Controls.Grid.SetRow(sampleView, 4);

                mainGrid.Children.Add(sampleView);
                sampleView.OnAppearing();
            }
        }

        private void TabBar_SelectedIndexChanged(object sender, CoreEventArgs e)
        { 
            int index = (int)e.NewIndex;

            if (topScrollIndex == index) return;

            topScrollIndex = index;
            SampleModel sampleModel = TabBar.ItemsSource[index] as SampleModel;
            sampleName = sampleModel?.Name;

            if (previousSample?.Name != sampleName)
            {
                if (mainGrid != null)
                {
                    sampleView?.OnDisappearing();
                    mainGrid.Children.Remove(sampleView);
                    sampleView = null;
                }

                if (sampleName == null)
                {
                    string category = topListViewItemsSource[index].Title;
                    bottomListViewItemsSource = SamplesGroupCollection[category!];
                    var bottomSampleModel = bottomListViewItemsSource[0];
                    sampleName = bottomSampleModel.Name;

                    bottomSampleListView.ItemsSource = bottomListViewItemsSource;
                    foreach (var item in bottomListViewItemsSource)
                    {
                        (item as SampleModel).BackgroundColor = Colors.White;
                    }
                    bottomScrollIndex = 0;
                    bottomSampleModel.BackgroundColor = Color.FromArgb("EAEAEA");
                }
                else
                {
                    bottomSampleListView.ItemsSource = bottomListViewItemsSource = null;
                    bottomSampleLVRow.Height = bottomBoxView.Height = 0;
                }

                CreateInstance(sampleName);

                if (bottomListViewItemsSource != null)
                {
                        bottomSampleLVRow.Height = 38;
                        emptySpaceRow.Height = 17;
                }
            }
        }

        private void TapGestureTapped(object sender, System.EventArgs eventArgs)
        {
            foreach (var item in bottomListViewItemsSource)
            {
                (item as SampleModel).BackgroundColor = Colors.White;
            }

            SampleModel sModel = (sender as Grid).BindingContext as SampleModel;
            sModel.BackgroundColor = Color.FromArgb("#EAEAEA");
            int index = bottomListViewItemsSource!.IndexOf(sModel!);

            if (bottomScrollIndex == index) return;

            bottomScrollIndex = index;
            bottomSampleListView.ScrollTo(index, -1, ScrollToPosition.Center, true);
            SampleModel sampleModel = sModel;

            if (sampleModel != null)
            {
                sampleName = sampleModel.Name;

                if (previousSample?.Name != sampleName)
                {
                    if (mainGrid != null)
                    {
                        if (sampleView != null)
                        {
                            sampleView.OnDisappearing();
                            mainGrid.Children.Remove(sampleView);
                            sampleView = null;
                        }
                    }

                    CreateInstance(sampleName);
                }
            }
        }

        #endregion

        #region events

        /// <summary>
        /// Triggered when loading a sample.
        /// </summary>
        public static event EventHandler<SampleLoadedEventArgs> SampleLoaded;


        #endregion
    }

    #region SampleLoadedEventArgs

    /// <summary>
    /// Event argument for sample loaded event.
    /// </summary>
    public class SampleLoadedEventArgs : EventArgs
    {
        #region ctor

        /// <summary>
        /// Event hooked when Samples Loaded in HockeyApp Integration.
        /// </summary>
        public SampleLoadedEventArgs(string sampleName)
        {
            SampleName = sampleName;
        }

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets SampleName.
        /// </summary>
        public string SampleName { get; set; }

        #endregion
    }

    #endregion
}
