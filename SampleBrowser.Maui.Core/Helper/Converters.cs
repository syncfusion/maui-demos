#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System.Globalization;

namespace SampleBrowser.Maui.Core
{
    internal class TextToTagsConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tagstring = (string)value;
            if (!string.IsNullOrEmpty(tagstring))
            {
                if (tagstring.Equals("new", StringComparison.OrdinalIgnoreCase))
                {
                    return "N";
                }
                else
                {
                    return "U";
                }
            }
            return null;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    internal class TextToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tagstring = (string)value;
            if (!string.IsNullOrEmpty(tagstring))
            {
#if WINDOWS
                return new Thickness(-15, 0, 0, 0);
#else
				return new Thickness(-15, 0, 5, 0);
#endif
            }
            return new Thickness(0);
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    internal class TextToPaddingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tagstring = (string)value;
            if (!string.IsNullOrEmpty(tagstring))
            {
                return new Thickness(30, 5, 30, 5);
            }
            return new Thickness(15, 5, 15, 5);
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
