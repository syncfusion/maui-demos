using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataGrid;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class DetailsViewDataTable : SampleView
{
    public DetailsViewDataTable()
    {
        InitializeComponent();
    }

    private void dataGrid_DataGridLoaded(object? sender, EventArgs e)
    {
        this.dataGrid.ExpandDetailsViewAt(0);
        this.dataGrid.ExpandDetailsViewAt(1);
        this.dataGrid.ExpandDetailsViewAt(2);
    }
}