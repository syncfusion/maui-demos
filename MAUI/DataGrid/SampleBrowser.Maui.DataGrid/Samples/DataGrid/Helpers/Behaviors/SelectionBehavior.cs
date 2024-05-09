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
        private Syncfusion.Maui.Inputs.SfComboBox? comboBox;
        private Syncfusion.Maui.DataGrid.SfDataGrid? datagrid;

        protected override void OnAttachedTo(SampleView bindable)
        {
            this.datagrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
            this.comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");

            comboBox.SelectionChanged += SelectionPicker_SelectedIndexChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            this.comboBox!.SelectionChanged -= SelectionPicker_SelectedIndexChanged;

            this.datagrid = null;
            this.comboBox = null;

            base.OnDetachingFrom(bindable);
        }

        private void SelectionPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (this.comboBox!.SelectedIndex)
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
