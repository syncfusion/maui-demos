using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class VerticalPlotBandWindows : SampleView
    {
        public VerticalPlotBandWindows()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            verticalPlotBandWindowsChart.Handler?.DisconnectHandler();
        }
    }
}