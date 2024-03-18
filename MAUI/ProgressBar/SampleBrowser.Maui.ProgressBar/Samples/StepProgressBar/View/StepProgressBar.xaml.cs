#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.ProgressBar.SfStepProgressBar;

using SampleBrowser.Maui.Base;

public partial class StepProgressBar : SampleView
{
	public StepProgressBar()
	{
		InitializeComponent();
#if WINDOWS || MACCATALYST
        this.Content = new OrderTrackingDestopUI();
#elif ANDROID || IOS
        this.Content = new OrderTrackingMobileUI();
#endif
    }
}