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
    public class NavigationLinkModel : INotifyPropertyChanged
    {
        #region fields

        private string linkText;

        private string linkURL;

        private string linkIcons;

        #endregion

        #region events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets Icons for links in Master Page
        /// </summary>
        public string LinkIcons
		{
			get
            {
                return linkIcons;
            }

			set
			{
				linkIcons = value;
				OnPropertyChanged("LinkIcons");
			}
		}

        /// <summary>
        /// Gets or sets text displayed for Links in Master Page
        /// </summary>
        public string LinkText
		{
			get
            {
                return linkText;
            }

			set
			{
				linkText = value;
				OnPropertyChanged("LinkText");
			}
		}

        /// <summary>
        /// Gets or sets URl for links in Master Page
        /// </summary>
		public string LinkURL
		{
			get
            {
                return linkURL;
            }

			set
			{
				linkURL = value;
				OnPropertyChanged("LinkURL");
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