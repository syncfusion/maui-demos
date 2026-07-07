using System.Reflection;

namespace SampleBrowser.Maui.DocIO;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        //MainPage = new AppShell();

        var appInfo = typeof(App).GetTypeInfo().Assembly;
        SampleBrowser.Maui.Base.BaseConfig.IsIndividualSB = true;
#if DocumentSDK
        SampleBrowser.Maui.Base.BaseConfig.EssentialStudioProductType = Base.EssentialStudioProductType.DocumentSDK;
#elif WORDSDK
        SampleBrowser.Maui.Base.BaseConfig.EssentialStudioProductType = Base.EssentialStudioProductType.WordSDK;
#endif
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        Assembly assembly = typeof(App).GetTypeInfo().Assembly;
        var mainPage = SampleBrowser.Maui.Base.BaseConfig.MainPageInit(assembly);
        return new Window(mainPage);
    }
}
