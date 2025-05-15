#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class TypeSignatureItem : SignatureItem
    {
        internal string Text { get; }
        internal string FontFamily { get; }
        internal Color TextColor { get; }
        
        internal TypeSignatureItem(Label signatureView) : base(signatureView)
        {
            Text = signatureView.Text;
            FontFamily = signatureView.FontFamily;
            TextColor = signatureView.TextColor;
            View = signatureView;
        }
    }
}
