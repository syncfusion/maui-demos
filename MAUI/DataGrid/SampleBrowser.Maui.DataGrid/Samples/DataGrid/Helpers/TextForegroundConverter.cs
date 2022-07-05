#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    internal class TextForegroundConverter : IValueConverter
    {
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="info">The culture to use during the conversion.</param>
        /// <summary>Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="info" />.</summary>
        /// <returns>To be added.</returns>
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo info)
        {
            var data = value as double?;
            if (data != null && data > 10)
            {
                return Colors.Green;
            }
            else
            {
                return Colors.Red;
            }
        }

        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="info">The culture to use during the conversion.</param>
        /// <summary> Implement this method to convert <paramref name="value" /> back from <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="info" />.</summary>
        /// <returns> To be added.</returns>
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
