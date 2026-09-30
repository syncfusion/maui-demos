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
