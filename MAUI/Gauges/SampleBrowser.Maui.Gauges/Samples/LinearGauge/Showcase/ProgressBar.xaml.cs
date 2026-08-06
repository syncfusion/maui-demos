using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge;

public partial class ProgressBar : SampleView
{
	public ProgressBar()
	{
		InitializeComponent();
    }
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        progressBarGauge.Handler?.DisconnectHandler();
    }
}