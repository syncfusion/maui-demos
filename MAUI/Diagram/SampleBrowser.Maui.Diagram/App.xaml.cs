using System.Reflection;

namespace SampleBrowser.Maui.Diagram;

/// <summary>
/// The sample browser application entry point: initializes the XAML resources and hands the
/// assembly's sample configuration to <c>SampleBrowser.Maui.Base</c>'s shared shell.
/// </summary>
public partial class App : Application
{
	/// <summary>Loads the application's XAML resources.</summary>
	public App()
	{
		InitializeComponent();
	}

	/// <summary>
	/// Creates the main window, initializing the sample browser configuration from this assembly
	/// so <c>SampleBrowser.Maui.Base</c> can discover the diagram samples in
	/// <c>DiagramSamplesList.xml</c>.
	/// </summary>
	/// <param name="activationState">The activation state passed by the host.</param>
    protected override Window CreateWindow(IActivationState? activationState)
    {
        var appInfo = typeof(App).GetTypeInfo().Assembly;
        SampleBrowser.Maui.Base.BaseConfig.IsIndividualSB = true;
        return new Window(SampleBrowser.Maui.Base.BaseConfig.MainPageInit(appInfo));
    }
}
