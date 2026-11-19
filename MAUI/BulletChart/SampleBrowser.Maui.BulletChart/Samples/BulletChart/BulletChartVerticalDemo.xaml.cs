using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.BulletChart.BulletChart;

public partial class BulletChartVerticalDemo : ContentView
{
	public BulletChartVerticalDemo()
	{
		InitializeComponent();
	}

    private void SfLinearGauge_LabelCreated(object? sender, LabelCreatedEventArgs e)
	{
        e.Text += "%"; 
    }

	private void SfLinearGauge_LabelCreated_1(object? sender, LabelCreatedEventArgs e)
	{
        if (e.Text == "0")
        {
            e.Text = "$" + e.Text;
        }
        else
        {
            e.Text = "$" + e.Text + "K";
        }
    }

	private void SfLinearGauge_LabelCreated_2(object? sender, LabelCreatedEventArgs e)
	{
        e.Text = "$" + e.Text;
    }
}