#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
namespace SampleBrowser.Maui.LiquidGlass.SfCalendar;

public partial class CalendarPage : SampleView
{
	public CalendarPage()
	{
		InitializeComponent();
#if IOS || MACCATALYST
		this.Calendar.Background = new SolidColorBrush(Colors.Transparent);
#endif
	}
}