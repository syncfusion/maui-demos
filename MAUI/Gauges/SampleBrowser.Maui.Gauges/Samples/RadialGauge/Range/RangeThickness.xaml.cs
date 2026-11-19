using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class RangeThickness : SampleView
{
	public RangeThickness()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        rangeThicknessGauge.Handler?.DisconnectHandler();
    }
}