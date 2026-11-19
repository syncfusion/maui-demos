using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class ContextMenu : SampleView
{
	public ContextMenu()
	{
		InitializeComponent();
        // Ensure the ViewModel can access the DataGrid instance at runtime for actions
        if (BindingContext is SampleBrowser.Maui.DataGrid.OrderInfoViewModel vm)
        {
            vm.DataGrid = dataGrid;
        }
	}
}
