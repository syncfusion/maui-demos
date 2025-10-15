#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class ColumnSizingBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.DataGrid.SfDataGrid? datagrid;
        private Syncfusion.Maui.Inputs.SfComboBox? comboBox;

        protected override void OnAttachedTo(SampleView bindable)
        {
            datagrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid?>("dataGrid");
            this.comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");

            comboBox.SelectionChanged += ColumnSizingPicker_SelectedIndexChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            comboBox!.SelectionChanged -= ColumnSizingPicker_SelectedIndexChanged;
            
            datagrid = null;
            comboBox = null;
            base.OnDetachingFrom(bindable);
        }

        private void ColumnSizingPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch(this.comboBox!.SelectedIndex)
            {
                case 0:
                    SetColumnWidthMode(Syncfusion.Maui.DataGrid.ColumnWidthMode.Fill);
                    break;
                case 1:
                    SetColumnWidthMode(Syncfusion.Maui.DataGrid.ColumnWidthMode.Auto);
                    break;
                case 2:
                    SetColumnWidthMode(Syncfusion.Maui.DataGrid.ColumnWidthMode.FitByCell);
                    break;
                case 3:
                    SetColumnWidthMode(Syncfusion.Maui.DataGrid.ColumnWidthMode.FitByHeader);
                    break;
                case 4:
                    SetColumnWidthMode(Syncfusion.Maui.DataGrid.ColumnWidthMode.LastColumnFill);
                    break;
                case 5:
                    SetColumnWidthMode(Syncfusion.Maui.DataGrid.ColumnWidthMode.None);
                    break;
            }
        }

        private void SetColumnWidthMode(Syncfusion.Maui.DataGrid.ColumnWidthMode columnWidthMode)
        {
            if (this.datagrid!.ColumnWidthMode != columnWidthMode)
            {
                this.datagrid!.ColumnWidthMode = columnWidthMode;
            }
        }
    }
}
