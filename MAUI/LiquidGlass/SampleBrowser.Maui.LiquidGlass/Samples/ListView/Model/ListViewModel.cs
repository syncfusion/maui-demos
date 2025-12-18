#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace SampleBrowser.Maui.LiquidGlass.SfListView
{
    public class ListViewRecipeInfo : INotifyPropertyChanged
    {
        #region Fields

        private string? recipeName;
        private string? recipeDesc;
        private string? recipeImage;
        private string? recipeTime;

        #endregion

        #region Constructor

        public ListViewRecipeInfo()
        {

        }

        #endregion

        #region Properties

        public string? RecipeName
        {
            get { return recipeName; }
            set
            {
                recipeName = value;
                OnPropertyChanged("RecipeName");
            }
        }

        public string? RecipeDescription
        {
            get { return recipeDesc; }
            set
            {
                recipeDesc = value;
                OnPropertyChanged("RecipeDescription");
            }
        }

        public string? RecipeImage
        {
            get
            {
                return recipeImage;
            }

            set
            {
                recipeImage = value;
                OnPropertyChanged("RecipeImage");
            }
        }

        public string? RecipeTime
        {
            get
            {
                return recipeTime;
            }

            set
            {
                recipeTime = value;
                OnPropertyChanged("RecipeTime");
            }
        }

        #endregion

        #region Interface Member

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }

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
            "Pizzas",
            "Breads",
            "Soups",
            "Pancakes",
            "Thai",
            "Chinese",
            "Indian",
            "Sandwiches",
            "Dessert and pastry",
            "Coffee",
        };
        readonly string[] RecipeImages = new string[]
        {
            "pasta.png",
            "egg.png",
            "pizza.png",
            "bread.png",
            "soup.png",
            "pancakes.png",
            "thairecipe.png",
            "chineserecipe.png",
            "indianrecipe.png",
            "sandwiches.png",
            "pastry.png",
            "caffe.png",
        };
        readonly string[] RecipeDescriptions = new string[]
        {
            "Order pasta varieties such as white sauce, cream cheese, and more…",
            "Taste egg omelets, rolls, rice, curries, gravy, and more…",
            "Order veg and non-veg pizzas such as neapolitan, pepperoni & more…",
            "Order pane toscano, sourdough, brioche, mexican garlic, and more..",
            "Order soups like gumbo, chicken, crab, beef stew, & more…",
            "Order delicious dutch, red velvet, buttermilk pancakes & more.",
            "Order pad thai , tom yum goong, gaeng daeng and more.",
            "Order wonton soup, chow mein, hot pot, & more…",
            "Order naan, paneer tikka, biryani, korma, and more.",
            "Order some tasty french dip, tuna, meatball sandwiches and more.",
            "Tasty desserts including chocolates, cheese cake, and more.",
            "Refresh yourself by sipping latte, vanilla, cold brew, and more…",
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
