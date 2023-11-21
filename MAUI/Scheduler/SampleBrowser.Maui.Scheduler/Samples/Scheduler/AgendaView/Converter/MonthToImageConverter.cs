#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Base.Converters;
using System;
using System.Globalization;
using System.Reflection;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    /// <summary>
    /// Converter used to convert agenda view month header.
    /// </summary>
    internal class MonthToImageConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var monthName = String.Format("{0:MMMM}", value).ToLower();
                return ImageSource.FromResource(typeof(SfImageSourceConverter).GetTypeInfo().Assembly.GetName().Name + ".Resources.Images." + monthName + ".png", typeof(SfImageSourceConverter).GetTypeInfo().Assembly);
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}


