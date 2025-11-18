#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class DetailsViewDataViewModel
    {
        #region Fields

        private static Random _random = new Random();

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

        #region Properties

        public DataTable Orders { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the DetailsViewDataViewModel class.
        /// </summary>
        public DetailsViewDataViewModel()
        {
            SetShipCity();
            
            var parentTable = GetParentOrdersTable();
            var childTable = GetOrderDetailsTable();

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(parentTable);
            dataSet.Tables.Add(childTable);

            // Define DataRelation between Parent and Child tables using Order ID
            dataSet.Relations.Add(new DataRelation("Orders_OrderDetails", parentTable.Columns[0], childTable.Columns[0]));

            // Assign parent table as the main data source
            Orders = parentTable;
        }

        /// <summary>
        /// Gets the parent orders table with order information
        /// </summary>
        /// <returns>DataTable with orders data</returns>
        private DataTable GetParentOrdersTable()
        {
            DataTable table = new DataTable("Orders");
            
            // Define columns
            table.Columns.Add("OrderID", typeof(int));
            table.Columns.Add("CustomerID", typeof(string));
            table.Columns.Add("ShippingDate", typeof(DateTime));
            table.Columns.Add("SupplierID", typeof(int));
            table.Columns.Add("ShipCity", typeof(string));
            table.Columns.Add("ShipCountry", typeof(string));
            table.Columns.Add("Freight", typeof(double));
            
            // Generate rows
            for (int i = 1; i <= 100; i++)
            {
                var shipcountry = ShipCountry[_random.Next(5)];
                var shipcitycoll = ShipCity[shipcountry];
                var customerID = CustomerID[_random.Next(5)];
                var orderID = 1000 + i;
                
                table.Rows.Add(
                    orderID,
                    customerID,
                    GenerateRandomDate(),
                    _random.Next(1, 10),
                    shipcountry,
                    shipcitycoll[_random.Next(shipcitycoll.Length - 1)],
                    Math.Round(_random.Next(1, 500) + _random.NextDouble(), 2)
                );
            }
            
            return table;
        }

        /// <summary>
        /// Gets the child order details table with items for each order
        /// </summary>
        /// <returns>DataTable with order details</returns>
        private DataTable GetOrderDetailsTable()
        {
            DataTable table = new DataTable("OrderDetails");
            
            // Define columns
            table.Columns.Add("OrderID", typeof(int));
            table.Columns.Add("ProductID", typeof(int));
            table.Columns.Add("UnitPrice", typeof(double));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("Discount", typeof(double));
            table.Columns.Add("CustomerID", typeof(string));
            table.Columns.Add("OrderDate", typeof(DateTime));
            
            // For each order in the parent table
            for (int i = 1; i <= 100; i++)
            {
                int orderID = 1000 + i;
                string customerID = CustomerID[_random.Next(CustomerID.Length)];
                
                // Generate random number of details for each order
                int detailCount = _random.Next(2, 6);
                
                for (int j = 0; j < detailCount; j++)
                {
                    table.Rows.Add(
                        orderID,
                        _random.Next(1, 50),
                        Math.Round(_random.Next(10, 100) + _random.NextDouble(), 2),
                        _random.Next(1, 20),
                        Math.Round(_random.Next(0, 4) * 0.02, 2),
                        customerID,
                        GenerateRandomDate()
                    );
                }
            }
            
            return table;
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
    }
}