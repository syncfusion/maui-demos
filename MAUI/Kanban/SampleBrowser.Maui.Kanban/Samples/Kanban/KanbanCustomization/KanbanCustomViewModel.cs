#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using Syncfusion.Maui.Kanban;

namespace SampleBrowser.Maui.Kanban.SfKanban
{
    internal class KanbanCustomViewModel
    {
        public ObservableCollection<CustomKanbanModel> CustomCards { get; set; }

        public int LastOrderID { get; set; }

        public KanbanCustomViewModel()
        {
            LastOrderID = 16365;

            CustomCards = new ObservableCollection<CustomKanbanModel>();

            CustomCards.Add(
                new CustomKanbanModel()
                {
                    ID = 1,
                    Title = "Margherita",
                    ImageURL = "margherita.png",
                    Category = "Menu",
                    Rating = 4,
                    Description = "The classic. Fresh tomatoes, garlic, olive oil, and basil. For pizza purists and minimalists only.",
                    Tags = new List<string> { "Cheese" }
                }
            );

            CustomCards.Add(
                new CustomKanbanModel()
                {
                    ID = 2,
                    Title = "Double Cheese",
                    ImageURL = "doublecheesemargherita.png",
                    Category = "Menu",
                    Rating = 5,
                    Description = "The minimalist classic with a double helping of cheese",
                    Tags = new List<string> { "Cheese" }
                }
            );

            CustomCards.Add(
                new CustomKanbanModel()
                {
                    ID = 3,
                    Title = "Hawaiian",
                    ImageURL = "bucolicpie.png",
                    Category = "Menu",
                    Rating = 4.5f,
                    Description = "A tropical paradise on your plate! Enjoy the sweet tang of pineapple and the savory crunch of ham, all on a crispy crust.",
                    Tags = new List<string> { "Onions", "Capsicum" }
                }
            );

            CustomCards.Add(
                new CustomKanbanModel()
                {
                    ID = 4,
                    Title = "Pepperoni",
                    Rating = 3.25f,
                    ImageURL = "bumpercrop.png",
                    Category = "Menu",
                    Description = "A classic crowd-pleaser with zesty pepperoni, tangy tomato sauce, and melted mozzarella cheese. The perfect combination for any pizza lover.",
                    Tags = new List<string> { "Tomato", "Mushroom" }
                }
            );

            CustomCards.Add(
                new CustomKanbanModel()
                {
                    ID = 5,
                    Title = "Mediterranean Delight",
                    ImageURL = "spiceoflife.png",
                    Category = "Menu",
                    Rating = 4.75f,
                    Description = "A flavorful combination of sun-dried tomatoes, artichoke hearts, olives, and feta cheese, offering a taste of the Mediterranean.",
                    Tags = new List<string> { "Corn", "Gherkins" }
                }
            );

            CustomCards.Add(
                new CustomKanbanModel()
                {
                    ID = 6,
                    Title = "Veggie Delight",
                    ImageURL = "verynicoise.png",
                    Category = "Menu",
                    Rating = 3.75f,
                    Description = "A colorful medley of fresh vegetables, including spinach, mushrooms, onions, peppers, and tomatoes.",
                    Tags = new List<string> { "Red pepper", "Capsicum" }
                }
            );

            CustomCards.Add(
                new CustomKanbanModel()
                {
                    ID = 7,
                    Title = "Taco Pizza ",
                    ImageURL = "saladdaze.png",
                    Category = "Menu",
                    Rating = 5,
                    Description = "A Mexican-inspired twist with savory taco meat, zesty salsa, and melted cheese.",
                    Tags = new List<string> { "Onions", "Jalapeno" }
                }
            );

            CustomCards.Add(
                new CustomKanbanModel()
                {
                    ID = 4,
                    Title = "Pepperoni",
                    ImageURL = "bumpercrop.png",
                    Category = "Ready to Serve",
                    OrderID = "Order ID - #16362",
                    Description = "A classic crowd-pleaser with zesty pepperoni, tangy tomato sauce, and melted mozzarella cheese. The perfect combination for any pizza lover.",
                    Tags = new List<string> { "Tomato", "Mushroom" }
                }
            );

            CustomCards.Add(
                new CustomKanbanModel()
                {
                    ID = 5,
                    Title = "Mediterranean Delight",
                    ImageURL = "spiceoflife.png",
                    Category = "Ready to Serve",
                    OrderID = "Order ID - #16363",
                    Description = "A flavorful combination of sun-dried tomatoes, artichoke hearts, olives, and feta cheese, offering a taste of the Mediterranean.",
                    Tags = new List<string> { "Corn", "Gherkins" }
                }
            );

            CustomCards.Add(
                new CustomKanbanModel()
                {
                    ID = 3,
                    Title = "Hawaiian",
                    ImageURL = "bucolicpie.png",
                    Category = "Door Delivery",
                    OrderID = "Order ID - #16361",
                    Description = "A tropical paradise on your plate! Enjoy the sweet tang of pineapple and the savory crunch of ham, all on a crispy crust.",
                    Tags = new List<string> { "Onions", "Capsicum" }
                }
            );

            CustomCards.Add(
                new CustomKanbanModel()
                {
                    ID = 6,
                    Title = "Veggie Delight",
                    ImageURL = "verynicoise.png",
                    Category = "Dining",
                    OrderID = "Order ID - #16364",
                    Description = "A colorful medley of fresh vegetables, including spinach, mushrooms, onions, peppers, and tomatoes.",
                    Tags = new List<string> { "Red pepper", "Capsicum" }
                }
            );

            CustomCards.Add(
                new CustomKanbanModel()
                {
                    ID = 2,
                    Title = "Double Cheese",
                    ImageURL = "doublecheesemargherita.png",
                    Category = "Delivery",
                    OrderID = "Order ID - #16365",
                    AnimationDuration = 60000,
                    Description = "The minimalist classic with a double helping of cheese",
                    Tags = new List<string> { "Cheese" }
                }
            );
        }
    }

    public class CustomKanbanModel : KanbanModel
    {
        public float Rating { get; set; }

        public string? OrderID { get; set; }

        public int AnimationDuration { get; set; }

    }
}
