namespace SampleBrowser.Maui.TabView;

using Syncfusion.Maui.Core.Hosting;
using SampleBrowser.Maui.Base.Hosting;

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
                fonts.AddFont("MauiSampleFontIcon.ttf", "MauiSampleFontIcon");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Roboto-Medium.ttf", "Roboto-Medium");
                fonts.AddFont("Roboto-Regular.ttf", "Roboto-Regular");
            });
		builder.ConfigureSampleBrowserBase();
		
		return builder.Build();
	}
}
