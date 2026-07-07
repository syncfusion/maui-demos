using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class LabelCustomization : SampleView
{
	public LabelCustomization()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        labelCustomizationGauge.Handler?.DisconnectHandler();
    }
}