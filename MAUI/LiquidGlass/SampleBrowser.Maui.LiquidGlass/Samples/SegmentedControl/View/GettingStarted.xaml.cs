#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.LiquidGlass.SfSegmentedControl
{
    using SampleBrowser.Maui.Base;

    /// <summary>
    /// Provides the view for the Getting Started sample.
    /// </summary>
    public partial class GettingStarted : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GettingStarted"/> class.
        /// </summary>
        public GettingStarted()
        {
            InitializeComponent();
#if WINDOWS || MACCATALYST
            this.GlassEffectView.Content = new GettingStartedDesktopUI();
#elif ANDROID || IOS
            this.GlassEffectView.Content = new GettingStartedMobileUI();
#endif
        }
    }
}