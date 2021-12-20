using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfListView
{
    public class GridLayoutViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<ListViewGalleryInfo> galleryinfo;
        private string headerInfo;
        private bool deleteIcon;
        private double imageHeightRequest;

        private ObservableCollection<object> _selectedItems = new ObservableCollection<object>();

        #endregion

        #region Constructor

        public GridLayoutViewModel()
        {
            FavoriteImageCommand = new Command(SetFavorite);
            DeleteImageCommand = new Command(DeleteImageTapped_tapped);
            SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
            GenerateSource();
            UpdateHeaderStatus();
        }

        #endregion

        #region Properties
        public ObservableCollection<object> SelectedItems
        {
            get { return _selectedItems; }
            set { _selectedItems = value;
                OnPropertyChanged("SelectedItems");
            }
        }

        public bool ShowDeleteIcon
        {
            get { return deleteIcon; }
            set
            {
                deleteIcon = value;
                OnPropertyChanged("ShowDeleteIcon");
            }
        }

        public Command FavoriteImageCommand
        {
            get;
        }

        public Command DeleteImageCommand
        {
            get;
        }


        public ObservableCollection<ListViewGalleryInfo> Gallerynfo
        {
            get { return galleryinfo; }
            set { this.galleryinfo = value; }
        }

        public string HeaderStatus
        {
            get { return headerInfo; }
            set
            {
                if (headerInfo != value)
                {
                    headerInfo = value;
                    OnPropertyChanged("HeaderStatus");
                }
            }
        }

        public double ImageHeightRequest
        {
            get { return imageHeightRequest; }
            set
            {
                this.imageHeightRequest = value;
                this.OnPropertyChanged(nameof(ImageHeightRequest));
            }
        }

        #endregion

        public void GenerateSource()
        {
            ListViewGalleryInfoRepository bookRepository = new ListViewGalleryInfoRepository();
            galleryinfo = bookRepository.GetGalleryInfo();
        }

        public void UpdateHeaderStatus()
        {
            if (SelectedItems.Count > 0)
            {
                this.HeaderStatus = this.SelectedItems.Count == 1 ? this.SelectedItems.Count + " photo selected" : this.SelectedItems.Count + " photos selected";
                ShowDeleteIcon = true;
            }
            else
            {
                this.HeaderStatus = "Select Photos";
                ShowDeleteIcon = false;
            }
        }
        private void DeleteImageTapped_tapped(object obj)
        {
            var galleryInfo = SelectedItems.ToList();

            foreach (ListViewGalleryInfo item in galleryInfo)
            {
                if (this.Gallerynfo.Contains(item))
                    this.Gallerynfo.Remove(item);
            }
            UpdateHeaderStatus();
        }

        private void SetFavorite(object obj)
        {
            var item = obj as ListViewGalleryInfo;
            item.IsFavorite = !item.IsFavorite;
        }

        private void SelectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            for (int i = 0; e.NewItems != null && i < e.NewItems.Count; i++)
            {
                var item = e.NewItems[i];
                (item as ListViewGalleryInfo).IsSelected = true;
            }
            for (int i = 0; e.OldItems != null && i < e.OldItems.Count; i++)
            {
                var item = e.OldItems[i];
                (item as ListViewGalleryInfo).IsSelected = false;
            }
            UpdateHeaderStatus();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
