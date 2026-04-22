#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Charts;
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class CartesianCrossHair : SampleView
    {
        int _month = int.MaxValue;

        public CartesianCrossHair()
        {
            InitializeComponent();
        }

        // X axis label formatting logic
        private void Primary_LabelCreated(object sender, Syncfusion.Maui.Charts.ChartAxisLabelEventArgs e)
        {
            DateTime baseDate = new(2024, 09, 16);
            var date = baseDate.AddDays(e.Position);
            if (date.Month != _month)
            {
                ChartAxisLabelStyle labelStyle = new()
                {
                    LabelFormat = "MMM-dd",
                    FontAttributes = FontAttributes.Bold
                };
                e.LabelStyle = labelStyle;
                _month = date.Month;
            }
            else
            {
                ChartAxisLabelStyle labelStyle = new()
                {
                    LabelFormat = "dd"
                };
                e.LabelStyle = labelStyle;
            }
        }
    }
}
