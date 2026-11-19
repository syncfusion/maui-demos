namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    /// <summary>
    /// Represents the data of the PDF file that is to be loaded to the PDF viewer.
    /// </summary>
    public class PdfFileData
    {
        /// <summary>
        /// File name of the PDF with extension
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// The stream read from the PDF file.
        /// </summary>
        public Stream? Stream { get; }

        public PdfFileData(string fileName, Stream? stream)
        {
            FileName = fileName;
            Stream = stream;
        }
    }
}
