#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Buttons;
using Syncfusion.Maui.DataGrid;

namespace SampleBrowser.Maui.DataGrid
{
    /// <summary>
    /// Behavior for managing checkbox selection in DataGrid
    /// </summary>
    public class CheckBoxSelectorBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.DataGrid.SfDataGrid? dataGrid;
        private OrderInfoViewModel? viewModel;

        // Flag to suppress grid selection when tap originates from selector checkbox
        private bool suppressSelectionFromSelectorTap = false;

        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);

            // Get references to DataGrid and ViewModel
            this.dataGrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("SampleGrid");
            this.viewModel = bindable.BindingContext as OrderInfoViewModel;

            if (this.dataGrid != null)
            {
                this.dataGrid.SelectionChanged += OnDataGridSelectionChanged;
                this.dataGrid.SelectionChanging += OnDataGridSelectionChanging;
            }

            ApplyInitialSelection();
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            if (this.dataGrid != null)
            {
                this.dataGrid.SelectionChanged -= OnDataGridSelectionChanged;
                this.dataGrid.SelectionChanging -= OnDataGridSelectionChanging;
            }

            this.dataGrid = null;
            this.viewModel = null;

            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        /// Handles individual row checkbox state changes
        /// </summary>
        public void OnRowCheckBoxStateChanged(object sender, StateChangedEventArgs e)
        {
            if (viewModel != null && !viewModel.isUpdating && dataGrid != null)
            {
                viewModel.isUpdating = true;

                var checkbox = sender as SfCheckBox;
                var item = checkbox?.BindingContext as OrderInfo;

                if (item != null)
                {
                    // Update grid selection based on checkbox state
                    if (item.IsChecked == true)
                    {
                        if (!dataGrid.SelectedRows.Contains(item))
                        {
                            dataGrid.SelectedRows.Add(item);
                        }
                    }
                    else
                    {
                        if (dataGrid.SelectedRows.Contains(item))
                        {
                            dataGrid.SelectedRows.Remove(item);
                        }
                    }
                }

                // Update header checkbox state based on all checkboxes
                UpdateHeaderCheckBoxState();

                viewModel.isUpdating = false;
            }
        }

        /// <summary>
        /// Notified before a row checkbox state changes; used to suppress grid-driven selection.
        /// </summary>
        public void OnRowCheckBoxStateChanging(object sender, StateChangingEventArgs e)
        {
            // The next SelectionChanging event (triggered by the grid due to the same tap)
            // should be cancelled so that checkbox remains the single source of truth.
            suppressSelectionFromSelectorTap = true;
        }

        /// <summary>
        /// Handles header checkbox state changes (Select All)
        /// </summary>
        public void OnHeaderCheckBoxStateChanged(object sender, StateChangedEventArgs e)
        {
            if (viewModel != null && !viewModel.isUpdating && dataGrid != null)
            {
                viewModel.isUpdating = true;

                // Update all row checkboxes
                foreach (var item in viewModel.OrdersInfo!)
                {
                    item.IsChecked = viewModel.SelectAllChecked;
                }

                // Update grid selection
                if (viewModel.SelectAllChecked == true)
                {
                    dataGrid.SelectAll();
                }
                else if (viewModel.SelectAllChecked == false)
                {
                    dataGrid.SelectedRows.Clear();
                }

                viewModel.isUpdating = false;
            }
        }

        /// <summary>
        /// Handles DataGrid selection changes
        /// </summary>
        private void OnDataGridSelectionChanged(object? sender, Syncfusion.Maui.DataGrid.DataGridSelectionChangedEventArgs e)
        {
            if (viewModel != null && !viewModel.isUpdating)
            {
                viewModel.isUpdating = true;

                // Handle added rows (newly selected)
                if (e.AddedRows != null && e.AddedRows.Count > 0)
                {
                    foreach (var row in e.AddedRows)
                    {
                        var item = row as OrderInfo;
                        if (item != null && item.IsChecked != true)
                        {
                            item.IsChecked = true;
                        }
                    }
                }

                // Handle removed rows (deselected)
                if (e.RemovedRows != null && e.RemovedRows.Count > 0)
                {
                    foreach (var row in e.RemovedRows)
                    {
                        var item = row as OrderInfo;
                        if (item != null && item.IsChecked != false)
                        {
                            item.IsChecked = false;
                        }
                    }
                }

                // Update header checkbox state
                UpdateHeaderCheckBoxState();

                viewModel.isUpdating = false;
            }
        }

        /// <summary>
        /// Cancels grid selection changes that are a side-effect of tapping the selector checkbox.
        /// </summary>
        private void OnDataGridSelectionChanging(object? sender, DataGridSelectionChangingEventArgs e)
        {
            if (suppressSelectionFromSelectorTap)
            {
                e.Cancel = true;
                suppressSelectionFromSelectorTap = false;
            }
        }

        /// <summary>
        /// Updates the header checkbox state based on row checkbox states
        /// </summary>
        private void UpdateHeaderCheckBoxState()
        {
            if (viewModel?.OrdersInfo == null || viewModel.OrdersInfo.Count == 0)
                return;

            int checkedCount = viewModel.OrdersInfo.Count(x => x.IsChecked == true);

            if (checkedCount == viewModel.OrdersInfo.Count)
            {
                // All checked
                viewModel.SelectAllChecked = true;
            }
            else if (checkedCount == 0)
            {
                // None checked
                viewModel.SelectAllChecked = false;
            }
            else
            {
                // Some checked (indeterminate state)
                viewModel.SelectAllChecked = null;
            }
        }

        /// <summary>
        /// Applies initial selection to the rows on load.
        /// </summary>
        private void ApplyInitialSelection()
        {
            if (dataGrid == null || viewModel?.OrdersInfo == null || viewModel.OrdersInfo.Count == 0)
                return;

            if (viewModel.isUpdating)
                return;

            viewModel.isUpdating = true;

            int[] indices = new[] { 1, 3, 5 };

            foreach (var idx in indices)
            {
                if (idx >= 0 && idx < viewModel.OrdersInfo.Count)
                {
                    var item = viewModel.OrdersInfo[idx];
                    if (item != null)
                    {
                        item.IsChecked = true;
                        if (!dataGrid.SelectedRows.Contains(item))
                        {
                            dataGrid.SelectedRows.Add(item);
                        }
                    }
                }
            }

            UpdateHeaderCheckBoxState();

            viewModel.isUpdating = false;
        }
    }
}
