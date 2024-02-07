#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;

#nullable disable
namespace SampleBrowser.Maui.ListView.SfListView
{
    public class ListViewFoodCategory : INotifyPropertyChanged
    {
        #region Fields

        private string foodCategory;
        private bool isExpanded;
        private string foodIcon;
        private ObservableCollection<FoodMenu> foodMenuCollection;

        #endregion

        #region Properties

        public string FoodCategory
        {
            get { return foodCategory; }
            set
            {
                foodCategory = value;
                this.RaisedOnPropertyChanged("FoodCategory");
            }
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                this.RaisedOnPropertyChanged("IsExpanded");
            }
        }

        public string FoodIcon
        {
            get { return foodIcon; }
            set
            {
                foodIcon = value;
                this.RaisedOnPropertyChanged("FoodIcon");
            }
        }

        public ObservableCollection<FoodMenu> FoodMenuCollection
        {
            get { return foodMenuCollection; }
            set
            {
                foodMenuCollection = value;
                RaisedOnPropertyChanged("FoodMenuCollection");
            }
        }

        #endregion

        #region Constructor

        public ListViewFoodCategory()
        {

        }

        #endregion

        #region Interface Member

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }

        #endregion
    }
}