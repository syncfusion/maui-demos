namespace SampleBrowser.Maui.LiquidGlass
{
    using System.Reflection;
    using SampleBrowser.Maui.Base;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            BaseConfig.IsIndividualSB = true;
            BaseConfig.IsLiquidSB = true;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;
            var mainPage = BaseConfig.MainPageInit(assembly);
            return new Window(mainPage);
        }
    }
}