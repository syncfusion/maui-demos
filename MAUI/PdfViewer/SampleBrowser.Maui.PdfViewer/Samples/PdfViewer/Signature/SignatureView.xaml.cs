using Microsoft.Maui.Platform;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.PdfViewer;
using Syncfusion.Pdf.Parsing;
using System.Reflection;
namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SignatureView : SampleView
{
    Annotation? selectedAnnotation;
    public SignatureView()
	{
		InitializeComponent();
        viewModel = new SignatureViewmodel();
        pdfViewer.PropertyChanged += PdfViewer_PropertyChanged;
    }

    private void PdfViewer_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Syncfusion.Maui.PdfViewer.SfPdfViewer.AnnotationMode))
            signatureButton.IsVisible = pdfViewer.AnnotationMode != AnnotationMode.Signature;
    }

    private async void saveAsButton_Clicked(object? sender, EventArgs? e)
    {
        Stream outStream = new MemoryStream();
        await pdfViewer.SaveDocumentAsync(outStream);
        try
        {
            if (viewModel.FileData != null)
            {
                string? filePath = await FileService.SaveAsAsync(viewModel.FileData.FileName, outStream);
#if NET10_0_OR_GREATER
                await Application.Current!.Windows[0].Page!.DisplayAlertAsync("File saved", $"The file is saved to {filePath}", "OK");
#else
                await Application.Current!.Windows[0].Page!.DisplayAlert("File saved", $"The file is saved to {filePath}", "OK");
#endif
            }
        }
        catch (Exception exception)
        {
#if NET10_0_OR_GREATER
            await Application.Current!.Windows[0].Page!.DisplayAlertAsync("Error", $"The file is not saved. {exception.Message}", "OK");
#else
            await Application.Current!.Windows[0].Page!.DisplayAlert("Error", $"The file is not saved. {exception.Message}", "OK");
#endif
        }
    }

    void ToggleDeleteOptionVisibility(bool isVisible)
    {
        deleteButton.IsVisible = isVisible;
    }

    private void deleteButton_Clicked(object? sender, EventArgs? e)
    {
        if (selectedAnnotation != null)
            pdfViewer.RemoveAnnotation(selectedAnnotation);
        ToggleDeleteOptionVisibility(false);
    }

    private void pdfViewer_AnnotationSelected(object? sender, AnnotationEventArgs? e)
    {
        selectedAnnotation = e?.Annotation;
        ToggleDeleteOptionVisibility(true);
    }

    private void pdfViewer_AnnotationDeselected(object? sender, AnnotationEventArgs? e)
    {
        if (e?.Annotation == selectedAnnotation)
            selectedAnnotation = null;
        ToggleDeleteOptionVisibility(false);
    }
    ///// <summary>
    ///// Handles when leaving the current page
    ///// </summary>
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        pdfViewer?.UnloadDocument();
        pdfViewer?.Handler?.DisconnectHandler();
    }

    private void signatureButton_Clicked(object? sender, EventArgs? e)
    {
        pdfViewer.AnnotationMode = AnnotationMode.Signature;
    }
}