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
    /// Represents details about olympic medal statistics for a specific sport.
    /// </summary>
    internal class OlympicMedalDetails : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The sport name.
        /// </summary>
        private string sport;

        /// <summary>
        /// The number of gold medals achieved in the sport.
        /// </summary>
        private double goldMedals;

        /// <summary>
        /// The name of the icon associated with the sport.
        /// </summary>
        private string iconName;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OlympicMedalDetails"/> class.
        /// </summary>
        public OlympicMedalDetails()
        {
            this.sport = string.Empty;
            this.iconName = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the sport.
        /// </summary>
        public string Sport
        {
            get { return this.sport; }
            set
            {
                this.sport = value;
                this.RaisePropertyChanged(nameof(Sport));
            }
        }

        /// <summary>
        /// Gets or sets the number of gold medals achieved in the sport.
        /// </summary>
        public double GoldMedals
        {
            get { return this.goldMedals; }
            set
            {
                this.goldMedals = value;
                this.RaisePropertyChanged(nameof(GoldMedals));
            }
        }

        /// <summary>
        /// Gets or sets the name of the icon associated with the sport.
        /// </summary>
        public string IconName
        {
            get { return this.iconName; }
            set
            {
                this.iconName = value;
                this.RaisePropertyChanged(nameof(IconName));
            }
        }

        #endregion

        #region Event

        /// <summary>
        /// Occurs when a property value is changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Property changed

        /// <summary>
        /// Method to raise the property changed event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
