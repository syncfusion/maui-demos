#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace SampleBrowser.Maui.SfListView
{
    public class SortingFilteringViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ListViewSortOptions sortingOptions;

        #endregion

        #region Constructor
        public SortingFilteringViewModel()
        {
            AddItemDetails();
        }

        #endregion

        #region Properties

        public ObservableCollection<TaskInfo>? Items
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value whether indicates the sorting options, ascending or descending or none.
        /// </summary>
        public ListViewSortOptions SortingOptions
        {
            get
            {
                return sortingOptions;
            }
            set
            {
                sortingOptions = value;
                OnPropertyChanged("SortingOptions");
            }
        }

        #endregion

        #region Methods
        private void AddItemDetails()
        {
            Items = new ObservableCollection<TaskInfo>();
            var random = new Random();

            for (int i = 0; i < Features.Length; i++)
            {
                var details = new TaskInfo()
                {
                    Title = Features[i],
                    Description = Description[i],
                    Tag = Tags[random.Next(0, 4)],

                };
                Items?.Add(details);
            }
        }

        readonly string[] Tags = new string[]
        {
            "Feature Implementation",
            "Bug Fixing",
            "Testing",
            "Design",
            "Post Processing"
        };
        readonly string[] Features = new string[]
        {
            "Drag and drop",
            "Swiping",
            "Pull To Refresh",
            "Selection in row header",
            "Multiple selection color",
            "Animating the selected row",
            "Long press event",
            "Double click event",
            "Header Template",
            "Orientation for ListView",
            "Multi-line text",
            "Item Border",
            "Item Style",
            "Scroll to a row/column index",
            "Group expand",
            "Enabling / disabling the bouncing behavior",
            "Group collapse",
            "Auto row height",

        };
        readonly string[] Description = new string[] {
            "Rearrange the columns by dragging and dropping them",
            "Enables the users to swipe",
            "Pull To Refresh action refreshes the grid",
            "Apply selection using row header",
            "Apply different selection colors for different rows",
            "Add an animation upon selecting a row",
            "Users can listen to long-press gesture in the listview",
            "Users can listen to double-tap gesture in the listview",
            "Load custom views as templates in header cells",
            "Orientation are vertical and horizontal",
            "Displays multi-line text for the record",
            "Enable item border",
            "Set the items style",
            "Scroll to a particular row and/or column index",
            "Expand groups in runtime",
            "Enable/disable the bouncing of the grid when over-scrolled",
            "Collapse groups in runtime",
            "Automatically adjusts the height of item to fit the content",
        };

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }

    public enum ListViewSortOptions
    {
        None,
        Ascending,
        Descending
    }
}
