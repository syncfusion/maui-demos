#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class Resizing : SampleView
{
	public Resizing()
	{
		InitializeComponent();

#if ANDROID
		dataGrid.DefaultStyle.GridLineStrokeThickness *= (float)DeviceDisplay.MainDisplayInfo.Density;
		dataGrid.DefaultStyle.HeaderGridLineStrokeThickness *= (float)DeviceDisplay.MainDisplayInfo.Density;
#endif
	}
}