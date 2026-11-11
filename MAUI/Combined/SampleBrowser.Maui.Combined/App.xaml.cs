using System.Reflection;
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Combined;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		BaseConfig.IsIndividualSB = true;
		SampleCatalog.Load();
		var assembly = typeof(App).Assembly;
		BaseConfig.RegisterLicense(assembly, assembly.GetName().Name);
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		var page = BaseConfig.MainPageInit(typeof(App).Assembly);
		SampleCatalog.AssignAssemblies(page.BindingContext);
		return new Window(page);
	}
}
