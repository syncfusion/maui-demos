#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.ComponentModel;

namespace SampleBrowser.Maui.RadialMenu.SfRadialMenu
{
    internal class MainModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamName"></param>
        /// <param name="teamNameColor"></param>
        /// <param name="fifaCurrent"></param>
        /// <param name="fifaHighest"></param>
        /// <param name="fifaLowest"></param>
        /// <param name="eloCurrent"></param>
        /// <param name="eloHighest"></param>
        /// <param name="eloLowest"></param>
        public MainModel(string teamName, Color teamNameColor, string fifaCurrent, string fifaHighest, string fifaLowest, string eloCurrent, string eloHighest, string eloLowest)
        {
            TeamName = teamName;
            TeamColor = teamNameColor;
            FIFACurrent = fifaCurrent;
            FIFAHightest = fifaHighest;
            FIFALowest = fifaLowest;
            EloCurrent = eloCurrent;
            EloHightest = eloHighest;
            EloLowest = eloLowest;
        }

        private string teamName = string.Empty;

        public string TeamName
        {
            get { return teamName; }
            set
            {
                teamName = value;
            }
        }

        private Color color = Colors.Yellow;
        public Color TeamColor
        {
            get { return color; }
            set
            {
                color = value;
                RaisepropertyChanged("TeamColor");
            }
        }

        private string fifaCurrent = string.Empty;
        public string FIFACurrent
        {
            get { return fifaCurrent; }
            set { fifaCurrent = value; }
        }

        private string fifaHightest = string.Empty;
        public string FIFAHightest
        {
            get { return fifaHightest; }
            set { fifaHightest = value; }
        }

        private string fifaLowest = string.Empty;
        public string FIFALowest
        {
            get { return fifaLowest; }
            set { fifaLowest = value; }
        }

        private string eloCurrent = string.Empty;
        public string EloCurrent
        {
            get { return eloCurrent; }
            set { eloCurrent = value; }
        }

        private string eloHightest = string.Empty;
        public string EloHightest
        {
            get { return eloHightest; }
            set { eloHightest = value; }
        }

        private string eloLowest = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        void RaisepropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string EloLowest
        {
            get { return eloLowest; }
            set { eloLowest = value; }
        }

    }
}
