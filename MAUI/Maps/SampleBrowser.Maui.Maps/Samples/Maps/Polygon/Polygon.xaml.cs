#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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