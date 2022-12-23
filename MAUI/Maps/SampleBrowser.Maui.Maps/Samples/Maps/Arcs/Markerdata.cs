#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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