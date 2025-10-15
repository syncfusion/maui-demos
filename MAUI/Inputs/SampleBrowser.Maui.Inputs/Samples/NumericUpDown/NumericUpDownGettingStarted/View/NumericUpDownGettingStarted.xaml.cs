#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Inputs.NumericUpDown;

public partial class NumericUpDownGettingStarted : SampleView
{

#if ANDROID || IOS
    NumericUpDownGettingStartedMobile numericUpDownGettingStartedMobile;
#else
    NumericUpDownGettingStartedDesktop numericUpDownGettingStartedDesktop;
#endif
    public NumericUpDownGettingStarted()
	{
		InitializeComponent();
#if ANDROID || IOS
        numericUpDownGettingStartedMobile = new NumericUpDownGettingStartedMobile();
        this.Content = numericUpDownGettingStartedMobile.Content;
        this.OptionView = numericUpDownGettingStartedMobile.OptionView;
#else
        numericUpDownGettingStartedDesktop = new NumericUpDownGettingStartedDesktop();
        this.Content = numericUpDownGettingStartedDesktop.Content;
        this.WidthRequest = numericUpDownGettingStartedDesktop.WidthRequest;
        this.OptionView = numericUpDownGettingStartedDesktop.OptionView;
        this.Margin = numericUpDownGettingStartedDesktop.Margin;
#endif
    }
}