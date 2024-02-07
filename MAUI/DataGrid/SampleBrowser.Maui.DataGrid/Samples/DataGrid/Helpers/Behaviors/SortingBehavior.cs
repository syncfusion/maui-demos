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
    public class SortingBehavior : Behavior<SampleView>
    {

        private Syncfusion.Maui.DataGrid.SfDataGrid? dataGrid;
        private Switch? sortingSwitch;
        private Switch? triSortingSwitch;
        private Switch? multiSortingSwitch;
        private Switch? columnSortingSwitch;
        private Switch? sortNumbersSwitch;

        /// <summary>
        /// You can override this method to subscribe to AssociatedObject events and initialize properties.
        /// </summary>
        /// <param name="bindAble">SampleView type of para named as bindAble</param>
        protected override void OnAttachedTo(SampleView bindAble)
        {
            this.dataGrid = bindAble.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
            this.sortingSwitch = bindAble.FindByName<Switch>("sorting");
            this.triSortingSwitch = bindAble.FindByName<Switch>("triSorting");
            this.multiSortingSwitch = bindAble.FindByName<Switch>("multiSorting");
            this.columnSortingSwitch = bindAble.FindByName<Switch>("columnSorting");
            this.sortNumbersSwitch = bindAble.FindByName<Switch>("showSortNumbers");

            this.sortingSwitch.Toggled += this.Sorting_Toggled!;
            this.triSortingSwitch.Toggled += this.TriStateSorting_Toggled!;
            this.multiSortingSwitch.Toggled += this.MultiSorting_Toggled!;
            this.columnSortingSwitch.Toggled += this.ColumnSorting_Toggled!;
            this.sortNumbersSwitch.Toggled += SortNumbers_Toggled;
            base.OnAttachedTo(bindAble);
        }

        /// <summary>
        /// You can override this method while View was detached from window
        /// </summary>
        /// <param name="bindAble">SampleView type of bindAble parameter</param>
        protected override void OnDetachingFrom(SampleView bindAble)
        {
            this.sortingSwitch!.Toggled -= this.Sorting_Toggled!;
            this.triSortingSwitch!.Toggled -= this.TriStateSorting_Toggled!;
            this.multiSortingSwitch!.Toggled -= this.MultiSorting_Toggled!;
            this.columnSortingSwitch!.Toggled -= this.ColumnSorting_Toggled!;
            this.sortNumbersSwitch!.Toggled -= SortNumbers_Toggled;

            this.dataGrid = null;
            this.sortingSwitch = null;
            this.triSortingSwitch = null;
            this.multiSortingSwitch = null;
            this.columnSortingSwitch = null;

            base.OnDetachingFrom(bindAble);
        }

        /// <summary>
        /// Triggers while Switch was enabled 
        /// </summary>
        /// <param name="sender">Switch1_Toggled event sender</param>
        /// <param name="e">Switch1_Toggled event args e</param>
        private void Sorting_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                this.dataGrid!.SortingMode = Syncfusion.Maui.DataGrid.DataGridSortingMode.Single;
            }
            else
            {
                this.dataGrid!.SortingMode = Syncfusion.Maui.DataGrid.DataGridSortingMode.None;
            }
        }

        /// <summary>
        /// Triggers while Switch was enabled 
        /// </summary>
        /// <param name="sender">Switch2_Toggled event sender</param>
        /// <param name="e">Switch2_Toggled event args e</param>
        private void TriStateSorting_Toggled(object sender, ToggledEventArgs e)
        {
            this.dataGrid!.AllowTriStateSorting = e.Value;
        }

        /// <summary>
        /// Triggers while Switch was enabled 
        /// </summary>
        /// <param name="sender">Switch3_Toggled event sender</param>
        /// <param name="e">Switch3_Toggled event args e</param>
        private void MultiSorting_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                this.dataGrid!.SortingMode = Syncfusion.Maui.DataGrid.DataGridSortingMode.Multiple;
            }
            else
            {
                this.dataGrid!.SortingMode = Syncfusion.Maui.DataGrid.DataGridSortingMode.Single;
            }
        }

        /// <summary>
        /// Triggers while Switch was enabled 
        /// </summary>
        /// <param name="sender">Switch4_Toggled event sender</param>
        /// <param name="e">Switch4_Toggled event args e</param>
        private void ColumnSorting_Toggled(object sender, ToggledEventArgs e)
        {
            this.dataGrid!.Columns["ShipCity"]!.AllowSorting = e.Value;
        }

        private void SortNumbers_Toggled(object? sender, ToggledEventArgs e)
        {
            this.dataGrid!.ShowSortNumbers = e.Value;
        }
    }
}
