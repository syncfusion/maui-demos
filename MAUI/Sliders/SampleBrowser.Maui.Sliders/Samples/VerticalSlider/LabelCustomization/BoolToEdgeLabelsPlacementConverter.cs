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