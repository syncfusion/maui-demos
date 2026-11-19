using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class AutoScrolling : SampleView
    {
        public AutoScrolling()
        {
            InitializeComponent();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            realTimeViewModel.StopTimer();
            realTimeViewModel.StartTimer();
        }
        
        public override void OnDisappearing()
        {
            base.OnDisappearing();
            if (realTimeViewModel != null)
            {
                realTimeViewModel.StopTimer();
            }

            categoryChart.Handler?.DisconnectHandler();
        }
    }
}
