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

    /// <summary>
    /// A custom DataTemplateSelector that selects a DataTemplate based on the Title of a KanbanColumn.
    /// </summary>
    public class KanbanHeaderTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the DataTemplate used for items with the "To Do" title.
        /// </summary>
        public DataTemplate? ToDoTemplate { get; set; }

        /// <summary>
        /// Gets or sets the DataTemplate used for items with the "In Progress" title.
        /// </summary>
        public DataTemplate? InProgressTemplate { get; set; }

        /// <summary>
        /// Gets or sets the DataTemplate used for items with the "Review" title.
        /// </summary>
        public DataTemplate? ReviewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the DataTemplate used for items with the "Done" title.
        /// </summary>
        public DataTemplate? DoneTemplate { get; set; }

        /// <summary>
        /// Selects a DataTemplate based on the Title property of the item.
        /// </summary>
        /// <param name="item">The item info.</param>
        /// <param name="container">The bindable objects.</param>
        /// <returns>The data template.</returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is not KanbanColumn kanbanColumn)
            {
                return new DataTemplate();
            }

            string? title = kanbanColumn.Title?.ToString();
            if (string.IsNullOrEmpty(title))
            {
                return new DataTemplate();
            }

            var dataTemplate = title.Equals("Open", StringComparison.OrdinalIgnoreCase) ? ToDoTemplate :
                  title.Equals("In Progress", StringComparison.OrdinalIgnoreCase) ? InProgressTemplate :
                  title.Equals("Code Review", StringComparison.OrdinalIgnoreCase) ? ReviewTemplate :
                  title.Equals("Done", StringComparison.OrdinalIgnoreCase) ? DoneTemplate : null;
            return dataTemplate ?? new DataTemplate();
        }
    }
}