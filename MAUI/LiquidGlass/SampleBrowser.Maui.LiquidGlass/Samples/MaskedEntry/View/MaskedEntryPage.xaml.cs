#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfMaskedEntry;

public partial class MaskedEntryPage : SampleView
{
    
#if ANDROID || IOS
    MaskedEntryPageMobile maskedEntryGettingStartedMobile;
#else
    MaskedEntryPageDesktop maskedEntryGettingStartedDesktop;
#endif
    public MaskedEntryPage()
	{
		InitializeComponent();
#if ANDROID || IOS
        maskedEntryGettingStartedMobile = new MaskedEntryPageMobile();
        this.Content = maskedEntryGettingStartedMobile.Content;
        this.OptionView = maskedEntryGettingStartedMobile.OptionView;
#else
        maskedEntryGettingStartedDesktop = new MaskedEntryPageDesktop();
        this.Content = maskedEntryGettingStartedDesktop.Content;
        this.WidthRequest = maskedEntryGettingStartedDesktop.WidthRequest;
        this.OptionView = maskedEntryGettingStartedDesktop.OptionView;
        this.Margin = maskedEntryGettingStartedDesktop.Margin;
#endif
    }
}