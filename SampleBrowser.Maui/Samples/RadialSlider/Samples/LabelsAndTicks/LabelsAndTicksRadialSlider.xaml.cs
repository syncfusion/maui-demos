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

namespace SampleBrowser.Maui.RadialSlider
{
    public partial class LabelsAndTicksRadialSlider : SampleView
    {
        public LabelsAndTicksRadialSlider()
        {
            InitializeComponent();
        }

        #region Events

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            clockGauge.Handler?.DisconnectHandler();
        }

        private void labelsAndTicksPointer_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (Math.Abs(e.NewValue - e.OldValue) > 2.4)
                e.Cancel = true;
            else
            {
                clockAnnotationLabel.Text = e.NewValue.ToString() + (e.NewValue > 1 ? " hrs" : " hr");
            }
        }
        #endregion
    }
}