#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class LinearLayoutViewModel
    {
        #region Fields

        private ObservableCollection<ListViewRecipeInfo>? recipeInfo;

        #endregion

        #region Constructor

        public LinearLayoutViewModel()
        {
            GenerateSource();
        }

        #endregion

        #region Properties

        public ObservableCollection<ListViewRecipeInfo>? RecipeInfo
        {
            get { return recipeInfo; }
            set { this.recipeInfo = value; }
        }

        #endregion

        #region Generate Source

        private void GenerateSource()
        {
            RecipeInfoRepository recipeinfo = new();
            recipeInfo = recipeinfo.GetRecipeInfo();
        }

        #endregion
    }
}
