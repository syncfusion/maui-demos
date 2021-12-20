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
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using SampleBrowser.Maui.Core;

namespace SampleBrowser.Maui.Slider
{
    public partial class SliderDateInterval : SampleView
    {
        public SliderDateInterval()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            yearSlider.ToolTipLabelCreated -= OnYearToolTipLabelCreatedEvent;
            yearSlider.LabelCreated -= OnYearLabelCreatedEvent;
            hourSlider.ToolTipLabelCreated -= OnHourToolTipLabelCreatedEvent;
            hourSlider.LabelCreated -= OnHourLabelCreatedEvent;
            base.OnDisappearing();
        }

        private void OnYearToolTipLabelCreatedEvent(object sender, SliderLabelCreatedEventArgs e)
        {
            DateTime dateTime = Convert.ToDateTime(e.Text);
            e.Text = dateTime.ToString("MMM yyyy");
        }

        private void OnYearLabelCreatedEvent(object sender, SliderLabelCreatedEventArgs e)
        {
            e.Text = Convert.ToDateTime(e.Text).ToString("yyyy");
        }

        private void OnHourToolTipLabelCreatedEvent(object sender, SliderLabelCreatedEventArgs e)
        {
            DateTime dateTime = Convert.ToDateTime(e.Text);
            e.Text = dateTime.ToString("h:mm tt").ToUpper();
        }

        private void OnHourLabelCreatedEvent(object sender, SliderLabelCreatedEventArgs e)
        {
            e.Text = Convert.ToDateTime(e.Text).ToString("h tt").ToUpper();
        }
    }
}