#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.ComponentModel;

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class DocumentInfo : INotifyPropertyChanged
    {
        #region Fields

        private string? documentName;
        private string? documentSize;
        private string? documentImage;
        private bool isSelected;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value that indicates Document Name
        /// </summary>
        public string? DocumentName
        {
            get
            {
                return documentName;
            }
            set
            {
                documentName = value;
                this.RaisePropertyChanged("DocumentName");
            }
        }      

        /// <summary>
        /// Gets or sets a value that indicates Document size. 
        /// </summary>
        public string? DocumentSize
        {
            get
            {
                return documentSize;
            }
            set
            {
                documentSize = value;
                this.RaisePropertyChanged("DocumentSize");
            }
        }

        public string? DocumentImage
        {
            get
            {
                return documentImage;
            }
            set
            {
                documentImage = value;
                this.RaisePropertyChanged("DocumentImage");
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(String name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
