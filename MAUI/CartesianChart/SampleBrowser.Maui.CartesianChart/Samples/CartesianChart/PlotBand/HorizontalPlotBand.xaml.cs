using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class HorizontalPlotBand : SampleView
    {
        public HorizontalPlotBand()
        {
            InitializeComponent();
            YAxis.PlotBands = ViewModel.NumericalPlotBandFillCollection;
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            horizontalPlotBandChart.Handler?.DisconnectHandler();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is not Picker value || YAxis is null || ViewModel is null)
                return;

            int selectedIndex = value.SelectedIndex;
            if (selectedIndex == 0)
            {
                YAxis.PlotBands = ViewModel.NumericalPlotBandFillCollection;
            }
            else if (selectedIndex == 1)
            {
                YAxis.PlotBands = ViewModel.NumericalPlotBandLineCollection;
                YAxis.PlotOffsetEnd = 5;
            }
            else if (selectedIndex == 2)
            {
                YAxis.PlotBands = ViewModel.SegmentPlotBandCollection;
            }
        }
    }
}
