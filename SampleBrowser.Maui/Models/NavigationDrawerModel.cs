#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

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