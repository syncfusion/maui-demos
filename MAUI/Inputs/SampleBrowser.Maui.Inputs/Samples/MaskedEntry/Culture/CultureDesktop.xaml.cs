#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System.Globalization;

namespace SampleBrowser.Maui.Inputs.SfMaskedEntry;

public partial class CultureDesktop : SampleView
{
	public CultureDesktop()
	{
		InitializeComponent();
	}

    private void IndiaImageButton_Clicked(object sender, EventArgs e)
    {
        cultureMask.Culture = new CultureInfo("en-IN");
        ResetColor();
        ResetSize();
        indiaFrame.BorderColor = Colors.Black;
        indiaStack.HeightRequest = 34;
        indiaStack.WidthRequest = 42;
    }

    private void USImageButton_Clicked(object sender, EventArgs e)
    {
        cultureMask.Culture = new CultureInfo("en-US");
        ResetColor();
        ResetSize();
        usFrame.BorderColor = Colors.Black;
        usStack.HeightRequest = 34;
        usStack.WidthRequest = 42;
    }

    private void RussiaImageButton_Clicked(object sender, EventArgs e)
    {
        cultureMask.Culture = new CultureInfo("ru-RU");
        ResetColor();
        ResetSize();
        russiaFrame.BorderColor = Colors.Black;
        russiaStack.HeightRequest = 34;
        russiaStack.WidthRequest = 42;
    }


    private void FranceImageButton_Clicked(object sender, EventArgs e)
    {
        cultureMask.Culture = new CultureInfo("fr-FR");
        ResetColor();
        ResetSize();
        franceFrame.BorderColor = Colors.Black;
        franceStack.HeightRequest = 34;
        franceStack.WidthRequest = 42;
    }

    void ResetColor()
    {
        indiaFrame.BorderColor = Colors.Transparent;
        franceFrame.BorderColor = Colors.Transparent;
        usFrame.BorderColor = Colors.Transparent;
        russiaFrame.BorderColor = Colors.Transparent;
    }

    void ResetSize()
    {
        usStack.WidthRequest = 36;
        usStack.HeightRequest = 28;
        indiaStack.WidthRequest = 36;
        indiaStack.HeightRequest = 28;
        russiaStack.WidthRequest = 36;
        russiaStack.HeightRequest = 28;
        franceStack.WidthRequest = 36;
        franceStack.HeightRequest = 28;
    }
}