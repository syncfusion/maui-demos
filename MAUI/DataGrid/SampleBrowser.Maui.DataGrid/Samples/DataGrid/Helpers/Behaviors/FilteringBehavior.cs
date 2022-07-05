#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
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
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class FilteringBehavior : Behavior<SampleView>
    {

        private Syncfusion.Maui.DataGrid.SfDataGrid? dataGrid;
        private OrderInfoViewModel? viewModel;
        private Picker? optionsList;
        private Picker? columnsList;
        private SearchBar? filterText;

        /// <summary>
        /// Triggers while columns selection was changed
        /// </summary>
        /// <param name="sender">OnColumnsSelectionChanged event sender</param>
        /// <param name="e">OnColumnsSelectionChanged event args</param>
        public void OnColumnsSelectionChanged(object sender, EventArgs e)
        {
            Picker newPicker = (Picker)sender;
            this.viewModel!.SelectedColumn = GetColumnMappingName((string)newPicker.SelectedItem);
            if (this.viewModel.SelectedColumn == "All Columns")
            {
                this.viewModel.SelectedCondition = "Contains";
                this.optionsList!.IsVisible = false;
                this.OnFilterChanged();
            }
            else
            {
                this.optionsList!.IsVisible = true;
                foreach (var prop in typeof(OrderInfo).GetProperties())
                {
                    if (prop.Name == this.viewModel.SelectedColumn)
                    {
                        if (prop.PropertyType == typeof(string))
                        {
                            this.optionsList.Items.Clear();
                            this.optionsList.Items.Add("Contains");
                            this.optionsList.Items.Add("Equals");
                            this.optionsList.Items.Add("Does Not Equal");
                            if (this.viewModel.SelectedCondition == "Equals")
                            {
                                this.optionsList.SelectedIndex = 1;
                            }
                            else if (this.viewModel.SelectedCondition == "Does Not Equal")
                            {
                                this.optionsList.SelectedIndex = 2;
                            }
                            else
                            {
                                this.optionsList.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            this.optionsList.Items.Clear();
                            this.optionsList.Items.Add("Equals");
                            this.optionsList.Items.Add("Does Not Equal");
                            if (this.viewModel.SelectedCondition == "Equals")
                            {
                                this.optionsList.SelectedIndex = 0;
                            }
                            else
                            {
                                this.optionsList.SelectedIndex = 1;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Triggers while filter options are changed. 
        /// </summary>
        /// <param name="sender">OnFilterOptionsChanged event sender</param>
        /// <param name="e">OnFilterOptionsChanged event args e</param>
        public void OnFilterOptionsChanged(object sender, EventArgs e)
        {
            Picker newPicker = (Picker)sender;
            if (newPicker.SelectedIndex >= 0)
            {
                this.viewModel!.SelectedCondition = GetSelectedCondition(newPicker.Items[newPicker.SelectedIndex]);
                if (this.filterText!.Text != null)
                {
                    this.OnFilterChanged();
                }
            }
        }

        /// <summary>
        /// Triggers while filter text was changed 
        /// </summary>
        /// <param name="sender">OnFilterTextChanged event sender</param>
        /// <param name="e">OnFilterTextChanged event args e</param>
        public void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
            {
                this.viewModel!.FilterText = string.Empty;
            }
            else
            {
                this.viewModel!.FilterText = e.NewTextValue;
            }
        }

        /// <summary>
        /// Refreshes the filter.
        /// </summary>
        public void OnFilterChanged()
        {
            if (this.dataGrid!.View != null)
            {
                this.dataGrid.View.Filter = this.viewModel!.FilerRecords;
                this.dataGrid.View.RefreshFilter();
            }
        }

        /// <summary>
        /// You can override this method to subscribe to AssociatedObject events and initialize properties.
        /// </summary>
        /// <param name="bindAble">SampleView type of bindAble</param>
        protected override void OnAttachedTo(SampleView bindAble)
        {
            this.viewModel = new OrderInfoViewModel();
            this.dataGrid = bindAble.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
            bindAble.BindingContext = this.viewModel;
            this.optionsList = bindAble.FindByName<Picker>("OptionsList");
            this.columnsList = bindAble.FindByName<Picker>("ColumnsList");
            this.filterText = bindAble.FindByName<SearchBar>("filterText");

            this.columnsList.SelectedIndex = 0;
            this.viewModel.Filtertextchanged = this.OnFilterChanged;
            this.filterText.TextChanged += this.OnFilterTextChanged!;
            this.columnsList.SelectedIndexChanged += this.OnColumnsSelectionChanged!;
            this.optionsList.SelectedIndexChanged += this.OnFilterOptionsChanged!;
            base.OnAttachedTo(bindAble);
        }

        /// <summary>
        /// You can override this method while View was detached from window
        /// </summary>
        /// <param name="bindAble">SampleView type of bindAble parameter</param>
        protected override void OnDetachingFrom(SampleView bindAble)
        {
            this.optionsList!.SelectedIndexChanged -= this.OnFilterOptionsChanged!;
            this.columnsList!.SelectedIndexChanged -= this.OnColumnsSelectionChanged!;
            this.filterText!.TextChanged -= this.OnFilterTextChanged!;
            this.dataGrid = null;
            this.optionsList = null;
            this.columnsList = null;
            this.filterText = null;
            base.OnDetachingFrom(bindAble);
        }

        private string GetColumnMappingName(string ColumnName)
        {
            return this.dataGrid!.Columns.FirstOrDefault(column => column.HeaderText == ColumnName)?.MappingName ?? ColumnName;
        }

        private string GetSelectedCondition(string condition)
        {
            if (condition == "Does Not Equal")
                return "NotEquals";
            return condition;
        }
    }
}
