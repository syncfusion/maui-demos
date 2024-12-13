#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Globalization;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    public class FontAttributeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isActive)
            {
                if (isActive)
                {
                    return FontAttributes.Bold;
                }
                else
                {
                    return FontAttributes.None;
                }
            }
            return FontAttributes.None;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NavigationColorConverter : IValueConverter
    {
        // The Convert method to change bool to color
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isEnabled)
            {
                // Get the theme from the parameter
                var theme = Application.Current?.RequestedTheme;

                if (isEnabled)
                {
                    return theme == AppTheme.Light ? Color.FromArgb("#79747E") : Color.FromArgb("#ACACAC");
                }
                else
                {
                    return theme == AppTheme.Light ? Color.FromArgb("#CAC4D0") : Color.FromArgb("#49454F");

                }
            }

            return Colors.Transparent;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
