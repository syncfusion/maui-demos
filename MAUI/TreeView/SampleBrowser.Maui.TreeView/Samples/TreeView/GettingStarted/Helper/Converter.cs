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
