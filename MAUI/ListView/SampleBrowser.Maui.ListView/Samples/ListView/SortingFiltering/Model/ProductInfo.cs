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
