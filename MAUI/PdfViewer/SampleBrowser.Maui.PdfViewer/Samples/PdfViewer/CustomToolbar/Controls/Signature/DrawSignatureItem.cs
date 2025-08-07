#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class DrawSignatureItem : SignatureItem
    {
        internal Color Color { get; }
        internal List<List<float>> InkPoints { get; }
        internal ImageSource? ImageSource { get; }
        internal DrawSignatureItem(Color color, List<List<float>> inkpoints, ImageSource? imageSource, View signatureView) : base(signatureView)
        {
            Color = color;
            ImageSource = imageSource;
            InkPoints = inkpoints;
        }

        internal void Dispose()
        {
            InkPoints.Clear();
        }
    }
}
