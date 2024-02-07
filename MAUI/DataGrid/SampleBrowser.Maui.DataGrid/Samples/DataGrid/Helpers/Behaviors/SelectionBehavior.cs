#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
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
    public class SelectionBehavior : Behavior<SampleView>
    {
        private Microsoft.Maui.Controls.Picker? selectionPicker;
        private Syncfusion.Maui.DataGrid.SfDataGrid? datagrid;

        protected override void OnAttachedTo(SampleView bindable)
        {
            this.datagrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
            this.selectionPicker = bindable.FindByName<Microsoft.Maui.Controls.Picker>("SelectionPicker");

            selectionPicker.SelectedIndexChanged += SelectionPicker_SelectedIndexChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            this.selectionPicker!.SelectedIndexChanged -= SelectionPicker_SelectedIndexChanged;

            this.datagrid = null;
            this.selectionPicker = null;

            base.OnDetachingFrom(bindable);
        }

        private void SelectionPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (this.selectionPicker!.SelectedIndex)
            {
                case 0:
                    SetSelectionMode(Syncfusion.Maui.DataGrid.DataGridSelectionMode.None);
                    break;
                case 1:
                    SetSelectionMode(Syncfusion.Maui.DataGrid.DataGridSelectionMode.Single);
                    break;
                case 2:
                    SetSelectionMode(Syncfusion.Maui.DataGrid.DataGridSelectionMode.SingleDeselect);
                    break;
                case 3:
                    SetSelectionMode(Syncfusion.Maui.DataGrid.DataGridSelectionMode.Multiple);
                    break;
            }
        }

        private void SetSelectionMode(Syncfusion.Maui.DataGrid.DataGridSelectionMode selectionMode)
        {
            if (this.datagrid!.SelectionMode != selectionMode)
            {
                this.datagrid.SelectionMode = selectionMode;
            }
        }
    }
}
