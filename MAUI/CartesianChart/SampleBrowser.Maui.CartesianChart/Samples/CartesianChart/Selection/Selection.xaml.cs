#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using System.Globalization;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class Selection : SampleView
    {
        public Selection()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            chart.Handler?.DisconnectHandler();
        }

        private void checkbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            dataPointSelection.Type = e.Value ? ChartSelectionType.Multiple : ChartSelectionType.SingleDeselect;
            //series2.Fill = Color.FromArgb("#40314A6E");
        }
    }

    public class SelectionValueConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return ((DateTime)value).ToString("ddd-hh:mm");
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value;
        }
    }

}