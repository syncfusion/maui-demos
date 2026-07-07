using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class RangePointer : SampleView
{
	public RangePointer()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        rangePointerGauge.Handler?.DisconnectHandler();
    }
}