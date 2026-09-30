using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SampleBrowser.Maui.LiquidGlass.SfListView
{
    public class ListViewViewModel
    {
        #region Fields

        private ObservableCollection<ListViewRecipeInfo>? recipeInfo;

        #endregion

        #region Constructor

        public ListViewViewModel()
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
