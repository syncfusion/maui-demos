#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;


namespace SampleBrowser.Maui.Inputs.SfNumericEntry;

public partial class NumericEntryCultureAndFormatting : SampleView
{

#if ANDROID || IOS
    NumericEntryCultureAndFormattingMobile numericEntryCultureAndFormattingMobile;
#else
    NumericEntryCultureAndFormattingDesktop numericEntryCultureAndFormattingDesktop;
#endif
    public NumericEntryCultureAndFormatting()
	{
		InitializeComponent();
#if ANDROID || IOS
        numericEntryCultureAndFormattingMobile = new NumericEntryCultureAndFormattingMobile();
        this.Content = numericEntryCultureAndFormattingMobile.Content;
        this.OptionView = numericEntryCultureAndFormattingMobile.OptionView;
#else
        numericEntryCultureAndFormattingDesktop = new NumericEntryCultureAndFormattingDesktop();
        this.Content = numericEntryCultureAndFormattingDesktop.Content;
        this.WidthRequest = numericEntryCultureAndFormattingDesktop.WidthRequest;
        this.OptionView = numericEntryCultureAndFormattingDesktop.OptionView;
        this.Margin = numericEntryCultureAndFormattingDesktop.Margin;
#endif
    }
}