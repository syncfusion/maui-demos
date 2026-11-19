using System.Reflection;

namespace SampleBrowser.Maui.PdfViewer;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        //MainPage = new AppShell();
        var appInfo = typeof(App).GetTypeInfo().Assembly;
        SampleBrowser.Maui.Base.BaseConfig.IsIndividualSB = true;
#if PDFViewerSDK
        SampleBrowser.Maui.Base.BaseConfig.EssentialStudioProductType = SampleBrowser.Maui.Base.EssentialStudioProductType.PDFViewerSDK;
#endif
    }
    protected override Window CreateWindow(IActivationState? activationState)
    {
        Assembly assembly = typeof(App).GetTypeInfo().Assembly;
        var mainPage = SampleBrowser.Maui.Base.BaseConfig.MainPageInit(assembly);
        return new Window(mainPage);
    }
}
