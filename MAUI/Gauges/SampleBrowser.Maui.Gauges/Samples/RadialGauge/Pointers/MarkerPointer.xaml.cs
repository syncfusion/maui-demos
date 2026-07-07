using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class MarkerPointer : SampleView
{
	public MarkerPointer()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        markerPointerGauge.Handler?.DisconnectHandler();
    }
}