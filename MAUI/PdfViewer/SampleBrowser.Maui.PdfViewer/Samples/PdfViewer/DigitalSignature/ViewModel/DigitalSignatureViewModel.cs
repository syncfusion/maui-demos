using SampleBrowser.Maui.Base;
using System.ComponentModel;
using System.IO;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class DigitalSignatureViewModel : INotifyPropertyChanged
    {
        private Stream? _docStream;
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets or sets the PDF document as a stream. 
        /// </summary>
        public Stream? DocumentStream
        {
            get => _docStream;
            set
            {
                _docStream = value;
                OnPropertyChanged("DocumentStream");
            }
        }

        /// <summary>
        /// Constructor of the view model class
        /// </summary>
        public DigitalSignatureViewModel()
        {
            string fileName = "eSign_filling.pdf";
            string basePath = "SampleBrowser.Maui.Resources.Pdf.";
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