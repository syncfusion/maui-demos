#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.DataGrid
{
    /// <summary>
    /// A ViewModel for LoadMore sample.
    /// </summary>
    public class LoadMoreViewModel : INotifyPropertyChanged
    {
        #region Field

        private OrderInfoViewModel? order;

        private ObservableCollection<OrderInfo>? ordersInfo;

        #endregion

        /// <summary>
        /// Initializes a new instance of the LoadMoreView class.
        /// </summary>
        public LoadMoreViewModel()
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
        }

        #endregion

        #region LoadMore Generator

        /// <summary>
        /// Used to add more records when Load More View was pressed
        /// </summary>
        internal void LoadMoreItems()
        {
            for (int i = 0; i < 20; i++)
            {
                this.OrdersInfo!.Add(this.order!.RefreshItemsource(this.OrdersInfo!.Count + 10001));
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
