#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using System;
using System.Globalization;


namespace SampleBrowser.Maui.ListView.SfListView
{
    public class ListViewBoolToSortImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sortOptions = (ListViewSortOptions)value;
            if (sortOptions == ListViewSortOptions.Ascending)
                return "\ue709";
            else if (sortOptions == ListViewSortOptions.Descending)
                return "\ue719";
            else
                return "\ue709";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
