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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class ImageConverter : IValueConverter
    {
        private double? data;

        #region IValueConverter implementation

        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <summary>Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.</summary>
        /// <returns>To be added.</returns>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            this.data = double.Parse(value!.ToString()!.ToCharArray());
            var fontFamily = GetFontFamily();
            if (this.data != null && this.data > 0)
            {
                if (parameter is Label)
                {
                    var label = parameter as Label;
                    label!.FontFamily = fontFamily;
                    label!.TextColor = Color.FromRgb(76, 175, 80);
                    return "\ue705";
                }

                return " " + string.Format("{0:0.00}", this.data);
            }
            else
            {
                if (parameter is Label)
                {
                    var label = parameter as Label;
                    label!.FontFamily = fontFamily;
                    label!.TextColor = Color.FromRgb(239, 83, 80);
                    return "\ue704";
                }

                return string.Format("{0:0.00}", this.data);
            }
        }

        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <summary> Implement this method to convert <paramref name="value" /> back from <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.</summary>
        /// <returns> To be added.</returns>
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return this.data!;
        }

        #endregion

        #region Internal Methods

        private string GetFontFamily()
        {
            return "MauiSampleFontIcon";
        }

        #endregion
    }
}
