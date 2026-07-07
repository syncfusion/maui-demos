using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Inputs;

namespace SampleBrowser.Maui.LiquidGlass.SfMaskedEntry;

public partial class MaskedEntryPageDesktop : SampleView
{
	public MaskedEntryPageDesktop()
	{
		InitializeComponent();
	}

    private void PromptCharComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        switch (promptCharComboBox.SelectedIndex)
        {
            case 0:
                emailMask.PromptChar = '_';
                dateMask.PromptChar = '_';
                timeMask.PromptChar = '_';
                phoneMask.PromptChar = '_';
                ipMask.PromptChar = '_';
                break;
            case 1:
                emailMask.PromptChar = '*';
                dateMask.PromptChar = '*';
                timeMask.PromptChar = '*';
                phoneMask.PromptChar = '*';
                ipMask.PromptChar = '*';
                break;
            case 2:
                emailMask.PromptChar = '#';
                dateMask.PromptChar = '#';
                timeMask.PromptChar = '#';
                phoneMask.PromptChar = '#';
                ipMask.PromptChar = '#';
                break;
        }
    }

    private void Button_Clicked(object? sender, EventArgs e)
    {

    }

    private void PasswordCharComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        switch (passwordCharComboBox.SelectedIndex)
        {
            case 0:
                emailMask.PasswordChar = '●';
                dateMask.PasswordChar = '●';
                timeMask.PasswordChar = '●';
                phoneMask.PasswordChar = '●';
                ipMask.PasswordChar = '●';
                break;
            case 1:
                emailMask.PasswordChar = '★';
                dateMask.PasswordChar = '★';
                timeMask.PasswordChar = '★';
                phoneMask.PasswordChar = '★';
                ipMask.PasswordChar = '★';
                break;
            case 2:
                emailMask.PasswordChar = '■';
                dateMask.PasswordChar = '■';
                timeMask.PasswordChar = '■';
                phoneMask.PasswordChar = '■';
                ipMask.PasswordChar = '■';
                break;
            case 3:
                emailMask.PasswordChar = '\0';
                dateMask.PasswordChar = '\0';
                timeMask.PasswordChar = '\0';
                phoneMask.PasswordChar = '\0';
                ipMask.PasswordChar = '\0';
                break;
        }
    }

    private void ValidationModeComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        switch (validationModeComboBox.SelectedIndex)
        {
            case 0:
                emailMask.ValidationMode = InputValidationMode.LostFocus;
                dateMask.ValidationMode = InputValidationMode.LostFocus;
                timeMask.ValidationMode = InputValidationMode.LostFocus;
                phoneMask.ValidationMode = InputValidationMode.LostFocus;
                ipMask.ValidationMode = InputValidationMode.LostFocus;
                break;
            case 1:
                emailMask.ValidationMode = InputValidationMode.KeyPress;
                dateMask.ValidationMode = InputValidationMode.KeyPress;
                timeMask.ValidationMode = InputValidationMode.KeyPress;
                phoneMask.ValidationMode = InputValidationMode.KeyPress;
                ipMask.ValidationMode = InputValidationMode.KeyPress;
                break;
        }

    }
}