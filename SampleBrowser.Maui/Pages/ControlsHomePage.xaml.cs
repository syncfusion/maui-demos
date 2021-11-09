#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System.Linq;
using SampleBrowser.Maui.Core;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics;

namespace SampleBrowser.Maui
{
    public partial class ControlsHomePage : ContentPage
    {
        #region fields

        private Label loadingLabel;

        #endregion

        #region ctor

        public ControlsHomePage()
        {
            InitializeComponent();
            loadingLabel = new Label { Text = "Loading Examples...",BackgroundColor=Colors.White, TextColor= Colors.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };

            NavigationPage.SetBackButtonTitle(this, "Back");

        }

        #endregion
        View pageContent;
        #region methods
        private async void TapGestureTapped(object sender, System.EventArgs eventArgs )
        {
            this.backButton.Source = "back";
            ControlModel cModel = (sender as Grid).BindingContext as ControlModel;
            
            if (cModel is ControlModel controlModel)
            {
                var samplesData = new ObservableCollection<SampleModel>(controlModel.Samples);
                string controlName = controlModel.Title, assemblyName = null;

                assemblyName = "SampleBrowser";

                ContentPage page;

                var themeSample = samplesData.LastOrDefault(sample => sample.Name == "Themes");
                if (themeSample != null && samplesData.Count > 1)
                {
                    samplesData.Remove(themeSample);
                }

                page = Core.SampleBrowser.GetSamplesPage(samplesData, assemblyName, controlModel.ControlName, controlModel.Title);

                page.Title = controlModel.Title;
                this.titleLabel.Text = controlModel.Title;
                page.BackgroundColor = Colors.White;
                if (Device.RuntimePlatform == "Android")
                {
                    pageContent = page.Content;
                    //  page.Content = loadingLabel;
                    //   await Navigation.PushAsync(page);

                    this.controlsStack.IsVisible = false;
                    this.ContentArea.Children.Add(pageContent);
                    //this.Content = pageContent;
                }
                else
                {
                    //await Navigation.PushAsync(page);
                    this.ContentArea.Children.Add(pageContent);
                }
            }
        }

        private string RemoveSpaces(string searchText)
        {
            return new string(searchText?.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLower();
        }

        #endregion

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            this.controlsStack.IsVisible = true;
            this.backButton.Source = "logo";
            this.titleLabel.Text = " .NET MAUI Controls";
            this.ContentArea.Children.Remove(pageContent);
        }
    }
}
