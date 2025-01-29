#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class PlaceInfo : INotifyPropertyChanged
    {
        #region Fields

        private string? name;
        private string? description;
        private string? image;
        private bool isSelected;
        private ObservableCollection<PlaceInfo>? touristPlaces;

        #endregion

        #region Constructor

        public PlaceInfo()
        {
        }

        #endregion

        #region Properties

        public string? Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

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


        public string? Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

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

        public ObservableCollection<PlaceInfo>? TouristPlaces
        {
            get { return touristPlaces; }
            set
            {
                touristPlaces = value;
                OnPropertyChanged("TouristPlaces");
            }
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
