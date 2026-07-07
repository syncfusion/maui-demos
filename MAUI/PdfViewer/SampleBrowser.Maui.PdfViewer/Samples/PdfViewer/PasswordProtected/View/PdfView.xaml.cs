namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PdfView : ContentView
{
    public PdfView(PasswordProtectedViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }

    internal void Unload()
    {
        PdfViewer?.UnloadDocument();
        PdfViewer?.Handler?.DisconnectHandler();
    }

    private void PdfViewerDocumentLoadFailed(object? sender, Syncfusion.Maui.PdfViewer.DocumentLoadFailedEventArgs? e)
    {
        if (e?.Message == "Document loading has been cancelled")
        {
            if (this.BindingContext is PasswordProtectedViewModel bindingContext)
            {
                bindingContext.ToggleContent();
            }
        }
    }
}