#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.TreeMap.SfTreeMap
{
    using Syncfusion.Maui.Buttons;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents the view model for the group level sample functionality.
    /// </summary>
    public class GroupLevelViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupLevelViewModel"/> class.
        /// </summary>
        public GroupLevelViewModel()
        {
            this.PopulationDetails = this.GetPopulationDetails();
            this.SegmentColorOptionsInfo = this.GetSegmentColorOptionsInfo();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the collection of population details.
        /// </summary>
        public ObservableCollection<PopulationDetails> PopulationDetails { get; set; }

        /// <summary>
        /// Gets or sets the collection of segment color option info.
        /// </summary>
        public List<SfSegmentItem> SegmentColorOptionsInfo { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Method to get a collection of segment color details.
        /// </summary>
        /// <returns>The segment color details collection.</returns>
        private List<SfSegmentItem> GetSegmentColorOptionsInfo()
        {
            return new List<SfSegmentItem>()
            {
                new SfSegmentItem() { Text = "\uE91F", SelectedSegmentBackground = Color.FromArgb("#3F8D71"), TextStyle = new SegmentTextStyle() { TextColor = Color.FromArgb("#3F8D71"), FontSize = 23, FontFamily = "SegmentIcon" } },
                new SfSegmentItem() { Text = "\uE91F", SelectedSegmentBackground = Color.FromArgb("#F0A868"), TextStyle = new SegmentTextStyle() { TextColor = Color.FromArgb("#F0A868"), FontSize = 23, FontFamily = "SegmentIcon" } },
                new SfSegmentItem() { Text = "\uE91F", SelectedSegmentBackground = Color.FromArgb("#9E79CD"), TextStyle = new SegmentTextStyle() { TextColor = Color.FromArgb("#9E79CD"), FontSize = 23, FontFamily = "SegmentIcon" } },
                new SfSegmentItem() { Text = "\uE91F", SelectedSegmentBackground = Color.FromArgb("#0C869C"), TextStyle = new SegmentTextStyle() { TextColor = Color.FromArgb("#0C869C"), FontSize = 23, FontFamily = "SegmentIcon" } },
                new SfSegmentItem() { Text = "\uE91F", SelectedSegmentBackground = Color.FromArgb("#5BC0BE"), TextStyle = new SegmentTextStyle() { TextColor = Color.FromArgb("#5BC0BE"), FontSize = 23, FontFamily = "SegmentIcon" } },
            };
        }

        /// <summary>
        /// Method to get a collection of population details.
        /// </summary>
        /// <returns>The population details collection.</returns>
        private ObservableCollection<PopulationDetails> GetPopulationDetails()
        {
            return new ObservableCollection<PopulationDetails>()
            {
                new PopulationDetails() { Continent ="North America", Country = "United States of America", Population = 339996564 },
                new PopulationDetails() { Continent ="South America", Country = "Brazil", Population = 216422446 },
                new PopulationDetails() { Continent ="North America", Country = "Mexico", Population = 128455567 },
                new PopulationDetails() { Continent ="South America", Country = "Colombia", Population = 52085168 },
                new PopulationDetails() { Continent ="South America", Country = "Argentina", Population = 45773884 },
                new PopulationDetails() { Continent ="North America", Country = "Canada", Population = 38781292 },
                new PopulationDetails() { Continent ="South America", Country = "Peru", Population = 34352719 },
                new PopulationDetails() { Continent ="South America", Country = "Venezuela", Population = 28838499 },
                new PopulationDetails() { Continent ="South America", Country = "Chile", Population = 19629590 },
                new PopulationDetails() { Continent ="South America", Country = "Ecuador", Population = 18190484 },
                new PopulationDetails() { Continent ="North America", Country = "Guatemala", Population = 18092026 },
                new PopulationDetails() { Continent ="South America", Country = "Bolivia", Population = 12388571 },
                new PopulationDetails() { Continent ="North America", Country = "Honduras", Population = 10593798 },
                new PopulationDetails() { Continent ="North America", Country = "Nicaragua", Population = 7046311 },
                new PopulationDetails() { Continent ="South America", Country = "Paraguay", Population = 6861524 },
                new PopulationDetails() { Continent ="North America", Country = "El Salvador", Population = 6364943 },
                new PopulationDetails() { Continent ="North America", Country = "Costa Rica", Population = 5212173 },
                new PopulationDetails() { Continent ="South America", Country = "Uruguay", Population = 3423109 },
            };
        }

        #endregion
    }
}