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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class VerticalStepLineChart : SampleView
    {
        public VerticalStepLineChart()
        {
            InitializeComponent();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
#if IOS
            if (IsCardView)
            {
                Chart2.WidthRequest = 350;
                Chart2.HeightRequest = 400;
                Chart2.VerticalOptions = LayoutOptions.Start;
            }
#endif

            hyperLinkLayout.IsVisible = !IsCardView;
            if (!IsCardView)
            {
                XAxis.Title = new ChartAxisTitle() { Text = "Year" };
                YAxis.Title = new ChartAxisTitle() { Text = "Carbon Intensity" };
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Chart2.Handler?.DisconnectHandler();
        }
    }
}