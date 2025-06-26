#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Maps;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.Maps;

public class MarkerViewModel
{
    public ObservableCollection<CustomMarker>? Markers { get; set; }

    public MarkerViewModel()
    {
        this.Markers = new ObservableCollection<CustomMarker>();
        var currentTime = DateTime.UtcNow;
        this.Markers.Add(new CustomMarker()
        {
            Latitude = 47.60621,
            Longitude = -122.332071,
            Name = "Seattle",
            Time = currentTime.Subtract(new TimeSpan(7, 0, 0)).ToLongTimeString(),
            Offset = new Point(0, -4),
            IconWidth = 150,
            IconHeight = 150
        });


        this.Markers.Add(new CustomMarker()
        {
            Latitude = -1.455833,
            Longitude = -48.503887,
            Name = "Belem",
            Time = currentTime.Subtract(new TimeSpan(3, 0, 0)).ToLongTimeString(),
            Offset = new Point(0, -4),
            IconWidth = 150,
            IconHeight = 150
        });

        this.Markers.Add(new CustomMarker()
        {
            Latitude = 64.10,
            Longitude = -51.44,
            Name = "Nuuk",
            Time = currentTime.Subtract(new TimeSpan(2, 0, 0)).ToLongTimeString(),
            Offset = new Point(0, -4),
            IconWidth = 150,
            IconHeight = 150
        });

        this.Markers.Add(new CustomMarker()
        {
            Latitude = 62.035452,
            Longitude = 129.675475,
            Name = "Yakutsk",
            Time = currentTime.Add(new TimeSpan(9, 0, 0)).ToLongTimeString(),
            Offset = new Point(0, -4),
            IconWidth = 150,
            IconHeight = 150
        });

        this.Markers.Add(new CustomMarker()
        {
            Latitude = 28.704059,
            Longitude = 77.10249,
            Name = "Delhi",
            Time = currentTime.Add(new TimeSpan(5, 30, 0)).ToLongTimeString(),
            Offset = new Point(0, -4),
            IconWidth = 150,
            IconHeight = 150
        });

        this.Markers.Add(new CustomMarker()
        {
            Latitude = -27.469771,
            Longitude = 153.025124,
            Name = "Brisbane",
            Time = currentTime.Add(new TimeSpan(10, 0, 0)).ToLongTimeString(),
            Offset = new Point(0, -4),
            IconWidth = 150,
            IconHeight = 150
        });

        this.Markers.Add(new CustomMarker()
        {
            Latitude = -17.825166,
            Longitude = 31.03351,
            Name = "Harare",
            Time = currentTime.Add(new TimeSpan(2, 0, 0)).ToLongTimeString(),
            Offset = new Point(0, -4),
            IconWidth = 150,
            IconHeight = 150
        });
    }
}