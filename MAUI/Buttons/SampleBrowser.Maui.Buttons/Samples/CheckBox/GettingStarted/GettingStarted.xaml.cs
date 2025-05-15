#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Buttons.CheckBox;

public partial class GettingStarted : SampleView
{
#if ANDROID || IOS
    GettingStartedMobile gettingStartedMobile;
#else
    GettingStartedDesktop gettingStartedDesktop;
#endif
    public GettingStarted()
	{
		InitializeComponent();
#if ANDROID || IOS
        gettingStartedMobile = new GettingStartedMobile();
        this.Content = gettingStartedMobile.Content;
        this.OptionView = gettingStartedMobile.OptionView;
#else
        gettingStartedDesktop = new GettingStartedDesktop();
        this.Content = gettingStartedDesktop.Content;       
        this.OptionView = gettingStartedDesktop.OptionView;  
        this.WidthRequest = gettingStartedDesktop.WidthRequest;
#endif
    }
}