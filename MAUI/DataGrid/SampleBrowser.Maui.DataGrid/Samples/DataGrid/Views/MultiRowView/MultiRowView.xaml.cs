using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataGrid;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class MultiRowView : SampleView
{
	public MultiRowView()
	{
		InitializeComponent();

        if (DeviceInfo.Platform == DevicePlatform.WinUI || DeviceInfo.Platform == DevicePlatform.MacCatalyst)
        {
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                MappingName = "Telephone",
                HeaderText = "Telephone",
                Row = 2,
                Column = 3
            });
        }
    }
}