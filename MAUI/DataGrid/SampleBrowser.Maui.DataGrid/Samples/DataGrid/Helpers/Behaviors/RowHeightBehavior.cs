#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Syncfusion.Maui.DataGrid;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class RowHeightBehavior : Behavior<SampleView>
    {
        Syncfusion.Maui.DataGrid.SfDataGrid? dataGrid;
        protected override void OnAttachedTo(SampleView bindAble)
        {
            this.dataGrid = bindAble.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
            this.dataGrid.QueryRowHeight += DataGrid_QueryRowHeight;
            base.OnAttachedTo(bindAble);
        }

        private void DataGrid_QueryRowHeight(object? sender, DataGridQueryRowHeightEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                e.Height = e.GetIntrinsicRowHeight(e.RowIndex);
                e.Handled = true;
            }
        }

    }
}
