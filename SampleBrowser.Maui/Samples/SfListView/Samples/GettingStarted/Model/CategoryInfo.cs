#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfListView
{
    public class ListViewShoppingCategoryInfo : INotifyPropertyChanged
    {
        #region Fields

        private string categoryName;
        private string categoryDesc;
        private ImageSource categoryImage;

        #endregion

        #region Constructor

        public ListViewShoppingCategoryInfo()
        {

        }

        #endregion

        #region Properties

        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        public string CategoryDescription
        {
            get { return categoryDesc; }
            set
            {
                categoryDesc = value;
                OnPropertyChanged("CategoryDescription");
            }
        }

        public ImageSource CategoryImage
        {
            get
            {
                return categoryImage;
            }

            set
            {
                categoryImage = value;
                OnPropertyChanged("CategoryImage");
            }
        }

        #endregion

        #region Interface Member

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
