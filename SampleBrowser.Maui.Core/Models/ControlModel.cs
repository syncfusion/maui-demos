using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;
using System.ComponentModel;
    
namespace SampleBrowser.Maui.Core
{
	/// <summary>
	/// Model file which has Control Details.
	/// </summary>
    public class ControlModel : INotifyPropertyChanged
	{
        #region fields

        private string? imageId;

        private string? title;

		private string? description;

		private int samplesCount;

        private string? controlName;

        private string[]? tags;

        private string? categoryName;

        private string? tagType;

        private Color? tagColor = Color.FromArgb("#D9644A");

        #endregion

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlModel"/> class.
        /// </summary>
        public ControlModel()
        {
            Samples = new ObservableCollection<SampleModel>();
        }

        #endregion

        #region events

        /// <summary>
        /// PropertyChanged Event.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets list of Samples in the control.
        /// </summary>
        public ObservableCollection<SampleModel> Samples 
		{ 
			get; 
			set; 
		}

        /// <summary>
        /// Gets or sets Samples Count in Control.
        /// </summary>
        public int SamplesCount
		{
			get
            {
                return samplesCount;
            }

			set
			{
				samplesCount = value;
				OnPropertyChanged("SamplesCount");
			}
		}

        /// <summary>
        /// Gets or sets Category name of the Control.
        /// </summary>
        public string? CategoryName
        {
            get
            {
                return categoryName;
            }

            set
            {
                categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        /// <summary>
        /// Gets or sets Controls Images in ControlsHomePage
        /// </summary>
        public string? ImageId
		{
			get
            {
                return imageId;
            }

			set
			{
				imageId = value;
				OnPropertyChanged("ImageId");
			}
		}


        /// <summary>
        /// Gets or sets Control Title to be displayed in ControlsHomePage
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
        /// Gets or sets Control Description in ControlsHomePage
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
        /// Gets or sets Controls Name in ControlsHomePage
        /// </summary>
        public string? ControlName
		{
			get
            {
                return controlName;
            }

			set
			{
				controlName = value;
				OnPropertyChanged("ControlName");
			}
		}

        /// <summary>
        /// Gets or sets Control search tags in SamplesListPage
        /// </summary>
        public string[]? Tags
		{
			get
            {
                return tags;
            }

			set
			{
				tags = value;
				OnPropertyChanged("Tags");
			}
		}

        public string? TagType
        {
            get
            {
                return tagType;
            }

            set
            {
                tagType = value;
                OnPropertyChanged("TagType");
            }
        }

        public Color? TagColor
        {
            get
            {
                return tagColor;
            }

            set
            {
                tagColor = value;
                OnPropertyChanged("TagColor");
            }
        }

        public string? Type { get; set; } = "Controls";

        #endregion

        #region methods

        private void OnPropertyChanged(string name)
		{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}