using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class MultiRanges : SampleView
{
	public MultiRanges()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        multiRangesGauge.Handler?.DisconnectHandler();
    }
}