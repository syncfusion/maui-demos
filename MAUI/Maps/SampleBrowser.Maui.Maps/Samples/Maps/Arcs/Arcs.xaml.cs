#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Maps.Samples.Maps.Arcs;
using Syncfusion.Maui.Maps;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Maps.SfMaps;

public partial class Arcs : SampleView
{
    private bool isEnableDashArray;
    private bool isLineLayer;
    private int index = 0;
    public Arcs()
    {
        InitializeComponent();
        isEnableDashArray = false;
        isLineLayer = false;
        UpdateMarkers();
        GetArcLayer();
    }

    private void UpdateMarkers()
    {
        MapMarkerCollection mapMarkers = new MapMarkerCollection();
        if (this.BindingContext is AirRouteViewModel airRouteViewModel)
        {
            var airPorts = airRouteViewModel.Airports;
            var places = airRouteViewModel.Places[index];
            mapMarkers.Add(new CustomMarker()
            {
                Latitude = airPorts[0].From.Latitude,
                Longitude = airPorts[0].From.Longitude,
                IconHeight = 15,
                IconWidth = 15,
                Name = places.Place,
                IconType = MapIconType.Circle,
                IconFill = places.LayerFill,
                IconStroke = places.LayerFill,
            });

            for (int i = 0; i < airPorts.Count; i++)
            {
                mapMarkers.Add(new CustomMarker()
                {
                    Latitude = airPorts[i].To.Latitude,
                    Longitude = airPorts[i].To.Longitude,
                    IconHeight = 15,
                    IconWidth = 15,
                    Name = airPorts[i].Place,
                    IconType = MapIconType.Circle,
                    IconFill = places.LayerFill,
                    IconStroke = places.LayerFill,
                });
            }
        }

        layer.Markers = mapMarkers;
    }
    private void GetLineLayer()
    {
        if (this.BindingContext is AirRouteViewModel airRouteViewModel)
        {
            var airPorts = airRouteViewModel.Airports;
            var places = airRouteViewModel.Places[index];
            MapLineLayer lineLayer = new MapLineLayer();
            MapMarkerCollection mapMarkers = new MapMarkerCollection();
            lineLayer.Lines = new ObservableCollection<MapLine>();

            for (int i = 0; i < airPorts.Count; i++)
            {
                lineLayer.Lines.Add(new MapLine()
                {
                    From = new MapLatLng(airPorts[i].From.Latitude, airPorts[i].From.Longitude),
                    To = new MapLatLng(airPorts[i].To.Latitude, airPorts[i].To.Longitude),
                    StrokeLineCap = LineCap.Round,
                    StrokeThickness = 2,
                    Stroke = places.LayerFill,
                    StrokeDashArray = isEnableDashArray ? new DoubleCollection() { 8, 2, 2, 2 } : new DoubleCollection() { 0, 0 }

                });
            }

            layer.Sublayers = new ObservableCollection<MapSublayer>()
            {
                 lineLayer,
            };
        }
    }

    private void GetArcLayer()
    {
        if (this.BindingContext is AirRouteViewModel airRouteViewModel)
        {
            var airPorts = airRouteViewModel.Airports;
            var places = airRouteViewModel.Places[index];
            MapArcLayer arcLayer = new MapArcLayer();
            arcLayer.Arcs = new ObservableCollection<MapArc>();
            for (int i = 0; i < airPorts.Count; i++)
            {
                arcLayer.Arcs.Add(new MapArc()
                {
                    From = new MapLatLng(airPorts[i].From.Latitude, airPorts[i].From.Longitude),
                    To = new MapLatLng(airPorts[i].To.Latitude, airPorts[i].To.Longitude),
                    StrokeThickness = 2,
                    Stroke = places.LayerFill,
                    StrokeDashArray = isEnableDashArray ? new DoubleCollection() { 8, 2, 2, 2 } : new DoubleCollection() { 0, 0 },
                    HeightFactor = 0.2
                });
            }

            layer.Sublayers = new ObservableCollection<MapSublayer>()
            {
                arcLayer
            };
        }
    }

    private void UpdateLayer()
    {
        if(isLineLayer)
        {
            GetLineLayer();
        }
        else
        {
            GetArcLayer();
        }
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (picker.SelectedIndex == 0)
        {
            isLineLayer = false;
        }
        else
        {
            isLineLayer = true;
        }

        UpdateLayer();
    }

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        isEnableDashArray = e.Value;
        UpdateLayer();
    }

    private void Uri_Tapped(object sender, EventArgs e)
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
