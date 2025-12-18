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
    public partial class StateRadialSlider : SampleView
    {
        public StateRadialSlider()
        {
            InitializeComponent();
        }

        #region Events
        private void markerPointer_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (Math.Abs(e.NewValue - e.OldValue) > 20)
                e.Cancel = true;
            else
            {
                double value = e.NewValue;
                if (value > 99)
                {
                    value = value > 99.9 ? 100 : 99;
                }
                else
                {
                    value = value > 50 ? Math.Ceiling(value) : Math.Floor(value);
                }

                annotationLabel.Text = value.ToString() + "%";
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            stateGauge.Handler?.DisconnectHandler();
        }

        #endregion
    }
}
