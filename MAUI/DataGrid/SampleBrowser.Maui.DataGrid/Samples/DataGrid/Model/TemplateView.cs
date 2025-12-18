#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class TemplateView : INotifyPropertyChanged
    {
        #region private variables
        private int productNo;
        private int productID;
        private int orderID;
        private string? customerID;
        private int quantity;
        private double freight;
        private string? dealerName;
        private bool isOnline;
        private int productPrice;
        private double discount;
        private ImageSource? dealerImage;
        private DateTime shippedDate;
        private string? shipCity;
        private string? shipCountry;
        private string? shipPortalCode;
        private int deliveryDelay;
        private string? electonicProduct;
        #endregion

        /// <summary>
        /// Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged"></see> event raised when a property is changed on a component
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #region Public Properties

        /// <summary>
        /// Gets or sets the value of OrderID and notifies user when value gets changed
        /// </summary>
        public int OrderID
        {
            get { return this.orderID; }
            set
            {
                this.orderID = value;
                this.RaisePropertyChanged(nameof(OrderID));
            }
        }

        /// <summary>
        /// Gets or sets the value of CustomerID and notifies user when value gets changed
        /// </summary>
        public string? CustomerID
        {
            get { return this.customerID; }
            set
            {
                this.customerID = value;
                this.RaisePropertyChanged(nameof(CustomerID));
            }
        }

        public string? ElectonicProduct
        {
            get { return this.electonicProduct; }
            set
            {
                this.electonicProduct = value;
                this.RaisePropertyChanged(nameof(ElectonicProduct));
            }
        }

        public double Discount
        {
            get { return this.discount; }
            set
            {
                this.discount = value;
                this.RaisePropertyChanged(nameof(Discount));
            }
        }

        /// <summary>
        /// Gets or sets the value of Quantity and notifies user when value gets changed
        /// </summary>
        public int Quantity
        {
            get { return this.quantity; }
            set
            {
                this.quantity = value;
                this.RaisePropertyChanged(nameof(Quantity));
            }
        }

        /// <summary>
        /// Gets or sets the value of Freight and notifies user when value gets changed
        /// </summary>
        public double Freight
        {
            get { return this.freight; }
            set
            {
                this.freight = value;
                this.RaisePropertyChanged(nameof(Freight));
            }
        }

        /// <summary>
        /// Gets or sets the value of DealerImage and notifies user when value gets changed
        /// </summary>
        public ImageSource? DealerImage
        {
            get { return this.dealerImage; }
            set
            {
                this.dealerImage = value;
                this.RaisePropertyChanged(nameof(DealerImage));
            }
        }

        /// <summary>
        /// Gets or sets the value of ProductID and notifies user when value gets changed
        /// </summary>
        public int ProductID
        {
            get { return this.productID; }
            set
            {
                this.productID = value;
                this.RaisePropertyChanged(nameof(ProductID));
            }
        }

        /// <summary>
        /// Gets or sets the value of DealerName and notifies user when value gets changed
        /// </summary>
        public string? DealerName
        {
            get { return this.dealerName; }
            set
            {
                this.dealerName = value;
                this.RaisePropertyChanged(nameof(DealerName));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether property value IsOnline was true or false and notifies user when value gets changed
        /// </summary>
        public bool IsOnline
        {
            get { return this.isOnline; }
            set
            {
                this.isOnline = value;
                this.RaisePropertyChanged(nameof(IsOnline));
            }
        }

        /// <summary>
        /// Gets or sets the value of ProductPrice and notifies user when value gets changed
        /// </summary>
        public int ProductPrice
        {
            get { return this.productPrice; }
            set
            {
                this.productPrice = value;
                this.RaisePropertyChanged(nameof(ProductPrice));
            }
        }

        /// <summary>
        /// Gets or sets the value of ProductNo and notifies user when value gets changed
        /// </summary>
        public int ProductNo
        {
            get { return this.productNo; }
            set
            {
                this.productNo = value;
                this.RaisePropertyChanged(nameof(ProductNo));
            }
        }

        /// <summary>
        /// Gets or sets the value of ShippedDate and notifies user when value gets changed
        /// </summary>
        public DateTime ShippedDate
        {
            get { return this.shippedDate; }
            set
            {
                this.shippedDate = value;
                this.RaisePropertyChanged(nameof(ShippedDate));
            }
        }

        /// <summary>
        /// Gets or sets the value of ShipCountry and notifies user when value gets changed
        /// </summary>
        public string? ShipCountry
        {
            get { return this.shipCountry; }
            set
            {
                this.shipCountry = value;
                this.RaisePropertyChanged(nameof(ShipCountry));
            }
        }

        /// <summary>
        /// Gets or sets the value of ShipCity and notifies user when value gets changed
        /// </summary>
        public string? ShipCity
        {
            get { return this.shipCity; }
            set
            {
                this.shipCity = value;
                this.RaisePropertyChanged(nameof(ShipCity));
            }
        }

        public string? ShipPortalCode
        {
            get { return this.shipPortalCode; }
            set
            {
                this.shipPortalCode = value;
                this.RaisePropertyChanged(nameof(ShipPortalCode));
            }
        }

        public int DeliveryDelay
        {
            get { return this.deliveryDelay; }
            set
            {
                this.deliveryDelay = value;
                this.RaisePropertyChanged(nameof(DeliveryDelay));
            }
        }

        #endregion

        #region INotifyPropertyChanged implementation

        /// <summary>
        /// Triggers when Items Collections Changed.
        /// </summary>
        /// <param name="name">string type of name</param>
        private void RaisePropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
