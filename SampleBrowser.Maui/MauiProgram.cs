#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.ListView.Hosting;
using System;

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
                    fonts.AddFont("FontAwesome.ttf", "FontAwesome");
                    fonts.AddFont("SlidersThumbIcons.ttf", "SlidersThumbIcons");
                });

            builder.ConfigureSyncfusionCore();
            builder.ConfigureSyncfusionListView();
			
#if IOS || MACCATALYST
            builder.UseMauiApp<App>().ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler(typeof(SearchBar), typeof(ListViewSearchBarHandler));
            });
#endif

            return builder.Build();
        }
    }
}