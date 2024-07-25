#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Buttons.CheckBox;
using SampleBrowser.Maui.Buttons.Samples.CheckBox;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SampleBrowser.Maui.Buttons.CheckBox;

public partial class GettingStartedDesktop : SampleView
{
    public GettingStartedDesktop()
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
        if (green.IsChecked == true)
            green.IsChecked = false;
        else
            green.IsChecked = true;
    }

    private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
    {
        if (red.IsChecked == true)
            red.IsChecked = false;
        else
            red.IsChecked = true;
    }

    private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
    {
        if (sandal.IsChecked == true)
            sandal.IsChecked = false;
        else
            sandal.IsChecked = true;
    }

    private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
    {
        if (violet.IsChecked == true)
            violet.IsChecked = false;
        else
            violet.IsChecked = true;
    }
}
