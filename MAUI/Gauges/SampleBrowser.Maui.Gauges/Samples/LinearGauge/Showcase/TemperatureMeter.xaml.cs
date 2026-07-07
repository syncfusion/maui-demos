using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge;

public partial class TemperatureMeter : SampleView
{
	public TemperatureMeter()
	{
		InitializeComponent();
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        temperatureMeterGauge.Handler?.DisconnectHandler();
    }


    #region Temperature meter

    private void SfLinearGauge_LabelCreated_1(object? sender, LabelCreatedEventArgs e)
    {
        e.Text = e.Text + "°C";
    }

    #endregion

}