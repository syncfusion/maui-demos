#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Inputs.SfMaskedEntry;

public partial class MaskedEntryGettingStartedDesktop : SampleView
{
	public MaskedEntryGettingStartedDesktop()
	{
		InitializeComponent();
	}

    private void PromptCharComboBox_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        switch (promptCharComboBox.SelectedIndex)
        {
            case 0:
                emailMask.PromptChar = '_';
                dateMask.PromptChar = '_';
                timeMask.PromptChar = '_';
                phoneMask.PromptChar = '_';
                ipMask.PromptChar = '_';
                //debitMask.PromptChar = '_';
                break;
            case 1:
                emailMask.PromptChar = '*';
                dateMask.PromptChar = '*';
                timeMask.PromptChar = '*';
                phoneMask.PromptChar = '*';
                ipMask.PromptChar = '*';
                //debitMask.PromptChar = '*';
                break;
            case 2:
                emailMask.PromptChar = '#';
                dateMask.PromptChar = '#';
                timeMask.PromptChar = '#';
                phoneMask.PromptChar = '#';
                ipMask.PromptChar = '#';
                //bdebitMask.PromptChar = '#';
                break;
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}