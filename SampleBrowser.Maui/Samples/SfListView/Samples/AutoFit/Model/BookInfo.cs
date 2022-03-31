#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace SampleBrowser.Maui.SfListView
{

    public class ListViewBookInfo : INotifyPropertyChanged
    {
        #region Fields

        private string bookName = string.Empty;
        private string bookDesc = string.Empty;
        private string bookAuthor = string.Empty;
        private ImageSource? _authorImage;

        #endregion

        #region Constructor

        public ListViewBookInfo()
        {

        }

        #endregion

        #region Properties

        public string BookName
        {
            get { return bookName; }
            set
            {
                bookName = value;
                OnPropertyChanged("BookName");
            }
        }

        public string BookDescription
        {
            get { return bookDesc; }
            set
            {
                bookDesc = value;
                OnPropertyChanged("BookDescription");
            }
        }

        public string BookAuthor
        {
            get { return bookAuthor; }
            set
            {
                bookAuthor = value;
                OnPropertyChanged("BookAuthor");
            }
        }

        public ImageSource? AuthorImage
        {
            get { return _authorImage; }
            set
            {
                _authorImage = value;
                OnPropertyChanged("AuthorImage");
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
