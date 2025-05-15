#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SampleBrowser.Maui.Base.Converters;
using Syncfusion.Maui.Data;

namespace SampleBrowser.Maui.DataGrid
{
    public class DealerInfoViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DealerInfo>? dealerInformation;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<string>? Countries { get; set; }

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

        public string[] Customers = new string[]
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
            "Holmes",
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

        public string[] ImageMale = new string[]
        {
         "people_circle5.png",
         "people_circle8.png",
         "people_circle12.png",
         "people_circle14.png",
         "people_circle18.png",
         "people_circle23.png",
         "people_circle26.png",
         "people_circle27.png"
        };

        public string[] ImageFemale = new string[]
        {
           "people_circle0.png",
           "people_circle1.png",
           "people_circle2.png",
           "people_circle3.png",
           "people_circle4.png",
           "people_circle6.png",
           "people_circle7.png",
           "people_circle9.png",
           "people_circle10.png",
           "people_circle11.png",
           "people_circle13.png",
           "people_circle15.png",
           "people_circle16.png",
           "people_circle17.png",
           "people_circle19.png",
           "people_circle20.png",
           "people_circle21.png",
           "people_circle22.png",
           "people_circle24.png",
           "people_circle25.png",
        };

        #region private variables


        internal Dictionary<string, string[]> ShipCity = new Dictionary<string, string[]>();

        private Random random = new Random();

        private List<DateTime>? shippedDate;

        #endregion

        private int[] productNo = new int[]
        {
            1803,
            1345,
            4523,
            4932,
            9475,
            5243,
            4263,
            2435,
            3527,
            3634,
            2523,
            3652,
            3524,
            6532,
            2123
        };

