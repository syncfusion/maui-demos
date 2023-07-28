#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Inputs.SfNumericEntry;

public partial class NumericEntryGettingStarted : SampleView
{

#if ANDROID || IOS
    NumericEntryGettingStartedMobile numericEntryGettingStartedMobile;
#else
    NumericEntryGettingStartedDesktop numericEntryGettingStartedDesktop;
#endif
    public NumericEntryGettingStarted()
	{
		InitializeComponent();
#if ANDROID || IOS
        numericEntryGettingStartedMobile = new NumericEntryGettingStartedMobile();
        this.Content = numericEntryGettingStartedMobile.Content;
        this.OptionView = numericEntryGettingStartedMobile.OptionView;
#else
        numericEntryGettingStartedDesktop = new NumericEntryGettingStartedDesktop();
        this.Content = numericEntryGettingStartedDesktop.Content;
        this.WidthRequest = numericEntryGettingStartedDesktop.WidthRequest;
        this.OptionView = numericEntryGettingStartedDesktop.OptionView;
        this.Margin = numericEntryGettingStartedDesktop.Margin;
#endif
    }
}