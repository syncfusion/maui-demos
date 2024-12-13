#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.ListView.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

#nullable disable
namespace SampleBrowser.Maui.ListView.SfListView
{
    public class ExpandableListViewModel 
    {
        #region Properties
        public ICommand SelectCommand { get; set; }

        public ObservableCollection<ListViewFoodCategory> FoodCategoryInfo { get; set; }      

        public ObservableCollection<ObservableCollection<FoodMenu>> FoodMenu { get; set; }

        public ObservableCollection<FoodMenu> BreakfastMenu { get; set; }
        
        public ObservableCollection<FoodMenu> LunchMenu { get; set; }
        
        public ObservableCollection<FoodMenu> DinnerMenu { get; set; }

        public ObservableCollection<FoodMenu> SnacksMenu { get; set; }

        #endregion

        #region Constructor

        public ExpandableListViewModel()
        {
            SelectCommand = new Command(Selection);
            GenerateFoodMenu();
            GenerateFoodCategory();
        }

        #endregion

        #region GenerateSource

        private void GenerateFoodCategory()
        {      
            
            FoodCategoryInfo = new ObservableCollection<ListViewFoodCategory>();

            for (int i = 0; i < FootCategories.Length; i++)
            {
                var foodCategory = new ListViewFoodCategory();
                foodCategory.FoodCategory = FootCategories[i];
                if (i == 0 || i == 2)
                {
                    foodCategory.IsExpanded = true;
                }
                else
                {
                    foodCategory.IsExpanded = false;
                }
                foodCategory.FoodIcon = FoodIcons[i];
                foodCategory.FoodMenuCollection = FoodMenu[i];             


                FoodCategoryInfo.Add(foodCategory);
                foodCategory = null;
            }
        }

        private void GenerateFoodMenu()
        {
            FoodMenu = new ObservableCollection<ObservableCollection<FoodMenu>>();
            BreakfastMenu = new ObservableCollection<FoodMenu>();
            LunchMenu = new ObservableCollection<FoodMenu>();
            DinnerMenu = new ObservableCollection<FoodMenu>();
            SnacksMenu = new ObservableCollection<FoodMenu>();
           
            for (int i = 0; i<BreakfastMenuList.Length; i++)
            {
                var item = new FoodMenu();
                item.FoodName = BreakfastMenuList[i];
                item.IsSelected = false;
                BreakfastMenu.Add(item);
                item = null;                
            }

            for (int i = 0; i < LunchMenuList.Length; i++)
            {
                var item = new FoodMenu();
                item.FoodName = LunchMenuList[i];
                item.IsSelected = false;
                LunchMenu.Add(item);
                item = null;
            }

            for (int i = 0; i < DinnerMenuList.Length; i++)
            {
                var item = new FoodMenu();
                item.FoodName = DinnerMenuList[i];
                item.IsSelected = false;
                DinnerMenu.Add(item);
                item = null;
            }

            for (int i = 0; i < SnacksMenuList.Length; i++)
            {
                var item = new FoodMenu();
                item.FoodName = SnacksMenuList[i];
                item.IsSelected = false;
                SnacksMenu.Add(item);
                item = null;
            }

            FoodMenu.Add(BreakfastMenu);
            FoodMenu.Add(LunchMenu);
            FoodMenu.Add(SnacksMenu);
            FoodMenu.Add(DinnerMenu);
        }
        #endregion

        private void Selection(object item)
        {
            var food = item as FoodMenu;
            if (food.IsSelected)
            {
                food.IsSelected = false;
            }
            else
            {
                food.IsSelected = true;
            }
        }

        #region Collections 

        private string[] FootCategories = new string[]
        {
            "Breakfast",
            "Lunch",
            "Snacks",
            "Dinner",
        };

        private string[] BreakfastMenuList = new string[]
        {
            "Chicken and waffles",
            "French toast",
            "Home fries",
            "Buttermilk pancakes",
            "Breakfast wrap",
            "Breakfast sandwich",
        };
        private string[] LunchMenuList = new string[]
        {
            "Burger",
            "Cheeseburger",
            "Reuben sandwich",
            "Barbecue ribs",
            "Apple pie",
            "Philly cheesesteak",
        };
        private string[] DinnerMenuList = new string[]
        {
            "Meatloaf",
            "Clam Chowder",
            "Macaroni and cheese",
            "Chicago's deep-dish pizza",
            "Biscuits and Sausage Gravy",
            "Wild Alaskan salmon",
        };
        private string[] SnacksMenuList = new string[]
        {
            "Crackers",
            "Crisps",
            "Cookies",
            "Nuts & Seeds",
            "Popcorn",
            "Pretzels"
        };
        private string[] FoodIcons = new string[]
        {
            "\ue784",
            "\ue781",
            "\ue782",
            "\ue783",
        };

        #endregion
    }
}
