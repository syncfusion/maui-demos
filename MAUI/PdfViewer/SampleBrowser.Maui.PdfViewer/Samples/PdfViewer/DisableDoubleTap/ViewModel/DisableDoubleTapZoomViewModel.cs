using SampleBrowser.Maui.Base;
using Syncfusion.Maui.PdfViewer;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System.ComponentModel;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    public class DisableDoubleTapZoomViewModel : INotifyPropertyChanged
    {
        private Stream? _documentStream;
        public event PropertyChangedEventHandler? PropertyChanged;
        internal string basePath;

        /// <summary>
        /// Gets or sets the PDF document as a stream. 
        /// </summary>
        public Stream? DocumentStream
        {
            get => _documentStream;
            set
            {
                _documentStream = value;
                OnPropertyChanged("DocumentStream");
            }
        }

        /// <summary>
        /// Constructor of the view model class
        /// </summary>
        public DisableDoubleTapZoomViewModel(Syncfusion.Maui.PdfViewer.SfPdfViewer pdfViewer)
        {
            string fileName = "PDF_Succinctly.pdf";
            basePath = "SampleBrowser.Maui.Resources.Pdf.";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.PdfViewer.Samples.Pdf.";
            DocumentStream = this.GetType().Assembly.GetManifestResourceStream(basePath + fileName);
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}