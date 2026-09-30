namespace SampleBrowser.Maui.Sliders.SfSlider
{
    using System.Globalization;

    /// <summary>
    /// Represents the rating star converter class.
    /// </summary>
    public class RatingStarConverter : IValueConverter
    {
        /// <summary>
        /// Converts the rating value to a string of stars.
        /// </summary>
        /// <param name="value">The rating value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The string of stars.</returns>
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value?.ToString() switch
            {
                "1" => "⭐",
                "2" => "⭐⭐",
                "3" => "⭐⭐⭐",
                "4" => "⭐⭐⭐⭐",
                _ => string.Empty
            };
        }

        /// <summary>
        /// Converts back the string of stars to a rating value.
        /// </summary>
        /// <param name="value">The string of stars.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The rating value.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Represents the rating text converter class.
    /// </summary>
    public class RatingTextConverter : IValueConverter
    {
        /// <summary>
        /// Converts the rating value to a string of text.
        /// </summary>
        /// <param name="value">The rating value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The string of text.</returns>
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value?.ToString() switch
            {
                "1" => "Poor",
                "2" => "Average",
                "3" => "Good",
                "4" => "Excellent",
                _ => string.Empty
            };
        }

        /// <summary>
        /// Converts back the string of text to a rating value.
        /// </summary>
        /// <param name="value">The string of text.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The rating value.</returns>
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}