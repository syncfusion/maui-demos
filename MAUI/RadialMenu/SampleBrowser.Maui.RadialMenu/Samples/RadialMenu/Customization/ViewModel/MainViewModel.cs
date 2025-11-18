#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.ComponentModel;

namespace SampleBrowser.Maui.RadialMenu.SfRadialMenu
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            RotatorCollection.Add(new MainModel("Team Eagle", Color.FromArgb("#f99363"), "35", "4", "36", "19", "9", "85"));
            RotatorCollection.Add(new MainModel("Team Tiger", Color.FromArgb("#7d8ff2"), "2", "1", "22", "1", "1", "18"));
        }

        private List<MainModel> rotatorCollection = new List<MainModel>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<MainModel> RotatorCollection
        {
            get
            {
                return rotatorCollection;
            }
            set
            {
                rotatorCollection = value;
                RaisepropertyChanged("RotatorCollection");
            }
        }

        void RaisepropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
