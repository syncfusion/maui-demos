using System.Globalization;
using Syncfusion.Maui.PdfViewer;

namespace SampleBrowser.Maui.PdfViewer
{
    internal class AnnotationModeToBoolConvertor: IValueConverter
    {
        public AnnotationModeToBoolConvertor()
        {
            
        }
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is AnnotationMode annotationMode)
            {

                return annotationMode != AnnotationMode.None;
            }
            return false;
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
}
