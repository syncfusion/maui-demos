#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Kanban.SfKanban
{
    using System.Collections.ObjectModel;
    using System.Reflection;
    using SampleBrowser.Maui.Base.Converters;

    /// <summary>
    /// Represents the ViewModel responsible for managing a collection of Kanban cards with sorting functionality.
    /// </summary>
    public class SortingViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SortingViewModel"/> class.
        /// </summary>
        public SortingViewModel()
        {
            this.Cards = this.GetCardDetails();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the collection of <see cref="CardDetails"/> objects representing cards in various stages.
        /// </summary>
        public ObservableCollection<CardDetails> Cards { get; set; }

        #endregion

        #region Private method

        /// <summary>
        /// Method to retrieve a predefined collection of Kanban cards with sorting metadata.
        /// </summary>
        /// <returns>The card collection.</returns>
        private ObservableCollection<CardDetails> GetCardDetails()
        {
            var cardDetails = new ObservableCollection<CardDetails>();
            cardDetails.Add(new CardDetails()
            {
                Index = 5,
                Priority = Priority.Medium,
                Title = "Task - 1",
                Category = "Open",
                Description = "Fix the issue reported in the Edge browser.",
                Progress = 0,
                Tags = new List<string> { "Analyze", "Bug" }
            });

            cardDetails.Add(new CardDetails()
            {
                Index = 2,
                Title = "Task - 3",
                Priority = Priority.Low,
                Category = "Open",
                Description = "Analyze the new requirements gathered from the customer.",
                Progress = 0,
                Tags = new List<string> { "Trial preparation" }
            });

            cardDetails.Add(new CardDetails()
            {
                Index = 3,
                Title = "Task - 4",
                Priority = Priority.Critical,
                Category = "Open",
                Description = "Arrange a web meeting with the customer to get new requirements.",
                Progress = 0,
                Tags = new List<string> { "Requirements Gathering" }
            });

            cardDetails.Add(new CardDetails()
            {
                Title = "Task - 2",
                Priority = Priority.High,
                Index = 4,
                Category = "In Progress",
                Description = "Test the application in the Edge browser.",
                Progress = 40,
                Tags = new List<string> { "Documentation" }
            });

            cardDetails.Add(new CardDetails()
            {
                Index = 1,
                Title = "Task - 5",
                Priority = Priority.Medium,
                Category = "Open",
                Description = "Enhance editing functionality.",
                Progress = 0,
                Tags = new List<string> { "Analyze" }
            });

            cardDetails.Add(new CardDetails()
            {
                Index = 7,
                Title = "Task - 6",
                Priority = Priority.Low,
                Category = "Open",
                Description = "Arrange web meeting with the customer to show editing demo.",
                Progress = 0,
                Tags = new List<string> { "Bug" }
            });

            cardDetails.Add(new CardDetails()
            {
                Index = 6,
                Title = "Task - 8",
                Priority = Priority.Medium,
                Category = "Done",
                Description = "Improve application performance.",
                Progress = 100,
                Tags = new List<string> { "Enhancement" }
            });

            cardDetails.Add(new CardDetails()
            {
                Index = 10,
                Title = "Task - 7",
                Priority = Priority.Critical,
                Category = "In Progress",
                Description = "Fix cannot open user's default database SQL error.",
                Progress = 50,
                Tags = new List<string> { "Trial preparation" }
            });

            cardDetails.Add(new CardDetails()
            {
                Title = "Task - 9",
                Priority = Priority.Medium,
                Index = 8,
                Category = "In Progress",
                Description = "Improve the performance of the editing functionality.",
                Progress = 55,
                Tags = new List<string> { "Bug" }
            });

            cardDetails.Add(new CardDetails()
            {
                Index = 9,
                Title = "Task - 10",
                Priority = Priority.High,
                Category = "Done",
                Description = "Analyze grid control.",
                Progress = 100,
                Tags = new List<string> { "Documentation" }
            });

            cardDetails.Add(new CardDetails()
            {
                Index = 15,
                Title = "Task - 12",
                Priority = Priority.Low,
                Category = "Done",
                Description = "Analyze stored procedures.",
                Progress = 100,
                Tags = new List<string> { "Trial preparation" }
            });

            cardDetails.Add(new CardDetails()
            {
                Title = "Task - 13",
                Priority = Priority.Medium,
                Index = 20,
                Category = "Code Review",
                Description = "Analyze grid control.",
                Progress = 90,
                Tags = new List<string> { "Documentation" }
            });

            cardDetails.Add(new CardDetails()
            {
                Index = 18,
                Title = "Task - 14",
                Priority = Priority.Critical,
                Category = "Code Review",
                Description = "Stored procedure for initial data binding of the grid.",
                Progress = 90,
                Tags = new List<string> { "Bug" }
            });

            cardDetails.Add(new CardDetails()
            {
                Index = 23,
                Title = "Task - 15",
                Priority = Priority.Low,
                Category = "Code Review",
                Description = "Analyze stored procedures.",
                Progress = 90,
                Tags = new List<string> { "Analyze" }
            });

            return cardDetails;
        }

        #endregion
    }
}