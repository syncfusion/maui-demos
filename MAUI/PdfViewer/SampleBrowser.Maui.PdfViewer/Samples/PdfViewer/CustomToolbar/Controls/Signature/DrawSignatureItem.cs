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
