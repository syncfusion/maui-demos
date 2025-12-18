#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.LiquidGlass.SfStepProgressBar
{
    using SampleBrowser.Maui.Base;

    /// <summary>
    /// Represents a page that displays and manages a Step Progress Bar within the application.
    /// </summary>
    public partial class StepProgressBarPage : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StepProgressBarPage"/> class.
        /// </summary>
        public StepProgressBarPage()
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
}