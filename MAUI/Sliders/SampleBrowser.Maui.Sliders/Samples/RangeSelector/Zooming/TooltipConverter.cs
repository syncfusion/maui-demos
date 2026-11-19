using System.Globalization;
namespace SampleBrowser.Maui.Sliders.SfRangeSelector;

public class TooltipConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
           if(value != null && value is ZoomingModel model)
            {
                if(parameter != null && parameter.ToString() == "X")
                {
                    return model.X;
                }
                else if(parameter != null && parameter.ToString() == "Y")
                {
                   return model.Y;
                }
            }
           return null;      
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }