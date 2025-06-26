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
    /// <summary>
    /// A ViewModel for PullToRefresh sample.
    /// </summary>
    public class PullToRefreshViewModel : INotifyPropertyChanged
    {
        #region Field

        private OrderInfoViewModel? order;

        private ObservableCollection<OrderInfo>? ordersInfo;

        #endregion

        /// <summary>
        /// Initializes a new instance of the PullToRefresh class.
        /// </summary>
        public PullToRefreshViewModel()
        {
            this.SetRowstoGenerate(25);
        }

        /// <summary>
        /// Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged"></see> event raised when a property is changed on a component
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #region ItemsSource

        /// <summary>
        /// Gets or sets the value of OrdersInfo and notifies user when value gets changed 
        /// </summary>
        public ObservableCollection<OrderInfo>? OrdersInfo
        {
            get
            {
                return this.ordersInfo;
            }

            set
            {
                this.ordersInfo = value;
                this.RaisePropertyChanged("OrdersInfo");
            }
        }

        #endregion

        #region ItemSource Generator

        /// <summary>
        /// Generates record rows with given count
        /// </summary>
        /// <param name="count">generates row count</param>
        public void SetRowstoGenerate(int count)
        {
            this.order = new OrderInfoViewModel();
            if (this.order.OrdersInfo != null)
            {
                this.order.OrdersInfo.Clear();
            }
            this.ordersInfo = this.order.GetOrderDetails(count);

            //Reverse
            var list = this.ordersInfo.ToList();
            list.Reverse();
            this.ordersInfo.Clear();
            foreach (var item in list)
            {
                this.ordersInfo.Add(item);
            }
        }

        #endregion

        #region PullToRefresh Generator

        /// <summary>
        /// Used to insert more records when Refreshed
        /// </summary>
        internal void ItemsSourceRefresh()
        {
            for (int i = 0; i < 20; i++)
            {
                this.OrdersInfo!.Insert(0,this.order!.RefreshItemsource(this.OrdersInfo!.Count + 10001));
            }
        }

        #endregion

        #region INotifyPropertyChanged implementation  

        /// <summary>
        /// Triggers when Items Collections Changed.
        /// </summary>
        /// <param name="name">string type of parameter name</param>
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
