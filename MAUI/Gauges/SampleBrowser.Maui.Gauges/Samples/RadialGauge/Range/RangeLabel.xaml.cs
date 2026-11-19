using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class RangeLabel : SampleView
{
	public RangeLabel()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        rangeLabelGauge.Handler?.DisconnectHandler();
    }
}