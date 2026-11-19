using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Maps;

namespace SampleBrowser.Maui.Maps.SfMaps;

public partial class Bubble : SampleView
{
	public Bubble()
	{
        InitializeComponent();
        if (Base.BaseConfig.IsIndividualSB)
        {
            this.shapeLayer.ShapesSource = MapSource.FromResource("SampleBrowser.Maui.Maps.ShapeSource.world-map.json");
        }
        else
        {
            this.shapeLayer.ShapesSource = MapSource.FromResource("SampleBrowser.Maui.ShapeSource.world-map.json");
        }
    }
}