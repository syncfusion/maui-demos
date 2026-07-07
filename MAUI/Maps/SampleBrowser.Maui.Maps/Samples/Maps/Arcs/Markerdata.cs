using Syncfusion.Maui.Maps;

namespace SampleBrowser.Maui.Maps.SfMaps;

public class Markerdata 
{

    public string CountryName { get; set; }
    public MapLatLng Position { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="country"></param>
    /// <param name="position"></param>
    public Markerdata(string country, MapLatLng position)
	{
        this.CountryName = country;
        this.Position = position;
	}
}