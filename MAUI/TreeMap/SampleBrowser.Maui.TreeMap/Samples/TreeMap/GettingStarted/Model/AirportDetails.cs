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
    /// Represents the details of an airport. This class implements the INotifyPropertyChanged interface, enabling data binding functionality.
    /// </summary>
    public class AirportDetails : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Represents the state name.
        /// </summary>
        private string state;

        /// <summary>
        /// Represents the international airports count in the state.
        /// </summary>
        private int count;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AirportDetails"/> class.
        /// </summary>
        public AirportDetails()
        {
            this.state = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the state for which the international airport count is provided.
        /// </summary>
        public string State
        {
            get { return this.state; }
            set
            {
                this.state = value;
                this.RaisePropertyChanged(nameof(State));
            }
        }

        /// <summary>
        /// Gets or sets the count of international airports in the specified state.
        /// </summary>
        public int Count
        {
            get { return this.count; }
            set
            {
                this.count = value;
                this.RaisePropertyChanged(nameof(Count));
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