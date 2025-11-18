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
    /// Represents a menu item with details such as name, description, category, image, ingredients, and order state.
    /// </summary>
    public class MenuItem
    {
        #region Fields

        /// <summary>
        /// The name of the menu item.
        /// </summary>
        private string itemName = string.Empty;

        /// <summary>
        /// The description of the menu item.
        /// </summary>
        private string description = string.Empty;

        /// <summary>
        /// The category to which the menu item belongs.
        /// </summary>
        private object category = string.Empty;

        /// <summary>
        /// The image path or URL representing the menu item.
        /// </summary>
        private ImageSource image = string.Empty;

        /// <summary>
        /// The list of ingredients used in the menu item.
        /// </summary>
        private List<string> ingredients = new List<string>();

        /// <summary>
        /// The current order state of the menu item.
        /// </summary>
        private object orderState = string.Empty;

        /// <summary>
        /// The order ID of the menu item.
        /// </summary>
        private string orderID = string.Empty;

        /// <summary>
        /// The rating of the menu item.
        /// </summary>
        private float rating = 0f;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the menu item.
        /// </summary>
        public string ItemName
        {
            get { return this.itemName; }
            set { this.itemName = value; }
        }

        /// <summary>
        /// Gets or sets the description of the menu item.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        /// <summary>
        /// Gets or sets the category of the menu item.
        /// </summary>
        public object Category
        {
            get { return this.category; }
            set { this.category = value; }
        }

        /// <summary>
        /// Gets or sets the image source representing the menu item, which can be a file path, URI, or embedded resource.
        /// </summary>
        public ImageSource Image
        {
            get { return this.image; }
            set { this.image = value; }
        }

        /// <summary>
        /// Gets or sets the list of ingredients used in the menu item.
        /// </summary>
        public List<string> Ingredients
        {
            get { return this.ingredients; }
            set { this.ingredients = value; }
        }

        /// <summary>
        /// Gets or sets the current order state of the menu item (e.g., Dining, Delivery).
        /// </summary>
        public object OrderState
        {
            get { return this.orderState; }
            set { this.orderState = value; }
        }

        /// <summary>
        /// Gets or sets the rating of the menu item.
        /// </summary>
        public float Rating
        {
            get { return this.rating; }
            set { this.rating = value; }
        }

        /// <summary>
        /// Gets or sets the order ID of the menu item.
        /// </summary>
        public string OrderID
        {
            get { return this.orderID; }
            set { this.orderID = value; }
        }

        #endregion
    }
}