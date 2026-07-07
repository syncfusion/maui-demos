using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class DetailsViewColumnTypes : SampleView
{
	public DetailsViewColumnTypes()
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