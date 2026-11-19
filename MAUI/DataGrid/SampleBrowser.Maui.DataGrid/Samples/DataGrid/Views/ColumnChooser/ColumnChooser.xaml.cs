using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.DataGrid;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class ColumnChooser : SampleView
{
    public ColumnChooser()
    {
        InitializeComponent();
    }

    private void Button_Clicked(object? sender, TappedEventArgs e)
    {
        dataGrid.ShowColumnChooser = true;
    }
}
