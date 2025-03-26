#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;
namespace SampleBrowser.Maui.RadialSliders.RadialSlider
{
    public partial class GradientFillRadialSlider : SampleView
    {
        public GradientFillRadialSlider()
        {
            InitializeComponent();

            fahrenheitAnnotationLabel.Text = "60°F";
            celsiusAnnotationLabel.Text = "15.6°C";
        }

        #region Events
        private void gradientMarker_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            double value = e.NewValue > 50 ? Math.Ceiling(e.NewValue) : Math.Floor(e.NewValue);
            fahrenheitAnnotationLabel.Text = value.ToString() + "°F";
            celsiusAnnotationLabel.Text = Math.Round((value - 32) / 1.8, 1).ToString() + "°C";
        }

        private void RadialAxis_LabelCreated(object sender, LabelCreatedEventArgs e)
        {
            double axisValue = double.Parse(e.Text);
            double celsiusValue = (axisValue - 32) / 1.8;
            e.Text = Math.Round(celsiusValue, 1).ToString();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            gradientFillGauge.Handler?.DisconnectHandler();
        }
        #endregion
    }
}
