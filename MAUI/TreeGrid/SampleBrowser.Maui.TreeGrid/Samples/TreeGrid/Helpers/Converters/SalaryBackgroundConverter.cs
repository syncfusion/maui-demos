using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SampleBrowser.Maui.TreeGrid
{
    public class SalaryBackgroundConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is double salary)
            {
                if (salary <= 50000)
                {
                    if (Application.Current!.UserAppTheme == AppTheme.Dark)
                    {
                        return Color.FromArgb("#A15C07");
                    }
                    else
                    {
                        return Color.FromArgb("#FFD6AE");
                    }
                }
                else
                {
                    if (Application.Current!.UserAppTheme == AppTheme.Dark)
                    {
                        return Color.FromArgb("#542C0D");
                    }
                    else
                    {
                        return Color.FromArgb("#FF9C66");
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
