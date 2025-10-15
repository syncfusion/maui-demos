#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Kanban.SfKanban
{
    using System.Collections.ObjectModel;
    using SampleBrowser.Maui.Base.Converters;
    using System.Reflection;

    /// <summary>
    /// Represents a view model for managing a customizable menu of pizza items, including their details, categories, and order states.
    /// </summary>
    public class CustomizationViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the last order ID used in the menu, which is incremented each time a new order is created.
        /// </summary>
        public int LastOrderID { get; set; } = 16365;

        /// <summary>
        /// Gets or sets the collection of <see cref="MenuItem"/> representing the different pizza items available in the menu.
        /// </summary>
        public ObservableCollection<MenuItem> MenuItems { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomizationViewModel"/> class.
        /// </summary>
        public CustomizationViewModel()
        {
            this.MenuItems = new ObservableCollection<MenuItem>();
            Assembly assembly = typeof(SfImageSourceConverter).GetTypeInfo().Assembly;
            var assemblyName = assembly.GetName().Name + ".Resources.Images";

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Margherita",
                    Image = ImageSource.FromResource($"{assemblyName}.margherita.png", assembly),
                    Category = "Menu",
                    Rating = 4,
                    Description = "The classic. Fresh tomatoes, garlic, olive oil, and basil. For pizza purists and minimalists.",
                    Ingredients = new List<string> { "Cheese" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Double Cheese",
                    Image = ImageSource.FromResource($"{assemblyName}.margherita.png", assembly),
                    Category = "Menu",
                    Rating = 5,
                    Description = "The minimalist classic with a double helping of cheese",
                    Ingredients = new List<string> { "Cheese" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Bucolic Pie",
                    Image = ImageSource.FromResource($"{assemblyName}.bucolicpie.png", assembly),
                    Category = "Menu",
                    Rating = 4.5f,
                    Description = "The pizza you daydream about to escape city life. Onions, peppers, and tomatoes.",
                    Ingredients = new List<string> { "Onions", "Capsicum" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Bumper Crop",
                    Rating = 3.25f,
                    Image = ImageSource.FromResource($"{assemblyName}.bumpercrop.png", assembly),
                    Category = "Menu",
                    Description = "Can’t get enough veggies? Eat this. Carrots, mushrooms, potatoes, and way more",
                    Ingredients = new List<string> { "Tomato", "Mushroom" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Spice of Life",
                    Image = ImageSource.FromResource($"{assemblyName}.spiceoflife.png", assembly),
                    Category = "Menu",
                    Rating = 4.75f,
                    Description = "Thrill-seeking, heat-seeking pizza people only.  It’s hot. Trust us.",
                    Ingredients = new List<string> { "Corn", "Gherkins" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Very Nicoise",
                    Image = ImageSource.FromResource($"{assemblyName}.verynicoise.png", assembly),
                    Category = "Menu",
                    Rating = 3.75f,
                    Description = "Anchovies, Dijon vinaigrette, shallots, red peppers, and potatoes.",
                    Ingredients = new List<string> { "Red pepper", "Capsicum" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Salad Daze",
                    Image = ImageSource.FromResource($"{assemblyName}.saladdaze.png", assembly),
                    Category = "Menu",
                    Rating = 5,
                    Description = "Pretty much salad on a pizza. Broccoli, olives, cherry tomatoes, red onion.",
                    Ingredients = new List<string> { "Onions", "Jalapeno" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Bumper Crop",
                    Image = ImageSource.FromResource($"{assemblyName}.bumpercrop.png", assembly),
                    Category = "Ready to Serve",
                    OrderID = "Order ID - #16362",
                    Description = "Can’t get enough veggies? Eat this. Carrots, mushrooms, potatoes, and way more",
                    Ingredients = new List<string> { "Tomato", "Mushroom" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Spice of Life",
                    Image = ImageSource.FromResource($"{assemblyName}.spiceoflife.png", assembly),
                    Category = "Ready to Serve",
                    OrderID = "Order ID - #16363",
                    Description = "Thrill-seeking, heat-seeking pizza people only.  It’s hot. Trust us.",
                    Ingredients = new List<string> { "Corn", "Gherkins" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Bucolic Pie",
                    Image = ImageSource.FromResource($"{assemblyName}.bucolicpie.png", assembly),
                    Category = "Door Delivery",
                    OrderID = "Order ID - #16361",
                    Description = "The pizza you daydream about to escape city life. Onions, peppers, and tomatoes.",
                    Ingredients = new List<string> { "Onions", "Capsicum" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Very Nicoise",
                    Image = ImageSource.FromResource($"{assemblyName}.verynicoise.png", assembly),
                    Category = "Order for Dining",
                    OrderID = "Order ID - #16364",
                    Description = "Anchovies, Dijon vinaigrette, shallots, red peppers, and potatoes.",
                    Ingredients = new List<string> { "Red pepper", "Capsicum" }
                }
            );

            MenuItems.Add(
                new MenuItem()
                {
                    ItemName = "Double Cheese",
                    Image = ImageSource.FromResource($"{assemblyName}.margherita.png", assembly),
                    Category = "Order for Delivery",
                    OrderID = "Order ID - #16365",
                    Description = "The minimalist classic with a double helping of cheese",
                    Ingredients = new List<string> { "Cheese" }
                }
            );
        }

        #endregion
    }
}