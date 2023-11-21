#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Maps.SfMaps;

public class WonderViewModel
{
    public ObservableCollection<CustomMarker>? Markers { get; set; }

    public WonderViewModel() 
    {
        this.Markers = new ObservableCollection<CustomMarker>();
        this.Markers.Add(new CustomMarker()
        {
            Latitude = 20.6843,
            Longitude = -88.5678,
            Name = "Chichen Itza",
            State = "Yucatan",
            Country = "Mexico",
            VerticalAlignment = Syncfusion.Maui.Maps.MapAlignment.Start,
            HorizontalAlignment = Syncfusion.Maui.Maps.MapAlignment.Center,
        });

        this.Markers.Add(new CustomMarker()
        {
            Latitude = -13.1631,
            Longitude = -72.5450,
            Name = "Machu Picchu",
            State = "Cuzco",
            Country = "Peru",
            VerticalAlignment = Syncfusion.Maui.Maps.MapAlignment.Start,
            HorizontalAlignment = Syncfusion.Maui.Maps.MapAlignment.Center,
        });

        this.Markers.Add(new CustomMarker()
        {
            Latitude = -22.9519,
            Longitude = -43.2105,
            Name = "Christ the Redeemer",
            State = "Rio de Janeiro",
            Country = "Brazil",
            VerticalAlignment = Syncfusion.Maui.Maps.MapAlignment.Start,
            HorizontalAlignment = Syncfusion.Maui.Maps.MapAlignment.Center,
        });

        this.Markers.Add(new CustomMarker()
        {
            Latitude = 41.8902,
            Longitude = 12.4922,
            Name = "Colosseum",
            State = "Regio III Isis et Serapis",
            Country = "Rome",
            VerticalAlignment = Syncfusion.Maui.Maps.MapAlignment.Start,
            HorizontalAlignment = Syncfusion.Maui.Maps.MapAlignment.Center,
        });

        this.Markers.Add(new CustomMarker()
        {
            Latitude = 30.3285,
            Longitude = 35.4444,
            Name = "Petra",
            State = "Ma'an Governorate",
            Country = "Jordan",
            VerticalAlignment = Syncfusion.Maui.Maps.MapAlignment.Start,
            HorizontalAlignment = Syncfusion.Maui.Maps.MapAlignment.Center,
        });

        this.Markers.Add(new CustomMarker()
        {
            Latitude = 27.1751,
            Longitude = 78.0421,
            Name = "Taj Mahal",
            State = "Uttar Pradesh",
            Country = "India",
            VerticalAlignment = Syncfusion.Maui.Maps.MapAlignment.Start,
            HorizontalAlignment = Syncfusion.Maui.Maps.MapAlignment.Center,
        });

        this.Markers.Add(new CustomMarker()
        {
            Latitude = 40.4319,
            Longitude = 116.5704,
            Name = "Great Wall of China",
            State = "Beijing",
            Country = "China",
            VerticalAlignment = Syncfusion.Maui.Maps.MapAlignment.Start,
            HorizontalAlignment = Syncfusion.Maui.Maps.MapAlignment.Center,
        });
    }
}
