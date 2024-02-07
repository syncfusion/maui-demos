#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System.Reflection;

namespace SampleBrowser.Maui.Gauges;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		
        var appInfo = typeof(App).GetTypeInfo().Assembly;
        SampleBrowser.Maui.Base.BaseConfig.IsIndividualSB = true;
        MainPage = SampleBrowser.Maui.Base.BaseConfig.MainPageInit(appInfo);
	}
}
