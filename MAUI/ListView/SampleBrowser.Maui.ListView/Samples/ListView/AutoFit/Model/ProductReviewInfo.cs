#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace SampleBrowser.Maui.ListView.SfListView
{

    public class ListViewProductReviewInfo : INotifyPropertyChanged
    {
        #region Fields

        private string authorName = string.Empty;
        private string comments = string.Empty;
        private string _authorImage = string.Empty;
        private int? rating;

        #endregion

        #region Constructor

        public ListViewProductReviewInfo()
        {

        }

        #endregion

        #region Properties

        public string AuthorName
        {
            get { return authorName; }
            set
            {
                authorName = value;
                OnPropertyChanged("AuthorName");
            }
        }

        public string Comments
        {
            get { return comments; }
            set
            {
                comments = value;
                OnPropertyChanged("Comments");
            }
        }
        
        public string AuthorImage
        {
            get { return _authorImage; }
            set
            {
                _authorImage = value;
                OnPropertyChanged("AuthorImage");
            }
        }

        public int? Rating
        {
            get { return rating; }
            set
            {
                rating = value;
                OnPropertyChanged("Rating");
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
