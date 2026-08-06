using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class LinearAnimation : SampleView
{
	public LinearAnimation()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();

        linearAnimationGauge.Handler?.DisconnectHandler();
    }
}