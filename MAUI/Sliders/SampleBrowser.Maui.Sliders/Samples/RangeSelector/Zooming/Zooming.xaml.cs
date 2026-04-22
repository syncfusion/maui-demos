#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Sliders.SfRangeSelector
{
    public partial class RangeSelectorZoomingSample : SampleView
    {
        public RangeSelectorZoomingSample()
        {
            InitializeComponent();
        }

        private void SampleView_SizeChanged(object? sender, EventArgs e)
        {
            double totalHeight = this.Height;
            double quarterHeight = totalHeight / 4;
            rangeSelectorChart.HeightRequest = quarterHeight;
        }
    }
}