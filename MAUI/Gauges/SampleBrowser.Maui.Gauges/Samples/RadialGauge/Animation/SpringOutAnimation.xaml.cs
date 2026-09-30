using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class SpringOutAnimation : SampleView
{
	public SpringOutAnimation()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        springOutAnimationGauge.Handler?.DisconnectHandler();
    }
}