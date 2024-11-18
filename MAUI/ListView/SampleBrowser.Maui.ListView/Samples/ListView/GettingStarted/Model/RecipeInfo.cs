#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace SampleBrowser.Maui.ListView.SfListView
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
}
