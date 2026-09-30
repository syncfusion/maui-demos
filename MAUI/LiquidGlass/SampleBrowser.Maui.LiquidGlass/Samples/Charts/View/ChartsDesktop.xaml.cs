using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfCartesianChart;

public partial class ChartsDesktop : SampleView
{
	public ChartsDesktop()
	{
		InitializeComponent();
	}

    public override void OnDisappearing()
    {
        base.OnDisappearing();

        cartesianChart.Handler?.DisconnectHandler();
        funnelChart.Handler?.DisconnectHandler();
        sunburstChart.Handler?.DisconnectHandler();
    }
}