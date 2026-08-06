using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Maps;
using System.Reflection;
using System.Text.Json;

namespace SampleBrowser.Maui.Maps.SfMaps;

public partial class Sublayer : SampleView
{
    public Sublayer()
    {
        InitializeComponent();
        if (BaseConfig.IsIndividualSB)
        {
            this.shapeLayer.ShapesSource = MapSource.FromResource("SampleBrowser.Maui.Maps.ShapeSource.australia.shp");
            this.sublayer.ShapesSource = MapSource.FromResource("SampleBrowser.Maui.Maps.ShapeSource.river.json");
        }
        else
        {
            this.shapeLayer.ShapesSource = MapSource.FromResource("SampleBrowser.Maui.ShapeSource.australia.shp");
            this.sublayer.ShapesSource = MapSource.FromResource("SampleBrowser.Maui.ShapeSource.river.json");
        }
    }
}