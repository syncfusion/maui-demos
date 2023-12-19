using System.Reflection;

namespace SampleBrowser.Maui.SunburstChart;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        //MainPage = new AppShell();

        var appInfo = typeof(App).GetTypeInfo().Assembly;
        SampleBrowser.Maui.Base.BaseConfig.IsIndividualSB = true;
        MainPage = SampleBrowser.Maui.Base.BaseConfig.MainPageInit(appInfo);
    }
}
