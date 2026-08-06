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

    private void comboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if (this.comboBox!.SelectedIndex == 0)
        {
            this.dataGrid?.ColumnResizeMode = Syncfusion.Maui.DataGrid.DataGridColumnResizeMode.OnTouchUp;
            this.dataGrid?.RowResizeMode = Syncfusion.Maui.DataGrid.DataGridRowResizeMode.OnTouchUp;
        }
        else if (this.comboBox.SelectedIndex == 1)
        {
            this.dataGrid?.ColumnResizeMode = Syncfusion.Maui.DataGrid.DataGridColumnResizeMode.OnMoved;
            this.dataGrid?.RowResizeMode = Syncfusion.Maui.DataGrid.DataGridRowResizeMode.OnMoved;
        }
    }
}