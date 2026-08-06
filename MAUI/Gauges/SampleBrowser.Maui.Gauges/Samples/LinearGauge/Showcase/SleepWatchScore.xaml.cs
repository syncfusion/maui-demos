using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge;

public partial class SleepWatchScore : SampleView
{
	public SleepWatchScore()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        sleepWatchScoreGauge1.Handler?.DisconnectHandler();
        sleepWatchScoreGauge2.Handler?.DisconnectHandler();
    }
}