        private string[] shipCountry = new string[]
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
            "UK",
            "USA",
        };

        #region GetDealerDetails

        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Generates record rows with given count
        /// </summary>
        /// <param name="count">generates row count</param>
        /// <returns>generated row records</returns>
        public ObservableCollection<DealerInfo> GetDealerDetails(int count)
        {
            ObservableCollection<DealerInfo> dealerDetails = new ObservableCollection<DealerInfo>();
            var assembly = Assembly.GetAssembly(this.GetType());
            this.SetShipCity();
            this.shippedDate = this.GetDateBetween(2001, 2016, count);
            var ResourceAssembly = typeof(SfImageResourceExtension).GetTypeInfo().Assembly;
            for (int i = 1; i <= count; i++)
            {
                var shipcountry = this.shipCountry[this.random.Next(5)];
                var shipcitycoll = this.ShipCity[shipcountry];
                string Imagecondition = (i % 2 == 0) ? this.ImageMale[this.random.Next(7)] : this.ImageFemale[this.random.Next(15)];
                var ord = new DealerInfo()
                {
                    ProductID = 1100 + i,
                    ProductNo = this.productNo[this.random.Next(15)],
                    DealerName = (i % 2 == 0) ? this.CustomersMale[this.random.Next(15)] : this.CustomersFemale[this.random.Next(14)],
                    ProductPrice = this.random.Next(2000, 10000),
                    IsOnline = (i % this.random.Next(1, 10) > 2) ? true : false,
                    DealerImage = ImageSource.FromResource("SampleBrowser.Maui.Base.Resources.Images." + Imagecondition, ResourceAssembly),
                    ShippedDate = this.shippedDate[i - 1],
                    ShipCountry = shipcountry,
                    ShipCity = shipcitycoll[this.random.Next(shipcitycoll.Length - 1)],
                };
                dealerDetails.Add(ord);
            }

            return dealerDetails;
        }

        #endregion

        public DealerInfoViewModel()
        {
            this.dealerInformation = this.GetDealerDetails(100);
            this.Countries = this.shipCountry.ToObservableCollection();
        }


        #region ItemsSource

        /// <summary>
        /// Gets or sets the value of DealerInformation
        /// </summary>
        public ObservableCollection<DealerInfo>? DealerInformation
        {
            get { return this.dealerInformation; }
            set { this.dealerInformation = value;
                this.RaisePropertyChanged("DealerInformation");
            }
        }

        /// <summary>
        /// Used to generate DateTime and returns the value
        /// </summary>
        /// <param name="startYear">integer type of parameter startYear</param>
        /// <param name="endYear">integer type of parameter endYear</param>
        /// <param name="count">integer type of parameter count</param>
        /// <returns>returns the generated DateTime</returns>
        private List<DateTime> GetDateBetween(int startYear, int endYear, int count)
        {
            List<DateTime> date = new List<DateTime>();
            Random d = new Random(1);
            Random m = new Random(2);
            Random y = new Random(startYear);
            for (int i = 0; i < count; i++)
            {
                int year = y.Next(startYear, endYear);
                int month = m.Next(3, 13);
                int day = d.Next(1, 31);

                date.Add(new DateTime(year, month, day));
            }

            return date;
        }

        /// <summary>
        /// This method used to store the string items collections Value
        /// </summary>
        private void SetShipCity()
        {
            string[] argentina = new string[]
            {
                "Rosario",
                "Catamarca",
                "Formosa",
                "Salta"
            };

            string[] austria = new string[]
            {
                "Graz",
                "Salzburg",
                "Linz",
                "Wels"
            };

            string[] belgium = new string[]
            {
                "Bruxelles",
                "Charleroi",
                "Namur",
                "Mons"
            };

            string[] brazil = new string[]
            {
                "Campinas",
                "Resende",
                "Recife",
                "Manaus"
            };

            string[] canada = new string[]
            {
                "Alberta",
                "Montral",
                "Tsawassen",
                "Vancouver"
            };

            string[] denmark = new string[]
            {
                "Svendborg",
                "Farum",
                "rhus",
                "Kbenhavn"
            };

            string[] finland = new string[]
            {
                "Helsinki",
                "Helsinki",
                "Espoo",
                "Oulu"
            };

            string[] france = new string[]
            {
                "Lille",
                "Lyon",
                "Marseille",
                "Nantes",
                "Paris",
                "Reims",
                "Strasbourg",
                "Toulouse",
                "Versailles"
            };

            string[] germany = new string[]
            {
                "Aachen",
                "Berlin",
                "Brandenburg",
                "Cunewalde",
                "Frankfurt",
                "Kln",
                "Leipzig",
                "Mannheim",
                "Mnchen",
                "Mnster",
                "Stuttgart"
            };

            string[] ireland = new string[]
            {
                "Cork",
                "Waterford",
                "Bray",
                "Athlone"
            };

            string[] italy = new string[]
            {
                "Bergamo",
                "Reggio",
                "Torino",
                "Genoa"
            };

            string[] mexico = new string[]
            {
                "Mxico D.F.",
                "Puebla",
                "León",
                "Zapopan"
            };

            string[] norway = new string[]
            {
                "Stavern",
                "Hamar",
                "Harstad",
                "Narvik"
            };

            string[] poland = new string[]
            {
                "Warszawa",
                "Gdynia",
                "Rybnik",
                "Legnica"
            };

            string[] portugal = new string[]
            {
                "Lisboa",
                "Albufeira",
                "Elvas",
                "Estremoz"
            };

            string[] spain = new string[]
            {
                "Barcelona",
                "Madrid",
                "Sevilla",
                "Biscay"
            };

            string[] sweden = new string[]
            {
                "Brcke",
                "Pitea",
                "Robertsfors ",
                "Lule"
            };

            string[] switzerland = new string[]
            {
                "Bern",
                "Genve",
                "Charrat",
                "Châtillens"
            };

            string[] uk = new string[]
            {
                "Colchester",
                "Hedge End",
                "London",
                "Bristol"
            };

            string[] usa = new string[]
            {
                "Albuquerque",
                "Anchorage",
                "Boise",
                "Butte",
                "Elgin",
                "Eugene",
                "Kirkland",
                "Lander",
                "Portland",
                "San Francisco",
                "Seattle",
            };

            string[] venezuela = new string[]
            {
                "Barquisimeto",
                "Caracas", "I. de Margarita",
                "San Cristbal",
                "Cantaura"
            };

            this.ShipCity.Add("Argentina", argentina);
            this.ShipCity.Add("Austria", austria);
            this.ShipCity.Add("Belgium", belgium);
            this.ShipCity.Add("Brazil", brazil);
            this.ShipCity.Add("Canada", canada);
            this.ShipCity.Add("Denmark", denmark);
            this.ShipCity.Add("Finland", finland);
            this.ShipCity.Add("France", france);
            this.ShipCity.Add("Germany", germany);
            this.ShipCity.Add("Ireland", ireland);
            this.ShipCity.Add("Italy", italy);
            this.ShipCity.Add("Mexico", mexico);
            this.ShipCity.Add("Norway", norway);
            this.ShipCity.Add("Poland", poland);
            this.ShipCity.Add("Portugal", portugal);
            this.ShipCity.Add("Spain", spain);
            this.ShipCity.Add("Sweden", sweden);
            this.ShipCity.Add("Switzerland", switzerland);
            this.ShipCity.Add("UK", uk);
            this.ShipCity.Add("USA", usa);
            this.ShipCity.Add("Venezuela", venezuela);
        }
        #endregion
    }
}
