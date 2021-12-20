using Microsoft.Maui.Controls;
using Syncfusion.Maui.Sliders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleBrowser.Maui.Core;


namespace SampleBrowser.Maui.VerticalSlider
{
    public partial class SliderDateInterval : SampleView
    {
        public SliderDateInterval()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            hourSlider.LabelCreated -= OnHourLabelCreatedEvent;
            base.OnDisappearing();
        }

        private void OnHourLabelCreatedEvent(object sender, SliderLabelCreatedEventArgs e)
        {
            e.Text = Convert.ToDateTime(e.Text).ToString("h tt").ToUpper();
        }
    }
}