#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.TreeMap.SfTreeMap
{
    using System.ComponentModel;

    /// <summary>
    /// Represents details of population data for a specific country, implementing the INotifyPropertyChanged interface to facilitate data binding.
    /// </summary>
    public class PopulationDetails : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The name of the country.
        /// </summary>
        private string country;

        /// <summary>
        /// The continent where the country is located.
        /// </summary>
        private string continent;

        /// <summary>
        /// The population of the country.
        /// </summary>
        private double population;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PopulationDetails"/> class.
        /// </summary>
        public PopulationDetails()
        {
            this.country = string.Empty;
            this.continent = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        public string Country
        {
            get { return this.country; }
            set
            {
                this.country = value;
                this.RaisePropertyChanged(nameof(Country));
            }
        }

        /// <summary>
        /// Gets or sets the continent where the country is located.
        /// </summary>
        public string Continent
        {
            get { return this.continent; }
            set
            {
                this.continent = value;
                this.RaisePropertyChanged(nameof(Continent));
            }
        }

        /// <summary>
        /// Gets or sets the population of the country.
        /// </summary>
        public double Population
        {
            get { return this.population; }
            set
            {
                this.population = value;
                this.RaisePropertyChanged(nameof(Population));
            }
        }

        #endregion

        #region Event

        /// <summary>
        /// Occurs when a property value is changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region PropertyChanged

        /// <summary>
        /// Method to raise the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}