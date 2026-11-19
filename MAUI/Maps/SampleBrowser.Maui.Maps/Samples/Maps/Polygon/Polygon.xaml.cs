using Microsoft.Maui.Controls.Shapes;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Maps;
using System.Reflection;
using System.Text.Json;

namespace SampleBrowser.Maui.Maps.SfMaps;

public partial class Polygon : SampleView
{
    public Polygon()
	{
        InitializeComponent();
        if (BaseConfig.IsIndividualSB)
        {
            polygon.Points = PolyLine.GetJsondata("SampleBrowser.Maui.Maps.ShapeSource.maps_brazil_boundary.json");
        }
        else
        {
            polygon.Points = PolyLine.GetJsondata("SampleBrowser.Maui.ShapeSource.maps_brazil_boundary.json");
        }

        polygon.Fill = new SolidColorBrush(Color.FromRgba(0, 0, 0, 0.1));
    }

    private void Uri_Tapped(object? sender, EventArgs e)
    {
        try
        {
            Uri uri = new Uri("https://www.openstreetmap.org/copyright");
            Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception)
        {
        }
    }
}