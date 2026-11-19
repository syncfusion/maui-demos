using SampleBrowser.Maui.Base;
namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart;

public partial class RangeBarAnimation : SampleView
{
	public RangeBarAnimation()
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

        if (!IsCardView)
        {
            Chart.Title = (Label)layout.Resources["title"];
        }
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        if (viewModel != null)
            viewModel.StopTimer();

        Chart.Handler?.DisconnectHandler();
    }
}
