#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Picker.SfTimePicker
{
    using System.Globalization;

    public class TimeSpanConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                TimeSpan twelveHrsTime = timeSpan.Hours > 12 || timeSpan.Hours == 0 ? timeSpan.Subtract(new TimeSpan(12, 0, 0)) : timeSpan;
                if (timeSpan.Hours > 12)
                {
                    twelveHrsTime = timeSpan.Subtract(new TimeSpan(12, 0, 0));
                }
                else if (timeSpan.Hours == 0)
                {
                    twelveHrsTime = new TimeSpan(12, 0, 0);
                }

                if (parameter is Boolean parameterValue)
                {
                    if (parameterValue)
                    {
                        return $"{twelveHrsTime.Hours}:{timeSpan.Minutes:D2}";
                    }
                    else
                    {
                        return $"{((timeSpan.Hours < 12) ? " AM" : " PM")}";
                    }
                }
                else
                {
                    return $"{twelveHrsTime.Hours}:{timeSpan.Minutes:D2} {((timeSpan.Hours < 12) ? " AM" : " PM")}";
                }
            }

            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }

}