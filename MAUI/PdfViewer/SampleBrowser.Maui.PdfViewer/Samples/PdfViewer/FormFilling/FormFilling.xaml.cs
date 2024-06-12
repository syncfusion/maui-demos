#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
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
public partial class FormFilling : SampleView
{
    Annotation? selectedAnnotation;
    public FormFilling()
    {
        InitializeComponent();
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
                await Application.Current!.MainPage!.DisplayAlert("File saved", $"The file is saved to {filePath}", "OK");
            }
        }
        catch (Exception exception)
        {
            await Application.Current!.MainPage!.DisplayAlert("Error", $"The file is not saved. {exception.Message}", "OK");
        }
    }

    private async void importButton_Clicked(object sender, EventArgs e)
    {
        var fileData = await FileService.OpenFile("xfdf");
        if (fileData != null)
        {
            // Handle errors in importing. Please note that the form field names and values in the XFDF file should correspond to the form field names and values in the PDF document.
            // Therefore, not any XFDF file can be imported to any PDF file.
            try
            {
                if (fileData.Stream != null)
                    pdfViewer.ImportFormData(fileData.Stream, DataFormat.XFdf);
            }
            catch (Exception exception)
            {
                await Application.Current!.MainPage!.DisplayAlert("Error", $"The file is not imported. {exception.Message}", "OK");
            }
        }
    }

    private async void exportButton_Clicked(object sender, EventArgs e)
    {
        Stream outStream = new MemoryStream();
        await pdfViewer.ExportFormDataAsync(outStream, DataFormat.XFdf);
        try
        {
            if (viewModel.FileData != null)
            {
                string exportedFormFileName = viewModel.FileData.FileName.Replace(".pdf", ".xfdf");
                string? filePath = await FileService.SaveAsAsync(exportedFormFileName, outStream);
                await Application.Current!.MainPage!.DisplayAlert("File saved", $"The file is saved to {filePath}", "OK");
            }
        }
        catch (Exception exception)
        {
            await Application.Current!.MainPage!.DisplayAlert("Error", $"The file is not saved. {exception.Message}", "OK");
        }
    }

    void ToggleDeleteOptionVisibility(bool isVisible)
    {
#if WINDOWS || MACCATALYST
        deleteButton.IsVisible = isVisible;
#else
        bottomBar.IsVisible = isVisible;
#endif
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
}