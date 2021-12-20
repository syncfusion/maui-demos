#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Syncfusion.Maui.Sliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleBrowser.Maui.Core;


namespace SampleBrowser.Maui.VerticalRangeSlider
{
    public partial class RangeSliderDateInterval : SampleView
    {
        public RangeSliderDateInterval()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            hourRangeSlider.LabelCreated -= OnHourLabelCreatedEvent;
            base.OnDisappearing();
        }

        private void OnHourLabelCreatedEvent(object sender, SliderLabelCreatedEventArgs e)
        {
            e.Text = Convert.ToDateTime(e.Text).ToString("h tt").ToUpper();
        }
    }
}