using SampleBrowser.Maui.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SampleBrowser.Maui.DataGrid
{
    internal class CustomValidationBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.DataGrid.SfDataGrid? dataGrid;
        protected override void OnAttachedTo(SampleView bindable)
        {
            this.dataGrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
            this.dataGrid.CellValidating += DataGrid_CellValidating;
            this.dataGrid.RowValidating += DataGrid_RowValidating;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            this.dataGrid!.CellValidating -= DataGrid_CellValidating;
            this.dataGrid!.RowValidating -= DataGrid_RowValidating;
            this.dataGrid = null;
            base.OnDetachingFrom(bindable);
        }
        private void DataGrid_RowValidating(object? sender, Syncfusion.Maui.DataGrid.DataGridRowValidatingEventArgs e)
        {
            var data = e.RowData as Orders;
            decimal columnData = 0;
            decimal compareData = 0;
            double total = data!.Freight + data.Expense;
            decimal.TryParse(total.ToString(), out columnData);
            decimal.TryParse("3000", out compareData);

            if (Convert.ToDouble(columnData) < Convert.ToDouble(compareData))
            {
                e.ErrorMessages!.Add("Expense", "Discount applies only if Expense + Freight ≥ 3000.");
                e.IsValid = false;
            }
        }

        private void DataGrid_CellValidating(object? sender, Syncfusion.Maui.DataGrid.DataGridCellValidatingEventArgs e)
        {
            if (e.Column!.MappingName == "Discount" && Convert.ToDouble(e.NewValue) > 40)
            {
                e.ErrorMessage = "Discount should not exceed 40 percent.";
                e.IsValid = false;
            }
        }
    }
}
