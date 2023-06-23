#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Sliders;

namespace SampleBrowser.Maui.Sliders.SfSlider
{
    public partial class SliderLabelCustomizationSample : SampleView
    {
        public SliderLabelCustomizationSample()
        {
            InitializeComponent();
        }

        private void OnRatingSliderLabelCreated(object sender, SliderLabelCreatedEventArgs e)
        {
            if (e.Text == "4")
            {
                e.Text = "Excellent";
            }
            else if (e.Text == "3")
            {
                e.Text = "Good";
            }
            else if (e.Text == "2")
            {
                e.Text = "Average";
            }
            else if (e.Text == "1")
            {
                e.Text = "Poor";
            }
        }

        private void OnItemSizeLabelCreated(object sender, SliderLabelCreatedEventArgs e)
        {
            if (e.Text == "4")
            {
                e.Text = "8x10";
            }
            else if (e.Text == "3")
            {
                e.Text = "5x7";
            }
            else if (e.Text == "2")
            {
                e.Text = "4x6";
            }
            else if (e.Text == "1")
            {
                e.Text = "2x3";
            }
        }
    }
}
