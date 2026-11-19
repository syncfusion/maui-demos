using Syncfusion.Maui.Core.Hosting;
using SampleBrowser.Maui.Base.Hosting;

namespace SampleBrowser.Maui.Carousel;

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
                fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");
                fonts.AddFont("Roboto-Regular.ttf", "Roboto");
                fonts.AddFont("SegmentIcon.ttf", "SegmentIcon");
                fonts.AddFont("carousel.ttf", "Carousel");
                fonts.AddFont("CarouselIcon.ttf", "CarouselIcon");
            });

        return builder.Build();
    }
}
