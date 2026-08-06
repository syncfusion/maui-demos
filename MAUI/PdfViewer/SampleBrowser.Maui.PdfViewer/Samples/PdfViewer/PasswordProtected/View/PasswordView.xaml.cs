namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PasswordView : ContentView
{
    public PasswordView(PasswordProtectedViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}