#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.TreeMap.SfTreeMap
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents the view model for the group level sample functionality.
    /// </summary>
    public class DrillDownViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupLevelViewModel"/> class.
        /// </summary>
        public DrillDownViewModel()
        {
            this.DrillDownPopulationDetails = this.GetDrillDownPopulationDetails();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the collection of population details.
        /// </summary>
        public ObservableCollection<DrillDownPopulationDetails> DrillDownPopulationDetails { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Method to get a collection of population details.
        /// </summary>
        /// <returns>The population details collection.</returns>
        private ObservableCollection<DrillDownPopulationDetails> GetDrillDownPopulationDetails()
        {
            return new ObservableCollection<DrillDownPopulationDetails>()
            {

                #region Africa

                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Ethiopia", Population = 107534882 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Tanzania", Population = 59091392 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Kenya", Population = 50950879 },

                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Uganda", Population = 44270563 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Mozambique", Population = 30528673 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Madagascar", Population = 26262810 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Malawi", Population = 19164728 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Zambia", Population = 17609178 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Zimbabwe", Population = 16913261 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Somalia", Population = 15181925 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "South Sudan", Population = 12919053 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Rwanda", Population = 12501156 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Burundi", Population = 11794700 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Eritrea", Population = 5187948 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Mauritius", Population = 1268315 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Djibouti", Population = 971408 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Réunion", Population = 883247 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Comoros", Population = 832347 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Mayotte", Population = 259682 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Eastern Africa", Country = "Seychelles", Population = 95235 },

                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Middle Africa", Country = "Democratic Republic of the Congo", Population = 84004989 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Middle Africa", Country = "Angola", Population = 30774205 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Middle Africa", Country = "Cameroon", Population = 24678234 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Middle Africa", Country = "Chad", Population = 15353184 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Middle Africa", Country = "Congo", Population = 5399895 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Middle Africa", Country = "Central African Republic", Population = 4737423 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Middle Africa", Country = "Gabon", Population = 2067561 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Middle Africa", Country = "Equatorial Guinea", Population = 1313894 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Middle Africa", Country = "Sao Tome and Principe", Population = 208818 },

                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Northern Africa", Country = "Egypt", Population = 99375741 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Northern Africa", Country = "Algeria", Population = 42008054 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Northern Africa", Country = "Sudan", Population = 41511526 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Northern Africa", Country = "Morocco", Population = 36191805 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Northern Africa", Country = "Tunisia", Population = 11659174 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Northern Africa", Country = "Libya", Population = 6470956 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Northern Africa", Country = "Western Sahara", Population = 567421 },

                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Southern Africa", Country = "South Africa", Population = 57398421 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Southern Africa", Country = "Namibia", Population = 2587801 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Southern Africa", Country = "Botswana", Population = 2333201 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Southern Africa", Country = "Lesotho", Population = 2263010 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Southern Africa", Country = "Swaziland", Population = 1391385 },

                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Nigeria", Population = 195875237 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Ghana", Population = 29463643 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Côte d'Ivoire", Population = 24905843 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Niger", Population = 22311375 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Burkina Faso", Population = 19751651 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Mali", Population = 19107706 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Senegal", Population = 16294270 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Guinea", Population = 13052608 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Benin", Population = 11485674 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Togo", Population = 7990926 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Sierra Leone", Population = 7719729 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Liberia", Population = 4853516 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Mauritania", Population = 4540068 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Gambia", Population = 2163765 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Guinea-Bissau", Population = 1907268 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Cabo Verde", Population = 553335 },
                new DrillDownPopulationDetails() { Continent = "Africa", Subregion = "Western Africa", Country = "Saint Helena", Population = 563 },

                #endregion

                #region Asia

                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Central Asia", Country = "Uzbekistan", Population = 36629920 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Central Asia", Country = "Kazakhstan", Population = 18811900 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Central Asia", Country = "Kyrgyzstan", Population = 6719900 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Central Asia", Country = "Turkmenistan", Population = 6187850 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Central Asia", Country = "Tajikistan", Population = 9770891 },

                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Eastern Asia", Country = "China", Population = 1411778724 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Eastern Asia", Country = "Japan", Population = 124936000 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Eastern Asia", Country = "South Korea", Population = 51837627 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Eastern Asia", Country = "North Korea", Population = 25983200 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Eastern Asia", Country = "Mongolia", Population = 3488500 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Eastern Asia", Country = "Taiwan", Population = 23213962 },

                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southeastern Asia", Country = "Indonesia", Population = 277893337 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southeastern Asia", Country = "Philippines", Population = 113740000 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southeastern Asia", Country = "Vietnam", Population = 100880000 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southeastern Asia", Country = "Thailand", Population = 71141700 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southeastern Asia", Country = "Myanmar", Population = 54148400 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southeastern Asia", Country = "Cambodia", Population = 17585400 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southeastern Asia", Country = "Laos", Population = 7283500 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southeastern Asia", Country = "Singapore", Population = 5861780 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southeastern Asia", Country = "Brunei", Population = 483000 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southeastern Asia", Country = "Timor-Leste", Population = 1863000 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southeastern Asia", Country = "Brunei Darussalam", Population = 462721 },

                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southern Asia", Country = "India", Population = 1439533831 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southern Asia", Country = "Pakistan", Population = 243515000 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southern Asia", Country = "Bangladesh", Population = 171752000 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southern Asia", Country = "Afghanistan", Population = 42647492 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southern Asia", Country = "Nepal", Population = 31454200 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southern Asia", Country = "Sri Lanka", Population = 21413200 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southern Asia", Country = "Maldives", Population = 724800 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Southern Asia", Country = "Bhutan", Population = 524289 },

                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Western Asia", Country = "Turkey", Population = 86842500 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Western Asia", Country = "Saudi Arabia", Population = 35556920 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Western Asia", Country = "Israel", Population = 9390000 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Western Asia", Country = "Iran", Population = 87532000 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Western Asia", Country = "United Arab Emirates", Population = 9890000 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Western Asia", Country = "Jordan", Population = 11084000 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Western Asia", Country = "Lebanon", Population = 7000000 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Western Asia", Country = "Oman", Population = 5360000 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Western Asia", Country = "Yemen", Population = 31136000 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Western Asia", Country = "Syria", Population = 22000000 },
                new DrillDownPopulationDetails() { Continent = "Asia", Subregion = "Western Asia", Country = "Iraq", Population = 42400000 },

                #endregion

                #region North America

                new DrillDownPopulationDetails() { Continent = "North America", Subregion = "Central America", Country = "Mexico", Population = 129592000 },
                new DrillDownPopulationDetails() { Continent = "North America", Subregion = "Central America", Country = "Guatemala", Population = 19162300 },
                new DrillDownPopulationDetails() { Continent = "North America", Subregion = "Central America", Country = "Honduras", Population = 10478700 },
                new DrillDownPopulationDetails() { Continent = "North America", Subregion = "Central America", Country = "El Salvador", Population = 6979788 },
                new DrillDownPopulationDetails() { Continent = "North America", Subregion = "Central America", Country = "Nicaragua", Population = 6723613 },
                new DrillDownPopulationDetails() { Continent = "North America", Subregion = "Central America", Country = "Costa Rica", Population = 5409488 },
                new DrillDownPopulationDetails() { Continent = "North America", Subregion = "Central America", Country = "Panama", Population = 4859380 },

                new DrillDownPopulationDetails() { Continent = "North America", Subregion = "Northern America", Country = "United States", Population = 336710000 },
                new DrillDownPopulationDetails() { Continent = "North America", Subregion = "Northern America", Country = "Canada", Population = 39737500 },
                new DrillDownPopulationDetails() { Continent = "North America", Subregion = "Northern America", Country = "Bermuda", Population = 64000 },
                new DrillDownPopulationDetails() { Continent = "North America", Subregion = "Northern America", Country = "Greenland", Population = 56000 },

                #endregion

                #region SouthAmerica

                new DrillDownPopulationDetails() { Continent = "South America", Subregion = "Eastern South America", Country = "Brazil", Population = 217637297 },
                new DrillDownPopulationDetails() { Continent = "South America", Subregion = "Northern South America", Country = "Colombia", Population = 52886363 },
                new DrillDownPopulationDetails() { Continent = "South America", Subregion = "Southern South America", Country = "Argentina", Population = 46057866 },
                new DrillDownPopulationDetails() { Continent = "South America", Subregion = "Western South America", Country = "Peru", Population = 34683444 },
                new DrillDownPopulationDetails() { Continent = "South America", Subregion = "Northern South America", Country = "Venezuela", Population = 29395334 },
                new DrillDownPopulationDetails() { Continent = "South America", Subregion = "Southern South America", Country = "Chile", Population = 19764771 },
                new DrillDownPopulationDetails() { Continent = "South America", Subregion = "Western South America", Country = "Ecuador", Population = 18377367 },
                new DrillDownPopulationDetails() { Continent = "South America", Subregion = "Western South America", Country = "Bolivia", Population = 12567336 },
                new DrillDownPopulationDetails() { Continent = "South America", Subregion = "Eastern South America", Country = "Paraguay", Population = 6947270 },
                new DrillDownPopulationDetails() { Continent = "South America", Subregion = "Southern South America", Country = "Uruguay", Population = 3423316 },
                new DrillDownPopulationDetails() { Continent = "South America", Subregion = "Northern South America", Country = "Guyana", Population = 831087 },
                new DrillDownPopulationDetails() { Continent = "South America", Subregion = "Northern South America", Country = "Suriname", Population = 634431 },
                new DrillDownPopulationDetails() { Continent = "South America", Subregion = "Northern South America", Country = "French Guiana", Population = 319796 },
                new DrillDownPopulationDetails() { Continent = "South America", Subregion = "Southern South America", Country = "Falkland Islands", Population = 3803 },



                #endregion

                #region Europe

                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Eastern Europe", Country = "Poland", Population = 40221726 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Eastern Europe", Country = "Romania", Population = 19618996 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Eastern Europe", Country = "Czech Republic", Population = 10503734 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Eastern Europe", Country = "Hungary", Population = 9994993 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Eastern Europe", Country = "Belarus", Population = 9455037 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Eastern Europe", Country = "Bulgaria", Population = 6813800 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Eastern Europe", Country = "Slovakia", Population = 5449230 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Eastern Europe", Country = "Moldova", Population = 3435931 },

                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Northern Europe", Country = "United Kingdom", Population = 67961439 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Northern Europe", Country = "Sweden", Population = 10673669 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Northern Europe", Country = "Denmark", Population = 5873429 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Northern Europe", Country = "Finland", Population = 5599869 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Northern Europe", Country = "Norway", Population = 5233582 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Northern Europe", Country = "Ireland", Population = 5149139 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Northern Europe", Country = "Lithuania", Population = 2718352 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Northern Europe", Country = "Latvia", Population = 1830212 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Northern Europe", Country = "Estonia", Population = 1322766 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Northern Europe", Country = "Iceland", Population = 396960 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Northern Europe", Country = "Channel Islands", Population = 111803 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Northern Europe", Country = "Isle of Man", Population = 84710 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Northern Europe", Country = "Faroe Islands", Population = 54281 },

                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "Italy", Population = 59397744 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "Spain", Population = 47913373 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "Greece", Population = 10422720 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "Portugal", Population = 100473349 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "Serbia", Population = 6908227 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "Croatia", Population = 3879074 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "Bosnia and Herzegovina", Population = 3210848 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "Albania", Population = 2793592 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "North Macedonia", Population = 1836713 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "Slovenia", Population = 2107180 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "Montenegro", Population = 617683 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "Malta", Population = 520971 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "Andorra", Population = 80088 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "Gibraltar", Population = 34003 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "San Marino", Population = 33642 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Southern Europe", Country = "Holy See", Population = 496 },

                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Western Europe", Country = "Germany", Population = 84552242 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Western Europe", Country = "France", Population = 66548530 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Western Europe", Country = "Netherlands", Population = 18228742 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Western Europe", Country = "Belgium", Population = 11738763 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Western Europe", Country = "Austria", Population = 9120813 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Western Europe", Country = "Switzerland", Population = 8742753 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Western Europe", Country = "Luxembourg", Population = 674768 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Western Europe", Country = "Monaco", Population = 39298 },
                new DrillDownPopulationDetails() { Continent = "Europe", Subregion = "Western Europe", Country = "Liechtenstein", Population = 38585 },

                #endregion

            };

        }

        #endregion
    }
}