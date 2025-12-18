#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfScheduler
{
    /// <summary>
    /// Interaction logic for GettingStarted.xaml
    /// </summary>
    public partial class GettingStarted : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GettingStarted" /> class.
        /// </summary>
        public GettingStarted()
        {
            InitializeComponent();

#if IOS26_0_OR_GREATER || MACCATALYST26_0_OR_GREATER
            this.Scheduler.Background = Brush.Transparent;
            this.Scheduler.HeaderView.Background = Brush.Transparent;
#endif
        }
    }
}