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
    /// Represents the view model for the customization sample functionality.
    /// </summary>
    internal class CustomizationViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomizationViewModel"/> class.
        /// </summary>
        public CustomizationViewModel()
        {
            this.OlympicMedalDetails = this.GetOlympicMedalDetails();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the collection of olympics medal details.
        /// </summary>
        public ObservableCollection<OlympicMedalDetails> OlympicMedalDetails { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Method to get a collection of olympics medal details..
        /// </summary>
        /// <returns>The olympics medal details collection.</returns>
        private ObservableCollection<OlympicMedalDetails> GetOlympicMedalDetails()
        {
            return new ObservableCollection<OlympicMedalDetails>()
            {
                new OlympicMedalDetails { Sport = "Swimming", GoldMedals = 16, IconName = "\ue7da" },
                new OlympicMedalDetails { Sport = "Athletics", GoldMedals = 13, IconName = "\ue778" },
                new OlympicMedalDetails { Sport = "Gymnastics", GoldMedals = 4, IconName = "\ue7d9" },
                new OlympicMedalDetails { Sport = "Cycling", GoldMedals = 2, IconName = "\ue7a2" },
                new OlympicMedalDetails { Sport = "Wrestling", GoldMedals = 2, IconName = "\ue7d5" },
                new OlympicMedalDetails { Sport = "Basketball", GoldMedals = 2, IconName = "\ue7d3" },
                new OlympicMedalDetails { Sport = "Boxing", GoldMedals = 1, IconName = "\ue7d8" },
                new OlympicMedalDetails { Sport = "Tennis", GoldMedals = 1, IconName = "\ue7d7" },
                new OlympicMedalDetails { Sport = "Judo", GoldMedals = 1, IconName = "\ue7d6" },
                new OlympicMedalDetails { Sport = "Rowing", GoldMedals = 1, IconName = "\ue7cf" },
                new OlympicMedalDetails { Sport = "Shooting", GoldMedals = 1, IconName = "\ue7db" },
                new OlympicMedalDetails { Sport = "Triathlon", GoldMedals = 1, IconName = "\ue7dc" },
                new OlympicMedalDetails { Sport = "Water polo", GoldMedals = 1, IconName = "\ue7d4" }
            };
        }

        #endregion
    }
}