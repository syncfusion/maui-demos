#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.ComponentModel;


namespace SampleBrowser.Maui.ListView.SfListView
{
    public class ProductInfo : INotifyPropertyChanged
    {
        #region Fields

        private string? _ProductName;
        private int? _Quantity;    
        private string? productimage;

        #endregion

        #region Properties
        
        public string? ProductName
        {
            get
            {
                return _ProductName;
            }
            set
            {
                _ProductName = value;
                this.RaisePropertyChanged("ProductName");
            }
        }
        public string? ProductImage
        {
            get
            {
                return productimage;
            }
            set
            {
                productimage = value;
                this.RaisePropertyChanged("ProductImage");
            }
        }
      
        public int? Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
                this.RaisePropertyChanged("Quantity");
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
