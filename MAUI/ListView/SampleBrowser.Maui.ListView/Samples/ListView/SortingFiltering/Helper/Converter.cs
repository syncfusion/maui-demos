using Microsoft.Maui.Controls;
using System;
using System.Globalization;


namespace SampleBrowser.Maui.ListView.SfListView
{
    public class ListViewBoolToSortImageConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "\ue745";
            }
            var sortOptions = (ListViewSortOptions)value;
            if (sortOptions == ListViewSortOptions.Ascending)
                return "\ue745";
            else if (sortOptions == ListViewSortOptions.Descending)
                return "\ue746";
            else
                return "\ue745";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
