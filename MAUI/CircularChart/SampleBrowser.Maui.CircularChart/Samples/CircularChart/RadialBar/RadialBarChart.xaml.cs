#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Inputs;

namespace SampleBrowser.Maui.CircularChart.SfCircularChart
{
    public partial class RadialBarChart : SampleView
    {
        public RadialBarChart()
        {
            InitializeComponent();
            InitializeProperties();
        }

        private void InitializeProperties() 
        {
            capStyle.SelectedIndex = 2;
            trackFill.SelectedIndex = 0;
            trackStroke.SelectedIndex = 1;
            startAngle.Value = -90;
            endAngle.Value = 270;
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
#if IOS
            if (IsCardView)
            {
                chart.WidthRequest = 350;
                chart.HeightRequest = 400;
                chart.VerticalOptions = LayoutOptions.Start;
            }
#endif
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            chart.Handler?.DisconnectHandler();
        }
       

        private void trackFill_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var value = (SfComboBox)sender;
            switch (value.SelectedIndex)
            {
                case 1:
                    {
                        radialBarSeries.TrackFill = new SolidColorBrush(Color.FromRgba(0, 0, 0, 0.08));
                        break;
                    }
                case 2:
                    {
                        radialBarSeries.TrackFill = new SolidColorBrush(Color.FromRgba("#F1F5F9"));
                        break;
                    }
                case 3:
                    {
                        radialBarSeries.TrackFill = new SolidColorBrush(Color.FromRgba("#EFF6FF"));
                        break;
                    }
                case 4:
                    {
                        radialBarSeries.TrackFill = new SolidColorBrush(Color.FromRgba("#FFF7ED"));
                        break;
                    }
                case 5:
                    {
                        radialBarSeries.TrackFill = new SolidColorBrush(Color.FromRgba("#F5F3FF"));
                        break;
                    }
            }
        }

        private void trackStroke_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var value = (SfComboBox)sender;
            switch (value.SelectedIndex)
            {
                case 1:
                    {
                        radialBarSeries.TrackStroke = new SolidColorBrush(Color.FromRgba(0, 0, 0, 0.24));
                        break;
                    }
                case 2:
                    {
                        radialBarSeries.TrackStroke = new SolidColorBrush(Color.FromRgba("#CBD5E1"));
                        break;
                    }
                case 3:
                    {
                        radialBarSeries.TrackStroke = new SolidColorBrush(Color.FromRgba("#BFDBFE"));
                        break;
                    }
                case 4:
                    {
                        radialBarSeries.TrackStroke = new SolidColorBrush(Color.FromRgba("#FED7AA"));
                        break;
                    }
                case 5:
                    {
                        radialBarSeries.TrackStroke = new SolidColorBrush(Color.FromRgba("#DDD6FE"));
                        break;
                    }
            }
        }
    }
}

