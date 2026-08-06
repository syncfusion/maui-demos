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
