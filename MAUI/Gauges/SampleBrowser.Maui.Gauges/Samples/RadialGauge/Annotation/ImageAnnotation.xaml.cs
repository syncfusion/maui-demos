using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class ImageAnnotation : SampleView
{
	public ImageAnnotation()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        imageAnnotationGauge.Handler?.DisconnectHandler();
    }
}