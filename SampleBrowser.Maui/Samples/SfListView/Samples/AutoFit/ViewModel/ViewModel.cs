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
    public class ListViewAutoFitContentViewModel
    {
        #region Fields

        private ObservableCollection<ListViewBookInfo>? bookInfo;

        #endregion

        #region Constructor

        public ListViewAutoFitContentViewModel()
        {
            GenerateSource();
        }

        #endregion

        #region Properties

        public ObservableCollection<ListViewBookInfo>? BookInfo
        {
            get { return bookInfo; }
            set { this.bookInfo = value; }
        }
        #endregion

        #region Generate Source

        public void GenerateSource()
        {
            ListViewBookInfoRepository bookInfoRepository = new();
            bookInfo = bookInfoRepository.GetBookInfo();
        }

        #endregion
    }
}
