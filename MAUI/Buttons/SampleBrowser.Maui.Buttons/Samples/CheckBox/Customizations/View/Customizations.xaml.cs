#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Buttons.CheckBox;

public partial class Customizations : SampleView
{
    public Customizations()
	{
		InitializeComponent();
#if ANDROID || IOS
        this.Content = new CustomizationsMobile();
#else
        this.Content = new CustomizationsDesktop();
#endif
    }
}