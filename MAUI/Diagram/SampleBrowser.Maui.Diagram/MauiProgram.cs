using SampleBrowser.Maui.Base.Hosting;
using Syncfusion.Maui.Core.Hosting;

namespace SampleBrowser.Maui.Diagram
{
	/// <summary>
	/// Configures and builds the MAUI application: registers the Syncfusion core handlers, the
	/// sample browser base services, and the application fonts.
	/// </summary>
	public static class MauiProgram
	{
		/// <summary>Creates and configures the MAUI application instance.</summary>
		/// <returns>The configured <see cref="MauiApp"/>.</returns>
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureSyncfusionCore()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

			builder.ConfigureSampleBrowserBase();
			return builder.Build();
		}
	}
}
