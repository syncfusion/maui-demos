#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Popup;
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
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Roboto-Medium.ttf", "Roboto-Medium");
				fonts.AddFont("Roboto-Regular.ttf", "Roboto-Regular");
				fonts.AddFont("PopupFontIcons.ttf", "PopupFontIcons");
			});
		builder.ConfigureSampleBrowserBase();
		return builder.Build();
	}
}
