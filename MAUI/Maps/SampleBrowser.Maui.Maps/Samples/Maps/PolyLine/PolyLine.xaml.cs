#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Maps;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.Json;

namespace SampleBrowser.Maui.Maps.SfMaps;

public partial class PolyLine : SampleView
{
    private Button? previousSelectedButton;
    public ObservableCollection<PolylinePlaceDetails> PolylinePlaces { get; set; }

    public PolyLine()
	{
        InitializeComponent();
        this.UpdatePolyline("london_to_british");
        startMarker.Name = "London heatrow";
        endMarker.Name = "The British Museum";

#if MACCATALYST
        collectionView.MaximumHeightRequest = 33;
        collectionView.MaximumWidthRequest = 850;
#elif IOS
        collectionView.MaximumHeightRequest = 33;
        collectionView.MaximumWidthRequest = 370;
#endif
        PolylinePlaces = new ObservableCollection<PolylinePlaceDetails>()
        {
            new PolylinePlaceDetails("The British Museum"),
            new PolylinePlaceDetails("The Windsor Castle"),
            new PolylinePlaceDetails("Twickenham Stadium"),
            new PolylinePlaceDetails("Chessington World of Adventures"),
            new PolylinePlaceDetails("Hampton Court Palace")
        };

        this.BindingContext = this;
    }

    private void UpdatePolyline(string resource)
    {
        if (BaseConfig.IsIndividualSB)
        {
            polyLine.Points = GetJsondata("SampleBrowser.Maui.Maps.ShapeSource." + resource +".json");
        }
        else
        {
            polyLine.Points = GetJsondata("SampleBrowser.Maui.ShapeSource."+ resource + ".json");
        }

        startMarker.Latitude = polyLine.Points[0].Longitude;
        startMarker.Longitude = polyLine.Points[0].Latitude;
        endMarker.Latitude = polyLine.Points[polyLine.Points.Count - 1].Longitude;
        endMarker.Longitude = polyLine.Points[polyLine.Points.Count - 1].Latitude;
    }

    public static List<MapLatLng> GetJsondata(string resource)
    {
        var shapeStream = Assembly.GetCallingAssembly().GetManifestResourceStream(resource);
        string jsonText = "";
        if (shapeStream != null)
        {
            using (var reader = new System.IO.StreamReader(shapeStream))
            {
                jsonText = reader.ReadToEnd();
            }

           return ReadJsonFromText(jsonText);
        }

        return new List<MapLatLng>();
    }

    public static List<MapLatLng> ReadJsonFromText(string jsonText)
    {
        List<MapLatLng> polylinePointsList = new List<MapLatLng>();
        var shapePathElement = JsonSerializer.Deserialize<JsonElement>(jsonText);
        JsonElement features;
        bool shapeDataPropertyCheck = false;
        if (shapePathElement.TryGetProperty("features", out features))
        {
            features = shapePathElement.GetProperty("features");
            shapeDataPropertyCheck = true;
        }

        int featureLength = features.GetArrayLength();
        for (int j = 0; j < featureLength; j++)
        {
            JsonElement featureArray = features[j];
            JsonElement geometryArray = shapeDataPropertyCheck ? featureArray.GetProperty("geometry") : featureArray; string geometryArrayType = geometryArray.GetProperty("type").ToString();
            JsonElement coordinatesArray = geometryArray.GetProperty("coordinates");
            if (geometryArrayType == "Polyline")
            {
                int outerLength = coordinatesArray.GetArrayLength();
                for (int a = 0; a < outerLength; a++)
                {
                    double lat = coordinatesArray[a][0].GetDouble();
                    double lon = coordinatesArray[a][1].GetDouble();
                    polylinePointsList.Add(new MapLatLng((float)lat, (float)lon));
                }
            }

            if (geometryArrayType == "Polygon")
            {
                int outerLength = coordinatesArray.GetArrayLength();
                for (int a = 0; a < outerLength; a++)
                {
                    int innerLength = coordinatesArray[a].GetArrayLength();
                    for (int b = 0; b < innerLength; b++)
                    {
                        double lat = coordinatesArray[a][b][0].GetDouble();
                        double lon = coordinatesArray[a][b][1].GetDouble();
                        polylinePointsList.Add(new MapLatLng((float)lat, (float)lon));
                    }
                }
            }
        }

        return polylinePointsList;
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
        button.Background = Color.FromRgb(98, 0, 238);
        button.TextColor = Colors.White;
        if (place.Equals("The British Museum"))
        {
            layer.Center = new MapLatLng(51.4700, -0.2843);
            zoomPanBehavior.ZoomLevel = 10;
            endMarker.Name = place;
            this.UpdatePolyline("london_to_british");
        }
        else if (place.Equals("The Windsor Castle"))
        {
            layer.Center = new MapLatLng(51.4700, -0.5443);
            zoomPanBehavior.ZoomLevel = 11;
            endMarker.Name = place;
            this.UpdatePolyline("london_to_windsor_castle");
        }
        else if (place.Equals("Twickenham Stadium"))
        {
            layer.Center = new MapLatLng(51.4700, -0.3843);
            zoomPanBehavior.ZoomLevel = 11;
            endMarker.Name = place;
            this.UpdatePolyline("london_to_twickenham_stadium");
        }
        else if (place.Equals("Chessington World of Adventures"))
        {
            layer.Center = new MapLatLng(51.4050, -0.4300);
            zoomPanBehavior.ZoomLevel = 10;
            endMarker.Name = place;
            this.UpdatePolyline("london_to_chessington");
        }
        else if (place.Equals("Hampton Court Palace"))
        {
            layer.Center = new MapLatLng(51.4500, -0.4393);
            zoomPanBehavior.ZoomLevel = 11;
            endMarker.Name = place;
            this.UpdatePolyline("london_to_hampton_court_palace");
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

public class MarkerTemplateSelector : DataTemplateSelector
{
    public DataTemplate? CustomTemplate { get; set; }
    public DataTemplate? DefaultTemplate { get; set; }

    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        return (double)((MapMarker)item).Latitude == 51.520004272460938 ? CustomTemplate : DefaultTemplate;
    }
}

public class PolylinePlaceDetails
{
    public string Place { get; set; }

    public PolylinePlaceDetails(string place)
    {
        this.Place = place;
    }
}