using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class RangeColor : SampleView
{
	public RangeColor()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        rangeColorGauge.Handler?.DisconnectHandler();
    }
}