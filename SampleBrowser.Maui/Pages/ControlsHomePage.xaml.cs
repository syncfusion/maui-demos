#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using MauiCore = Syncfusion.Maui.Core;

namespace SampleBrowser.Maui
{
    public partial class ControlsHomePage : ContentPage
    {
        #region fields

        private readonly Label loadingLabel;

        internal bool IsDesktopMode = true;

        #endregion

        #region ctor

        public ControlsHomePage()
        {
            InitializeComponent();
            loadingLabel = new Label { Text = "Loading Examples...", BackgroundColor = Colors.White, TextColor = Colors.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };

            NavigationPage.SetBackButtonTitle(this, "Back");

            if (RunTimeDevice.IsMobileDevice())
            {
                this.IsDesktopMode = false;
                this.MainGrid.BackgroundColor = Colors.Transparent;
            }

            if (this.IsDesktopMode)
            {
                this.MobilePanelGrid.IsVisible = false;
                this.DesktopPanelGrid.IsVisible = true;
                this.SamplePageViewGrid.IsVisible = this.IsDesktopMode;
                if (RunTimeDevice.PlatformInfo == "Windows")
                {
                    this.MainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 300 });
                }
                else
                {
                    this.MainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 335 });
                }
                this.MainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = Microsoft.Maui.GridLength.Star });

                if (RunTimeDevice.PlatformInfo == "MacCatalyst")
                {
                    this.fileFormatGrid.IsVisible = false;
                    this.LayoutsMainGrid.IsVisible = true;

                    this.dataVisualization.RowDefinitions.Clear();
                    this.dataVisualization.RowDefinitions.Add(new RowDefinition() { Height = Microsoft.Maui.GridLength.Auto });

                    this.calendar.RowDefinitions.Clear();
                    this.calendar.RowDefinitions.Add(new RowDefinition() { Height = Microsoft.Maui.GridLength.Auto });

                    this.navigation.RowDefinitions.Clear();
                    this.navigation.RowDefinitions.Add(new RowDefinition() { Height = Microsoft.Maui.GridLength.Auto });

                    this.sliders.RowDefinitions.Clear();
                    this.sliders.RowDefinitions.Add(new RowDefinition() { Height = Microsoft.Maui.GridLength.Auto });

                    this.notification.RowDefinitions.Clear();
                    this.notification.RowDefinitions.Add(new RowDefinition() { Height = Microsoft.Maui.GridLength.Auto });

                    this.micellaneous.RowDefinitions.Clear();
                    this.micellaneous.RowDefinitions.Add(new RowDefinition() { Height = Microsoft.Maui.GridLength.Auto });

                    this.fileFormat.RowDefinitions.Clear();
                    this.fileFormat.RowDefinitions.Add(new RowDefinition() { Height = Microsoft.Maui.GridLength.Auto });

                    this.layouts.RowDefinitions.Clear();
                    this.layouts.RowDefinitions.Add(new RowDefinition() { Height = Microsoft.Maui.GridLength.Auto });
                }

                this.LoadDesktopSample(this.DataVisualizationLayout.Children[0]);
            }
        }

        #endregion

        #region methods

        Grid? selectedGrid = null;

        private async void TapGestureTapped(object sender, System.EventArgs eventArgs)
        {
            if (this.IsDesktopMode)
            {
                this.LoadDesktopSample(sender);
                return;
            }

            ControlModel? cModel = (ControlModel)((Grid)sender).BindingContext;

            if (cModel is ControlModel controlModel)
            {
                var samplesData = new ObservableCollection<SampleModel>(controlModel.Samples);
                string? controlName = controlModel.Title, assemblyName = null;

                assemblyName = "SampleBrowser";

                ContentPage? page = null;

                var themeSample = samplesData.LastOrDefault(sample => sample.Name == "Themes");
                if (themeSample != null && samplesData.Count > 1)
                {
                    samplesData.Remove(themeSample);
                }

                if (controlModel.ControlName != null && controlModel.Title != null)
                {
                    page = Core.SampleBrowser.GetSamplesPage(samplesData, assemblyName, controlModel.ControlName, controlModel.Title);
                    page.Title = controlModel.Title;
                    page.BackgroundColor = Colors.White;

                    if (DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        var content = page.Content;
                        page.Content = loadingLabel;
                        await Navigation.PushAsync(page);
                        page.Content = content;
                    }
                    else
                    {
                        await Navigation.PushAsync(page);
                    }
                }
            }
        }

        private void LoadDesktopSample(object sender)
        {
            ControlModel? cModel = null;

            Grid? senderGrid = null;

            if (sender is Syncfusion.Maui.Core.SfBadgeView)
            {
                cModel = (sender as Syncfusion.Maui.Core.SfBadgeView)?.BindingContext as ControlModel;
                senderGrid = (sender as Syncfusion.Maui.Core.SfBadgeView)?.Content as Grid;
            }
            else if (sender is Grid)
            {
                cModel = (sender as Grid)?.BindingContext as ControlModel;
                senderGrid = (sender as Grid);
            }

            if (cModel is ControlModel controlModel)
            {
                var samplesData = new ObservableCollection<SampleModel>(controlModel.Samples);
                string? controlName = controlModel.Title, assemblyName = null;

                assemblyName = "SampleBrowser";

                ContentPage page;

                var themeSample = samplesData.LastOrDefault(sample => sample.Name == "Themes");
                if (themeSample != null && samplesData.Count > 1)
                {
                    samplesData.Remove(themeSample);
                }

                if (controlModel.ControlName != null && controlModel.Title != null)
                {
                    page = Core.SampleBrowser.GetSamplesPage(samplesData, assemblyName, controlModel.ControlName, controlModel.Title);
                    page.Title = controlModel.Title;
                    page.BackgroundColor = Colors.White;
                    var content = page.Content;
                    page.Content = loadingLabel;

                    if (page is SamplePage)
                    {
                        this.SamplePageView.Children.Clear();
                        this.SamplePageView.Children.Add(content);
                    }
                }

                if (this.selectedGrid != null)
                {
                    this.selectedGrid.BackgroundColor = Colors.Transparent;
                    ((ContentView)this.selectedGrid.Children[0]).BackgroundColor = Colors.Transparent;
                }

                this.selectedGrid = senderGrid;

                if (this.selectedGrid != null)
                {
                    this.selectedGrid.BackgroundColor = Color.FromRgb(241, 232, 255);
                    ((ContentView)this.selectedGrid.Children[0]).BackgroundColor = Color.FromRgb(98, 0, 238);
                }

                this.SelectedControlLabel.Text = cModel.Title;
            }
        }

        #endregion
    }
}
