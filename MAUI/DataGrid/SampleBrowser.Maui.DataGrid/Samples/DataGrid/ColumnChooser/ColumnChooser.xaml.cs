#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.DataGrid;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class ColumnChooser : SampleView
{
    public ColumnChooser()
    {
        InitializeComponent();
    }

    private void Button_Clicked(object sender, TappedEventArgs e)
    {
        dataGrid.ShowColumnChooser = true;
    }
}
