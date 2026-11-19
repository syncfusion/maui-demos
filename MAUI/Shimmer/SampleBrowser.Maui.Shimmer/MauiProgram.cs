using SampleBrowser.Maui.Base.Hosting;
using Syncfusion.Maui.Core.Hosting;

namespace SampleBrowser.Maui.Shimmer;

public static class MauiProgram
{
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
