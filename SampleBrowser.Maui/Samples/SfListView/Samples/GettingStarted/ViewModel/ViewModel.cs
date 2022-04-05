#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.SfListView
{
    public class LinearLayoutViewModel
    {
        #region Fields

        private ObservableCollection<ListViewShoppingCategoryInfo>? categoryInfo;

        #endregion

        #region Constructor

        public LinearLayoutViewModel()
        {
            GenerateSource();
        }

        #endregion

        #region Properties

        public ObservableCollection<ListViewShoppingCategoryInfo>? CategoryInfo
        {
            get { return categoryInfo; }
            set { this.categoryInfo = value; }
        }

        #endregion

        #region Generate Source

        private void GenerateSource()
        {
            ShoppingCategoryInfoRepository categoryinfo = new();
            categoryInfo = categoryinfo.GetCategoryInfo();
        }

        #endregion
    }
}
