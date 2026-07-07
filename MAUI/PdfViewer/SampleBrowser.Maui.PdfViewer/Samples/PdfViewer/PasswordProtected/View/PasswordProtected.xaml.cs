using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PasswordProtected : SampleView
{
    public PasswordProtected()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Handles when leaving the current page
    /// </summary>
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is PasswordProtectedViewModel viewModel)
        {
            viewModel?.Unload();
        }
    }
}