#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Syncfusion.Maui.TabView;
using System.Collections.ObjectModel;
using System.Reflection;

namespace SampleBrowser.Maui.Core
{
    public partial class SamplePage : ContentPage
    {
        #region Fields

        public Type? SampleType;

        ObservableCollection<SampleModel>? topListViewItemsSource, bottomListViewItemsSource;

        int topScrollIndex, bottomScrollIndex;
        private string? sampleName;
        private readonly string? controlName;
        Type? previousSample;

        SampleView? sampleView;


        #endregion

        #region Properties

        public static string? CurrentBrowser { get; set; }
        public static string? CurrentSampleName { get; set; }
        public static string? CurrentControlName { get; set; }

        public ObservableCollection<SampleModel>? Samples { get; set; }

        public Dictionary<string, ObservableCollection<SampleModel>> SamplesGroupCollection { get; set; }

        public Dictionary<string, ObservableCollection<SampleModel>>? SubSamplesGroupCollection { get; set; }

        private object? selectedItem = null;

        /// <summary>
        /// Gets or sets selected
        /// </summary>
        public object? SelectedItem
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

        public SamplePage(object? samples, string? cntrlName, SampleModel? sample, string? title = null)
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

            Label loadingLabel = new() { Text = "Loading Examples...", TextColor = Colors.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, VerticalTextAlignment = Microsoft.Maui.TextAlignment.Center, HorizontalTextAlignment = Microsoft.Maui.TextAlignment.Center };
            this.loadingSamplesView.Add(loadingLabel);
            this.loadingSamplesView.BackgroundColor = Colors.White;
            Grid.SetRow(this.loadingSamplesView, 4);
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

        void GenerateItemsSource(SampleModel? sample)
        {
            List<string?> categories = Samples!.Select(X => X.Category).Distinct().ToList();

            foreach (string? category in categories!)
            {
                var samplesGroup = Samples!.Where(X => X.Category == category).ToList();
                ObservableCollection<SampleModel> samplesList = new();
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

                    if (SamplesGroupCollection[category].Contains(sample!))
                    {
                        bottomScrollIndex = SamplesGroupCollection[category].IndexOf(sample!);
                        topScrollIndex = i;
                    }

                    var uType = SamplesGroupCollection[category][0].UpdateGroupType;
                    topListViewItemsSource?.Insert(i, new SampleModel() { Title = category, Control = controlName, UpdateType = uType });
                }
            }
            else
                topListViewItemsSource = Samples;

            if (topListViewItemsSource!.Contains(sample!))
                topScrollIndex = topListViewItemsSource.IndexOf(sample!);

            foreach (var item in topListViewItemsSource)
            {
                if (item != null)
                {
                    var tabItem = new Syncfusion.Maui.TabView.SfTabItem
                    {
                        Header = item.Title!
                    };
                    if (!string.IsNullOrEmpty(item.UpdateType))
                    {
                        if (item.UpdateType.Equals("new", StringComparison.OrdinalIgnoreCase))
                        {
                            tabItem.BadgeText = "N";
                        }
                        else
                        {
                            tabItem.BadgeText = "U";
                        }

                        tabItem.BadgeSettings = new Syncfusion.Maui.Core.BadgeSettings()
                        {
                            Type = Syncfusion.Maui.Core.BadgeType.None,
                            Background = new SolidColorBrush(Color.FromArgb("#6200EE")),
                            Position = Syncfusion.Maui.Core.BadgePosition.Right,
#if WINDOWS
                        TextPadding = 1,
                        FontSize = 10
#endif
                        };
                    }

                    this.tabView.Items.Add(tabItem);
                }
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
                    string? category = topSampleModel.Title;
                    bottomListViewItemsSource = SamplesGroupCollection?[category!];
                    var bottomSampleModel = bottomListViewItemsSource?[bottomScrollIndex];
                    sampleName = bottomSampleModel?.Name;
                    BindableLayout.SetItemsSource(bottomSampleListView, bottomListViewItemsSource);
                    ClearChipSelection();
                    bottomSampleModel!.BackgroundColor = Color.FromArgb("#EAEAEA");
                }

                CreateInstance(sampleName);

                if (bottomListViewItemsSource?.Count > 0 && bottomListViewItemsSource != null)
                {
                    bottomBoxView.Height = 7;
                    bottomSampleLVRow.Height = 38;
                }
            }
        }

        public static Type? GetAssembliesType(string? assemblyName, string? sampleName, string? controlName)
        {
            Assembly assm = Assembly.Load(assemblyName + ", Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            return assm.GetType(assemblyName + "." + controlName + "." + sampleName);
        }

        internal void CreateInstance(string? sampleName)
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
                sampleView!.OnAppearing();
            }
        }

        readonly Grid loadingSamplesView = new();

        private async void TabBar_SelectedIndexChanged(object sender, TabSelectionChangedEventArgs e)
        {
            this.ClearChipSelection();
            int index = (int)e.NewIndex;

            if (topScrollIndex == index) return;

            topScrollIndex = index;


            SampleModel? sampleModel = null;
            if (topListViewItemsSource != null)
            {
                foreach (var item in topListViewItemsSource)
                {
                    if ((tabView.Items[index] as SfTabItem).Header == item.Title)
                    {
                        sampleModel = item;
                    }
                }
            }
            sampleName = sampleModel?.Name;

            if (previousSample?.Name != sampleName)
            {
                if (mainGrid != null)
                {
                    sampleView?.OnDisappearing();
                    mainGrid.Children.Remove(sampleView);
                    if(this.loadingSamplesView.Parent !=null)
                    {
                        var parent = this.loadingSamplesView.Parent;
                        ((Grid)parent).Remove(this.loadingSamplesView);
                    }
                    mainGrid.Children.Add(this.loadingSamplesView);
                    if (sampleView != null)
                    {
                        sampleView.Handler?.DisconnectHandler();
                        sampleView = null;
                    }
                }



                await Task.Delay(100);
                if (topListViewItemsSource != null)
                {
                    string? category = topListViewItemsSource[index].Title;
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
                        bottomSampleModel.BackgroundColor = Color.FromArgb("#EAEAEA");
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
        }

        private void ClearChipSelection()
        {
            if (bottomListViewItemsSource != null)
            {
                foreach (var item in bottomListViewItemsSource)
                {
                    (item as SampleModel).BackgroundColor = Colors.White;
                }
            }
        }

        private void TapGestureTapped(object sender, System.EventArgs eventArgs)
        {
            this.ClearChipSelection();

            var modelGrid = (sender as Grid);
            SampleModel? sModel = modelGrid!.BindingContext as SampleModel;
            sModel!.BackgroundColor = Color.FromArgb("#EAEAEA");
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
