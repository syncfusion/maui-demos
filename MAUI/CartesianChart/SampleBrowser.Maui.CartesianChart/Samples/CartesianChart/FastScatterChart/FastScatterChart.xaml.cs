using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
	public partial class FastScatterChart : SampleView
	{
		public FastScatterChart()
		{
			InitializeComponent();
		}

		public override void OnDisappearing()
		{
			base.OnDisappearing();
			fastScatterChart.Handler?.DisconnectHandler();
		}
	}
}