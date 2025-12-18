#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.LiquidGlass.SfChip;

public partial class ChipPage : SampleView
{
	public ChipPage()
	{
		InitializeComponent();
#if WINDOWS || MACCATALYST
		this.Content=new ChipPageDesktop();
#elif ANDROID || IOS
		this.Content=new ChipPageMobile();
#endif
    }
}