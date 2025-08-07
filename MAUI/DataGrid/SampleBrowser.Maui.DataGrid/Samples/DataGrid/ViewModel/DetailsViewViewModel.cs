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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class DetailsViewViewModel
    {
        #region Fields
        private static Random _random = new Random();
        private ObservableCollection<Orders>? orders_list;
        private string[] ShipCountry = new string[]
        {

            "Argentina",
            "Austria",
            "Belgium",
            "Brazil",
            "Canada",
            "Denmark",
            "Finland",
            "France",
            "Germany",
            "Ireland",
            "Italy",
            "Mexico",
            "Norway",
            "Poland",
            "Portugal",
            "Spain",
            "Sweden",
            "Switzerland",
            "UK",
            "USA",
            "Venezuela"
        };
        private string[] CustomerID = new string[]
        {
            "ALFKI",
            "FRANS",
            "MEREP",
            "FOLKO",
            "SIMOB",
            "WARTH",
            "VAFFE",
            "FURIB",
            "SEVES",
            "LINOD",
            "RISCU",
            "PICCO",
            "BLONP",
            "WELLI",
            "FOLIG"
        };
        private Dictionary<string, string[]> ShipCity = new Dictionary<string, string[]>();
        #endregion

        #region ItemsSource

        /// <summary>
        /// Initializes a new instance of the DetailsViewViewModel class.
        /// </summary>
        public DetailsViewViewModel()
        {
            orders_list = this.GenerateOrders(100);
        }

        public ObservableCollection<Orders>? OrderList
        {
            get { return orders_list; }
            set { orders_list = value; }
        }

        /// <summary>
        /// Generates parent grid record rows with given count
        /// </summary>
        /// <param name="count">integer type of count parameter</param>
        /// <returns>stored Items Values</returns>
        private ObservableCollection<Orders> GenerateOrders(int count)
        {
            var orders = new ObservableCollection<Orders>();
            SetShipCity();
            for (int i = 1; i <= count; i++)
            {
                var shipcountry = ShipCountry[_random.Next(5)];
                var shipcitycoll = ShipCity[shipcountry];
                var customerID = CustomerID[_random.Next(5)];
                var orderID = 1000 + i;
                var order = new Orders
                {
                    OrderID = orderID,
                    CustomerID = customerID,
                    ShippingDate = GenerateRandomDate(),
                    SupplierID = _random.Next(1, 10),
                    ShipCity = shipcountry,
                    ShipCountry = shipcitycoll[_random.Next(shipcitycoll.Length - 1)],
                    Freight = Math.Round(_random.Next(1, 500) + _random.NextDouble(), 2),
                    OrdersList = GenerateOrderDetails(orderID, customerID)
                };

                orders.Add(order);
            }
            return orders;
        }

        /// <summary>
        /// Generates inner grid record rows
        /// </summary>
        /// <param name="orderId">integer type of OrderID</param>
        /// <param name="customerID">string type of CustomerID</param>
        /// <returns>stored Items Values</returns>
        private ObservableCollection<OrderDetails> GenerateOrderDetails(int orderId, string customerID)
        {
            var details = new ObservableCollection<OrderDetails>();
            int detailCount = _random.Next(2, 6);

            for (int j = 0; j < detailCount; j++)
            {
                details.Add(new OrderDetails
                {
                    OrderID = orderId,
                    ProductID = _random.Next(1, 50),
                    UnitPrice = Math.Round(_random.Next(10, 100) + _random.NextDouble(), 2),
                    Quantity = _random.Next(1, 20),
                    Discount = Math.Round(_random.Next(0, 4) * 0.02, 2),
                    CustomerID = customerID,
                    OrderDate = GenerateRandomDate()
                });
            }

            return details;
        }

        /// <summary>
        /// Generates Date for the records
        /// </summary>
        /// <returns>generated date</returns>
        private static DateTime GenerateRandomDate()
        {
            int year = _random.Next(2000, 2023);
            int month = _random.Next(1, 13);
            int day = _random.Next(1, 29);  // Simplified for all months
            return new DateTime(year, month, day);
        }

        /// <summary>
        /// Sets the ship city.
        /// </summary>
        private void SetShipCity()
        {
            string[] argentina = new string[] { "Buenos Aires" };

            string[] austria = new string[] { "Graz", "Salzburg" };

            string[] belgium = new string[] { "Bruxelles", "Charleroi" };

            string[] brazil = new string[] { "Campinas", "Resende", "Rio de Janeiro", "São Paulo" };

            string[] canada = new string[] { "Montréal", "Tsawassen", "Vancouver" };

            string[] denmark = new string[] { "Århus", "København" };

            string[] finland = new string[] { "Helsinki", "Oulu" };

            string[] france = new string[] { "Lille", "Lyon", "Marseille", "Nantes", "Paris", "Reims", "Strasbourg", "Toulouse", "Versailles" };

            string[] germany = new string[] { "Aachen", "Berlin", "Brandenburg", "Cunewalde", "Frankfurt a.M.", "Köln", "Leipzig", "Mannheim", "München", "Münster", "Stuttgart" };

            string[] ireland = new string[] { "Cork" };

            string[] italy = new string[] { "Bergamo", "Reggio Emilia", "Torino" };

            string[] mexico = new string[] { "México D.F." };

            string[] norway = new string[] { "Stavern" };

            string[] poland = new string[] { "Warszawa" };

            string[] portugal = new string[] { "Lisboa" };

            string[] spain = new string[] { "Barcelona", "Madrid", "Sevilla" };

            string[] sweden = new string[] { "Bräcke", "Luleå" };

            string[] switzerland = new string[] { "Bern", "Genève" };

            string[] uk = new string[] { "Colchester", "Hedge End", "London" };

            string[] usa = new string[] { "Albuquerque", "Anchorage", "Boise", "Butte", "Elgin", "Eugene", "Kirkland", "Lander", "Portland", "San Francisco", "Seattle", "Walla Walla" };

            string[] venezuela = new string[] { "Barquisimeto", "Caracas", "I. de Margarita", "San Cristóbal" };

            ShipCity.Add("Argentina", argentina);
            ShipCity.Add("Austria", austria);
            ShipCity.Add("Belgium", belgium);
            ShipCity.Add("Brazil", brazil);
            ShipCity.Add("Canada", canada);
            ShipCity.Add("Denmark", denmark);
            ShipCity.Add("Finland", finland);
            ShipCity.Add("France", france);
            ShipCity.Add("Germany", germany);
            ShipCity.Add("Ireland", ireland);
            ShipCity.Add("Italy", italy);
            ShipCity.Add("Mexico", mexico);
            ShipCity.Add("Norway", norway);
            ShipCity.Add("Poland", poland);
            ShipCity.Add("Portugal", portugal);
            ShipCity.Add("Spain", spain);
            ShipCity.Add("Sweden", sweden);
            ShipCity.Add("Switzerland", switzerland);
            ShipCity.Add("UK", uk);
            ShipCity.Add("USA", usa);
            ShipCity.Add("Venezuela", venezuela);
        }

        #endregion
    }
}

