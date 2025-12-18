#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;


namespace SampleBrowser.Maui.BusyIndicator.SfBusyIndicator;

public partial class AnimationType : SampleView
{
#if ANDROID || IOS
    AnimationTypeMobile animationTypeMobile;
#else
AnimationTypeDesktop animationTypeDesktop;
#endif
    public AnimationType()
    {
        InitializeComponent();
#if ANDROID || IOS
        animationTypeMobile = new AnimationTypeMobile();
        this.Content = animationTypeMobile.Content;
        this.OptionView = animationTypeMobile.OptionView;
        this.WidthRequest = animationTypeMobile.WidthRequest;
#else
    animationTypeDesktop = new AnimationTypeDesktop();
    this.Content = animationTypeDesktop.Content;
    this.OptionView = animationTypeDesktop.OptionView;
    this.WidthRequest = animationTypeDesktop.WidthRequest;
#endif
    }
}