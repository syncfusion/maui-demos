using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class Line_DynamicUpdate : SampleView
    {
        public Line_DynamicUpdate()
        {
            InitializeComponent();

            if (!(BaseConfig.RunTimeDeviceLayout == SBLayout.Mobile))
                viewModel1.StartTimer();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            if (BaseConfig.RunTimeDeviceLayout == SBLayout.Mobile)
            {
                viewModel1.StopTimer();
                viewModel1.StartTimer();
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            if (viewModel1 != null)
            {
                viewModel1.StopTimer();
            }

            Chart2.Handler?.DisconnectHandler();
        }
    }
}
