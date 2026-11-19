using System;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal interface IIntegratedSigatureDialog
    {
        internal event EventHandler? SignatureCreated;
        internal event EventHandler? CloseRequested;
    }
}
