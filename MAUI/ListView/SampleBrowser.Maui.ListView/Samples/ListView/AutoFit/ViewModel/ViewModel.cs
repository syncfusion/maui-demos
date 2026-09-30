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
