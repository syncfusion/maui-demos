#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Rotator.Rotator;

public partial class GettingStarted : SampleView
{
    [Obsolete]
    public GettingStarted()
	{
		InitializeComponent();
#if ANDROID || IOS
        GettingStartedMobile gettingStartedMobile = new GettingStartedMobile();
        this.Content = gettingStartedMobile;
        this.OptionView = gettingStartedMobile.OptionView;
#elif WINDOWS || MACCATALYST
        GettingStartedDesktop gettingStartedDesktop = new GettingStartedDesktop();
        this.Content = gettingStartedDesktop;
        this.OptionView = gettingStartedDesktop.OptionView;
#endif
    }
}