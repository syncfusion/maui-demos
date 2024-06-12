#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Maps.SfMaps;

public partial class OpenStreetMap : SampleView
{
    private Button? previousSelectedButton;
    
    public OpenStreetMap()
	{
		InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (previousSelectedButton != null)
        {
            previousSelectedButton.Background = Colors.White;
            previousSelectedButton.TextColor = Colors.Black;
        }

        Button button = previousSelectedButton = (Button)sender;
        string place = button.Text;
        button.Background = Color.FromRgba("#6750A4");
        button.TextColor  = Colors.White;
        if (place.Equals("Chichen Itza"))
        {
            layer.Center = new Syncfusion.Maui.Maps.MapLatLng(20.6843, -88.5678);
        }
        else if (place.Equals("Machu Picchu"))
        {
            layer.Center = new Syncfusion.Maui.Maps.MapLatLng(-13.1631, -72.5450);
        }
        else if (place.Equals("Christ the Redeemer"))
        {
            layer.Center = new Syncfusion.Maui.Maps.MapLatLng(-22.9519, -43.2105);
        }
        else if (place.Equals("Colosseum"))
        {
            layer.Center = new Syncfusion.Maui.Maps.MapLatLng(41.8902, 12.4922);
        }
        else if (place.Equals("Petra"))
        {
            layer.Center = new Syncfusion.Maui.Maps.MapLatLng(30.3285, 35.4444);
        }
        else if (place.Equals("Taj Mahal"))
        {
            layer.Center = new Syncfusion.Maui.Maps.MapLatLng(27.1751, 78.0421);
        }
        else if (place.Equals("Great Wall of China"))
        {
            layer.Center = new Syncfusion.Maui.Maps.MapLatLng(40.4319, 116.5704);
        }
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
