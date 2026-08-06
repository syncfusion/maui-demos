using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.TreeView.SfTreeView
{
    public class LoadOnDemandDemoModel : INotifyPropertyChanged
    {
        public string? itemName;
        public int id;
        public bool hasChildNodes;

        public string? ItemName
        {
            get 
            { 
                return itemName;
            }

            set
            {
                itemName = value;
                OnPropertyChanged("ItemName");
            }
        }

        public int ID
        {
            get 
            { 
                return id;
            }

            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        public bool HasChildNodes
        {
            get 
            { 
                return hasChildNodes;
            }

            set
            {
                hasChildNodes = value;
                OnPropertyChanged("HasChildNodes");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}