#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfCartesianChart
{
    public partial class ChartAxis : SampleView
    {
        int month = int.MaxValue;

        public ChartAxis()
        {
            InitializeComponent();
            Chart.PrimaryAxis.LabelCreated += Primary_LabelCreated;
        }

        public override void OnScrollingToNewCardViewExt(CardViewExt cardViewExt)
        {
            var content = cardViewExt.MainContent as Syncfusion.Maui.Charts.SfCartesianChart;
            content.AnimateSeries();
        }

        private void Primary_LabelCreated(object sender, ChartAxisLabelEventArgs e)
        {
            DateTime baseDate = new DateTime(1899, 12, 30);
            var date = baseDate.AddDays(e.Position);
            if (date.Month != month)
            {
                ChartAxisLabelStyle labelStyle = new ChartAxisLabelStyle();
                labelStyle.LabelFormat = "MMM-dd";
                labelStyle.FontAttributes = FontAttributes.Bold;
                e.LabelStyle = labelStyle;

                month = date.Month;
            }
            else
            {
                ChartAxisLabelStyle labelStyle = new ChartAxisLabelStyle();
                labelStyle.LabelFormat = "dd";
                e.LabelStyle = labelStyle;
            }
        }
    }
}
