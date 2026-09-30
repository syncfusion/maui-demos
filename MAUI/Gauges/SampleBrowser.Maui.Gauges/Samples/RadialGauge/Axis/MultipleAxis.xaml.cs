using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class MultipleAxis : SampleView
{
	public MultipleAxis()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        multipleAxisGauge.Handler?.DisconnectHandler();
    }
}