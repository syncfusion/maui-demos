using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SampleBrowser.Maui.TreeGrid
{
    public class HikeBackgroundConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is double hike)
            {
                if (hike >= 10)
                {
                    if (Application.Current!.UserAppTheme == AppTheme.Dark)
                    {
                        return Color.FromArgb("#0B4A6F");
                    }
                    else
                    {
                        return Color.FromArgb("#A4BCFD");
                    }
                }
                else
                {
                    if (Application.Current!.UserAppTheme == AppTheme.Dark)
                    {
                        return Color.FromArgb("#026AA2");
                    }
                    else
                    {
                        return Color.FromArgb("#C7D7FE");
                    }
                }
            }
                return Colors.Transparent;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
