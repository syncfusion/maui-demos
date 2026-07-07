using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class DistanceTracker : SampleView
{
	public DistanceTracker()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        distanceTrackerGauge.Handler?.DisconnectHandler();
    }
}