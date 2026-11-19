using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class WaterFallChartIsTransposed : SampleView
    {
        public WaterFallChartIsTransposed()
        {
            InitializeComponent();
        }
        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Chart.Handler?.DisconnectHandler();
        }
        public override void OnAppearing()
        {
            base.OnAppearing();
            headingLabel.IsVisible =! IsCardView;
            firstLabel.IsVisible = ! IsCardView;
            secondLabel.IsVisible = ! IsCardView;
        }
    }
}
