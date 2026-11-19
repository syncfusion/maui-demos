using System.Reflection;

namespace SampleBrowser.Maui.Cards
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            var appInfo = typeof(App).GetTypeInfo().Assembly;
            SampleBrowser.Maui.Base.BaseConfig.IsIndividualSB = true;
            return new Window(SampleBrowser.Maui.Base.BaseConfig.MainPageInit(appInfo));
        }
    }
}
