using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class FavoriteIconConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return "\ue78A";
            else if ((bool)value)
                return "\ue7CC";
            else
                return "\ue78A";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class FavoriteIconColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return Application.Current!.RequestedTheme != AppTheme.Light ? Color.FromArgb("#E6E1E5") : Color.FromArgb("#1C1B1F");
            else if ((bool)value)
                return Application.Current!.RequestedTheme != AppTheme.Light ? Color.FromArgb("#B3261E") : Color.FromArgb("#C10000");
            else
                return Application.Current!.RequestedTheme != AppTheme.Light ? Color.FromArgb("#E6E1E5") : Color.FromArgb("#1C1B1F");
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
