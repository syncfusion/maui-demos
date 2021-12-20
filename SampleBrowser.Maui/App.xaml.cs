#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Application = Microsoft.Maui.Controls.Application;
using SampleBrowser.Maui.Core;
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

			RunTimeDeviceInfo info = new RunTimeDeviceInfo();
			RunTimeDevice.PlatformInfo = info.GetPlatform();

#if AppCenter
            AppCenter.Start("android=40246674-ab0e-4097-92b5-dabe814e848e;" +
                  "uwp=1b84315a-5e41-4518-a649-e533866358da;" +
                 "ios=f1553bca-16a7-4e6d-b41b-097bfb39399c",
                typeof(Analytics), typeof(Crashes));
#endif

			//MainPage = new MainPage();
			Application.Current.UserAppTheme = OSAppTheme.Light;
			MainPage = new NavigationPage(new ControlsHomePage()) { BarBackgroundColor = Colors.White, BarTextColor = Colors.Black };
		}
	}
}