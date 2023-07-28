#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class SongInfo : INotifyPropertyChanged
    {
        #region Fields

        private string? songTitle;
        private string? singerName;
        private string? trackImage;

        #endregion

        #region Properties

        public string? SongTitle
        {
            get { return songTitle; }
            set
            {
                songTitle = value;
                OnPropertyChanged("SongTitle");
            }
        }
        
        public string? SingerName
        {
            get
            {
                return singerName;
            }

            set
            {
                singerName = value;
                OnPropertyChanged("SingerName");
            }
        }

        public string? TrackImage
        {
            get
            {
                return trackImage;
            }

            set
            {
                trackImage = value;
                OnPropertyChanged("TrackImage");
            }
        }

        #endregion

        #region Constructor

        public SongInfo()
        {

        }

        #endregion

        #region Interface Member

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
