using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SampleBrowser.Maui.SfListView
{
    public class ListViewSelectionViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<Musiqnfo> musicInfo;
        private string headerInfo;
        private bool isAllSelected;
        private ObservableCollection<object> selectedItems;
        #endregion

        #region Constructor

        public ListViewSelectionViewModel()
        {
            GenerateSource();
            this.SelectedItems = new ObservableCollection<object>();
            AddSeletedItems();
            headerInfo = "3 item(s) selected";
        }

        private void AddSeletedItems()
        {
            this.MusicInfo[0].IsSelected = true;
            this.SelectedItems.Add(this.MusicInfo[0]);
            this.MusicInfo[1].IsSelected = true;
            this.SelectedItems.Add(this.MusicInfo[1]);
            this.MusicInfo[4].IsSelected = true;
            this.SelectedItems.Add(this.MusicInfo[4]);
        }

        #endregion

        #region Properties

        public ObservableCollection<Musiqnfo> MusicInfo
        {
            get { return musicInfo; }
            set { this.musicInfo = value; }
        }

        public ObservableCollection<object> SelectedItems
        {
            get { return selectedItems; }
            set 
            {
                this.selectedItems = value;
                this.OnPropertyChanged("SelectedItems");
            }
        }

        public bool IsAllSelected
        {
            get { return isAllSelected; }
            set
            {
                isAllSelected = value;
                OnPropertyChanged("IsAllSelected");
            }
        }
        public string HeaderInfo
        {
            get { return headerInfo; }
            set
            {
                if (headerInfo != value)
                {
                    headerInfo = value;
                    OnPropertyChanged("HeaderInfo");
                }
            }
        }

        #endregion

        #region Generate Source

        private void GenerateSource()
        {
            MusiqInfoRepository musiqinfo = new MusiqInfoRepository();
            musicInfo = musiqinfo.GetMusiqInfo();
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
