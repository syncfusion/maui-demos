using Syncfusion.Maui.PdfViewer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.PdfViewer
{
    public class AnnotationModeToIconConverter : IValueConverter
    {
        string annotationType = string.Empty;
        public AnnotationModeToIconConverter(string annotationType)
        {
            this.annotationType = annotationType;
        }
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is AnnotationMode annotationMode)
            {
                if (annotationType == "TextMarkup")
                {
                    if (annotationMode == AnnotationMode.Highlight)
                        return "\uE760";
                    else if (annotationMode == AnnotationMode.Underline)
                        return "\uE762";
                    else if (annotationMode == AnnotationMode.StrikeOut)
                        return "\uE763";
                    else if (annotationMode == AnnotationMode.Squiggly)
                        return "\uE765";
                    else
                        return "\uE72e";
                }
                else if (annotationType == "Shape")
                {
                    if (annotationMode == AnnotationMode.Square)
                        return "\uE731";
                    else if (annotationMode == AnnotationMode.Circle)
                        return "\uE73f";
                    else if (annotationMode == AnnotationMode.Line)
                        return "\uE73d";
                    else if (annotationMode == AnnotationMode.Arrow)
                        return "\uE73c";
                    else if (annotationMode == AnnotationMode.Polyline)
                        return "\uE786";
                    else if (annotationMode == AnnotationMode.Polygon)
                        return "\uE789";
                    else
                        return "\uE73b";
                }
            }
            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
