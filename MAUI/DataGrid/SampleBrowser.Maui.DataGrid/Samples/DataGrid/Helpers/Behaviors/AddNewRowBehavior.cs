#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class AddNewRowBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.DataGrid.SfDataGrid? datagrid;
        private Syncfusion.Maui.Inputs.SfComboBox? comboBox;

        protected override void OnAttachedTo(SampleView bindable)
        {
            datagrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid?>("dataGrid");
            this.comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");

            if (comboBox != null)
                comboBox.SelectionChanged += AddNewRowPositionPicker_SelectedIndexChanged;
            if(datagrid != null)
            {
                datagrid.AddNewRowInitiating += Datagrid_AddNewRowInitiating;
            }
            base.OnAttachedTo(bindable);
        }

        private void Datagrid_AddNewRowInitiating(object? sender, Syncfusion.Maui.DataGrid.DataGridAddNewRowInitiatingEventArgs e)
        {
            var data = e.Object as DealerInfo;
            if (data != null)
            {
                data.ShippedDate = DateTime.Now;
            }
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            if (comboBox != null)
                comboBox.SelectionChanged -= AddNewRowPositionPicker_SelectedIndexChanged;

            datagrid = null;
            comboBox = null;
            base.OnDetachingFrom(bindable);
        }

        private void AddNewRowPositionPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if(this.comboBox != null && datagrid != null)
            {
                switch (this.comboBox.SelectedIndex)
                {
                    case 0:
                        SetAddNewRowPosition(Syncfusion.Maui.DataGrid.DataGridAddNewRowPosition.None);
                        break;
                    case 1:
                        SetAddNewRowPosition(Syncfusion.Maui.DataGrid.DataGridAddNewRowPosition.Top);
                        datagrid.NewItemPlaceholderPosition = NewItemPlaceholderPosition.AtBeginning;
                        break;
                    case 2:
                        SetAddNewRowPosition(Syncfusion.Maui.DataGrid.DataGridAddNewRowPosition.FixedTop);
                        datagrid.NewItemPlaceholderPosition = NewItemPlaceholderPosition.AtBeginning;
                        break;
                    case 3:
                        SetAddNewRowPosition(Syncfusion.Maui.DataGrid.DataGridAddNewRowPosition.Bottom);
                        datagrid.NewItemPlaceholderPosition = NewItemPlaceholderPosition.AtEnd;
                        break;
                }
            }
        }

        private void SetAddNewRowPosition(Syncfusion.Maui.DataGrid.DataGridAddNewRowPosition addNewRowPosition)
        {
            if (this.datagrid != null)
            {
                if (this.datagrid.AddNewRowPosition != addNewRowPosition)
                {
                    this.datagrid.AddNewRowPosition = addNewRowPosition;
                }
            }
        }
    }
}

