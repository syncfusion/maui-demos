#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base.Converters;
using System.Collections.ObjectModel;
using System.Reflection;

namespace SampleBrowser.Maui.DataGrid
{
    /// <summary>
    /// ViewModel for Master-Details view using DealerDetails as the single model type
    /// Follows the pattern of DetailsViewViewModel with a single model type
    /// </summary>
    public class DealerDetailsViewModel
    {
        #region Fields

        private static Random random = new Random();
        private ObservableCollection<Orders>? dealerList;
        private string[] ShipCountry = new string[]
        {
            "Argentina", "Austria", "Belgium", "Brazil", "Canada",
            "Denmark", "Finland", "France", "Germany", "Ireland",
            "Italy", "Mexico", "Norway", "Poland", "Portugal",
            "Spain", "Sweden", "Switzerland", "UK", "USA", "Venezuela"
        };

        private string[] CustomerID = new string[]
        {
            "ALFKI", "FRANS", "MEREP", "FOLKO", "SIMOB", 
            "WARTH", "VAFFE", "FURIB", "SEVES", "LINOD", 
            "RISCU", "PICCO", "BLONP", "WELLI", "FOLIG"
        };


        public string[] CustomersMale = new string[]
        {
            "Adams",
            "Owens",
            "Thomas",
            "Doran",
            "Jefferson",
            "Spencer",
            "Vargas",
            "Grimes",
            "Edwards",
            "Stark",
            "Cruise",
            "Fitz",
            "Chief",
            "Blanc",
            "Stone",
            "Williams",
            "Jobs",
            "Holmes"
        };

        public string[] CustomersFemale = new string[]
        {
            "Crowley",
            "Waddell",
            "Irvine",
            "Keefe",
            "Ellis",
            "Gable",
            "Mendoza",
            "Rooney",
            "Lane",
            "Landry",
            "Perry",
            "Perez",
            "Newberry",
            "Betts",
            "Fitzgerald",
        };

        private Dictionary<string, string[]> ShipCity = new Dictionary<string, string[]>();

        #endregion

        public DealerDetailsViewModel()
        {         
            dealerList= GenerateOrders(100);
        }
        public ObservableCollection<Orders>? DealerList
        {
            get { return dealerList; }
            set { dealerList = value; }
        }

        #region Implementation
        /// <summary>
        /// Generates sample dealers with order details
        /// </summary>
        /// <summary>
        /// Generates parent grid record rows with given count
        /// </summary>
        /// <param name="count">integer type of count parameter</param>
        /// <returns>stored Items Values</returns>
        public ObservableCollection<Orders> GenerateOrders(int count)
        {
            var orders = new ObservableCollection<Orders>();
            var dealerInfo = new DealerInfoViewModel();
            SetShipCity();
            for (int i = 1; i <= count; i++)
            {
                var shipCountry = ShipCountry[random.Next(5)];
                var shipCitycoll = ShipCity[shipCountry];
                var customerID = CustomerID[random.Next(5)];
                var orderID = 1000 + i;
                var order = new Orders
                {
                    OrderID = orderID,
                    CustomerID = customerID,
                    ShippingDate = GenerateRandomDate(),
                    SupplierID = random.Next(1, 10),
                    ShipCity = shipCountry,
                    ShipCountry = shipCitycoll[random.Next(shipCitycoll.Length - 1)],
                    Freight = Math.Round(random.Next(1, 500) + random.NextDouble(), 2),
                    OrdersList = GenerateOrderDetails(orderID)
                };

                orders.Add(order);
            }
            return orders;
        }

        /// <summary>
        /// Generates inner grid record rows
        /// </summary>
        /// <param name="orderId">integer type of OrderID</param>
        /// <returns>stored Items Values</returns>
        private ObservableCollection<OrderDetails> GenerateOrderDetails(int orderId)
        {
            var details = new ObservableCollection<OrderDetails>();
            var dealerInfo = new DealerInfoViewModel();
            int detailCount = random.Next(2, 6);       
            var ResourceAssembly = typeof(SfImageResourceExtension).GetTypeInfo().Assembly;
            for (int j = 0; j < detailCount; j++)
            {
                string imageCondition = (j % 2 == 0) ? dealerInfo.ImageMale[random.Next(7)] : dealerInfo.ImageFemale[random.Next(15)];
                details.Add(new OrderDetails
                {
                    OrderID = orderId,
                    IsOnline = (random.Next(1, 6) > 2) ? true : false,
                    DealerName = (j % 2 == 0) ? this.CustomersMale[random.Next(15)] : CustomersFemale[random.Next(14)],
                    DealerImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images." + imageCondition, ResourceAssembly),
                    OrderDate = OrderedRandomDate(),
                });
            }

            return details;
        }

        /// <summary>
        /// Generates a random shipped date for the records - copied from DetailsViewViewModel
        /// </summary>
        private static DateTime GenerateRandomDate()
        {
            int year = random.Next(2008, 2014);
            int month = random.Next(1, 13);
            int day = random.Next(1, 29);  // Simplified for all months
            return new DateTime(year, month, day);
        }

        /// <summary>
        /// Generates a random ordered date for the records - copied from DetailsViewViewModel
        /// </summary>
        private static DateTime OrderedRandomDate()
        {
            int year = random.Next(2000, 2007);
            int month = random.Next(1, 13);
            int day = random.Next(1, 29);  // Simplified for all months
            return new DateTime(year, month, day);
        }

        /// <summary>
        /// Sets up the dictionary of ship cities for each country - copied from DetailsViewViewModel
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