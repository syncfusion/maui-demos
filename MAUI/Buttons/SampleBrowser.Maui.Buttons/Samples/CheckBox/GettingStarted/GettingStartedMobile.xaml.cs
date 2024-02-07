#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Buttons.CheckBox;

public partial class GettingStartedMobile : SampleView
{
    public GettingStartedMobile()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        if (brown.IsChecked == true)
            brown.IsChecked = false;
        else
            brown.IsChecked = true;
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        if(green.IsChecked == true)
            green.IsChecked = false;
        else
            green.IsChecked = true;
    }

    private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
    {
        if(red.IsChecked == true)
            red.IsChecked = false;
        else
            red.IsChecked = true;
    }

    private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
    {
        if(sandal.IsChecked == true)
            sandal.IsChecked = false;
        else
            sandal.IsChecked = true;
    }
}