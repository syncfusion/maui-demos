using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Buttons;
using Syncfusion.Maui.DataGrid;
using System.Collections.ObjectModel; 

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class CheckBoxSelectorColumn : SampleView
{
    public CheckBoxSelectorColumn()
    {
        InitializeComponent();

        ApplyInitialSelection();
    }

    /// <summary>
    /// Applies initial selection to the rows on load.
    /// </summary>
    private void ApplyInitialSelection()
    {
        if (dataGrid == null || viewModel?.OrdersInfo == null || viewModel.OrdersInfo.Count == 0)
            return;

        if (viewModel.isUpdating)
            return;

        viewModel.isUpdating = true;

        int[] indices = new[] { 1, 3, 5 };

        foreach (var idx in indices)
        {
            if (idx >= 0 && idx < viewModel.OrdersInfo.Count)
            {
                var item = viewModel.OrdersInfo[idx];
                if (item != null)
                {
                    item.IsChecked = true;
                    if (!dataGrid.SelectedRows.Contains(item))
                    {
                        dataGrid.SelectedRows.Add(item);
                    }
                }
            }
        }

        viewModel.isUpdating = false;
    }
}
