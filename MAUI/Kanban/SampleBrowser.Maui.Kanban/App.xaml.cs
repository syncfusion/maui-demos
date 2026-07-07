namespace SampleBrowser.Maui.Kanban
{
    using System.Reflection;
    using SampleBrowser.Maui.Base;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var appInfo = typeof(App).GetTypeInfo().Assembly;
            BaseConfig.IsIndividualSB = true;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;
            var mainPage = BaseConfig.MainPageInit(assembly);
            return new Window(mainPage);
        }
    }
}