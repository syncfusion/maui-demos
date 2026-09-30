using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class BounceOutAnimation : SampleView
{
	public BounceOutAnimation()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        bounceOutAnimationGauge.Handler?.DisconnectHandler();
    }
}