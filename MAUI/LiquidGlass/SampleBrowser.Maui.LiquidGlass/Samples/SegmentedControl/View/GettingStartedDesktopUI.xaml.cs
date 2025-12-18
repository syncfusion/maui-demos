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
    /// Provides the view for the Getting Started sample for the desktop.
    /// </summary>
    public partial class GettingStartedDesktopUI : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GettingStartedDesktopUI"/> class.
        /// </summary>
        public GettingStartedDesktopUI()
        {
            InitializeComponent();
#if WINDOWS
            double density = DeviceDisplay.MainDisplayInfo.Density;
            if (density == 1.0)
            {
                this.SegmentsContainerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                this.SegmentsContainerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.15, GridUnitType.Star) });
                return;
            }

            this.SegmentsContainerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.85, GridUnitType.Star) });
            this.SegmentsContainerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.15, GridUnitType.Star) });
#endif
        }
    }
}