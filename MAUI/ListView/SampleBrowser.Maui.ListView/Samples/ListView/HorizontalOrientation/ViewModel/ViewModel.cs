#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.ListView;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class ListViewOrientationViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<PlaceInfo>? places;
        private ObservableCollection<PlaceInfo>? touristPlaces;
        private PlaceInfo? selectedItem;

        #endregion

        #region Constructor

        public ListViewOrientationViewModel()
        {
            touristPlaces = new ObservableCollection<PlaceInfo>();
            var placesRepository = new PlaceInfoRepository();
            Places = placesRepository.GeneratePlaces();
            SelectedItem = Places[0];
        }

        #endregion

        #region Properties

        public PlaceInfo? SelectedItem
        {
            get
            {
                return selectedItem;
            }

            set
            {
                if (selectedItem != value)
                {
                    if (selectedItem != null)
                    {
                        selectedItem.IsSelected = false;  
                    }

                    selectedItem = value;
                    if (selectedItem != null)
                    {
                        this.TouristPlaces = selectedItem.TouristPlaces;
                        selectedItem.IsSelected = true;
                    }
                    else
                    {
                        this.TouristPlaces = null;
                    }
                }

                OnPropertyChanged("SelectedItem");
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

        public ObservableCollection<PlaceInfo>? Places
        {
            get { return places; }
            set { this.places = value; OnPropertyChanged("Places"); }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
