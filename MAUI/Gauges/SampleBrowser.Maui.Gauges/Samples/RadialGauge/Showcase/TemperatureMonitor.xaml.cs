using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class TemperatureMonitor : SampleView
{
	public TemperatureMonitor()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        temperatureMonitorGauge.Handler?.DisconnectHandler();
    }
}