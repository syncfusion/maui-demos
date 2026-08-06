using System.Reflection;

namespace SampleBrowser.Maui.XlsIO;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        //MainPage = new AppShell();

        var appInfo = typeof(App).GetTypeInfo().Assembly;
        SampleBrowser.Maui.Base.BaseConfig.IsIndividualSB = true;
#if DocumentSDK
        SampleBrowser.Maui.Base.BaseConfig.EssentialStudioProductType = SampleBrowser.Maui.Base.EssentialStudioProductType.DocumentSDK;
#endif
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        Assembly assembly = typeof(App).GetTypeInfo().Assembly;
        var mainPage = SampleBrowser.Maui.Base.BaseConfig.MainPageInit(assembly);
        return new Window(mainPage);
    }
}
