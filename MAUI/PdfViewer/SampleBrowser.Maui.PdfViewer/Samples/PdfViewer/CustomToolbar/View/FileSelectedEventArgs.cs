namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public class FileSelectedEventArgs : EventArgs
{
    public string? FileName { get; set; }

    public FileSelectedEventArgs(string? fileName)
    {
        FileName = fileName;
    }
}