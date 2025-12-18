#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfNavigationDrawer;

public partial class NavigationDrawerPage : SampleView
{
#if ANDROID || IOS
    NavigationDrawerPageMobile navigationDrawerPageMobile;
#elif WINDOWS || MACCATALYST
    NavigationDrawerPageDesktop navigationDrawerPageDesktop;
#endif
    public NavigationDrawerPage()
	{
		InitializeComponent();
#if ANDROID || IOS     
        navigationDrawerPageMobile = new NavigationDrawerPageMobile();
        this.Content = navigationDrawerPageMobile.Content;
        this.OptionView = navigationDrawerPageMobile.OptionView;
#elif WINDOWS || MACCATALYST
        navigationDrawerPageDesktop = new NavigationDrawerPageDesktop();
        this.Content = navigationDrawerPageDesktop.Content;
        this.OptionView = navigationDrawerPageDesktop.OptionView;
#endif
    }
}