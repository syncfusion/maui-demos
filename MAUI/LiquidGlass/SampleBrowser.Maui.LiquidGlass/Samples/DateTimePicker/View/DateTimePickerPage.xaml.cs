#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
namespace SampleBrowser.Maui.LiquidGlass.SfDateTimePicker;

public partial class DateTimePickerPage : SampleView
{
	public DateTimePickerPage()
	{
		InitializeComponent();
#if IOS || MACCATALYST
		this.DateTimePicker.Background = new SolidColorBrush(Colors.Transparent);
		this.DateTimePicker.ColumnHeaderView.Background = new SolidColorBrush(Colors.Transparent);
		this.DateTimePicker.HeaderView.Background = new SolidColorBrush(Colors.Transparent);
#endif
	}
}