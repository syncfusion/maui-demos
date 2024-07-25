#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using System.IO;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class ImageSignatureItem : SignatureItem
    {
        internal Stream? ImageStream { get; }
        internal string? FilePath { get; }
        internal SizeF ImageSize { get; }
        internal ImageSignatureItem(Stream? imageStream, string? filePath, View signatureView) : base(signatureView)
        {
            if (imageStream != null)
                imageStream.Position = 0;
            ImageStream = imageStream;
            FilePath = filePath;
            float imageWidth = 200;
            float imageHeight = 200;
#if NET8_0_OR_GREATER
            Microsoft.Maui.Graphics.IImage platformImage = Microsoft.Maui.Graphics.Platform.PlatformImage.FromStream(imageStream);
            imageWidth = platformImage.Width;
            imageHeight = platformImage.Height;
#endif
            if (imageWidth < imageHeight)
            {
                if (imageWidth > 200)
                {
                    imageHeight = 200 * (imageHeight / imageWidth);
                    imageWidth = 200;
                }
            }
            else
            {
                if (imageHeight > 200)
                {
                    imageWidth = 200 * (imageWidth / imageHeight);
                    imageHeight = 200;
                }
            }
            ImageSize = new SizeF(imageWidth, imageHeight);
            if (imageStream != null && imageStream.CanSeek)
                imageStream.Position = 0;
        }
    }
}
