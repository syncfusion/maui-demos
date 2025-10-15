#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataGrid;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class Searching : SampleView
{
    public Searching()
    {
        InitializeComponent();
    }

    private void FindNextClicked(object sender, TappedEventArgs e)
    {
        this.dataGrid.SearchController.FindNext(filterText.Text);
    }

    private void FindPreviousClicked(object sender, TappedEventArgs e)
    {
        this.dataGrid.SearchController.FindPrevious(filterText.Text);
    }

    private void SearchClicked(object sender, TappedEventArgs e)
    {
        this.dataGrid.SearchController.Search(filterText.Text);
    }

    private void ClearButtonClicked(object sender, TappedEventArgs e)
    {
        this.filterText.Value = string.Empty;
    }

}
