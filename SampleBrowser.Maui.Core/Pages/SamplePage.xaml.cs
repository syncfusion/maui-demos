#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Graphics;
using System.Reflection;
using Syncfusion.Maui.TabView;
using System.Threading.Tasks;

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

            Label loadingLabel = new Label() { Text = "Loading Examples...", TextColor = Colors.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, VerticalTextAlignment = Microsoft.Maui.TextAlignment.Center, HorizontalTextAlignment = Microsoft.Maui.TextAlignment.Center };
            this.loadingSamplesView.Add(loadingLabel);
            this.loadingSamplesView.BackgroundColor = Colors.White;
            Grid.SetRow(this.loadingSamplesView, 4);
        }


        #endregion

        #region Methods

        public void HideOptionButton()
        {
        }

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

            if (SamplesGroupCollection.Count >= 1)
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

            foreach (var item in topListViewItemsSource)
            {
                var tabItem = new Syncfusion.Maui.TabView.SfTabItem();
                tabItem.Header = item.Title;
                 this.tabView.Items.Add(tabItem);
            }
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
                    BindableLayout.SetItemsSource(bottomSampleListView, bottomListViewItemsSource);
                    ClearChipSelection();
                    bottomSampleModel.Opacity = 1;
                }

                CreateInstance(sampleName);

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

        public static string CurrentBrowser = string.Empty;
        public static string  CurrentSampleName= string.Empty;
        public static string CurrentControlName = string.Empty;

        internal void CreateInstance(string sampleName)
        {
            if (!SampleBrowser.IsIndividualSB)
            {
                SampleType = GetAssembliesType("SampleBrowser.Maui", sampleName, controlName);
                CurrentBrowser = "SampleBrowser.Maui";
                CurrentSampleName = sampleName;
                CurrentControlName = controlName;
            }
            else
                SampleType = GetAssembliesType("SampleBrowser." + controlName, sampleName, string.Empty);

            if (SampleType != null)
            {
                previousSample = SampleType;
                sampleView = Activator.CreateInstance(SampleType!) as SampleView;
                Microsoft.Maui.Controls.Grid.SetRow(sampleView, 4);
                mainGrid.Children.Remove(this.loadingSamplesView);
                mainGrid.Children.Add(sampleView);
                sampleView.OnAppearing();
            }
        }

        Grid loadingSamplesView = new Grid();

        private async void TabBar_SelectedIndexChanged(object sender, TabSelectionChangedEventArgs e)
        {
            this.ClearChipSelection();
            this.HideOptionButton();
            int index = (int)e.NewIndex;

            if (topScrollIndex == index) return;

            topScrollIndex = index;

            
            SampleModel sampleModel = null;
            foreach (var item in topListViewItemsSource)
            {
              if( (tabView.Items[index] as SfTabItem).Header == item.Title)
                {
                    sampleModel = item;
                }
            }
            sampleName = sampleModel?.Name;

            if (previousSample?.Name != sampleName)
            {
                if (mainGrid != null)
                {
                    sampleView?.OnDisappearing();
                    mainGrid.Children.Remove(sampleView);
                    mainGrid.Children.Add(this.loadingSamplesView);
                    if (sampleView != null)
                    {
                        sampleView.Handler?.DisconnectHandler();
                        sampleView = null;
                    }
                }

               

                await Task.Delay(100);

                string category = topListViewItemsSource[index].Title;
                if (sampleName == null)
                {
                    bottomListViewItemsSource = SamplesGroupCollection[category!];
                    // Added the clear method again , its not clearing some cases, need to validate it later and remove from here.
                    this.ClearChipSelection();
                    var bottomSampleModel = bottomListViewItemsSource[0];
                    sampleName = bottomSampleModel.Name;
                    BindableLayout.SetItemsSource(bottomSampleListView, bottomListViewItemsSource);
                    foreach (var item in bottomListViewItemsSource)
                    {
                        (item as SampleModel).BackgroundColor = Colors.White;
                    }
                    bottomScrollIndex = 0;
                    bottomSampleModel.Opacity = 1;
                }
                else
                {
                    BindableLayout.SetItemsSource(bottomSampleListView, null);
                    bottomSampleLVRow.Height = bottomBoxView.Height = 0;

                }

                CreateInstance(sampleName);

                if (bottomListViewItemsSource != null && SamplesGroupCollection.ContainsKey(category!))
                {
                        bottomSampleLVRow.Height = 38;
                        emptySpaceRow.Height = 17;
                }
            }
        }

        BoxView previousBoxView = null;

        private void SelectionColorUpdate(Grid modelGrid)
        {
            //TODO: Background color property is not working in BoxView dynamically to this Workaround is added (In preivew 10)
                   
            if (modelGrid.Children[2] is BoxView)
            {

                var selectionBoxView = modelGrid.Children[2] as BoxView;
                if (selectionBoxView.Opacity != 1)
                {
                    (selectionBoxView).Opacity = 1;
                    if (previousBoxView != null)
                    {
                        previousBoxView.Opacity = 0;
                    }
                    previousBoxView = selectionBoxView;
                }
            }
        }

        private void ClearChipSelection()
        {
            if (bottomListViewItemsSource != null)
            {
                foreach (var item in bottomListViewItemsSource)
                {
                    (item as SampleModel).Opacity = 0;
                }
            }
        }

        private void TapGestureTapped(object sender, System.EventArgs eventArgs)
        {
            this.ClearChipSelection();

            this.HideOptionButton();

            var modelGrid = (sender as Grid);
            SampleModel sModel = modelGrid.BindingContext as SampleModel;
            sModel.Opacity = 1;
            int index = bottomListViewItemsSource!.IndexOf(sModel!);

            if (bottomScrollIndex == index) return;

            bottomScrollIndex = index;
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
                            if (sampleView != null)
                            {
                                sampleView.Handler?.DisconnectHandler();
                                sampleView = null;
                            }
                        }
                    }

                    CreateInstance(sampleName);
                }
            }
        }

        #endregion
    }
}
