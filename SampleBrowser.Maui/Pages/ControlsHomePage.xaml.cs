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

        private bool isListItemClicked;

        #endregion

        #region ctor

        public ControlsHomePage()
        {
            InitializeComponent();
            loadingLabel = new Label { Text = "Loading Examples...",BackgroundColor=Colors.White, TextColor= Colors.Black, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };

            NavigationPage.SetBackButtonTitle(this, "Back");

        }

        #endregion

        #region methods
        private async void TapGestureTapped(object sender, System.EventArgs eventArgs )
        {
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
                page.BackgroundColor = Colors.White;
                if (Device.RuntimePlatform == "Android")
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

        private string RemoveSpaces(string searchText)
        {
            return new string(searchText?.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray()).ToLower();
        }

        #endregion
    }
}
