#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SampleBrowser.Maui.ListView.SfListView
{
    public class ListViewSwipingViewModel : INotifyPropertyChanged
    {
         #region Fields

        private ObservableCollection<ListViewInboxInfo>? inboxInfo;
        private ObservableCollection<ListViewInboxInfo>? archivedMessages;
        private Command? favoritesImageCommand;
        private Command? undoCommand;
        private Command? deleteImageCommand;
        private bool? isDeleted;
        private ListViewInboxInfo? listViewItem;
        private int listViewItemIndex;
        private Command? archiveCommand;
        private string? popUpText;

        #endregion


        #region Interface Member

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region EventHandler
        public event EventHandler<ResetEventArgs>? ResetSwipeView;

        protected virtual void OnResetSwipe(ResetEventArgs e)
        {
            EventHandler<ResetEventArgs>? handler = ResetSwipeView;
            handler?.Invoke(this, e);
        }
        #endregion

        #region Constructor

        public ListViewSwipingViewModel()
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

        public ObservableCollection<ListViewInboxInfo>? ArchivedMessages
        {
            get { return archivedMessages; }
            set { this.archivedMessages = value; OnPropertyChanged("ArchivedMessages"); }
        }

        public Command? FavoritesImageCommand
        {
            get { return favoritesImageCommand; }
            protected set { favoritesImageCommand = value; }
        }

        public Command? DeleteImageCommand
        {
            get { return deleteImageCommand; }
            protected set { deleteImageCommand = value; }
        }

        public Command? UndoCommand
        {
            get { return undoCommand; }
            protected set { undoCommand = value; }
        }

        public Command? ArchiveCommand
        {
            get { return archiveCommand; }
            protected set { archiveCommand = value; }
        }

        public bool? IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; OnPropertyChanged("IsDeleted"); }
        }

        public string? PopUpText
        {
            get { return popUpText; }
            set { popUpText = value;OnPropertyChanged("PopUpText"); }
        }
            

        #endregion

        #region Generate Source

        private void GenerateSource()
        {
            this.IsDeleted = false;
            ListViewInboxInfoRepository inboxinfo = new ListViewInboxInfoRepository();
            archivedMessages = new ObservableCollection<ListViewInboxInfo>();
            inboxInfo = inboxinfo.GetInboxInfo();
            deleteImageCommand = new Command(Delete);
            undoCommand = new Command(UndoAction);
            favoritesImageCommand = new Command(SetFavorites);
            archiveCommand = new Command(Archive);
        }

        private async void Delete(object item)
        {
            PopUpText = "Deleted";
            listViewItem= (ListViewInboxInfo)item;
            listViewItemIndex = inboxInfo!.IndexOf(listViewItem);   
            inboxInfo!.Remove(listViewItem);
            this.IsDeleted = true;
            await Task.Delay(3000);
            this.IsDeleted = false;                    
        }

        private async void Archive(object item)
        {
            PopUpText = "Archived";
            listViewItem = (ListViewInboxInfo)item;
            listViewItemIndex = inboxInfo!.IndexOf(listViewItem);
            inboxInfo!.Remove(listViewItem);
            archivedMessages!.Add(listViewItem);
            this.IsDeleted = true;
            await Task.Delay(3000);
            this.IsDeleted = false;
        }

        private void UndoAction()
        {
            this.IsDeleted = false;
            if (listViewItem != null)
            {
                inboxInfo!.Insert(listViewItemIndex, listViewItem);
            }
            listViewItemIndex = 0;
            listViewItem = null;
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
            OnResetSwipe(new ResetEventArgs());
        }

        #endregion

        #region ResetEvent
        public class ResetEventArgs : EventArgs
        {

        }
        #endregion
    }
}

