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
using System.Globalization;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    internal class DateToColorConverter : IValueConverter
    {
        public Brush lightGrey { get; set; }
        public Brush lightGreen { get; set; }
        public Brush midGreen { get; set; }
        public Brush darkGreen { get; set; }
        public Brush darkerGreen { get; set; }

        public DateToColorConverter()
        {
            lightGrey = Color.FromArgb("#eeeeee");
            lightGreen = Color.FromArgb("#c6e48b");
            midGreen = Color.FromArgb("#7bc96f");
            darkGreen = Color.FromArgb("#239a3b");
            darkerGreen = Color.FromArgb("#196127");
        }

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var date = value as DateTime?;
            if (date.HasValue)
            {
                if (date.Value.Month % 2 == 0)
                {
                    if (date.Value.Day % 6 == 0)
                    {
                        // 6, 12, 18, 24, 30
                        return darkerGreen;
                    }
                    else if (date.Value.Day % 5 == 0)
                    {
                        // 5, 10, 15, 20, 25
                        return midGreen;
                    }
                    else if (date.Value.Day % 8 == 0 || date.Value.Day % 4 == 0)
                    {
                        //  4, 8, 16, 24, 28
                        return darkGreen;
                    }
                    else if (date.Value.Day % 9 == 0 || date.Value.Day % 3 == 0)
                    {
                        // 3, 9, 18, 21, 27
                        return lightGrey;
                    }
                    else
                    {
                        // 1, 2, 7, 11, 13, 19, 22, 23, 26, 29
                        return lightGreen;
                    }
                }
                else
                {
                    if (date.Value.Day % 6 == 0)
                    {
                        // 6, 12, 18, 24, 30
                        return lightGreen;
                    }
                    else if (date.Value.Day % 5 == 0)
                    {
                        // 5, 10, 15, 20, 25
                        return lightGrey;
                    }
                    else if (date.Value.Day % 8 == 0 || date.Value.Day % 4 == 0)
                    {
                        //  4, 8, 16, 24, 28
                        return midGreen;
                    }
                    else if (date.Value.Day % 9 == 0 || date.Value.Day % 3 == 0)
                    {
                        // 3, 9, 18, 21, 27
                        return darkerGreen;
                    }
                    else
                    {
                        // 1, 2, 7, 11, 13, 19, 22, 23, 26, 29
                        return darkGreen;
                    }
                }
            }

            return lightGrey;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}


