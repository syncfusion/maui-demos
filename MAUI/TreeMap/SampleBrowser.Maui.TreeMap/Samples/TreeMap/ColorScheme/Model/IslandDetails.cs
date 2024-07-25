#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.TreeMap.SfTreeMap
{
    using System.ComponentModel;

    /// <summary>
    /// Represents details about an island.
    /// </summary>
    public class IslandDetails : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The island name.
        /// </summary>
        private string name;

        /// <summary>
        /// The island area
        /// </summary>
        private double area;

        /// <summary>
        /// The island location
        /// </summary>
        private string location;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="IslandDetails"/> class.
        /// </summary>
        public IslandDetails()
        {
            this.name = string.Empty;
            this.location = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the island.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.RaisePropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Gets or sets the area of the island in square kilometers.
        /// </summary>
        public double Area
        {
            get { return this.area; }
            set
            {
                this.area = value;
                this.RaisePropertyChanged(nameof(Area));
            }
        }

        /// <summary>
        /// Gets or sets the location of the island.
        /// </summary>
        public string Location
        {
            get { return this.location; }
            set
            {
                this.location = value;
                this.RaisePropertyChanged(nameof(Location));
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