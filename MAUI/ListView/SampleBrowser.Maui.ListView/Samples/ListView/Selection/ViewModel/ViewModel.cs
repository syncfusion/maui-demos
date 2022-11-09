#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using System.ComponentModel;

#nullable disable 
namespace SampleBrowser.Maui.ListView.SfListView
{
    public class ListViewSelectionViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<DocumentInfo> documentInfo;
        private string headerInfo;
        private bool? isAllSelected;
        private ObservableCollection<object> selectedItems;
        #endregion

        #region Constructor

        public ListViewSelectionViewModel()
        {
            GenerateSource();
            this.SelectedItems = new ObservableCollection<object>();
            AddSeletedItems();
            headerInfo = "3 Documents Selected";
        }

        private void AddSeletedItems()
        {
            this.DocumentInfo[0].IsSelected = true;
            this.SelectedItems.Add(this.DocumentInfo[0]);
            this.DocumentInfo[1].IsSelected = true;
            this.SelectedItems.Add(this.DocumentInfo[1]);
            this.DocumentInfo[4].IsSelected = true;
            this.SelectedItems.Add(this.DocumentInfo[4]);
        }

        #endregion

        #region Properties

        public ObservableCollection<DocumentInfo> DocumentInfo
        {
            get { return documentInfo; }
            set { this.documentInfo = value; }
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

        public bool? IsAllSelected
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
            DocumentInfoRepository documentinfo = new DocumentInfoRepository();
            documentInfo = documentinfo.GetDocumentInfo();
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
