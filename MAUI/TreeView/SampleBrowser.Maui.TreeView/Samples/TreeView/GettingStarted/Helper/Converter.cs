#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Syncfusion.Maui.TreeView;
using System;
using System.Globalization;


namespace SampleBrowser.Maui.TreeView
{
    public class TreeViewBoolToSortImageConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "\ue747";
            }
            var sortOptions = (TreeViewSortDirection)value;
            if (sortOptions == TreeViewSortDirection.Ascending)
                return "\ue747";
            else if (sortOptions == TreeViewSortDirection.Descending)
                return "\ue748";
            else
                return "\ue747";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
