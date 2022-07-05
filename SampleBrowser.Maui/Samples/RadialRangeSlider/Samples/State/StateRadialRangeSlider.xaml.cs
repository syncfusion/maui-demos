#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Gauges;
using System;

namespace SampleBrowser.Maui.RadialRangeSlider
{
    public partial class StateRadialRangeSlider : SampleView
    {
        public StateRadialRangeSlider()
        {
            InitializeComponent();
        }

        #region Events
        private void markerPointer_ValueChanging1(object sender, ValueChangingEventArgs e)
        {
            if (Math.Abs(e.NewValue - e.OldValue) > 20 || e.NewValue >= markerPointer2.Value ||
            Math.Abs(e.NewValue - markerPointer1.Value) > 10)
                e.Cancel = true;
            else
                UpdateAnnotationLabel(e.NewValue, true);
        }

        private void markerPointer_ValueChanging2(object sender, ValueChangingEventArgs e)
        {
            if (Math.Abs(e.NewValue - e.OldValue) > 20 || e.NewValue <= markerPointer1.Value ||
            Math.Abs(e.NewValue - markerPointer2.Value) > 10)
                e.Cancel = true;
            else
                UpdateAnnotationLabel(e.NewValue, false);
        }

        private void UpdateAnnotationLabel(double newValue, bool isFirstMarker)
        {
            double value = newValue;
            value = value > 50 ? Math.Ceiling(value) : Math.Floor(value);

            if (isFirstMarker)
                annotationLabel.Text = value.ToString() + " - " + (int)markerPointer2.Value + "%";
            else
                annotationLabel.Text = (int)markerPointer1.Value + " - " + value.ToString() + "%";
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            stateGauge.Handler?.DisconnectHandler();
        }

        #endregion
    }
}
