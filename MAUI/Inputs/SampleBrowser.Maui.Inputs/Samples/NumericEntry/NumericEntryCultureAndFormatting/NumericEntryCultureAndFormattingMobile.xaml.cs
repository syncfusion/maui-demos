#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System.Globalization;

namespace SampleBrowser.Maui.Inputs.SfNumericEntry;

public partial class NumericEntryCultureAndFormattingMobile : SampleView
{
	public NumericEntryCultureAndFormattingMobile()
	{
		InitializeComponent();
        currencyCulture.Culture = new CultureInfo("en-US");
        decimalCulture.Culture = new CultureInfo("en-US");
        percentCulture.Culture = new CultureInfo("en-US");
    }

    private void IndiaImageButton_Clicked(object sender, EventArgs e)
    {
        currencyCulture.Culture = new CultureInfo("en-IN");
        decimalCulture.Culture = new CultureInfo("en-IN");
        percentCulture.Culture = new CultureInfo("en-IN");
        ResetColor();
        ResetSize();
        indiaFrame.BorderColor = Colors.Black;
        indiaStack.HeightRequest = 34;
        indiaStack.WidthRequest = 42;
#if IOS
        indiaImage.WidthRequest = 40;
        indiaImage.HeightRequest = 32;
#endif
    }

    private void USImageButton_Clicked(object sender, EventArgs e)
    {
        currencyCulture.Culture = new CultureInfo("en-US");
        decimalCulture.Culture = new CultureInfo("en-US");
        percentCulture.Culture = new CultureInfo("en-US");
        ResetColor();
        ResetSize();
        usFrame.BorderColor = Colors.Black;
        usStack.HeightRequest = 34;
        usStack.WidthRequest = 42;
#if IOS
        usImage.WidthRequest = 40;
        usImage.HeightRequest = 32;
#endif
    }

    private void RussiaImageButton_Clicked(object sender, EventArgs e)
    {
        currencyCulture.Culture = new CultureInfo("ru-RU");
        decimalCulture.Culture = new CultureInfo("ru-RU");
        percentCulture.Culture = new CultureInfo("ru-RU");
        ResetColor();
        ResetSize();
        russiaFrame.BorderColor = Colors.Black;
        russiaStack.HeightRequest = 34;
        russiaStack.WidthRequest = 42;
#if IOS
        russiaImage.WidthRequest = 40;
        russiaImage.HeightRequest = 32;
#endif
    }


    private void FranceImageButton_Clicked(object sender, EventArgs e)
    {
        currencyCulture.Culture = new CultureInfo("fr-FR");
        decimalCulture.Culture = new CultureInfo("fr-FR");
        percentCulture.Culture = new CultureInfo("fr-FR");
        ResetColor();
        ResetSize();
        franceFrame.BorderColor = Colors.Black;
        franceStack.HeightRequest = 34;
        franceStack.WidthRequest = 42;
#if IOS
        franceImage.WidthRequest = 40;
        franceImage.HeightRequest = 32;
#endif
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
#if IOS
        usImage.WidthRequest = 36;
        usImage.HeightRequest = 28;
        indiaImage.WidthRequest = 36;
        indiaImage.HeightRequest = 28;
        russiaImage.WidthRequest = 36;
        russiaImage.HeightRequest = 28;
        franceImage.WidthRequest = 36;
        franceImage.HeightRequest = 28;
#endif
    }
}