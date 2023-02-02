#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISortDirection = Syncfusion.Maui.Data.ISortDirection;

namespace SampleBrowser.Maui.DataGrid
{
    public class CustomSortComparer : IComparer<object>, ISortDirection
    {

        private int namX;

        private int namY;
        //// Get or Set the SortDirection value
        private  ListSortDirection sortDirection;

        /// <summary>
        /// Gets or sets the value of SortDirection
        /// </summary>
        public ListSortDirection SortDirection
        {
            get { return this.sortDirection; }
            set { this.sortDirection = value; }
        }

        /// <summary>
        /// Used to compare the x and y value
        /// </summary>
        /// <param name="x">object type of comparer x value</param>
        /// <param name="y">object type of comparer y value</param>
        /// <returns>sort description</returns>
        public int Compare(object? x, object? y)
        {
            //// For Customers Type data
            //// else if => For Group type Data   

            if (x!.GetType() == typeof(OrderInfo))
            {
                //// Calculating the length of CustomerName if the object type is Customers
                this.namX = ((OrderInfo)x!).FirstName!.Length;
                this.namY = ((OrderInfo)y!).FirstName!.Length;
            }
            else
            {
                this.namX = x.ToString()!.Length;
                this.namY = y!.ToString()!.Length;
            }

            //// Objects are compared and return the SortDirection
            if (this.namX.CompareTo(this.namY) > 0)
            {
                return this.SortDirection == ListSortDirection.Ascending ? 1 : -1;
            }
            else if (this.namX.CompareTo(this.namY) == -1)
            {
                return this.SortDirection == ListSortDirection.Ascending ? -1 : 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
