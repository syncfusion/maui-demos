#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Kanban;
using System.Collections.ObjectModel;
using SampleBrowser.Maui.Base.Converters;
using System.Reflection;

namespace SampleBrowser.Maui.Kanban.SfKanban
{
    internal class ViewModel
    {
        public ObservableCollection<KanbanModel> Cards { get; set; }

        public ViewModel()
        {
            Assembly assemblyName = typeof(SfImageSourceConverter).GetTypeInfo().Assembly;

            Cards = new ObservableCollection<KanbanModel>();

            Cards.Add(
                new KanbanModel()
                {
                    ID = 1,
                    Title = "iOS - 1",
                    ImageURL = assemblyName + ".people_circle1.png",
                    Category = "Open",
                    Description = "Analyze customer requirements.",
                    IndicatorFill = Colors.Red,
                    Tags = new List<string> { "Bug", "Customer", "Release Bug" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 6,
                    Title = "Xamarin - 6",
                    ImageURL = assemblyName + ".people_circle2.png",
                    Category = "Open",
                    Description = "Show the retrieved data from the server in Grid control.",
                    IndicatorFill = Colors.Red,
                    Tags = new List<string> { "Bug", "Customer", "Breaking Issue" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 3,
                    Title = "iOS - 3",
                    ImageURL = assemblyName + ".people_circle3.png",
                    Category = "Open",
                    Description = "Fix the filtering issues reported in Safari.",
                    IndicatorFill = Colors.Red,
                    Tags = new List<string> { "Bug", "Customer", "Breaking Issue" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 11,
                    Title = "iOS - 21",
                    ImageURL = assemblyName + ".people_circle4.png",
                    Category = "Open",
                    Description = "Add input validation for editing.",
                    IndicatorFill = Colors.Red,
                    Tags = new List<string> { "Bug", "Customer", "Breaking Issue" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 15,
                    Title = "Android - 15",
                    Category = "Open",
                    ImageURL = assemblyName + ".people_circle5.png",
                    Description = "Arrange web meetings for customers.",
                    IndicatorFill = Colors.Red,
                    Tags = new List<string> { "Story", "Kanban" }
                });

            Cards.Add(
                new KanbanModel()
                {
                    ID = 3,
                    Title = "Android - 3",
                    Category = "Code Review",
                    ImageURL = assemblyName + ".people_circle6.png",
                    Description = "API Improvements.",
                    IndicatorFill = Colors.Purple,
                    Tags = new List<string> { "Bug", "Customer" }
                });

            Cards.Add(
                new KanbanModel()
                {
                    ID = 4,
                    Title = "UWP - 4",
                    ImageURL = assemblyName + ".people_circle7.png",
                    Category = "Code Review",
                    Description = "Enhance editing functionality.",
                    IndicatorFill = Colors.Brown,
                    Tags = new List<string> { "Story", "Kanban" }
                });

            Cards.Add(
                new KanbanModel()
                {
                    ID = 9,
                    Title = "Xamarin - 9",
                    ImageURL = assemblyName + ".people_circle8.png",
                    Category = "Code Review",
                    Description = "Improve application's performance.",
                    IndicatorFill = Colors.Orange,
                    Tags = new List<string> { "Story", "Kanban" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 13,
                    Title = "UWP - 13",
                    ImageURL = assemblyName + ".people_circle9.png",
                    Category = "In Progress",
                    Description = "Add responsive support to applications.",
                    IndicatorFill = Colors.Brown,
                    Tags = new List<string> { "Story", "Kanban" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 17,
                    Title = "Xamarin - 17",
                    Category = "In Progress",
                    ImageURL = assemblyName + ".people_circle10.png",
                    Description = "Fix the issues reported in the IE browser.",
                    IndicatorFill = Colors.Brown,
                    Tags = new List<string> { "Bug", "Customer" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 21,
                    Title = "Xamarin - 21",
                    Category = "In Progress",
                    ImageURL = assemblyName + ".people_circle11.png",
                    Description = "Improve the performance of editing functionality.",
                    IndicatorFill = Colors.Purple,
                    Tags = new List<string> { "Bug", "Customer" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 19,
                    Title = "iOS - 19",
                    Category = "In Progress",
                    ImageURL = assemblyName + ".people_circle12.png",
                    Description = "Fix the issues reported by the customer.",
                    IndicatorFill = Colors.Purple,
                    Tags = new List<string> { "Bug" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 8,
                    Title = "Android",
                    Category = "Code Review",
                    ImageURL = assemblyName + ".people_circle13.png",
                    Description = "Check login page validation.",
                    IndicatorFill = Colors.Brown,
                    Tags = new List<string> { "Feature" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 24,
                    Title = "UWP - 24",
                    ImageURL = assemblyName + ".people_circle14.png",
                    Category = "In Progress",
                    Description = "Test editing functionality.",
                    IndicatorFill = Colors.Orange,
                    Tags = new List<string> { "Feature", "Customer", "Release" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 20,
                    Title = "iOS - 20",
                    Category = "In Progress",
                    ImageURL = assemblyName + ".people_circle15.png",
                    Description = "Fix the issues reported in data binding.",
                    IndicatorFill = Colors.Red,
                    Tags = new List<string> { "Feature", "Release" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 12,
                    Title = "Xamarin - 12",
                    Category = "In Progress",
                    ImageURL = assemblyName + ".people_circle16.png",
                    Description = "Test editing functionality.",
                    IndicatorFill = Colors.Red,
                    Tags = new List<string> { "Feature", "Release" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 11,
                    Title = "iOS - 11",
                    Category = "In Progress",
                    ImageURL = assemblyName + ".people_circle17.png",
                    Description = "Check filtering validation.",
                    IndicatorFill = Colors.Red,
                    Tags = new List<string> { "Feature", "Release" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 13,
                    Title = "UWP - 13",
                    ImageURL = assemblyName + ".people_circle18.png",
                    Category = "Closed",
                    Description = "Fix cannot open user's default database SQL error.",
                    IndicatorFill = Colors.Purple,
                    Tags = new List<string> { "Bug", "Internal", "Release" }
                });

            Cards.Add(
                new KanbanModel()
                {
                    ID = 14,
                    Title = "Android - 14",
                    Category = "Closed",
                    ImageURL = assemblyName + ".people_circle19.png",
                    Description = "Arrange a web meeting with the customer to get the login page requirement.",
                    IndicatorFill = Colors.Red,
                    Tags = new List<string> { "Feature" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 15,
                    Title = "Xamarin - 15",
                    Category = "Closed",
                    ImageURL = assemblyName + ".people_circle20.png",
                    Description = "Login page validation.",
                    IndicatorFill = Colors.Red,
                    Tags = new List<string> { "Bug" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 16,
                    Title = "Xamarin - 16",
                    ImageURL = assemblyName + ".people_circle21.png",
                    Category = "Closed",
                    Description = "Test the application in the IE browser.",
                    IndicatorFill = Colors.Purple,
                    Tags = new List<string> { "Bug" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 20,
                    Title = "UWP - 20",
                    ImageURL = assemblyName + ".people_circle22.png",
                    Category = "Closed",
                    Description = "Analyze stored procedure.",
                    IndicatorFill = Colors.Brown,
                    Tags = new List<string> { "CustomSample", "Customer", "Incident" }
                }
            );

            Cards.Add(
                new KanbanModel()
                {
                    ID = 21,
                    Title = "Android - 21",
                    Category = "Closed",
                    ImageURL = assemblyName + ".people_circle23.png",
                    Description = "Arrange a web meeting with the customer to get editing requirements.",
                    IndicatorFill = Colors.Orange,
                    Tags = new List<string> { "Story", "Improvement" }
                }
            );

        }
    }
}
