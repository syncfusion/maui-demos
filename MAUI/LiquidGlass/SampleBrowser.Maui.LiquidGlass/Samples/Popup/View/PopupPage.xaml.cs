#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
namespace SampleBrowser.Maui.LiquidGlass.SfPopup;

public partial class PopupPage : SampleView
{
	public PopupPage()
	{
		InitializeComponent();
	}

    private void ClickToShowPopup(object sender, EventArgs e)
    {
        alertWithTitle.Show();
    }

}