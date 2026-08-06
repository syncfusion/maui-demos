using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class DefaultView : SampleView
{
	public DefaultView()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        defaultViewGauge.Handler?.DisconnectHandler();
    }
}