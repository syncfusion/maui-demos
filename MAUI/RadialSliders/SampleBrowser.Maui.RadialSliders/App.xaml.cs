using System.Reflection;

namespace SampleBrowser.Maui.RadialSliders;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}
    protected override Window CreateWindow(IActivationState activationState)
    {
        var appInfo = typeof(App).GetTypeInfo().Assembly;
        Base.BaseConfig.IsIndividualSB = true;
        return new Window(Base.BaseConfig.MainPageInit(appInfo));
    }
}
