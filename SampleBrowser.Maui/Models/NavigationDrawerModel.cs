using System.ComponentModel;

namespace SampleBrowser.Maui
{
	public class NavigationDrawerModel : INotifyPropertyChanged
	{
        #region fields

        private string appVersion;

		private string appDesc;

        private string appName;

        #endregion

        #region events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets Application version in MasterPage
        /// </summary>
        public string AppVersion
		{
			get
			{
				return appVersion;
			}

			set
			{
				appVersion = value;
				OnPropertyChanged("AppVersion");
			}
		}

        /// <summary>
        /// Gets or sets Application Name
        /// </summary>
        public string AppName
		{
			get
			{
				return appName;
			}

			set
			{
				appName = value;
				OnPropertyChanged("AppName");
			}
		}

        /// <summary>
        /// Gets or sets Application description in MasterPage
        /// </summary>
        public string AppDesc
		{
			get
			{
				return appDesc;
			}

			set
			{
				appDesc = value;
				OnPropertyChanged("AppDesc");
			}
		}

        #endregion

        #region methods

        public void OnPropertyChanged(string name)
		{
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }
}