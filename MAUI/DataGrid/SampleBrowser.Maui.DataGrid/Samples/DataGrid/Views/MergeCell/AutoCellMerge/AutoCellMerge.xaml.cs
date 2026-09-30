using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Data;
using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.DataGrid.Helper;

namespace SampleBrowser.Maui.DataGrid.SfDataGrid;

public partial class AutoCellMerge : SampleView
{
    IPropertyAccessProvider? reflector = null;
    public AutoCellMerge()
    {
        InitializeComponent();
    }

    void DataGrid_ItemsSourceChanged(object? sender, DataGridItemsSourceChangedEventArgs e)
    {
        reflector = dataGrid.View != null ? dataGrid.View.GetPropertyAccessProvider() : null;
    }

    private void Grid_QueryCoveredRange(object? sender, DataGridQueryCoveredRangeEventArgs e)
    {
        // Content-based merge
        var range = GetRange(e.Column, e.RowColumnIndex.RowIndex, e.RowColumnIndex.ColumnIndex, e.Record);

        if (range == null)
            return;

        if (!dataGrid.CoveredCells.IsInRange(range))
        {
            e.Range = range;
            e.Handled = true;
        }
    }

    private CoveredCellInfo? GetRange(DataGridColumn? column, int rowIndex, int columnIndex, object rowData)
    {
        // Basic validations
        if (column == null || rowData == null || string.IsNullOrEmpty(column.MappingName))
            return null;

        if (reflector == null)
            return null;

        // Unwrap RecordEntry to get actual data
        object actualData = rowData is RecordEntry re ? re.Data : rowData;

        // Start with single cell
        var range = new CoveredCellInfo(columnIndex, columnIndex, rowIndex, rowIndex);

        object? data;
        try
        {
            data = reflector.GetFormattedValue(actualData, column.MappingName);
        }
        catch
        {
            return null;
        }

        if (data == null)
            return null;

        DataGridColumn? leftColumn = null;
        DataGridColumn? rightColumn = null;

        int colPos = dataGrid.Columns.IndexOf(column);
        if (colPos < 0)
            return null;

        // Compare right columns
        for (int i = colPos; i < dataGrid.Columns.Count - 1; i++)
        {
            var nextCol = dataGrid.Columns[i + 1];
                object? comp = null;
            try { comp = reflector.GetFormattedValue(actualData, nextCol.MappingName); } catch { break; }
            if (comp == null || !Equals(comp, data))
                break;
            rightColumn = nextCol;
        }

        // Compare left columns
        for (int i = colPos; i > 0; i--)
        {
            var prevCol = dataGrid.Columns[i - 1];
                object? comp = null;
            try { comp = reflector.GetFormattedValue(actualData, prevCol.MappingName); } catch { break; }
            if (comp == null || !Equals(comp, data))
                break;
            leftColumn = prevCol;
        }

        if (leftColumn != null || rightColumn != null)
        {
            if (leftColumn != null)
            {
                var leftIndex = dataGrid.ResolveToScrollColumnIndex(dataGrid.Columns.IndexOf(leftColumn));
                range = new CoveredCellInfo(leftIndex, range.Right, range.Top, range.Bottom);
            }

            if (rightColumn != null)
            {
                var rightIndex = dataGrid.ResolveToScrollColumnIndex(dataGrid.Columns.IndexOf(rightColumn));
                range = new CoveredCellInfo(range.Left, rightIndex, range.Top, range.Bottom);
            }

            return range;
        }

        // Merge vertically
        int previousRowIndex = -1;
        int nextRowIndex = -1;

        var view = dataGrid.View;
        int recordsCount;
        if (dataGrid.GroupColumnDescriptions.Count != 0)
        {
            int topLevelCount = view?.TopLevelGroup?.DisplayElements?.Count ?? 0;
            recordsCount = topLevelCount + dataGrid.TableSummaryRows.Count + dataGrid.UnboundRows.Count + (dataGrid.AddNewRowPosition == DataGridAddNewRowPosition.Top ? 1 : 0);
        }
        else
        {
            int recCount = view?.Records?.Count ?? 0;
            recordsCount = recCount + dataGrid.TableSummaryRows.Count + dataGrid.UnboundRows.Count + (dataGrid.AddNewRowPosition == DataGridAddNewRowPosition.Top ? 1 : 0);
        }

        var startIndex = dataGrid.ResolveStartIndexBasedOnPosition();

        for (int i = rowIndex - 1; i >= startIndex; i--)
        {
            var prevRecord = dataGrid.GetRecordAtRowIndex(i);
            if (prevRecord == null)
                break;

            object prevData = (prevRecord as RecordEntry)?.Data ?? prevRecord;
            object? comp = null;
            try { comp = reflector.GetFormattedValue(prevData, column.MappingName); } catch { break; }
            if (comp == null || !Equals(comp, data))
                break;
            previousRowIndex = i;
        }

        for (int i = rowIndex + 1; i < recordsCount + 1; i++)
        {
            var nextRecord = dataGrid.GetRecordAtRowIndex(i);
            if (nextRecord == null)
                break;

            object nextData = (nextRecord as RecordEntry)?.Data ?? nextRecord;
            object? comp = null;
            try { comp = reflector.GetFormattedValue(nextData, column.MappingName); } catch { break; }
            if (comp == null || !Equals(comp, data))
                break;
            nextRowIndex = i;
        }

        if (previousRowIndex != -1 || nextRowIndex != -1)
        {
            if (previousRowIndex != -1)
                range = new CoveredCellInfo(range.Left, range.Right, previousRowIndex, range.Bottom);

            if (nextRowIndex != -1)
                range = new CoveredCellInfo(range.Left, range.Right, range.Top, nextRowIndex);

            return range;
        }

        return null;
    }
}