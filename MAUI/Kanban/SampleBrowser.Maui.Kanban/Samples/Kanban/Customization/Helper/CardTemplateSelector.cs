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
    /// A custom DataTemplateSelector that selects a DataTemplate based on the Category of a MenuItem.
    /// </summary>
    public class CardTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the DataTemplate used for items with the "Menu" category.
        /// </summary>
        public DataTemplate? MenuTemplate { get; set; }

        /// <summary>
        /// Gets or sets the DataTemplate used for items with the "Order" category.
        /// </summary>
        public DataTemplate? OrderTemplate { get; set; }

        /// <summary>
        /// Gets or sets the DataTemplate used for items with the "Ready to Delivery" category.
        /// </summary>
        public DataTemplate? DeliveryTemplate { get; set; }

        /// <summary>
        /// Gets or sets the DataTemplate used for items with the "Ready to Serve" category.
        /// </summary>
        public DataTemplate? ReadyToServeTemplate { get; set; }

        /// <summary>
        /// Selects a DataTemplate based on the Category property of the MenuItem.
        /// Returns the appropriate template or falls back to the base implementation if no match is found.
        /// </summary>
        /// <param name="item">The data object to evaluate for template selection.</param>
        /// <param name="container">The container in which the template will be applied.</param>
        /// <returns>A DataTemplate corresponding to the item's category.</returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is not MenuItem menuItem)
            {
                return new DataTemplate();
            }

            string? category = menuItem.Category?.ToString();
            if (string.IsNullOrEmpty(category))
            {
                return new DataTemplate();
            }

            var dataTemplate = category.Equals("Menu", StringComparison.OrdinalIgnoreCase) ? MenuTemplate :
                   category.Equals("Order for Dining", StringComparison.OrdinalIgnoreCase) || category.Equals("Order for Delivery", StringComparison.OrdinalIgnoreCase) ? OrderTemplate :
                   category.Equals("Ready to Serve", StringComparison.OrdinalIgnoreCase) ? ReadyToServeTemplate : DeliveryTemplate;
            return dataTemplate ?? new DataTemplate();
        }
    }
}