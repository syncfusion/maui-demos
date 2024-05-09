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