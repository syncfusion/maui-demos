#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.SfLinearGauge
{
    public partial class LinearGaugeRangeHorizontal : ContentView
    {
        public LinearGaugeRangeHorizontal()
        {
            InitializeComponent();
        }
    }

    public class CustomLinearRange : LinearRange
    {
        protected override void UpdateMidRangePath(PathF pathF, PointF startPoint, PointF midPoint, PointF endPoint)
        {
            pathF.CurveTo(startPoint, midPoint, endPoint);
        }
    }
}