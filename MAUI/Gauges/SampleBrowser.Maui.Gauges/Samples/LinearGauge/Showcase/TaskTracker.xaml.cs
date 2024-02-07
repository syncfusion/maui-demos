#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge;

public partial class TaskTracker : SampleView
{
	public TaskTracker()
	{
		InitializeComponent();
#if MACCATALYST
#else
        if (DeviceInfo.Platform == DevicePlatform.iOS && DeviceInfo.Idiom == DeviceIdiom.Tablet)
        {
            taskTrackerLayout.WidthRequest = 340;
            taskTrackerLayout.HorizontalOptions = LayoutOptions.Center;
        }
#endif
    }
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        taskTrackerGauge.Handler?.DisconnectHandler();
    }
}