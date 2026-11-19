using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class TickCustomization : SampleView
{
	public TickCustomization()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        tickCustomizationGauge.Handler?.DisconnectHandler();
    }
}