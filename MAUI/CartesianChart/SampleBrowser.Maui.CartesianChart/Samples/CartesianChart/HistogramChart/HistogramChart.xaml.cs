#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using System;
using System.Globalization;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class HistogramChart : SampleView
    {
        public HistogramChart()
        {
            InitializeComponent();
        }
        public override void OnAppearing()
        {
            base.OnAppearing();

            if (!IsCardView)
            {
                Chart.Title = (Label)layout.Resources["title"];
                xAxis.Title = new ChartAxisTitle() { Text = "Final Examination Score", FontSize=15 };
                yAxis.Title = new ChartAxisTitle() { Text = "Number of Students", FontSize = 15 };
            }
        }
        

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Chart.Handler?.DisconnectHandler();
        }
    }

    public class TooltipValueConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is List<object> data)
            {
                return " : " + data.Count.ToString();
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class TooltipLabelConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var interval = 20;
            if (value is List<object> list)
            {
                var data = list[0] as ChartDataModel;
                int index = (int)data!.Value / interval;
                int x = index * interval;
                string text = x.ToString() + " - " + (x + interval).ToString();
                return text;
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value;
        }
    }
}