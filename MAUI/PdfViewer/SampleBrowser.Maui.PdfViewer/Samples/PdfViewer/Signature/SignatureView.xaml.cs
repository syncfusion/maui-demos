#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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

    private async void saveAsButton_Clicked(object sender, EventArgs e)
    {
        Stream outStream = new MemoryStream();
        await pdfViewer.SaveDocumentAsync(outStream);
        try
        {
            if (viewModel.FileData != null)
            {
                string? filePath = await FileService.SaveAsAsync(viewModel.FileData.FileName, outStream);
                await Application.Current!.Windows[0].Page!.DisplayAlert("File saved", $"The file is saved to {filePath}", "OK");
            }
        }
        catch (Exception exception)
        {
            await Application.Current!.Windows[0].Page!.DisplayAlert("Error", $"The file is not saved. {exception.Message}", "OK");
        }
    }

    void ToggleDeleteOptionVisibility(bool isVisible)
    {
        deleteButton.IsVisible = isVisible;
    }

    private void deleteButton_Clicked(object sender, EventArgs e)
    {
        if (selectedAnnotation != null)
            pdfViewer.RemoveAnnotation(selectedAnnotation);
        ToggleDeleteOptionVisibility(false);
    }

    private void pdfViewer_AnnotationSelected(object sender, AnnotationEventArgs e)
    {
        selectedAnnotation = e.Annotation;
        ToggleDeleteOptionVisibility(true);
    }

    private void pdfViewer_AnnotationDeselected(object sender, AnnotationEventArgs e)
    {
        if (e.Annotation == selectedAnnotation)
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

    private void signatureButton_Clicked(object sender, EventArgs e)
    {
        pdfViewer.AnnotationMode = AnnotationMode.Signature;
    }
}