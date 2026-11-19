using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class BounceInAnimation : SampleView
{
	public BounceInAnimation()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        bounceInAnimationGauge.Handler?.DisconnectHandler();
    }
}