#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using Application = Microsoft.Maui.Controls.Application;
#if AppCenter
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
#endif

namespace SampleBrowser.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

#if __ANDROID__
            RunTimeDevice.PlatformInfo = "Android";
#elif __MACCATALYST__
            RunTimeDevice.PlatformInfo = "MACCATALYST";
#elif WINDOWS
            RunTimeDevice.PlatformInfo = "Windows";
#else
            RunTimeDevice.PlatformInfo = "iOS";
#endif


#if AppCenter
            AppCenter.Start("android=40246674-ab0e-4097-92b5-dabe814e848e;" +
                  "uwp=4e277dd0-01f4-4746-96db-d13459b02498;" +
                 "ios=f1553bca-16a7-4e6d-b41b-097bfb39399c",
                typeof(Analytics), typeof(Crashes));
#endif

            //MainPage = new MainPage();
            if (Application.Current != null)
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }

#if WINDOWS
            MainPage = new ControlsHomePage();
#else
            MainPage = new NavigationPage(new ControlsHomePage()) { BarBackgroundColor = Colors.White, BarTextColor = Colors.Black };
#endif
        }
    }
}