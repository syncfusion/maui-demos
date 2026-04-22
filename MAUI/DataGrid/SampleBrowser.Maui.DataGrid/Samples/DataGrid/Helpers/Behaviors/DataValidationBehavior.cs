#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleBrowser.Maui.DataGrid
{
    public class DataValidationBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.DataGrid.SfDataGrid? datagrid;
        private Syncfusion.Maui.Inputs.SfComboBox? comboBox;

        protected override void OnAttachedTo(SampleView bindable)
        {
            datagrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid?>("dataGrid");
            this.comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");

            comboBox.SelectionChanged += DataValidationPicker_SelectedIndexChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            comboBox!.SelectionChanged -= DataValidationPicker_SelectedIndexChanged;

            datagrid = null;
            comboBox = null;
            base.OnDetachingFrom(bindable);
        }

        private void DataValidationPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (this.comboBox!.SelectedIndex)
            {
                case 0:
                    SetValidationMode(Syncfusion.Maui.DataGrid.DataGridValidationMode.None);
                    break;
                case 1:
                    SetValidationMode(Syncfusion.Maui.DataGrid.DataGridValidationMode.InView);
                    break;
                case 2:
                    SetValidationMode(Syncfusion.Maui.DataGrid.DataGridValidationMode.InEdit);
                    break;
            }
        }

        private void SetValidationMode(Syncfusion.Maui.DataGrid.DataGridValidationMode validationMode)
        {
            if (this.datagrid!.ValidationMode != validationMode)
            {
                this.datagrid!.ValidationMode = validationMode;
            }
        }
    }
}
