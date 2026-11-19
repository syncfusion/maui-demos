using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.SmartPdfViewer.SfSmartPdfViewer;

public partial class Summarizer : SampleView
{
	public Summarizer()
	{
		InitializeComponent();
        PdfViewer.DocumentLoaded += PdfViewer_DocumentLoaded;
	}

    private void PdfViewer_DocumentLoaded(object? sender, EventArgs? e)
    {
         PdfViewer.IsAssistViewVisible = true;
        PdfViewer.ZoomMode = Syncfusion.Maui.PdfViewer.ZoomMode.FitToWidth;
    }

    private void SmartRedaction_Clicked(object? sender, EventArgs? e)
    {
        PdfViewer.IsAssistViewVisible = !PdfViewer.IsAssistViewVisible;
    }

    private void SavePDF(object? sender, EventArgs? e)
    {
        var filePath = Path.Combine(FileSystem.AppDataDirectory, "SavedSample.pdf");

        var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        PdfViewer.SaveDocument(stream);
#pragma warning disable CS0618 // Type or member is obsolete
        Application.Current?.MainPage?.DisplayAlertAsync("Success", $"Document saved successfully at:\n{filePath}", "OK");
#pragma warning restore CS0618 // Type or member is obsolete

    }
    private void PrintPDF(object? sender, EventArgs? e)
    {
        PdfViewer.PrintDocument();
    }
}