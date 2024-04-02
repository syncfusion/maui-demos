#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Globalization;

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class IconColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return Application.Current!.RequestedTheme == AppTheme.Light ? Color.FromArgb("#6200EE") : Color.FromArgb("#CAC4D0");
            else if ((bool)value)
                return Application.Current!.RequestedTheme == AppTheme.Light ? Color.FromArgb("#6200EE") : Color.FromArgb("#CAC4D0");
            else
                return Application.Current!.RequestedTheme == AppTheme.Light ? Color.FromArgb("#b3b3b3") : Color.FromArgb("#CAC4D0");
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class SelectionIconConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return "\ue7D0";
            else if ((bool)value)
                return "\ue722";
            else
                return "\ue723";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }   
}
