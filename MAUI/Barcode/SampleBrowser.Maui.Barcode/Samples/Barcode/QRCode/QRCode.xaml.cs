using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Barcode;

namespace SampleBrowser.Maui.Barcode.SfBarcodeGenerator;

public partial class QRCode : SampleView
{
    public QRCode()
    {
        InitializeComponent();
        ViewModel viewModel = new ViewModel();
        errorCorrectionLevelPicker.ItemsSource = viewModel.ErrorCorrectionLevelArray;
        errorCorrectionLevelPicker.SelectedIndex = 0;
    }

    private void inputValueEntry_TextChanged(object? sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
        {
            warningLabel.Text = "Value cannot be null or empty";
        }
        else
        {
            warningLabel.Text = "";
            barcodeGenerator.Value = e.NewTextValue;
        }
    }
}

public class ViewModel
{
    public Array InputModeArray => Enum.GetValues<QRInputMode>();
    public Array ErrorCorrectionLevelArray => Enum.GetValues<ErrorCorrectionLevel>();
}
