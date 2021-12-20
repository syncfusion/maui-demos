#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Syncfusion.Maui.Core.Hosting;
#if __ANDROID__ || __IOS__ || __MACCATALYST__
using Syncfusion.Maui.ListView.Hosting;
#endif

namespace SampleBrowser.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("Font Gauge Icons.ttf", "FontGaugeIcons");
                    fonts.AddFont("Scheduler Icons.ttf", "SchedulerIcons");
                });

            builder.ConfigureSyncfusionCore();

#if __ANDROID__ || __IOS__ || __MACCATALYST__
            builder.ConfigureSyncfusionListView();
#endif

            return builder.Build();
        }
    }
}