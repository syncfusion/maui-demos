#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Buttons.RadioButton;

public partial class GettingStartedMobile : SampleView
{
	public GettingStartedMobile()
	{
		InitializeComponent();
	}

    private void SfRadioGroupKey_CheckedChanged(object sender, Syncfusion.Maui.Buttons.CheckedChangedEventArgs e)
    {
        button.IsEnabled = true;
        
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        bankCreditCard.IsChecked = false;
        bankDebitCard.IsChecked = false;
        creditCard.IsChecked = false;
        debitOrCreditCard.IsChecked = false;
        netBanking.IsChecked = false;
        cashButton.IsChecked = false;
        button.IsEnabled = false;
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        cashButton.IsChecked = true;
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        bankCreditCard.IsChecked = true;
    }

    private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
    {
        creditCard.IsChecked = true;
    }

    private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
    {
        bankDebitCard.IsChecked = true;
    }
}