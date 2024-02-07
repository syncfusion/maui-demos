#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.PdfViewer;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;
[XamlCompilation(XamlCompilationOptions.Compile)]

public partial class InvisibleSignature : SampleView
{
    InvisibleSignatureViewModel viewModel;
    public InvisibleSignature()
    {
        InitializeComponent();
        viewModel = new InvisibleSignatureViewModel(PdfViewer);
        PdfViewer.FormFieldValueChanged += PdfViewer_FormFieldValueChanged;
        BindingContext = viewModel;
        viewModel.IsEnableSave = false;
#if WINDOWS || MACCATALYST
        this.SizeChanged += InvisibleSignature_SizeChanged;
#endif
    }

    void FitToHeight()
    {
        double toolbarHeight = 48;
        double documentPageHeight = 596;
        // Zoom the document to fit to page to view the signature field in the view.
        PdfViewer.ZoomFactor = (this.Height - toolbarHeight) / (documentPageHeight * DeviceDisplay.MainDisplayInfo.Density);
    }

    private void InvisibleSignature_SizeChanged(object? sender, EventArgs e)
    {
        FitToHeight();
    }

    private void PdfViewer_FormFieldValueChanged(object? sender, Syncfusion.Maui.PdfViewer.FormFieldValueChangedEventArgs? e)
    {
        if ((e?.FormField as SignatureFormField)?.Signature != null)
        {
            viewModel.IsCompleteSigningEnable = true;
            PdfViewer.AnnotationSettings.IsLocked = true;
        }
    }

    private void Save_Clicked(object? sender, EventArgs e)
    {
        viewModel?.SaveDocument();
    }

    private async void PdfViewer_DocumentLoaded(object sender, EventArgs e)
    {
        viewModel.IsCompleteSigningEnable = false;
#if WINDOWS || MACCATALYST
        FitToHeight();
#endif
        await Task.Delay(2000);
        viewModel.ValidatedSignature(viewModel.signatureValidatedStream);
        await Task.Delay(5000);
        viewModel.IsSuccessMsgVisible = false;
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
}
