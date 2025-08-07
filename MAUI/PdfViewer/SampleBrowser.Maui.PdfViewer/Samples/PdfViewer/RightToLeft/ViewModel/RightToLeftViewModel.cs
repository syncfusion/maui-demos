#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System.ComponentModel;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    internal class RightToLeftViewModel : INotifyPropertyChanged
    {
        private Stream? _documentStream;
        public event PropertyChangedEventHandler? PropertyChanged;

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
        public RightToLeftViewModel()
        {
            string fileName = "rtl_document.pdf";
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