#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfNumericEntry;

public partial class NumericEntryPage : SampleView
{

#if ANDROID || IOS
    NumericEntryPageMobile numericEntryPageMobile;
#else
    NumericEntryPageDesktop numericEntryPageDesktop;
#endif
    public NumericEntryPage()
	{
		InitializeComponent();
#if ANDROID || IOS
        numericEntryPageMobile = new NumericEntryPageMobile();
        this.Content = numericEntryPageMobile.Content;
        this.OptionView = numericEntryPageMobile.OptionView;
#else
        numericEntryPageDesktop = new NumericEntryPageDesktop();
        this.Content = numericEntryPageDesktop.Content;
        this.WidthRequest = numericEntryPageDesktop.WidthRequest;
        this.OptionView = numericEntryPageDesktop.OptionView;
        this.Margin = numericEntryPageDesktop.Margin;
#endif
    }
}