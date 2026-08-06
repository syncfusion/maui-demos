using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Sliders.SfRangeSelector
{
    public partial class RangeSelectorZoomingSample : SampleView
    {
        public RangeSelectorZoomingSample()
        {
            InitializeComponent();
        }

        private void SampleView_SizeChanged(object? sender, EventArgs e)
        {
            double totalHeight = this.Height;
            double quarterHeight = totalHeight / 4;
            rangeSelectorChart.HeightRequest = quarterHeight;
        }
    }
}