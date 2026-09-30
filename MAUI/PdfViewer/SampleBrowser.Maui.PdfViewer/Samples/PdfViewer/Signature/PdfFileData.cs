using SampleBrowser.Maui.PdfViewer.SfPdfViewer;

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