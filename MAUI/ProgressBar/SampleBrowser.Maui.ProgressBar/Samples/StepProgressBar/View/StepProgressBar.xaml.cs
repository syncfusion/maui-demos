#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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
        OrderTrackingDestopUI orderTrackingDestopUI = new OrderTrackingDestopUI();
        this.Content = orderTrackingDestopUI;
        this.OptionView = orderTrackingDestopUI.OptionView;
#elif ANDROID || IOS
        OrderTrackingMobileUI orderTrackingMobileUI = new OrderTrackingMobileUI();
        this.Content = orderTrackingMobileUI;
        this.OptionView = orderTrackingMobileUI.OptionView;
#endif
    }
}