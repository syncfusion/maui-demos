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
    public class ExpandCollapseIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if((bool)value)
            {
                return "\ue72C";
            }
            else
            {
                return "\ue72B";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FoodSelectionIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return "\ue72D";
            }
            else
            {
                return "\ue72E";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
