using SampleBrowser.Maui.Base;
using System.Globalization;
namespace SampleBrowser.Maui.SunburstChart.SfSunburstChart;

public partial class ToolTip : SampleView
{
    public ToolTip()
    {
        InitializeComponent();
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();

        sunburstChart.Handler?.DisconnectHandler();
    }
}