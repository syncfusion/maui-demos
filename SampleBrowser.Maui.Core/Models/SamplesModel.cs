using Microsoft.Maui.Graphics;
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

        private double opacity = 0;

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
                OnPropertyChanged("TextColor");
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
                OnPropertyChanged("BackgroundColor");
            }
        }


        public double Opacity
        {
            get
            {
                return opacity;
            }

            set
            {
                opacity = value;
                OnPropertyChanged("Opacity");
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
				OnPropertyChanged("Category");
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
				OnPropertyChanged("Description");
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
				OnPropertyChanged("Name");
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
				OnPropertyChanged("Icon");
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
				OnPropertyChanged("Title");
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
				OnPropertyChanged("IsSelected");
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
                OnPropertyChanged("SearchTags");
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