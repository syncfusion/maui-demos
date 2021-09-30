using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Xaml;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.TabView;

[assembly: XamlCompilationAttribute(XamlCompilationOptions.Compile)]

namespace SampleBrowser.Maui
{
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
					fonts.AddFont("Roboto-Regular.ttf", "Roboto");
					fonts.AddFont("Roboto-Medium.ttf", "Roboto Medium");
				});

            return builder.Build();
        }
    }
}