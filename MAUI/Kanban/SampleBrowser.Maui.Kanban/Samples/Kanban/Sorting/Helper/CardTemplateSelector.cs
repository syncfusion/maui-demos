#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Kanban.SfKanban
{
    /// <summary>
    /// A custom DataTemplateSelector that selects a DataTemplate based on the Category of a CardDetails.
    /// </summary>
    public class KanbanCardTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the DataTemplate used for items with the "Open" category.
        /// </summary>
        public DataTemplate? ToDoTemplate { get; set; }

        /// <summary>
        /// Gets or sets the DataTemplate used for items with the "In Progress" category.
        /// </summary>
        public DataTemplate? InProgressTemplate { get; set; }

        /// <summary>
        /// Gets or sets the DataTemplate used for items with the "Code Review" category.
        /// </summary>
        public DataTemplate? ReviewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the DataTemplate used for items with the "Done" category.
        /// </summary>
        public DataTemplate? DoneTemplate { get; set; }

        /// <summary>
        /// Selects a DataTemplate based on the Category property of the item.
        /// </summary>
        /// <param name="item">The item info.</param>
        /// <param name="container">The bindable objects.</param>
        /// <returns>The data template.</returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is not CardDetails cardDetails)
            {
                return new DataTemplate();
            }

            string? category = cardDetails.Category?.ToString();
            if (string.IsNullOrEmpty(category))
            {
                return new DataTemplate();
            }

            var dataTemplate = category.Equals("Open", StringComparison.OrdinalIgnoreCase) ? ToDoTemplate :
                  category.Equals("In Progress", StringComparison.OrdinalIgnoreCase) ? InProgressTemplate :
                  category.Equals("Code Review", StringComparison.OrdinalIgnoreCase) ? ReviewTemplate :
                  category.Equals("Done", StringComparison.OrdinalIgnoreCase) ? DoneTemplate : null;
            return dataTemplate ?? new DataTemplate();
        }
    }
}