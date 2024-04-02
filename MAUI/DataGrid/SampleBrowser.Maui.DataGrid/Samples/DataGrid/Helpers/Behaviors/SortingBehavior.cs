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
        private Syncfusion.Maui.Buttons.SfSwitch? sortingSwitch;
        private Syncfusion.Maui.Buttons.SfSwitch? triSortingSwitch;
        private Syncfusion.Maui.Buttons.SfSwitch? multiSortingSwitch;
        private Syncfusion.Maui.Buttons.SfSwitch? columnSortingSwitch;
        private Syncfusion.Maui.Buttons.SfSwitch? sortNumbersSwitch;

        /// <summary>
        /// You can override this method to subscribe to AssociatedObject events and initialize properties.
        /// </summary>
        /// <param name="bindAble">SampleView type of para named as bindAble</param>
        protected override void OnAttachedTo(SampleView bindAble)
        {
            this.dataGrid = bindAble.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
            this.sortingSwitch = bindAble.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("sorting");
            this.triSortingSwitch = bindAble.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("triSorting");
            this.multiSortingSwitch = bindAble.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("multiSorting");
            this.columnSortingSwitch = bindAble.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("columnSorting");
            this.sortNumbersSwitch = bindAble.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("showSortNumbers");

            this.sortingSwitch.StateChanged += SortingSwitch_StateChanged;
            this.triSortingSwitch.StateChanged += TriSortingSwitch_StateChanged;
            this.multiSortingSwitch.StateChanged += MultiSortingSwitch_StateChanged;
            this.columnSortingSwitch.StateChanged += ColumnSortingSwitch_StateChanged;
            this.sortNumbersSwitch.StateChanged += SortNumbersSwitch_StateChanged;
            base.OnAttachedTo(bindAble);
        }

        private void SortNumbersSwitch_StateChanged(object? sender, Syncfusion.Maui.Buttons.SwitchStateChangedEventArgs e)
        {
            this.dataGrid!.ShowSortNumbers = (bool)e.NewValue!;
        }

        private void ColumnSortingSwitch_StateChanged(object? sender, Syncfusion.Maui.Buttons.SwitchStateChangedEventArgs e)
        {
            this.dataGrid!.Columns["ShipCity"]!.AllowSorting = (bool)e.NewValue!;
        }

        private void MultiSortingSwitch_StateChanged(object? sender, Syncfusion.Maui.Buttons.SwitchStateChangedEventArgs e)
        {
            if ((bool)e.NewValue!)
            {
                this.dataGrid!.SortingMode = Syncfusion.Maui.DataGrid.DataGridSortingMode.Multiple;
            }
            else
            {
                this.dataGrid!.SortingMode = Syncfusion.Maui.DataGrid.DataGridSortingMode.Single;
            }
        }

        private void TriSortingSwitch_StateChanged(object? sender, Syncfusion.Maui.Buttons.SwitchStateChangedEventArgs e)
        {
            this.dataGrid!.AllowTriStateSorting = (bool)e.NewValue!;
        }

        private void SortingSwitch_StateChanged(object? sender, Syncfusion.Maui.Buttons.SwitchStateChangedEventArgs e)
        {
            if ((bool)e.NewValue!)
            {
                this.dataGrid!.SortingMode = Syncfusion.Maui.DataGrid.DataGridSortingMode.Single;
            }
            else
            {
                this.dataGrid!.SortingMode = Syncfusion.Maui.DataGrid.DataGridSortingMode.None;
            }
        }

        /// <summary>
        /// You can override this method while View was detached from window
        /// </summary>
        /// <param name="bindAble">SampleView type of bindAble parameter</param>
        protected override void OnDetachingFrom(SampleView bindAble)
        {
            this.sortingSwitch!.StateChanged -= SortingSwitch_StateChanged;
            this.triSortingSwitch!.StateChanged -= TriSortingSwitch_StateChanged;
            this.multiSortingSwitch!.StateChanged -= MultiSortingSwitch_StateChanged;
            this.columnSortingSwitch!.StateChanged -= ColumnSortingSwitch_StateChanged;
            this.sortNumbersSwitch!.StateChanged -= SortNumbersSwitch_StateChanged;

            this.dataGrid = null;
            this.sortingSwitch = null;
            this.triSortingSwitch = null;
            this.multiSortingSwitch = null;
            this.columnSortingSwitch = null;

            base.OnDetachingFrom(bindAble);
        }
    }
}
