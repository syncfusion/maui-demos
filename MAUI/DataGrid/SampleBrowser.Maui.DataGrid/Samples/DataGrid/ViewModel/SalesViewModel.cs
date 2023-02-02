#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
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
    public class SalesViewModel 
    {
        #region Field

        private readonly List<string>? salesParsonNames = new List<string>()
        {
            "Gary",
            "Maciej",
            "Shelley",
            "Linda",
            "Carla",
            "Carol",
            "Shannon",
            "Jauna",
            "Michael",
            "Terry",
            "John",
            "Gail",
            "Mark",
            "Martha",
            "Julie",
            "Janeth",
            "Twanna"
        };
        private ObservableCollection<SalesInfo>? dailySalesDetails = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the SalesViewModel class.
        /// </summary>
        public SalesViewModel()
        {
        }

        #endregion

        #region ItemsSource

        /// <summary>
        /// Gets the value of DailySalesDetails
        /// </summary>
        public ObservableCollection<SalesInfo> DailySalesDetails
        {
            get
            {
                if (this.dailySalesDetails == null)
                {
                    return this.GetSalesDetailsByDay(5);
                }
                else
                {
                    return this.dailySalesDetails;
                }
            }
        }

        /// <summary>
        /// Generates days with given count
        /// </summary>
        /// <param name="days">generates row count</param>
        /// <returns>SalesByDate collection values</returns>
        public ObservableCollection<SalesInfo> GetSalesDetailsByDay(int days)
        {
            var collection = new ObservableCollection<SalesInfo>();
            var r = new Random();
            for (var i = 0; i < days; i++)
            {
                var dt = DateTime.Now;
                foreach (var person in this.salesParsonNames!)
                {
                    if (r.Next(0, 3) == 0)
                    {
                        continue;
                    }

                    {
                        var s = new SalesInfo
                        {
                            Name = person,
                            QS1 = r.Next(100000, 1000000) * 0.01,
                            QS2 = r.Next(100000, 1000000) * 0.01,
                            QS3 = r.Next(100000, 1000000) * 0.01,
                            QS4 = r.Next(100000, 1000000) * 0.01,
                        };
                        s.Total = s.QS1 + s.QS2 + s.QS3 + s.QS4;
                        s.Date = dt.AddDays(-1 * i);
                        collection.Add(s);
                    }
                }
            }

            return collection;
        }

        #endregion
    }
}
