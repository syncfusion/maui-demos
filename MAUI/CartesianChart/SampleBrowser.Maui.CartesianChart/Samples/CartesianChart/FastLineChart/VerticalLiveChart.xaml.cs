using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class VerticalLiveChart : SampleView
    {
        public VerticalLiveChart()
        {
            InitializeComponent();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            realTimeVerticalChartViewModel.StartVerticalTimer();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            verticalLiveChart.Handler?.DisconnectHandler();
        }
    }
}
