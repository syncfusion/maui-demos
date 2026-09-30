using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class Column_DynamicUpdate : SampleView
    {
        public Column_DynamicUpdate()
        {
            InitializeComponent();

            if (!(BaseConfig.RunTimeDeviceLayout == SBLayout.Mobile))
                viewModel.StartTimer();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            if (BaseConfig.RunTimeDeviceLayout == SBLayout.Mobile)
            {
                viewModel.StopTimer();
                viewModel.StartTimer();
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            if (viewModel != null)
                viewModel.StopTimer();

            Chart4.Handler?.DisconnectHandler();
        }
    }
}
