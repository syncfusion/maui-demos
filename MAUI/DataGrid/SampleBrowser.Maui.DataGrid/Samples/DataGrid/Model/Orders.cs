#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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

namespace SampleBrowser.Maui.DataGrid
{
    public class Orders
    {
        #region Fields

        private int orderID;
        private int supplierID;
        private string? customerID;
        private string? shipCity;
        private string? shipCountry;
        private double freight;
        private DateTime shippingDate;
        private ObservableCollection<OrderDetails>? orders;

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
            get
            {
                return this.orderID;
            }

            set
            {
                this.orderID = value;
                this.RaisePropertyChanged("OrderID");
            }
        }

        /// <summary>
        /// Gets or sets the value of CustomerID and notifies user when value gets changed
        /// </summary>
        public string? CustomerID
        {
            get
            {
                return this.customerID;
            }

            set
            {
                this.customerID = value;
                this.RaisePropertyChanged("CustomerID");
            }
        }

        /// <summary>
        /// Gets or sets the value of ShippingDate and notifies user when value gets changed
        /// </summary>
        public DateTime ShippingDate
        {
            get
            {
                return this.shippingDate;
            }

            set
            {
                this.shippingDate = value;
                this.RaisePropertyChanged("ShippingDate");
            }
        }

        /// <summary>
        /// Gets or sets the value of SupplierID and notifies user when value gets changed
        /// </summary>
        public int SupplierID
        {
            get
            {
                return this.supplierID;
            }

            set
            {
                this.supplierID = value;
                this.RaisePropertyChanged("SupplierID");
            }
        }

        /// <summary>
        /// Gets or sets the value of ShipCity and notifies user when value gets changed
        /// </summary>
        public string? ShipCity
        {
            get
            {
                return this.shipCity;
            }

            set
            {
                this.shipCity = value;
                this.RaisePropertyChanged("ShipCity");
            }
        }

        /// <summary>
        /// Gets or sets the value of ShipCountry and notifies user when value gets changed
        /// </summary>
        public string? ShipCountry
        {
            get
            {
                return this.shipCountry;
            }

            set
            {
                this.shipCountry = value;
                this.RaisePropertyChanged("ShipCountry");
            }
        }

        /// <summary>
        /// Gets or sets the value of Freight and notifies user when value gets changed
        /// </summary>
        public double Freight
        {
            get
            {
                return this.freight;
            }

            set
            {
                this.freight = value;
                this.RaisePropertyChanged("Freight");
            }
        }

        /// <summary>
        /// Gets or sets the value of OrdersList and notifies user when value gets changed
        /// </summary>
        public ObservableCollection<OrderDetails>? OrdersList
        {
            get
            {
                return orders;
            }
            set
            {
                this.orders = value;
                this.RaisePropertyChanged("OrdersList");
            }
        }

        #endregion

        #region INotifyPropertyChanged implementation

        /// <summary>
        /// Triggers when Items Collections Changed.
        /// </summary>
        /// <param name="name">string type parameter name</param>
        private void RaisePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }
}
