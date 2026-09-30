using Microsoft.Maui.Platform;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Buttons;
using Syncfusion.Maui.Charts;
using MAUIPicker = Microsoft.Maui.Controls.Picker;

namespace SampleBrowser.Maui.PolarChart.SfPolarChart;

public partial class DefaultPolar : SampleView
{
    public DefaultPolar()
    {
        InitializeComponent();
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        Chart.Handler?.DisconnectHandler();
    }

    private void Switch_StateChanged(object? sender, SwitchStateChangedEventArgs e)
    {
        bool state = e.NewValue ?? false;

        if (area1 is null || area2 is null || area3 is null ||
             line1 is null || line2 is null || line3 is null)
        {
            return;
        }

        area1.IsClosed = area2.IsClosed = area3.IsClosed = state;
        line1.IsClosed = line2.IsClosed = line3.IsClosed = state;
    }

    private void Angle_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (sender is not MAUIPicker picker || Chart is null)
            return;

        int selectedIndex = picker.SelectedIndex;
        if (selectedIndex == 0)
        {
            Chart.StartAngle = ChartPolarAngle.Rotate0;
        }
        else if (selectedIndex == 1)
        {
            Chart.StartAngle = ChartPolarAngle.Rotate90;
        }
        else if (selectedIndex == 2)
        {
            Chart.StartAngle = ChartPolarAngle.Rotate180;
        }
        else
        {
            Chart.StartAngle = ChartPolarAngle.Rotate270;
        }
    }

    private void Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (sender is not MAUIPicker picker)
            return;

        bool showArea = picker.SelectedIndex == 0;
        bool showLine = picker.SelectedIndex == 1;

        area1.IsVisible = area2.IsVisible = area3.IsVisible = showArea;
        area1.IsVisibleOnLegend = area2.IsVisibleOnLegend = area3.IsVisibleOnLegend = showArea;

        line1.IsVisible = line2.IsVisible = line3.IsVisible = showLine;
        line1.IsVisibleOnLegend = line2.IsVisibleOnLegend = line3.IsVisibleOnLegend = showLine;
    }
}