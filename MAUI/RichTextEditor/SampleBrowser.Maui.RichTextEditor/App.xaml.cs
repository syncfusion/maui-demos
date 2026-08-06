using System.Reflection;

namespace SampleBrowser.Maui.RichTextEditor
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SampleBrowser.Maui.Base.BaseConfig.IsIndividualSB = true;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;
            var mainPage = SampleBrowser.Maui.Base.BaseConfig.MainPageInit(assembly);
            return new Window(mainPage);
        }
    }
}