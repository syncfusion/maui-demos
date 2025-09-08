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
    public class StylingBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.Inputs.SfComboBox? visibilityComboBox;
        private Syncfusion.Maui.Inputs.SfComboBox? headerVisibilityComboBox;
        private Syncfusion.Maui.DataGrid.SfDataGrid? datagrid;

        protected override void OnAttachedTo(SampleView bindable)
        {
            this.datagrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
            this.visibilityComboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("VisibilityComboBox");
            this.headerVisibilityComboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("HeaderVisibilityComboBox");

            this.visibilityComboBox.SelectionChanged += StylingPicker_SelectedIndexChanged;
            this.headerVisibilityComboBox.SelectionChanged += HeaderVisibilityPicker_SelectedIndexChanged; ;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            this.visibilityComboBox!.SelectionChanged -= StylingPicker_SelectedIndexChanged;
            this.headerVisibilityComboBox!.SelectionChanged -= HeaderVisibilityPicker_SelectedIndexChanged;

            this.datagrid = null;
            this.visibilityComboBox = null;
            this.headerVisibilityComboBox = null;

            base.OnDetachingFrom(bindable);
        }

        private void StylingPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (this.visibilityComboBox!.SelectedIndex)
            {
                case 0:
                    SetGriLinesVisibility(Syncfusion.Maui.DataGrid.GridLinesVisibility.Horizontal);
                    break;
                case 1:
                    SetGriLinesVisibility(Syncfusion.Maui.DataGrid.GridLinesVisibility.Vertical);
                    break;
                case 2:
                    SetGriLinesVisibility(Syncfusion.Maui.DataGrid.GridLinesVisibility.Both);
                    break;
                case 3:
                    SetGriLinesVisibility(Syncfusion.Maui.DataGrid.GridLinesVisibility.None);
                    break;
                default:
                    SetGriLinesVisibility(Syncfusion.Maui.DataGrid.GridLinesVisibility.Horizontal);
                    break;
            }
        }

        private void HeaderVisibilityPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (this.headerVisibilityComboBox!.SelectedIndex)
            {
                case 0:
                    SetGriLinesVisibility(Syncfusion.Maui.DataGrid.GridLinesVisibility.Horizontal, true);
                    break;
                case 1:
                    SetGriLinesVisibility(Syncfusion.Maui.DataGrid.GridLinesVisibility.Vertical, true);
                    break;
                case 2:
                    SetGriLinesVisibility(Syncfusion.Maui.DataGrid.GridLinesVisibility.Both, true);
                    break;
                case 3:
                    SetGriLinesVisibility(Syncfusion.Maui.DataGrid.GridLinesVisibility.None, true);
                    break;
                default:
                    SetGriLinesVisibility(Syncfusion.Maui.DataGrid.GridLinesVisibility.Horizontal, true);
                    break;
            }
        }

        private void SetGriLinesVisibility(Syncfusion.Maui.DataGrid.GridLinesVisibility gridLinesVisibility, bool isHeader = false)
        {
            if (isHeader)
            {
                if (this.datagrid!.HeaderGridLinesVisibility != gridLinesVisibility)
                    this.datagrid!.HeaderGridLinesVisibility = gridLinesVisibility;
            }
            else
            {
                if (this.datagrid!.GridLinesVisibility != gridLinesVisibility)
                    this.datagrid!.GridLinesVisibility = gridLinesVisibility;
            }
        }
    }
}
