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
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfCartesianChart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Selection : SampleView
    {
        public Selection()
        {
            InitializeComponent();
            chart.SelectionChanging += Chart_SelectionChanging;
        }

        private void Chart_SelectionChanging(object sender, Syncfusion.Maui.Charts.ChartSelectionChangingEventArgs e)
        {
            if (e.CurrentIndex == e.PreviousIndex || e.CurrentIndex == -1)
            {
                e.Cancel = true;
            }
            else if (e.CurrentIndex != -1)
            {
                series2.Background = new SolidColorBrush(Color.FromArgb("#40314A6E"));
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            chart.Handler?.DisconnectHandler();
        }

        public override void OnExpandedViewDisappearing(View view)
        {
            base.OnExpandedViewDisappearing(view);

            view.Handler?.DisconnectHandler();
        }
    }

    public class SelectionValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                return ((DateTime)value).ToString("ddd-hh:mm");
            }
            
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

}
