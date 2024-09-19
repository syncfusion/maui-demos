#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.DataGrid;

namespace SampleBrowser.Maui.DataGrid
{
    /// <summary>
    ///  Base generic class for generalized user-defined behaviors that can respond to arbitrary conditions and events.
    ///  Type parameters:T: The type of the objects with which this Forms.Behavior`1 can be associated in the Paging samples
    /// </summary>
    public class LoadMoreBehavior : Behavior<SampleView>
    {
        private Syncfusion.Maui.DataGrid.SfDataGrid? dataGrid;
        private LoadMoreViewModel? viewModel;

        /// <summary>
        /// You can override this method to subscribe to AssociatedObject events and initialize properties.
        /// </summary>
        /// <param name="bindable">SampleView type of bindAble</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            this.dataGrid = bindable.FindByName<Syncfusion.Maui.DataGrid.SfDataGrid>("dataGrid");
            this.viewModel = new LoadMoreViewModel();
            this.dataGrid.BindingContext = this.viewModel;
            this.dataGrid.ItemsSource = this.viewModel.OrdersInfo;
            this.InitializeLoadMoreCommand();
            this.dataGrid.LoadMoreView.Children.OfType<Button>().First().FontAttributes = FontAttributes.Bold;
            base.OnAttachedTo(bindable);
        }

        /// <summary>
        /// You can override this method while View was detached from window
        /// </summary>
        /// <param name="bindable">A sampleView type of bindAble</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {       
            this.dataGrid = null;
            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        /// A method used to initialize the LoadMoreCommand of DataGrid
        /// </summary>
        private void InitializeLoadMoreCommand()
        {
            this.dataGrid!.LoadMoreCommand = new Command(this.ExecuteLoadMoreCommand);
        }

        /// <summary>
        /// Executes the LoadMoreCommand to add more records items in View.
        /// </summary>
        private async void ExecuteLoadMoreCommand()
        {
            try
            {
                this.dataGrid!.IsBusy = true;
                await Task.Delay(new TimeSpan(0, 0, 2));
                this.viewModel?.LoadMoreItems();
                this.dataGrid.IsBusy = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
    }
}
