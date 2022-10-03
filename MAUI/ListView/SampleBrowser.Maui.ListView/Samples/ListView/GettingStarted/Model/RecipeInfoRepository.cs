#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class RecipeInfoRepository
    {
        #region Constructor

        public RecipeInfoRepository()
        {

        }

        #endregion

        #region Properties

        internal ObservableCollection<ListViewRecipeInfo> GetRecipeInfo()
        {
            var recipeInfo = new ObservableCollection<ListViewRecipeInfo>();
            for (int i = 0; i < RecipeNames.Length; i++)
            {
                var info = new ListViewRecipeInfo()
                {
                    RecipeName = RecipeNames[i],
                    RecipeDescription = RecipeDescriptions[i],
                    RecipeImage = RecipeImages[i],
                    RecipeTime = RecipeTime[i],
                };
                recipeInfo.Add(info);
            }
            return recipeInfo;
        }

        #endregion

        #region RecipeInfo

        readonly string[] RecipeNames = new string[]
        {
            "Pasta varieties",
            "Egg varieties",
            "Common pizzas",
            "Pane (bread)",
            "Soups and sauces",
            "Pan cakes",
            "Pesce (fish dishes)",
            "Carne (Meat dishes)",
            "Nut dishes",
            "Cheeses dishes",
            "Desserts and pastry",
            "Caffe (Coffee)",           
        };
        readonly string[] RecipeImages = new string[]
        {
            "pasta.png",
            "egg.png",
            "pizza.png",
            "pane.png",
            "soup.png",
            "pancake.png",
            "pesce.png",
            "carne.png",
            "nuts.png",          
            "cheese.png",
            "pastry.png",           
            "caffe.png",
        };
        readonly string[] RecipeDescriptions = new string[]
        {
            "Order pasta varieties such as white sauce pasta, cheese & more…",
            "Taste egg omelet, rolls, rice, curries, gravy and more…",
            "Order veg’s and non-veg pizzas such as cheese, paneer and more…",
            "Have flavors of garlic, creamy garlic, and mexican garlic.",
            "Eat soups like chicken, mutton, veg, crab, pepper & more…",
            "Pan cakes are made traditionally with fluffy such as banana & more.",
            "Order fresh dishes fish finger, curries, tikka, tawa fish and more.",
            "Eat fresh meat dishes such as grill, lollipop & more…",
            "Healthy eatable dishes including shake, cookies, fried cashew etc.",
            "Delicious cheese dishes such as cheese cake, pasta, and more…",
            "Tasty desserts including platter, chocolate, gulab jamun and more.",
            "Refresh yourself by sipping filter coffee, cold & hot coffee etc.",           
        };

        readonly string[] RecipeTime = new string[]
        {
            "5 min",
            "6 min",
            "3 min",
            "5 min",
            "4 min",
            "8 min",
            "3 min",
            "4 min",
            "6 min",
            "7 min",
            "5 min",
            "6 min",
        };
        #endregion
    }
}
