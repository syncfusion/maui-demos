#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.ComponentModel;

namespace SampleBrowser.Maui.Core
{
    /// <summary>
    /// Model for setting details for SamplesModel.
    /// </summary>
    public class SampleModel : INotifyPropertyChanged
    {
        #region Fields

        private string? title;

        private string? icon;

        private string? name;

        private string? category;

        private string? description;

        private string[]? searchTags;

        private Color textColor = Colors.Black;

        private bool isSelected = false;

        private Color backgroundColor = Colors.White;

        private string? updateType;

        private string? updateGroupType;


        #endregion

        #region Events

        /// <summary>
        /// Event to check property changed
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Properties

        public Color TextColor
        {
            get
            {
                return textColor;
            }

            set
            {
                textColor = value;
                OnPropertyChanged(nameof(TextColor));
            }
        }

        public Color BackgroundColor
        {
            get
            {
                return backgroundColor;
            }

            set
            {
                backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }


        /// <summary>
        /// Gets or sets Samples Category in SamplesListPage
        /// </summary>
        /// <value>This property takes either "Types" or "Features" as value.</value>
        public string? Category
        {
            get
            {
                return category;
            }

            set
            {
                category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        /// <summary>
        /// Gets or sets Samples Description in SamplesListPage
        /// </summary>
        public string? Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }


        /// <summary>
        /// Gets or sets Samples Name for creating instance in SamplesListPage
        /// </summary>
        public string? Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Gets or sets Samples Icon in SamplesListPage
        /// </summary>
        public string? Icon
        {
            get
            {
                return icon;
            }

            set
            {
                icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        /// <summary>
        /// Gets or sets Samples Title to be displayed in SamplesListPage
        /// </summary>
        public string? Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        /// <summary>
        /// Gets or sets selected
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public string[]? SearchTags
        {
            get
            {
                return searchTags;
            }

            set
            {
                searchTags = value;
                OnPropertyChanged(nameof(SearchTags));
            }
        }

        /// <summary>
        /// Gets or sets Samples Update type for Samples in SamplesListPage
        /// </summary>
        /// <value>Set true for any of these attributes "IsPreview" or "IsNew" or "IsUpdated" value.</value>
        public string? UpdateType
        {
            get
            {
                return updateType;
            }

            set
            {
                updateType = value;
                OnPropertyChanged(nameof(UpdateType));
            }
        }

        /// <summary>
        /// Gets or sets Samples Update type for Samples in SamplesListPage
        /// </summary>
        /// <value>Set true for any of these attributes "IsPreview" or "IsNew" or "IsUpdated" value.</value>
        public string? UpdateGroupType
        {
            get
            {
                return updateGroupType;
            }

            set
            {
                updateGroupType = value;
                OnPropertyChanged(nameof(UpdateGroupType));
            }
        }

        public string? Type { get; set; } = "Samples";

        public string? Control { get; set; }

        #endregion

        #region Methods

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}