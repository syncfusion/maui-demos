#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System.Diagnostics;

namespace SampleBrowser.Maui.Buttons.RadioButton;

public partial class GettingStartedDesktop:SampleView
{
	public GettingStartedDesktop()
	{
		InitializeComponent();
	}

    private void SfRadioGroupKey_CheckedChanged(object sender, Syncfusion.Maui.Buttons.CheckedChangedEventArgs e)
    {
        button.IsEnabled = true;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        masterCard.IsChecked = false;
        visa.IsChecked = false;
        payDirect.IsChecked = false;
        noCost.IsChecked = false;
        nineMonths.IsChecked = false;
        twelveMonths.IsChecked = false;
        button.IsEnabled = false;
    }

    private void PayDirect_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
    {
        if (e.IsChecked == true)
        {
            noCost.IsEnabled = false;
            nineMonths.IsEnabled = false;
            twelveMonths.IsEnabled = false;
            noCost.IsChecked = false;
            nineMonths.IsChecked = false;
            twelveMonths.IsChecked = false;
        }
    }

    private void Visa_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
    {
        if (e.IsChecked == true)
        {
            noCost.IsEnabled = true;
            nineMonths.IsEnabled = true;
            twelveMonths.IsEnabled = true;
        }
    }

    private void MasterCard_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
    {
        if (e.IsChecked == true)
        {
            noCost.IsEnabled = true;
            nineMonths.IsEnabled = true;
            twelveMonths.IsEnabled = true;
        } 
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        masterCard.IsChecked = true;
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        visa.IsChecked = true;
    }

    private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
    {
        payDirect.IsChecked = true;
    }
}