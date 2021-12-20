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