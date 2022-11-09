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
    public class ListViewAutoFitContentViewModel
    {
        #region Fields

        private ObservableCollection<ListViewProductReviewInfo>? reviewInfo;

        #endregion

        #region Constructor

        public ListViewAutoFitContentViewModel()
        {
            GenerateSource();
        }

        #endregion

        #region Properties

        public ObservableCollection<ListViewProductReviewInfo>? ReviewInfo
        {
            get { return reviewInfo; }
            set { this.reviewInfo = value; }
        }
        #endregion

        #region Generate Source

        public void GenerateSource()
        {
            ListViewProductReviewInfoRepository reviewInfoRepository = new();
            reviewInfo = reviewInfoRepository.GetReviewInfo();
        }

        #endregion
    }
}
