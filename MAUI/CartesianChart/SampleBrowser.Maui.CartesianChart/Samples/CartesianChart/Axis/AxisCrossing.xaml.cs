using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class AxisCrossing : SampleView
    {
        public AxisCrossing()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            axisCrossingChart.Handler?.DisconnectHandler();
        }
    }
}
