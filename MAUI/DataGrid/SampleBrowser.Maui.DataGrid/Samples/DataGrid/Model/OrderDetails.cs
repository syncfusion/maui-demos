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

namespace SampleBrowser.Maui.DataGrid;

public class OrderDetails : INotifyPropertyChanged
{
    #region private variables


    private int orderID;
    private int productID;
    private double unitPrice;
    private int quantity;   
    private double discount;
    private string? customerID;
    private DateTime orderDate;

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
    /// Gets or sets the value of ProductID and notifies user when value gets changed
    /// </summary>
    public int ProductID
    {
        get
        {
            return this.productID;
        }

        set
        {
            this.productID = value;
            this.RaisePropertyChanged("ProductID");
        }
    }

    /// <summary>
    /// Gets or sets the value of UnitPrice and notifies user when value gets changed
    /// </summary>
    public double UnitPrice
    {
        get
        {
            return this.unitPrice;
        }

        set
        {
            this.unitPrice = value;
            this.RaisePropertyChanged("UnitPrice");
        }
    }

    /// <summary>
    /// Gets or sets the value of Quantity and notifies user when value gets changed
    /// </summary>
    public int Quantity
    {
        get
        {
            return this.quantity;
        }

        set
        {
            this.quantity = value;
            this.RaisePropertyChanged("Quantity");
        }
    }

    /// <summary>
    /// Gets or sets the value of Discount and notifies user when value gets changed
    /// </summary>
    public double Discount
    {
        get
        {
            return this.discount;
        }

        set
        {
            this.discount = value;
            this.RaisePropertyChanged("Discount");
        }
    }

    /// <summary>
    /// Gets or sets the value of CustomerID and notifies user when value gets changed
    /// </summary>
    public string? CustomerID
    {
        get
        {
            return customerID;
        }
        set
        {
            this.customerID = value;
            this.RaisePropertyChanged("CustomerID");
        }
    }

    /// <summary>
    /// Gets or sets the value of OrderDate and notifies user when value gets changed
    /// </summary>
    public DateTime OrderDate
    {
        get
        {
            return this.orderDate;
        }

        set
        {
            this.orderDate = value;
            this.RaisePropertyChanged("OrderDate");
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
        if (this.PropertyChanged != null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }

    #endregion
}
