using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Gauges.SfLinearGauge;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge;

public partial class WaterLevelIndicator : SampleView
{
	public WaterLevelIndicator()
	{
		InitializeComponent();

        if (graphicsView.Drawable is GraphicsDrawable graphicsDrawable)
        {
            graphicsDrawable.WaterLevelFactor = waterLevelGauge.ValueToFactor(150);
        }
    }

    #region Water level indicator

    private void waterLevelShapePointer_ValueChanged(object? sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
    {
        waterLevelLabel.Text = (int)e.Value + " ml";
        if (graphicsView.Drawable is GraphicsDrawable graphicsDrawable)
        {
            graphicsDrawable.WaterLevelFactor = waterLevelGauge.ValueToFactor(e.Value);
        }

        graphicsView.Invalidate();

    }
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        waterLevelGauge.Handler?.DisconnectHandler();
    }

    #endregion
}