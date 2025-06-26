#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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
                    if (Application.Current!.UserAppTheme == AppTheme.Dark)
                    {
                        return Color.FromArgb("#A15C07");
                    }
                    else
                    {
                        return Color.FromArgb("#FFD6AE");
                    }
                }
                else
                {
                    if (Application.Current!.UserAppTheme == AppTheme.Dark)
                    {
                        return Color.FromArgb("#542C0D");
                    }
                    else
                    {
                        return Color.FromArgb("#FF9C66");
                    }
                }
            }
            else if (columnName!.Equals("QS2"))
            {
                if (valueNew < 6500 && valueNew > 2000)
                {
                    if (Application.Current!.UserAppTheme == AppTheme.Dark)
                    {
                        return Color.FromArgb("#2B4212");
                    }
                    else
                    {
                        return Color.FromArgb("#A6EF67");
                    }
                }
                else
                {
                    if (Application.Current!.UserAppTheme == AppTheme.Dark)
                    {
                        return Color.FromArgb("#4F7A21");
                    }
                    else
                    {
                        return Color.FromArgb("#D0F8AB");
                    }
                }
            }
            else if (columnName!.Equals("QS3"))
            {
                if (valueNew < 6500 && valueNew > 2000)
                {
                    if (Application.Current!.UserAppTheme == AppTheme.Dark)
                    {
                        return Color.FromArgb("#0B4A6F");
                    }
                    else
                    {
                        return Color.FromArgb("#A4BCFD");
                    }
                }
                else
                {
                    if (Application.Current!.UserAppTheme == AppTheme.Dark)
                    {
                        return Color.FromArgb("#026AA2");
                    }
                    else
                    {
                        return Color.FromArgb("#C7D7FE");
                    }
                }
            }
            else
            {
                if (valueNew < 6500 && valueNew > 2000)
                {
                    if (Application.Current!.UserAppTheme == AppTheme.Dark)
                    {
                        return Color.FromArgb("#134E48");
                    }
                    else
                    {
                        return Color.FromArgb("#73E2A3");
                    }
                }
                else
                {
                    if (Application.Current!.UserAppTheme == AppTheme.Dark)
                    {
                        return Color.FromArgb("#107569");
                    }
                    else
                    {
                        return Color.FromArgb("#AAF0C4");
                    }
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
