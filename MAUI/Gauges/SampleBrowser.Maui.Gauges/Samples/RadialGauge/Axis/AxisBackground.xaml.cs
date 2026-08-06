using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.Gauges.SfRadialGauge;

public partial class AxisBackground : SampleView
{
	public AxisBackground()
	{
		InitializeComponent();
	}
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        axisBackgroundGauge.Handler?.DisconnectHandler();
    }

    private void RadialAxis_LabelCreated(object? sender, LabelCreatedEventArgs e)
    {
        if (e.Text == "90")
        {
            e.Text = "E";
        }
        else if (e.Text == "360")
        {
            e.Text = string.Empty;
        }
        else
        {
            if (e.Text == "0")
            {
                e.Text = "N";
            }
            else if (e.Text == "180")
            {
                e.Text = "S";
            }
            else if (e.Text == "270")
            {
                e.Text = "W";
            }
        }
    }
}