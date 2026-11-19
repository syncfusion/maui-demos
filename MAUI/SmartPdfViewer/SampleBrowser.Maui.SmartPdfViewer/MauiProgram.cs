using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using SampleBrowser.Maui.Base.Hosting;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.SmartComponents.Hosting;
using System.ClientModel;
namespace SampleBrowser.Maui.SmartPdfViewer;
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
                fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");
                fonts.AddFont("Roboto-Regular.ttf", "Roboto");
            });
        builder.ConfigureSampleBrowserBase();
        return builder.Build();
	}
}
