#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class ListViewRatingColorConverter : IValueConverter
    {
        int ratingCount = 0;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ratingCount++;
            if (ratingCount > 5)
            {
                ratingCount = 1;
            }
            if (ratingCount <= (int)value)
            {              
                return Color.FromArgb("#F9BC16");              
            }
            else
            {
                return Color.FromArgb("#BDBDBB");
            }          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
