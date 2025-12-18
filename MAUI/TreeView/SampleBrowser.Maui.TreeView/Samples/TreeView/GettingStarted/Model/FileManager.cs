#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.TreeView
{
    public class Folder:INotifyPropertyChanged
    {
        private string? folderName;
        private string? imageIcon;
        private ObservableCollection<File>? filesInfo;

        public Folder()
        {

        }

        public string? FolderName
        {
            get
            {
                return folderName;
            }

            set
            {
                folderName = value;
                RaisedOnPropertyChanged("FolderName");
            }
        }

        public string? ImageIcon
        {
            get
            {
                return imageIcon; 
            }

            set
            {
                imageIcon = value;
                RaisedOnPropertyChanged("Icon");
            }
        }

        public ObservableCollection<File>? FilesInfo
        {
            get
            {
                return filesInfo;
            }
            set 
            {
                filesInfo = value;
                RaisedOnPropertyChanged("FilesInfo");

            }
        }

        private void RaisedOnPropertyChanged(string propertyName)
        {
           if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
    public class File : INotifyPropertyChanged
    {
        private string? folderName;
        private string? imageIcon;
        private ObservableCollection<SubFiles>? subFiles;

        public string? FolderName
        {
            get 
            {
                return folderName;
            }

            set
            {
                folderName = value;
                RaiseOnPropertyChanged("FolderName");

            }
        }
        public string? ImageIcon
        {
            get
            {
                return imageIcon;
            }

            set
            {
                imageIcon = value;
                RaiseOnPropertyChanged("Icon");
            }
        }
        public ObservableCollection<SubFiles>? SubFiles
        {
            get 
            {
                return subFiles;
            }

            set
            {
                subFiles = value;
                RaiseOnPropertyChanged("SubFiles");

            }
        }

        private void RaiseOnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public class SubFiles : INotifyPropertyChanged
    {
        private string? folderName;
        private string? imageIcon;
        public SubFiles() 
        {
        }

        public string? FolderName
        {
            get 
            {
                return folderName;
            }

            set 
            {
                folderName = value;
                RaiseOnPropertyChanged("FolderName");

            }
        }
        public string? ImageIcon
        {
            get 
            {
                return imageIcon; 
            }

            set
            {
                imageIcon = value;
                RaiseOnPropertyChanged("Icon");
            }
        }

        private void RaiseOnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged; 
    }
}