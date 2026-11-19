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

        private void OnRatingSliderLabelCreated(object? sender, SliderLabelCreatedEventArgs e)
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

        private void OnItemSizeLabelCreated(object? sender, SliderLabelCreatedEventArgs e)
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
