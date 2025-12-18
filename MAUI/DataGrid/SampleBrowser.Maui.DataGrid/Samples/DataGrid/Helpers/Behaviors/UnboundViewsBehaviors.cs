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
using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.DataGrid.Helper;

namespace SampleBrowser.Maui.DataGrid
{
    internal class UnboundViewsBehaviors : Behavior<SampleView>
    {
        private Syncfusion.Maui.DataGrid.SfDataGrid? datagrid;

        protected override void OnAttachedTo(SampleView bindable)
        {
            datagrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid?>("dataGrid");
            datagrid!.SelectionChanged += UnboundViewsBehaviors_SelectionChanged;
            datagrid!.QueryRowHeight += UnboundViewsBehaviors_QueryRowHeight;
            datagrid!.QueryUnboundRow += Datagrid_QueryUnboundRow;
            base.OnAttachedTo(bindable);
        }

        private void UnboundViewsBehaviors_SelectionChanged(object? sender, DataGridSelectionChangedEventArgs e)
        {
            datagrid!.InvalidateUnboundRow(datagrid!.UnboundRows[0]);
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            datagrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid?>("dataGrid");
            datagrid!.SelectionChanged -= UnboundViewsBehaviors_SelectionChanged;
            datagrid!.QueryRowHeight -= UnboundViewsBehaviors_QueryRowHeight;
            datagrid!.QueryUnboundRow -= Datagrid_QueryUnboundRow;

            datagrid = null;
            base.OnDetachingFrom(bindable);
        }

        private void UnboundViewsBehaviors_QueryRowHeight(object? sender, DataGridQueryRowHeightEventArgs e)
        {
            if (datagrid!.IsUnboundRow(e.RowIndex))
            {
                e.Height = 61;
                e.Handled = true;
            }
        }

        private void Datagrid_QueryUnboundRow(object? sender, Syncfusion.Maui.DataGrid.DataGridUnboundRowEventArgs e)
        {
            if (e.UnboundAction == DataGridUnboundActions.QueryData)
            {
                    double sum = 0;
                if (e.RowColumnIndex.ColumnIndex == 0)
                {
                    e.Value = "Sum of selection";
                }
                else if (e.RowColumnIndex.ColumnIndex == 1)
                {
                    if (datagrid!.SelectedRows.Count > 0)
                    {
                        foreach (var selectedRow in datagrid!.SelectedRows)
                        {
                            if (selectedRow is SalesInfo row)
                            {
                                e.Value = sum += row.QS1;
                            }
                        }
                    }
                    else
                        e.Value = 0;
                }
                else if (e.RowColumnIndex.ColumnIndex == 2)
                {
                    if (datagrid!.SelectedRows.Count > 0)
                    {
                        foreach (var selectedRow in datagrid!.SelectedRows)
                        {
                            if (selectedRow is SalesInfo row)
                            {
                                e.Value = sum += row.QS2;
                            }
                        }
                    }
                    else
                        e.Value = 0;
                }
                else if (e.RowColumnIndex.ColumnIndex == 3)
                {
                    if (datagrid!.SelectedRows.Count > 0)
                    {
                        foreach (var selectedRow in datagrid!.SelectedRows)
                        {
                            if (selectedRow is SalesInfo row)
                            {
                                e.Value = sum += row.QS3;
                            }
                        }
                    }
                    else
                        e.Value = 0;
                }
                else if (e.RowColumnIndex.ColumnIndex == 4)
                {
                    if (datagrid!.SelectedRows.Count > 0)
                    {
                        foreach (var selectedRow in datagrid!.SelectedRows)
                        {
                            if (selectedRow is SalesInfo row)
                            {
                                e.Value = sum += (row.QS1 + row.QS2 + row.QS3);
                            }
                        }
                    }
                    else
                        e.Value = 0;
                }

                e.Handled = true;                
            }
        }
    }
}
