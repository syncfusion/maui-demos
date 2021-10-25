#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
# endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Application = Microsoft.Maui.Controls.Application;

namespace SampleBrowser.Maui
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			//MainPage = new NavigationPage(new ControlsHomePage()) { BarBackgroundColor = Colors.White, BarTextColor = Colors.Black };
			MainPage = new ControlsHomePage() ;
		}
	}
}