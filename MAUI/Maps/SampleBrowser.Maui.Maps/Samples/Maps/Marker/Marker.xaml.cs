#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Dispatching;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Maps;

namespace SampleBrowser.Maui.Maps.SfMaps;

public partial class Marker : SampleView
{
    private bool canStopTimer;
    public Marker()
	{
        InitializeComponent();
        if (Base.BaseConfig.IsIndividualSB)
        {
            this.shapeLayer.ShapesSource = MapSource.FromResource("SampleBrowser.Maui.Maps.ShapeSource.world-map.json");
        }
        else
        {
            this.shapeLayer.ShapesSource = MapSource.FromResource("SampleBrowser.Maui.ShapeSource.world-map.json");
        }

        StartTimer();
    }

    private void StopTimer()
    {
        canStopTimer = true;
    }

    private async void StartTimer()
    {
        await Task.Delay(500);
        if (Application.Current != null)
        {
            Application.Current.Dispatcher.StartTimer(new TimeSpan(0, 0, 0, 1, 0), UpdateMarker);
        }

        canStopTimer = false;
    }

    private bool UpdateMarker()
    {
        if (canStopTimer) return false;
        foreach (var marker in shapeLayer.Markers)
        {
            if (marker is CustomMarker customMarker)
            {
                if (customMarker.Name == "Seattle")
                {
                    customMarker.Time = DateTime.UtcNow.Subtract(new TimeSpan(7, 0, 0)).ToLongTimeString();
                }
                else if (customMarker.Name == "Belem")
                {
                    customMarker.Time = DateTime.UtcNow.Subtract(new TimeSpan(3, 0, 0)).ToLongTimeString();
                }
                else if (customMarker.Name == "Nuuk")
                {
                    customMarker.Time = DateTime.UtcNow.Subtract(new TimeSpan(2, 0, 0)).ToLongTimeString();
                }
                else if (customMarker.Name == "Yakutsk")
                {
                    customMarker.Time = DateTime.UtcNow.Add(new TimeSpan(9, 0, 0)).ToLongTimeString();
                }
                else if (customMarker.Name == "Delhi")
                {
                    customMarker.Time = DateTime.UtcNow.Add(new TimeSpan(5, 30, 0)).ToLongTimeString();
                }
                else if (customMarker.Name == "Brisbane")
                {
                    customMarker.Time = DateTime.UtcNow.Add(new TimeSpan(10, 0, 0)).ToLongTimeString();
                }
                else if (customMarker.Name == "Harare")
                {
                    customMarker.Time = DateTime.UtcNow.Add(new TimeSpan(2, 0, 0)).ToLongTimeString();
                }
            }
        }

        return true;
    }

    public override void OnDisappearing()
    {
        base.OnDisappearing();
        StopTimer();
    }

}