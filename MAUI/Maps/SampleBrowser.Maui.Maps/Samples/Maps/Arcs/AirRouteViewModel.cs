#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Maps.Samples.Maps.Arcs;
using Syncfusion.Maui.Maps;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Maps.SfMaps;

public class AirRouteViewModel
{
    public ObservableCollection<PlaceDetails> Places { get; set; }

    public ObservableCollection<AirRouteDetails> Airports { get; set; }

    public Array LayerTypeArray => Enum.GetValues<LayerType>();

    public AirRouteViewModel()
    {
        Places = new ObservableCollection<PlaceDetails>()
        {
            new PlaceDetails("New York",new SolidColorBrush(Color.FromRgb(167, 61, 233))),
        };

        Airports = new ObservableCollection<AirRouteDetails>()
        {
             new AirRouteDetails(new MapLatLng(40.7128, -74.0060),
                   new MapLatLng(4.7110, -74.0721), "New York - Bogata", "Bogata"),
             new AirRouteDetails( new MapLatLng(40.7128, -74.0060),
                   new MapLatLng(51.0447, -114.0719), "New York - Calgary","Calgary"),
             new AirRouteDetails( new MapLatLng(40.7128, -74.0060),
                   new MapLatLng(41.2995, 69.2401), "New York - Tashkent", "Tashkent"),
             new AirRouteDetails( new MapLatLng(40.7128, -74.0060),
                   new MapLatLng(14.7167, -17.4677), "New York - Dakar", "Dakar"),
             new AirRouteDetails( new MapLatLng(40.7128, -74.0060),
                   new MapLatLng(33.3700, -7.5857), "New York - Casablanca", "Casablanca"),
             new AirRouteDetails( new MapLatLng(40.7128, -74.0060),
                   new MapLatLng(29.7604, -95.3698), "New York - Houston", "Houston"),
             new AirRouteDetails( new MapLatLng(40.7128, -74.0060),
                   new MapLatLng(53.5461, -113.4938), "New York - Edmonton", "Edmonton"),
             new AirRouteDetails( new MapLatLng(40.7128, -74.0060),
                   new MapLatLng(8.9824, -79.5199), "New York - Panama City", "Panama City"),
             new AirRouteDetails( new MapLatLng(40.7128, -74.0060),
                   new MapLatLng(39.7392, -104.9903), "New york - Denver", "Denver")
        };
    }
}