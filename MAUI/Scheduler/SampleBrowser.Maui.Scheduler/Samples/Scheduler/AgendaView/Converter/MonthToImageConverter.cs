using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Base.Converters;
using System;
using System.Globalization;
using System.Reflection;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    /// <summary>
    /// Converter used to convert agenda view month header.
    /// </summary>
    internal class MonthToImageConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var monthName = String.Format("{0:MMMM}", value).ToLower();
                return ImageSource.FromResource(typeof(SfImageSourceConverter).GetTypeInfo().Assembly.GetName().Name + ".Resources.Images." + monthName + ".png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly);
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}


