#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.Popup;

namespace SampleBrowser.Maui.DataGrid;

public class ClipboardBehavior : Behavior<SampleView>
{	
    private Syncfusion.Maui.Inputs.SfComboBox? comboBox;
    private Syncfusion.Maui.Inputs.SfComboBox? selectionUnitComboBox;
    private Syncfusion.Maui.Inputs.SfComboBox? clipboardCopyComboBox;
    private Syncfusion.Maui.Inputs.SfComboBox? clipboardPasteComboBox;
    private Syncfusion.Maui.DataGrid.SfDataGrid? datagrid;
    Label? clipboard;
    SfPopup? popup;

    protected override void OnAttachedTo(SampleView bindable)
    {
        this.datagrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
        this.comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");
        this.selectionUnitComboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("selectionUnitComboBox");
        this.clipboardCopyComboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("clipboardCopyComboBox");
        this.clipboardPasteComboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("clipboardPasteComboBox");
        this.clipboard = bindable.FindByName<Label>("clipboard");
        popup = bindable.FindByName<SfPopup>("contextMenuPopup");

        this.datagrid.CopyContent += Datagrid_CopyContent;
        comboBox.SelectionChanged += SelectionPicker_SelectedIndexChanged;
        selectionUnitComboBox.SelectionChanged += SelectionPicker_SelectionUnitChanged;
        clipboardCopyComboBox.SelectionChanged += ClipboardCopyComboBox_SelectionChanged;
        clipboardPasteComboBox.SelectionChanged += ClipboardPasteComboBox_SelectionChanged;
        datagrid.CellLongPress += Datagrid_CellLongPress;
        base.OnAttachedTo(bindable);
    }

    private void Datagrid_CellLongPress(object? sender, DataGridCellLongPressEventArgs e)
    {
        if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
        {
            if (popup != null && datagrid != null)
            {
                var visualContainer = datagrid.GetVisualContainer();
                double verticalOffset = visualContainer.VerticalOffset;
                double horizontalOffset = visualContainer.HorizontalOffset;
                double xOffset = 0;
                for (int i = 0; i < e.RowColumnIndex.ColumnIndex; i++)
                {
                    xOffset += datagrid.Columns[i].ActualWidth;
                }
                xOffset -= horizontalOffset;
                double yOffset = datagrid.HeaderRowHeight + (e.RowColumnIndex.RowIndex * datagrid.RowHeight);
                yOffset -= verticalOffset;
                popup.Show(xOffset, yOffset);
                
            }
        }
    }

    private async void Datagrid_CopyContent(object? sender, DataGridCopyPasteEventArgs e)
    {
        await Task.Delay(50);
        var clipboardText = await Clipboard.GetTextAsync();
        if (!string.IsNullOrEmpty(clipboardText) && this.clipboard != null)
        {
            Dispatcher.Dispatch(() =>
            {
                this.clipboard.Text = clipboardText;
            });
        }
    }

    private void ClipboardPasteComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if (this.datagrid == null || clipboardPasteComboBox == null)
        {
            return;
        }

        var selectedItems = clipboardPasteComboBox.SelectedItems;
        if (selectedItems == null || selectedItems.Count == 0 || selectedItems.Contains("None"))
        {
            datagrid.PasteOption = DataGridPasteOption.None;
            return;
        }

        DataGridPasteOption newPasteOption = 0;
        foreach (var item in selectedItems)
        {
            switch (item.ToString())
            {
                case "PasteData":
                {
                    newPasteOption |= DataGridPasteOption.PasteData;
                    break; 
                }
                case "ExcludeFirstLine":
                {
                    newPasteOption |= DataGridPasteOption.ExcludeFirstLine;
                    break;
                }
                case "IncludeHiddenColumn":
                {
                    newPasteOption |= DataGridPasteOption.IncludeHiddenColumn;
                    break;
                }
            }
        }

        datagrid.PasteOption = newPasteOption;
    }

    private void ClipboardCopyComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if (this.datagrid == null || clipboardCopyComboBox == null)
        {
            return;
        }

        var selectedItems = clipboardCopyComboBox.SelectedItems;
        if (selectedItems == null || selectedItems.Count == 0 || selectedItems.Contains("None"))
        {
            datagrid.CopyOption = DataGridCopyOption.None;
            return;
        }

        DataGridCopyOption newCopyOption = 0;
        foreach (var item in selectedItems)
        {
            switch (item.ToString())
            {
                case "CopyData":
                {
                    newCopyOption |= DataGridCopyOption.CopyData;
                    break;
                }
                case "CutData":
                {
                    newCopyOption |= DataGridCopyOption.CutData;
                    break;
                }
                case "IncludeHeaders":
                {
                    newCopyOption |= DataGridCopyOption.IncludeHeaders;
                    break;
                }
                case "IncludeFormat":
                {
                    newCopyOption |= DataGridCopyOption.IncludeFormat;
                    break;
                }
                case "IncludeHiddenColumn":
                {
                    newCopyOption |= DataGridCopyOption.IncludeHiddenColumn;
                    break;
                }
            }
        }
        datagrid.CopyOption = newCopyOption;

    }

    protected override void OnDetachingFrom(SampleView bindable)
    {
        if(this.comboBox == null || this.selectionUnitComboBox == null || this.clipboardCopyComboBox == null || this.clipboardPasteComboBox == null)
        {
            return;
        }

        this.comboBox.SelectionChanged -= SelectionPicker_SelectedIndexChanged;
        this.selectionUnitComboBox.SelectionChanged -= SelectionPicker_SelectionUnitChanged;
        this.clipboardCopyComboBox.SelectionChanged -= ClipboardCopyComboBox_SelectionChanged;
        this.clipboardPasteComboBox.SelectionChanged -= ClipboardPasteComboBox_SelectionChanged;

        this.datagrid = null;
        this.comboBox = null;
        this.selectionUnitComboBox = null;
        this.clipboardCopyComboBox = null;
        this.clipboardPasteComboBox = null;

        base.OnDetachingFrom(bindable);
    }
    private void SelectionPicker_SelectionUnitChanged(object? sender, EventArgs e)
    {
        if (this.datagrid != null && this.selectionUnitComboBox != null)
        {
            if (this.selectionUnitComboBox.SelectedIndex == 0)
            {
                datagrid.SelectionUnit = Syncfusion.Maui.DataGrid.DataGridSelectionUnit.Row;
            }
            else
            {
                datagrid.SelectionUnit = Syncfusion.Maui.DataGrid.DataGridSelectionUnit.Cell;
            }
        }
    }

    private void SelectionPicker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if(this.comboBox == null)
        {
            return;
        }

        switch (this.comboBox.SelectedIndex)
        {
            case 0:
            {
                SetSelectionMode(Syncfusion.Maui.DataGrid.DataGridSelectionMode.None);
                break;
            }
            case 1:
            {
                SetSelectionMode(Syncfusion.Maui.DataGrid.DataGridSelectionMode.Single);
                break;
            }
            case 2:
            {
                SetSelectionMode(Syncfusion.Maui.DataGrid.DataGridSelectionMode.SingleDeselect);
                break;
            }
            case 3:
            {
                SetSelectionMode(Syncfusion.Maui.DataGrid.DataGridSelectionMode.Multiple);
                break;
            }
        }
    }

    private void SetSelectionMode(Syncfusion.Maui.DataGrid.DataGridSelectionMode selectionMode)
    {
        if (this.datagrid != null && this.datagrid.SelectionMode != selectionMode)
        {
            this.datagrid.SelectionMode = selectionMode;
        }
    }

}