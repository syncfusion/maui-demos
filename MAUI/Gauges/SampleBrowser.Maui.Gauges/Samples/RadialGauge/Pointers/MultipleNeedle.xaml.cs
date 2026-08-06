using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class MultipleNeedle : SampleView
{
	public MultipleNeedle()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        multipleNeedleGauge.Handler?.DisconnectHandler();
    }

}