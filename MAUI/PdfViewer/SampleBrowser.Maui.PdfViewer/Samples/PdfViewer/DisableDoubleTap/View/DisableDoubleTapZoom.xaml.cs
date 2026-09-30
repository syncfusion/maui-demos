using SampleBrowser.Maui.Base;
using Syncfusion.Maui.PdfViewer;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
[XamlCompilation(XamlCompilationOptions.Compile)]

public partial class DisableDoubleTapZoom : SampleView
{
    DisableDoubleTapZoomViewModel viewModel;
    public DisableDoubleTapZoom()
    {
        InitializeComponent();
        viewModel = new DisableDoubleTapZoomViewModel(PdfViewer);
        BindingContext = viewModel;

    }

    ///// <summary>
    ///// Handles when leaving the current page
    ///// </summary>
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        PdfViewer?.UnloadDocument();
        PdfViewer?.Handler?.DisconnectHandler();
    }

    private void DoubleTapToggle_Toggled(object? sender, ToggledEventArgs? e)
    {
        if (e != null)
        {
            PdfViewer.AllowDoubleTapZoom = (bool)(e.Value);
        }
    }
}
