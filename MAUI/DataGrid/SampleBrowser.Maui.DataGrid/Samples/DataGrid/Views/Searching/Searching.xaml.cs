using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataGrid;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class Searching : SampleView
{
    public Searching()
    {
        InitializeComponent();
    }

    private void FindNextClicked(object? sender, TappedEventArgs e)
    {
        //this.dataGrid.SearchController.FindNext(filterText.Text);
    }

    private void FindPreviousClicked(object? sender, TappedEventArgs e)
    {
       // this.dataGrid.SearchController.FindPrevious(filterText.Text);
    }

    private void SearchClicked(object? sender, TappedEventArgs e)
    {
        //this.dataGrid.SearchController.Search(filterText.Text);
    }

    private void ClearButtonClicked(object? sender, TappedEventArgs e)
    {
        //this.filterText.Value = string.Empty;
    }

}
