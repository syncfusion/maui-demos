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
    /// <summary>
    ///  Base generic class for generalized user-defined behaviors that can respond to arbitrary conditions and events.
    ///  Type parameters:T: The type of the objects with which this Forms.Behavior`1 can be associated in the Paging samples
    /// </summary>
    public class PullToRefreshBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.DataGrid.SfDataGrid? dataGrid;
        private Syncfusion.Maui.PullToRefresh.SfPullToRefresh? pullToRefresh;
        private Syncfusion.Maui.Inputs.SfComboBox? comboBox;
        private PullToRefreshViewModel? viewModel;

        /// <summary>
        /// You can override this method to subscribe to AssociatedObject events and initialize properties.
        /// </summary>
        /// <param name="bindable">SampleView type of bindAble</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            this.dataGrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
            this.pullToRefresh = bindable.FindByName<Syncfusion.Maui.PullToRefresh.SfPullToRefresh>("pullToRefresh");
            this.viewModel = new PullToRefreshViewModel();
            this.dataGrid.BindingContext = this.viewModel;
            this.dataGrid.ItemsSource = this.viewModel.OrdersInfo;
            this.InitializePullToRefreshRefreshing();
            this.comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");
            this.comboBox.SelectionChanged += this.SelectionPicker_SelectedIndexChanged!;
            base.OnAttachedTo(bindable);
        }

        /// <summary>
        /// You can override this method while View was detached from window
        /// </summary>
        /// <param name="bindAble">A sampleView type of bindAble</param>
        protected override void OnDetachingFrom(SampleView bindAble)
        {
            this.dataGrid = null;
            this.pullToRefresh = null;
            this.comboBox!.SelectionChanged -= this.SelectionPicker_SelectedIndexChanged!;
            this.comboBox = null;
            base.OnDetachingFrom(bindAble);
        }

        /// <summary>
        /// A method used to initialize the LoadMoreCommand of DataGrid
        /// </summary>
        private void InitializePullToRefreshRefreshing()
        {
            this.pullToRefresh!.Refreshing += PullToRefreshBehavior_Refreshing;
        }

        /// <summary>
        /// Triggers while selected Index was changed, used to set a Collection
        /// </summary>
        /// <param name="sender">OnSelectionChanged event sender</param>
        /// <param name="e">EventArgs e</param>
        private void SelectionPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox!.SelectedIndex == 0)
            {
                this.pullToRefresh!.TransitionMode = Syncfusion.Maui.PullToRefresh.PullToRefreshTransitionType.Push;
            }
            else if (this.comboBox.SelectedIndex == 1)
            {
                this.pullToRefresh!.TransitionMode = Syncfusion.Maui.PullToRefresh.PullToRefreshTransitionType.SlideOnTop;
            }
        }

        private async void PullToRefreshBehavior_Refreshing(object? sender, EventArgs e)
        {
            this.pullToRefresh!.IsRefreshing = true;
            await Task.Delay(new TimeSpan(0, 0, 3));
            this.viewModel?.ItemsSourceRefresh();
            this.pullToRefresh!.IsRefreshing = false;
        }
    }
}
