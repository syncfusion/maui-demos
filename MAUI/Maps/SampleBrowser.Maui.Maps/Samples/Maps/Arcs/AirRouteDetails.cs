#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Maps;

namespace SampleBrowser.Maui.Maps.Samples.Maps.Arcs;

public class AirRouteDetails
{
	public MapLatLng From { get; set; }
    public MapLatLng To { get; set; }
    public string Destination { get; set; }
    public string Place { get; set; }

    

    public AirRouteDetails(MapLatLng from, MapLatLng to, string destination, string place)
	{
		this.From = from;
		this.To = to;
		this.Destination = destination;
		this.Place = place;
	}
}

public class PlaceDetails
{
	public string Place { get; set; }
	public Brush LayerFill { get; set; }
	public PlaceDetails(string place, Brush layerFill)
	{
		this.Place = place;
		this.LayerFill = layerFill;
	}
}