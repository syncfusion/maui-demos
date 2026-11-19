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
