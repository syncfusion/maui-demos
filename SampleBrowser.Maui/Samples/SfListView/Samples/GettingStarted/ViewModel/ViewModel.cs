#if __ANDROID__ || __IOS__ || __MACCATALYST__
using Microsoft.Maui.Controls;
using Syncfusion.Maui.ListView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfListView
{
    public class LinearLayoutViewModel
    {
    #region Fields

        private ObservableCollection<ListViewShoppingCategoryInfo> categoryInfo;

    #endregion

    #region Constructor

        public LinearLayoutViewModel()
        {
            GenerateSource();
        }

    #endregion

    #region Properties

        public ObservableCollection<ListViewShoppingCategoryInfo> CategoryInfo
        {
            get { return categoryInfo; }
            set { this.categoryInfo = value; }
        }

    #endregion

    #region Generate Source

        private void GenerateSource()
        {
            ShoppingCategoryInfoRepository categoryinfo = new ShoppingCategoryInfoRepository();
            categoryInfo = categoryinfo.GetCategoryInfo();
        }

    #endregion
    }
}
#endif
