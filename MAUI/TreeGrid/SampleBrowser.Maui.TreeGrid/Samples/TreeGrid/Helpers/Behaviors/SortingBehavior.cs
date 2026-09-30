using SampleBrowser.Maui.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleBrowser.Maui.TreeGrid
{

    public class SortingBehavior : Behavior<SampleView>
    {

        private Syncfusion.Maui.TreeGrid.SfTreeGrid? treeGrid;
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
            this.treeGrid = bindAble.FindByName<Syncfusion.Maui.TreeGrid.SfTreeGrid>("treeGrid");
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
            if(treeGrid != null && e.NewValue != null)
                this.treeGrid.ShowSortNumbers = (bool)e.NewValue;
        }

        private void ColumnSortingSwitch_StateChanged(object? sender, Syncfusion.Maui.Buttons.SwitchStateChangedEventArgs e)
        {
            if (treeGrid != null && e.NewValue != null)
                this.treeGrid.Columns["City"]!.AllowSorting = (bool)e.NewValue;
        }

        private void MultiSortingSwitch_StateChanged(object? sender, Syncfusion.Maui.Buttons.SwitchStateChangedEventArgs e)
        {
            if (treeGrid == null)
                return;

            if (e.NewValue != null && (bool)e.NewValue)
            {
                this.treeGrid.SortingMode = Syncfusion.Maui.TreeGrid.TreeGridSortingMode.Multiple;
            }
            else
            {
                this.treeGrid.SortingMode = Syncfusion.Maui.TreeGrid.TreeGridSortingMode.Single;
            }
        }

        private void TriSortingSwitch_StateChanged(object? sender, Syncfusion.Maui.Buttons.SwitchStateChangedEventArgs e)
        {
            if (treeGrid != null && e.NewValue != null)
                this.treeGrid.AllowTriStateSorting = (bool)e.NewValue;
        }

        private void SortingSwitch_StateChanged(object? sender, Syncfusion.Maui.Buttons.SwitchStateChangedEventArgs e)
        {
            if (treeGrid == null)
                return;

            if (e.NewValue != null && (bool)e.NewValue)
            {
                this.treeGrid.SortingMode = Syncfusion.Maui.TreeGrid.TreeGridSortingMode.Single;
            }
            else
            {
                this.treeGrid.SortingMode = Syncfusion.Maui.TreeGrid.TreeGridSortingMode.None;
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

            this.treeGrid = null;
            this.sortingSwitch = null;
            this.triSortingSwitch = null;
            this.multiSortingSwitch = null;
            this.columnSortingSwitch = null;

            base.OnDetachingFrom(bindAble);
        }
    }
}
