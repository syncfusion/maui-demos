#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.TreeMap.SfTreeMap
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents the view model for the island functionality.
    /// </summary>
    public class IslandViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="IslandViewModel"/> class.
        /// </summary>
        public IslandViewModel()
        {
            this.IslandDetails = this.GetIslandDetails();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the collection of island details.
        /// </summary>
        public ObservableCollection<IslandDetails> IslandDetails { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Method to get a collection of Island details.
        /// </summary>
        /// <returns>The Island details collection.</returns>
        private ObservableCollection<IslandDetails> GetIslandDetails()
        {
            return new ObservableCollection<IslandDetails>()
            {
                new IslandDetails { Name = "Greenland", Area = 2130800, Location = "North America" },
                new IslandDetails { Name = "New Guinea", Area = 785753, Location = "Oceania" },
                new IslandDetails { Name = "Borneo", Area = 748168, Location = "Asia" },
                new IslandDetails { Name = "Madagascar", Area = 587041, Location = "Africa" },
                new IslandDetails { Name = "Baffin Island", Area = 507451, Location = "North America" },
                new IslandDetails { Name = "Sumatra", Area = 443065, Location = "Asia" },
                new IslandDetails { Name = "Honshu", Area = 227938, Location = "Asia" },
                new IslandDetails { Name = "Victoria Island", Area = 217291, Location = "North America" },
                new IslandDetails { Name = "Great Britain", Area = 209331, Location = "Europe" },
                new IslandDetails { Name = "Ellesmere Island", Area = 196236, Location = "North America" }
            };
        }

        #endregion
    }
}