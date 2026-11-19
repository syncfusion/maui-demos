using System.Reflection;

namespace SampleBrowser.Maui.TreeView;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		var appInfo = typeof(App).GetTypeInfo().Assembly;
		SampleBrowser.Maui.Base.BaseConfig.IsIndividualSB = true;
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		Assembly assembly = typeof(App).GetTypeInfo().Assembly;
		var mainPage = SampleBrowser.Maui.Base.BaseConfig.MainPageInit(assembly);
		return new Window(mainPage);
	}
}
