using SampleBrowser.Maui.Base;
using Syncfusion.Maui.PdfViewer;
using Syncfusion.Pdf.Security;
using System.Reflection;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class DigitalSignature : SampleView
{    
    string? currentFileName = "eSign_filling.pdf";
    public Stream? CertificateStream { get; set; }
    public string? CertificatePassword { get; set; }
    public string? Location { get; set; }
    public string? Reason { get; set; }
    public string? ContactInfo { get; set; }

    // Embedded SampleBrowser certificate resource (matches InvisibleSignature sample).
    private const string CertificateFileName = "certificate.pfx";
    private const string DefaultCertificatePassword = "password123";

    public DigitalSignature()
    {
        InitializeComponent();
        PdfViewer.DigitalSignatureSettings.EnableSigning = true;
        PdfViewer.DigitalSignatureSettings.EnableValidation = true;
        PdfViewer.DigitalSignatureSettings.IsValidationBannerVisible = true;
        PdfViewer.DigitalSignatureModalViewAppearing += PdfViewer_DigitalSignatureModalViewAppearing;
       

    }

    private void PdfViewer_DigitalSignatureModalViewAppearing(object? sender, DigitalSignatureModalViewAppearingEventArgs e)
    {
        // Use the user-selected certificate if one was picked; otherwise fall back
        // to the certificate.pfx bundled with the SampleBrowser.
        Stream? certificateStream = CertificateStream;
        string? certificatePassword = CertificatePassword;

        if (certificateStream == null || string.IsNullOrWhiteSpace(certificatePassword))
        {
            certificateStream = OpenDefaultCertificateStream();
            certificatePassword = DefaultCertificatePassword;
        }

        e.Options = new SigningOptions
        {
            SignatureField = e.SignatureField,
            CertificateStream = certificateStream,
            CertificatePassword = certificatePassword,
            Reason = Reason,
            LocationInfo = Location,
            ContactInfo= ContactInfo,
            DigestAlgorithm = DigestAlgorithm.SHA256,
            CryptographicStandard = CryptographicStandard.CADES,
            Appearance = new SignatureAppearanceSettings
            {
                ShowSignerName = true,
                ShowDate = true,
                ShowReason = true,
                ShowLocation = true
            }
        };

    }

    /// <summary>
    /// Opens the bundled SampleBrowser <c>certificate.pfx</c> as a seekable
    /// MemoryStream. The PdfViewer takes ownership of the stream supplied to
    /// <see cref="SigningOptions.CertificateStream"/>, so a fresh copy is
    /// returned on every signing request.
    /// </summary>
    private Stream? OpenDefaultCertificateStream()
    {
        string basePath = BaseConfig.IsIndividualSB
            ? "SampleBrowser.Maui.PdfViewer.Samples.Pdf."
            : "SampleBrowser.Maui.Resources.Pdf.";

        Stream? resource = typeof(DigitalSignature).GetTypeInfo().Assembly
            .GetManifestResourceStream(basePath + CertificateFileName);

        if (resource == null)
        {
            return null;
        }

        // Materialize the manifest resource into memory so it is seekable
        // and survives the manifest stream being disposed.
        var buffer = new MemoryStream();
        resource.CopyTo(buffer);
        buffer.Position = 0;
        resource.Dispose();
        return buffer;
    }
    private static void ResetButtonVisual(Button button)
    {
        button.BackgroundColor = Colors.Transparent;
    }

    /// <summary>
    /// Handles when leaving the current page
    /// </summary>
    public override void OnDisappearing()
    {
        base.OnDisappearing();
        PdfViewer?.UnloadDocument();
        PdfViewer?.Handler?.DisconnectHandler();
    }

    private async void OnOpenClicked(object? sender, EventArgs? e)
    {
        PdfFileData? fileData = await FileService.OpenFile("pdf");
        if (fileData != null)
        {
            currentFileName = fileData.FileName;
            PdfViewer.LoadDocument(fileData.Stream);
        }
        if (sender is Button button)
        {
            ResetButtonVisual(button);
        }
    }

    private void OnDigitalSignatureClicked(object? sender, EventArgs? e)
    {
        SignatureDialogControl.IsVisible = true;
        if (sender is Button button)
        {
            ResetButtonVisual(button);
        }
    }

    private async void OnSaveClicked(object? sender, EventArgs? e)
    {
        Stream savedStream = new MemoryStream();
        await PdfViewer.SaveDocumentAsync(savedStream);
        if (!string.IsNullOrEmpty(currentFileName))
        {
            try
            {
                string? filePath = await FileService.SaveAsAsync(currentFileName, savedStream);
#if NET10_0_OR_GREATER
                await Application.Current!.Windows[0].Page!.DisplayAlertAsync("File saved", $"The file is saved to {filePath}", "OK");
#else
                await Application.Current!.Windows[0].Page!.DisplayAlert("File saved", $"The file is saved to {filePath}", "OK");
#endif
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
        if (sender is Button button)
        {
            ResetButtonVisual(button);
        }
    }

    private void OnSignaturePanelClicked(object? sender, EventArgs? e)
    {
        PdfViewer.DigitalSignatureSettings.IsSignaturePanelVisible = !PdfViewer.DigitalSignatureSettings.IsSignaturePanelVisible;
        if (sender is Button button)
        {
            ResetButtonVisual(button);
        }
    }

    private void OnSignatureApplied(object? sender, SignatureDialogEventArgs? e)
    {
        if(e == null)
        {
            return;
        }
        if (e.CertificateStream == null || string.IsNullOrEmpty(e.CertificatePassword))
        {
            Application.Current!.Windows[0].Page!.DisplayAlertAsync("Error", "Please select a certificate and enter the password.", "OK");
            return;
        }
        CertificateStream = e.CertificateStream;
        CertificatePassword = e.CertificatePassword;
        Location = e.Location;
        Reason = e.Reason;
        ContactInfo = e.ContactInfo;
    }
}
