#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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