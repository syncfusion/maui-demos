using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge;

public partial class BatteryIndicator : SampleView
{
    public BatteryIndicator()
    {
        InitializeComponent();
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        batteryIndicatorGauge.Handler?.DisconnectHandler();
    }
}