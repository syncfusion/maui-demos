#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Globalization;
using Syncfusion.Maui.Sliders;

namespace SampleBrowser.Maui.Sliders.VerticalSlider;

public class BoolToEdgeLabelsPlacementConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value != null && (bool)value)
        {
            return SliderEdgeLabelsPlacement.Inside;
        }
        else
        {
            return SliderEdgeLabelsPlacement.Default;
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value != null)
        {
            return (SliderEdgeLabelsPlacement)value == SliderEdgeLabelsPlacement.Inside;
        }
        return false;
    }
}