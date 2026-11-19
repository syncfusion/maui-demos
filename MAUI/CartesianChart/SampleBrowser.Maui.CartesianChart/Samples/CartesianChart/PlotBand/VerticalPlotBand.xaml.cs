using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
	public partial class VerticalPlotBand : SampleView
	{
		public VerticalPlotBand ()
		{
			InitializeComponent ();
		}

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            verticalPlotBandChart.Handler?.DisconnectHandler();
        }
    }
}