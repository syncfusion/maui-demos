#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.ParallaxView.SfParallaxView
{
    public class ViewModel : INotifyPropertyChanged
    {
         #region Fields

        private ObservableCollection<ListViewInboxInfo>? inboxInfo;
        private Command? favoritesImageCommand;

        #endregion

        #region Interface Member

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Constructor

        public ViewModel()
        {
            GenerateSource();
        }

        #endregion

        #region Properties

        public ObservableCollection<ListViewInboxInfo>? InboxInfo
        {
            get { return inboxInfo; }
            set { this.inboxInfo = value; OnPropertyChanged("InboxInfo"); }
        }

        public Command? FavoritesImageCommand
        {
            get { return favoritesImageCommand; }
            protected set { favoritesImageCommand = value; }
        }

        #endregion

        #region Generate Source

        private void GenerateSource()
        {
            ListViewInboxInfoRepository inboxinfo = new ListViewInboxInfoRepository();
            inboxInfo = inboxinfo.GetInboxInfo();
            favoritesImageCommand = new Command(SetFavorites);
        }

        private void SetFavorites(object item)
        {
            var listViewItem = item as ListViewInboxInfo;
            if ((bool)listViewItem!.IsFavorite)
            {
                listViewItem.IsFavorite = false;
            }
            else
            {
                listViewItem.IsFavorite = true;
            }
        }

        #endregion
    }
}

