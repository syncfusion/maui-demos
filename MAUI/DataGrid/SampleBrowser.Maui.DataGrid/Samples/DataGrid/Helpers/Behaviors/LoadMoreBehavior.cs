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
        private Syncfusion.Maui.Inputs.SfComboBox? comboBox;
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
            this.comboBox = bindable.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");
            this.comboBox.SelectionChanged += this.SelectionPicker_SelectedIndexChanged;
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
            this.comboBox!.SelectionChanged -= this.SelectionPicker_SelectedIndexChanged;
            this.comboBox = null;
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
        /// Triggers while selected Index was changed, used to set a Collection
        /// </summary>
        /// <param name="sender">OnSelectionChanged event sender</param>
        /// <param name="e">EventArgs e</param>
        private void SelectionPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (this.comboBox!.SelectedIndex)
            {
                case 0:
                    SetLoadMoreOption(Syncfusion.Maui.DataGrid.DataGridLoadMoreOption.Manual);
                    break;
                case 1:
                    SetLoadMoreOption(Syncfusion.Maui.DataGrid.DataGridLoadMoreOption.Auto);
                    break;
            }
        }

        private void SetLoadMoreOption(Syncfusion.Maui.DataGrid.DataGridLoadMoreOption loadMoreOption)
        {
            if (this.dataGrid!.LoadMoreOption != loadMoreOption)
            {
                this.dataGrid!.LoadMoreOption = loadMoreOption;
            }
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
