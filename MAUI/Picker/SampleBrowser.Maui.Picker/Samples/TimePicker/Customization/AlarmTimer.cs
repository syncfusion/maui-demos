#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Picker.SfTimePicker
{
    using System.Globalization;

    public class AlarmTimer : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                timeSpan = currentTime.Hours >= timeSpan.Hours ? timeSpan.Add(new TimeSpan(24, 0, 0)) : timeSpan;
                var timeDifference = timeSpan.Subtract(currentTime);
                if (timeDifference.Minutes == 0 && timeDifference.Hours == 0)
                {
                    return $"Alarm in {timeDifference.Seconds} seconds";
                }
                else
                {
                    return $"Alarm in {timeDifference.Hours} hours {timeDifference.Minutes} minutes";
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
