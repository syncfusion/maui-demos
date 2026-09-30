using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class Clipboard : SampleView
{
    public Clipboard()
    {
        InitializeComponent();
        viewModel.DataGrid = this.dataGrid;
        viewModel.Popup = this.contextMenuPopup;
    }
}
