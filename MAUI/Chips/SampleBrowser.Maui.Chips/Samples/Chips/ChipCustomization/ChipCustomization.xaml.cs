#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.Chips.SfChip;

public partial class ChipCustomization : SampleView
{
	public ChipCustomization()
	{
		InitializeComponent();
#if ANDROID || IOS
        this.Content=new ChipCustomizationMobile();
#elif WINDOWS || MACCATALYST
        this.Content=new ChipCustomizationDesktop();
#endif
    }
}