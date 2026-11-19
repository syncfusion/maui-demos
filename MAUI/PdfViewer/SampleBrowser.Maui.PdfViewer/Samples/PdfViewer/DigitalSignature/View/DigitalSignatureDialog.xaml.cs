namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer;

public partial class DigitalSignatureDialog : ContentView
{
    private Stream? _certificateStream;
    public event EventHandler<SignatureDialogEventArgs>? SignatureApplied;
    public DigitalSignatureDialog()
	{
		InitializeComponent();
	}
    private Entry GetPasswordEntry()
    {
        return PasswordEntry;
    }

    private Entry GetLocationEntry()
    {
        return LocationEntry;
    }

    private Entry GetReasonEntry()
    {
        return ReasonEntry;
    }
    private Label GetCertificateFileNameLabel()
    {
        return CertificateFileName;
    }
    private Entry GetContactInfoEntry()
    {
        return ContactInfoEntry;
    }

    private async void OnSelectCertificateClicked(object? sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select Certificate File"
            });

            if (result != null)
            {
                _certificateStream = await result.OpenReadAsync();
                if (result.FileName.EndsWith(".pfx", StringComparison.OrdinalIgnoreCase) || result.FileName.EndsWith(".p12", StringComparison.OrdinalIgnoreCase))
                {
                    GetCertificateFileNameLabel().Text = result.FileName;
                    SetCertificateError(null); // restore neutral look
                }
                else
                {
                    SetCertificateError("Invalid file selected");
                    _certificateStream = null;
                }
            }
        }
        catch (Exception ex)
        {
            await Application.Current!.Windows[0].Page!.DisplayAlertAsync("Error", $"Failed to select certificate: {ex.Message}", "OK");
        }
    }

    private void OnCancelClicked(object? sender, EventArgs e)
    {
        ResetForm();
        IsVisible = false;
    }

    private void PasswordEntry_TextChanged(object? sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(PasswordEntry.Text))
            SetPasswordError(null);
    }
    private void OnApplySignatureClicked(object? sender, EventArgs e)
    {
        // Validate certificate selection
        // Clear any previous inline errors
        SetCertificateError(null);
        SetPasswordError(null);

        bool isValid = true;

        // Validate certificate selection
        if (_certificateStream == null)
        {
            SetCertificateError("Please select a certificate file.");
            isValid = false;
        }

        // Validate password
        if (string.IsNullOrWhiteSpace(GetPasswordEntry().Text))
        {
            SetPasswordError("Please enter the certificate password.");
            isValid = false;
        }

        if (!isValid)
            return;

        try
        {
            var args = new SignatureDialogEventArgs
            {
                CertificateStream = _certificateStream,
                CertificatePassword = GetPasswordEntry().Text,
                Location = GetLocationEntry().Text?.Trim(),
                Reason = GetReasonEntry().Text?.Trim(),
                ContactInfo = GetContactInfoEntry().Text?.Trim()
            };

            SignatureApplied?.Invoke(this, args);
            ResetForm();
            IsVisible = false;
        }
        catch (Exception ex)
        {
            DisplayValidationError($"Error applying signature: {ex.Message}");
        }
    }
    private void SetCertificateError(string? message)
    {
        var label = GetCertificateFileNameLabel();
        if (message == null)
        {
            // Restore neutral, theme-aware color
            label.SetAppThemeColor(Label.TextColorProperty,
                Color.FromArgb("#666666"),
                Color.FromArgb("#CCCCCC"));
        }
        else
        {
            label.Text = message;
            label.SetAppThemeColor(Label.TextColorProperty,
                Color.FromArgb("#B3261E"),
                Color.FromArgb("#F2B8B5"));
        }
    }

    private void SetPasswordError(string? message)
    {
        PasswordErrorLabel.Text = message ?? string.Empty;
        PasswordErrorLabel.IsVisible = !string.IsNullOrEmpty(message);
    }
    private async void DisplayValidationError(string message)
    {
        await Application.Current!.Windows[0].Page!.DisplayAlertAsync("Validation Error", message, "OK");
    }

    private void ResetForm()
    {
        _certificateStream = null;
        SetCertificateError(null);                 // restores "No certificate selected"
        GetCertificateFileNameLabel().Text = "No certificate selected";
        SetPasswordError(null);
        GetPasswordEntry().Text = string.Empty;
        GetLocationEntry().Text = string.Empty;
        GetReasonEntry().Text = string.Empty;
        GetContactInfoEntry().Text = string.Empty;
        passwordInputLayout.Unfocus();
        locationInputLayout.Unfocus();
        reasonInputLayout.Unfocus();
        contactinfoInputLayout.Unfocus();
        passwordInputLayout.ShowHint = true;
        locationInputLayout.ShowHint = true;
        reasonInputLayout.ShowHint = true;
        contactinfoInputLayout.ShowHint = true;
    }

    private void PasswordEntry_Focused(object? sender, FocusEventArgs? e)
    {
        passwordInputLayout.ShowHint = false;
    }

    private void PasswordEntry_Unfocused(object? sender, FocusEventArgs? e)
    {
        if (string.IsNullOrEmpty(PasswordEntry.Text))
        {
            passwordInputLayout.ShowHint = true;
        }
    }

    private void LocationEntry_Focused(object? sender, FocusEventArgs? e)
    {
        locationInputLayout.ShowHint = false;
    }

    private void LocationEntry_Unfocused(object? sender, FocusEventArgs? e)
    {
        if (string.IsNullOrEmpty(LocationEntry.Text))
        {
            locationInputLayout.ShowHint = true;
        }
    }

    private void ReasonEntry_Focused(object? sender, FocusEventArgs? e)
    {
        reasonInputLayout.ShowHint = false;
    }

    private void ReasonEntry_Unfocused(object? sender, FocusEventArgs? e)
    {
        if (string.IsNullOrEmpty(ReasonEntry.Text))
        {
            reasonInputLayout.ShowHint = true;
        }
    }

    private void ContactInfoEntry_Focused(object? sender, FocusEventArgs? e)
    {
        contactinfoInputLayout.ShowHint = false;
    }

    private void ContactInfoEntry_Unfocused(object? sender, FocusEventArgs? e)
    {
        if (string.IsNullOrEmpty(ContactInfoEntry.Text))
        {
            contactinfoInputLayout.ShowHint = true;
        }
    }
}

public class SignatureDialogEventArgs : EventArgs
{
    public Stream? CertificateStream { get; set; }
    public string? CertificatePassword { get; set; }
    public string? Location { get; set; }
    public string? Reason { get; set; }
    public string? ContactInfo { get; set; }
}