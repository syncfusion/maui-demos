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
