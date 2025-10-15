#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Kanban.SfKanban
{
	using Syncfusion.Maui.Kanban;
	using SampleBrowser.Maui.Base;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents a customizable Kanban board view that uses a flexible data model to define workflows, card details, and styles etc.
    /// </summary>
    public partial class Customization : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Customization"/> class.
        /// </summary>
        public Customization()
        {
            InitializeComponent();
            List<KanbanWorkflow> workflows =
            [
                new KanbanWorkflow("Menu", new List<object>() { "Order for Dining", "Order for Delivery" }),
				new KanbanWorkflow("Order for Dining", new List<object>() { "Ready to Serve" }),
				new KanbanWorkflow("Order for Delivery", new List<object>() { "Door Delivery" }),
				new KanbanWorkflow("Ready to Serve", new List<object>() { }),
				new KanbanWorkflow("Door Delivery", new List<object>() { }),
			];

			this.kanban.Workflows = workflows;
		}

        /// <summary>
        /// Handles the drag end event of the Kanban board to manage the movement of menu items between columns and categories.
        /// </summary>
        /// <param name="sender">The Kanban control.</param>
        /// <param name="e">The KanbanDragEndEventArgs.</param>
        private void OnHandleDragEnd(object sender, KanbanDragEndEventArgs e)
        {
            if (e.Data is not MenuItem menuItem)
                return;

            var viewModel = this.BindingContext as CustomizationViewModel;

            if (viewModel == null)
                return;

            if (e.TargetColumn != null && e.SourceColumn?.Title.ToString() == "Menu" && e.TargetColumn.Title.ToString() == "Order")
            {
                e.Cancel = true;
                viewModel.LastOrderID += 1;
                viewModel.MenuItems.Add(GetClonedItemModel(menuItem, e.TargetCategory));
            }
        }

        /// <summary>
        /// Creates a new <see cref="MenuItem"/> instance by cloning the properties of the provided data model.
        /// </summary>
        /// <param name="datamodel">The Dragged MenuItem.</param>
        /// <param name="category">The Category in which the card is dropped.</param>
        /// <returns></returns>
        private MenuItem GetClonedItemModel(MenuItem datamodel, object? category)
        {
            MenuItem newModel = new MenuItem();
            var viewModel = this.BindingContext as CustomizationViewModel;

            newModel.Image = datamodel.Image;
            newModel.Category = category ?? datamodel.Category;
            newModel.Description = datamodel.Description;
            newModel.OrderID = datamodel.OrderID;
            newModel.Ingredients = datamodel.Ingredients;
            newModel.ItemName = datamodel.ItemName;
            newModel.OrderID = "Order ID - #" + viewModel?.LastOrderID.ToString();
            return newModel;
        }
    }
}