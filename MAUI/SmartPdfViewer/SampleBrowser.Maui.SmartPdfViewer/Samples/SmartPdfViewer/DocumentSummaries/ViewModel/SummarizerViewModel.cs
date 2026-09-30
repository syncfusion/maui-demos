using SampleBrowser.Maui.Base;
using System.ComponentModel;

namespace SampleBrowser.Maui.SmartPdfViewer.SfSmartPdfViewer
{
    public class SummarizerViewModel : INotifyPropertyChanged
    {
        // Backing field for the PdfFile property
        private Stream? _pdfFile;
        // Property to hold the PDF file stream
        public Stream? PdfFile
        {
            get { return _pdfFile; }
            set
            {
                _pdfFile = value;
                // Notify that PdfFile property has changed
                OnPropertyChange(nameof(PdfFile));
            }
        }

        // Event handler for property changes
        public event PropertyChangedEventHandler? PropertyChanged;

        // Method to notify listeners of property changes
        private void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Constructor to initialize the view model
        public SummarizerViewModel()
        {
            string basePath = "SampleBrowser.Maui.Resources.Pdf";
            if (BaseConfig.IsIndividualSB)
                basePath = "SampleBrowser.Maui.SmartPdfViewer.Samples.Pdf";

            // Load a PDF file as a resource stream
            _pdfFile = this.GetType().Assembly.GetManifestResourceStream($"{basePath}.PDF_Succinctly.pdf");
        }
}
}