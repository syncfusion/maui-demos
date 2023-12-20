#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.NavigationDrawer.NavigationDrawer;

public partial class GettingStarted : SampleView
{
#if ANDROID || IOS
    GettingStartedMobile gettingStartedMobile;
#elif WINDOWS || MACCATALYST
    GettingStartedDesktop gettingStartedDesktop;
#endif
    public GettingStarted()
	{
		InitializeComponent();
#if ANDROID || IOS     
        gettingStartedMobile = new GettingStartedMobile();
        this.Content = gettingStartedMobile.Content;
        this.OptionView = gettingStartedMobile.OptionView;
#elif WINDOWS || MACCATALYST
        gettingStartedDesktop = new GettingStartedDesktop();
        this.Content = gettingStartedDesktop.Content;
        this.OptionView = gettingStartedDesktop.OptionView;
#endif
    }
}