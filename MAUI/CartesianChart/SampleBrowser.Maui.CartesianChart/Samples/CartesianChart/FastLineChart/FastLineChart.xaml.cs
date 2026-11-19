using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class FastLineChart : SampleView
    {
        public FastLineChart()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            fastLineChart.Handler?.DisconnectHandler();
        }
    }
}
