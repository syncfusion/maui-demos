using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class RowTemplateView : SampleView
{
	public RowTemplateView()
	{
		InitializeComponent();
	}

    private void dataGrid_DataGridLoaded(object? sender, EventArgs e)
    {
		this.dataGrid.ExpandAllDetailsView();
    }
}