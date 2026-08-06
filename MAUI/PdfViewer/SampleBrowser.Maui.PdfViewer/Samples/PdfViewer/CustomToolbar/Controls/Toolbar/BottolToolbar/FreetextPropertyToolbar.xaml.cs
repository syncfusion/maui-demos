namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class FreetextPropertyToolbar : PropertyToolbar
{
    public FreetextPropertyToolbar(CustomToolbarViewModel bindingContext)
    {
        BindingContext = bindingContext;
        InitializeComponent();
    }
}