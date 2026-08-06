using SampleBrowser.Maui.Base;
using Syncfusion.Maui.SunburstChart;
using MAUIPicker = Microsoft.Maui.Controls.Picker;

namespace SampleBrowser.Maui.SunburstChart.SfSunburstChart;

public partial class DrillDown : SampleView
{
    public DrillDown()
    {
        InitializeComponent();
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();

        sunburstChart.Handler?.DisconnectHandler();
    }
}
