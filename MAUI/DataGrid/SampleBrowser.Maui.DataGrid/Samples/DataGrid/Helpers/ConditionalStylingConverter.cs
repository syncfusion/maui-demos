#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    internal class ConditionalStylingConverter : IValueConverter
    {
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <summary>Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.</summary>
        /// <returns>To be added.</returns>
        public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            double valueNew = (double)value!;
            string? columnName = parameter!.ToString();
            if (columnName!.Equals("QS1"))
            {
                if (valueNew < 6500 && valueNew > 2000)
                {
                    return Color.FromArgb("#F4C5BB");
                }
                else
                {
                    return Color.FromArgb("#EA552B");
                }
            }
            else if (columnName!.Equals("QS2"))
            {
                if (valueNew < 6500 && valueNew > 2000)
                {
                    return Color.FromArgb("#F8DBAF");
                }
                else
                {
                    return Color.FromArgb("#F6BD16");
                }
            }
            else if (columnName!.Equals("QS3"))
            {
                if (valueNew < 6500 && valueNew > 2000)
                {
                    return Color.FromArgb("#C58FC1");
                }
                else
                {
                    return Color.FromArgb("#8B3C97");
                }
            }
            else
            {
                if (valueNew < 6500 && valueNew > 2000)
                {
                    return Color.FromArgb("#7BC182");
                }
                else
                {
                    return Color.FromArgb("#4CAB4D");
                }
            }
        }

        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <summary> Implement this method to convert <paramref name="value" /> back from <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.</summary>
        /// <returns> To be added.</returns>
        public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
