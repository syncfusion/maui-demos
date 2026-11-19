namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class PasswordView : ContentView
{
    public PasswordView(PasswordProtectedViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}