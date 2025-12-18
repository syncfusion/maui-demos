#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using System;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class SplineRangeAreaChart : SampleView
    {
        int month = int.MaxValue;
        public SplineRangeAreaChart()
        {
            InitializeComponent();
        }
        private void Primary_LabelCreated(object? sender, ChartAxisLabelEventArgs e)
        {
            DateTime baseDate = new(1899, 12, 30);
            var date = baseDate.AddDays(e.Position);
            if (date.Month != month)
            {
                ChartAxisLabelStyle labelStyle = new();
                labelStyle.LabelFormat = "MMM-dd";
                labelStyle.FontAttributes = FontAttributes.Bold;
                e.LabelStyle = labelStyle;
                month = date.Month;
            }
            else
            {
                ChartAxisLabelStyle labelStyle = new();
                labelStyle.LabelFormat = "dd";
                e.LabelStyle = labelStyle;
            }
        }
        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Chart.Handler?.DisconnectHandler();
        }

        private void Chart_Scroll(object sender, ChartScrollEventArgs e)
        {
            month = int.MaxValue;
        }

        private void Chart_SizeChanged(object sender, EventArgs e)
        {
            month = int.MaxValue;
        }
    }
}