using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class SpringInAnimation : SampleView
{
	public SpringInAnimation()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        springInAnimationGauge.Handler?.DisconnectHandler();
    }
}