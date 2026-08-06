namespace SampleBrowser.Maui.Sliders;

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
            .ConfigureSampleBrowserBase()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.ConfigureSampleBrowserBase();
        return builder.Build();
    }
}
