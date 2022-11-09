#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;
namespace SampleBrowser.Maui.RadialSliders.RadialRangeSlider
{
    public partial class GradientFillRadialRangeSlider : SampleView
    {
        public GradientFillRadialRangeSlider()
        {
            InitializeComponent();
            fahrenheitAnnotationLabel.Text = "0°F to 60°F";
            celsiusAnnotationLabel.Text = "-17.8°C to 15.6°C";
        }

        #region Events
        private void RadialAxis_LabelCreated(object sender, LabelCreatedEventArgs e)
        {
            double axisValue = double.Parse(e.Text);
            double celsiusValue = (axisValue - 32) / 1.8;
            e.Text = Math.Round(celsiusValue, 1).ToString();
        }

        private void gradientMarker1_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (e.NewValue >= gradientMarker2.Value || Math.Abs(e.NewValue - gradientMarker1.Value) > 10)
                e.Cancel = true;
            else
            {
                double firstMarkerValue = e.NewValue > 50 ? Math.Ceiling(e.NewValue) : Math.Floor(e.NewValue);
                double secondMarkerValue = (int)gradientMarker2.Value;

                UpdateGradientAnnotationLabel(firstMarkerValue, secondMarkerValue);
            }
        }

        private void gradientMarker2_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (e.NewValue <= gradientMarker1.Value || Math.Abs(e.NewValue - gradientMarker2.Value) > 10)
                e.Cancel = true;
            else
            {
                double firstMarkerValue = (int)gradientMarker1.Value;
                double secondMarkerValue = e.NewValue > 50 ? Math.Ceiling(e.NewValue) : Math.Floor(e.NewValue);

                UpdateGradientAnnotationLabel(firstMarkerValue, secondMarkerValue);
            }
        }

        private void UpdateGradientAnnotationLabel(double firstMarkerValue, double secondMarkerValue)
        {
            fahrenheitAnnotationLabel.Text = firstMarkerValue.ToString() + "°F to " + secondMarkerValue + "°F";
            celsiusAnnotationLabel.Text = Math.Round((firstMarkerValue - 32) / 1.8, 1) + "°C to " +
                Math.Round((secondMarkerValue - 32) / 1.8, 1) + "°C";
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            gradientFillGauge.Handler?.DisconnectHandler();
        }

        #endregion
    }
}
