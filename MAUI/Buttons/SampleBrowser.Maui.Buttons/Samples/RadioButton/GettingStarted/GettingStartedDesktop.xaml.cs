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
            grid3.BackgroundColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackgroundDark"] : (Color)App.Current.Resources["PrimaryBackgroundLight1"];
            grid3.BorderColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        }
        else 
        {
            grid3.BackgroundColor = Colors.Transparent;
            grid3.BorderColor = Colors.LightGray;
        }
    }

    private void Visa_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
    {
        if (e.IsChecked == true)
        {
            noCost.IsEnabled = true;
            nineMonths.IsEnabled = true;
            twelveMonths.IsEnabled = true;
            grid2.BackgroundColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackgroundDark"] : (Color)App.Current.Resources["PrimaryBackgroundLight1"];
            grid2.BorderColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        }
        else {
            grid2.BackgroundColor = Colors.Transparent;
            grid2.BorderColor = Colors.LightGray;
        }
    }

    private void MasterCard_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
    {
        if (e.IsChecked == true)
        {
            noCost.IsEnabled = true;
            nineMonths.IsEnabled = true;
            twelveMonths.IsEnabled = true;
            grid1.BackgroundColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackgroundDark"] : (Color)App.Current.Resources["PrimaryBackgroundLight1"];
            grid1.BorderColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        }
        else 
        {
            grid1.BackgroundColor = Colors.Transparent;
            grid1.BorderColor = Colors.LightGray;
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

    private void noCost_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
    {
        if(e.IsChecked==true) 
        {
            grid4.BackgroundColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackgroundDark"] : (Color)App.Current.Resources["PrimaryBackgroundLight1"];
            grid4.BorderColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        }
        else 
        {
            grid4.BackgroundColor = Colors.Transparent;
            grid4.BorderColor = Colors.LightGray;
        }
    }

    private void nineMonths_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e) 
    {
        if (e.IsChecked == true) 
        {
            grid5.BackgroundColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackgroundDark"] : (Color)App.Current.Resources["PrimaryBackgroundLight1"];
            grid5.BorderColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        }
        else 
        {
            grid5.BackgroundColor = Colors.Transparent;
            grid5.BorderColor = Colors.LightGray;
        }
    }

    private void twelveMonths_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
    {
        if (e.IsChecked == true) 
        {
            grid6.BackgroundColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackgroundDark"] : (Color)App.Current.Resources["PrimaryBackgroundLight1"];
            grid6.BorderColor = Application.Current!.UserAppTheme == AppTheme.Dark ? (Color)App.Current.Resources["PrimaryBackground"] : (Color)App.Current.Resources["PrimaryBackgroundLight"];
        }
        else 
        {
            grid6.BackgroundColor = Colors.Transparent;
            grid6.BorderColor = Colors.LightGray;
        }
    }
}