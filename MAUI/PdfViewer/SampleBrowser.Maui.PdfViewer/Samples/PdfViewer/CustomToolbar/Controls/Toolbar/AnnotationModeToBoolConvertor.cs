#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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
