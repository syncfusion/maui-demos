#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Inputs.SfMaskedEntry;

public partial class Culture : SampleView
{
    public Culture()
	{
		InitializeComponent();
#if ANDROID || IOS
        this.Content = new CultureMobile();
#else
        CultureDesktop cultureDesktop = new();
        this.Content = cultureDesktop.Content;
        this.Margin = cultureDesktop.Margin;
#endif

    }

   
}