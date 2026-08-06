using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge;

public partial class HeightCalculator : SampleView
{
	public HeightCalculator()
	{
		InitializeComponent();
	}

    #region Height calculator

    private void SfLinearGauge_LabelCreated(object? sender, LabelCreatedEventArgs e)
    {
        e.Text = e.Text + " cm";
    }

    private void Pointer1_ValueChanged(object? sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
    {
        heightAnnotationLabel.Text = (int)e.Value + " cm";
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        heightCalculatorGauge.Handler?.DisconnectHandler();
    }
    #endregion
}