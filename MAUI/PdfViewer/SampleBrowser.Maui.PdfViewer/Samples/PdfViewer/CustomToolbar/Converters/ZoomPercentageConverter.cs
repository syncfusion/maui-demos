using System.Globalization;

namespace SampleBrowser.Maui.PdfViewer
{
    internal class ZoomPercentageConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is double zoomFactor)
            {
                double zoomPercentage = Math.Round(zoomFactor * 100, 0);
                return $"{zoomPercentage}%";
            }
            return "";
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}