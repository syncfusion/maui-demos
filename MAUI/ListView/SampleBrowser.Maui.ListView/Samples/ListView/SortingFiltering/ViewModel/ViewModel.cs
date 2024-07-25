#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace SampleBrowser.Maui.ListView.SfListView
{
    public class SortingFilteringViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ListViewSortOptions sortingOptions;

        #endregion

        #region Constructor
        public SortingFilteringViewModel()
        {
            AddItemDetails();
        }

        #endregion

        #region Properties

        public ObservableCollection<ProductInfo>? Items
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value whether indicates the sorting options, ascending or descending or none.
        /// </summary>
        public ListViewSortOptions SortingOptions
        {
            get
            {
                return sortingOptions;
            }
            set
            {
                sortingOptions = value;
                OnPropertyChanged("SortingOptions");
            }
        }

        #endregion

        #region Methods
        private void AddItemDetails()
        {
            Items = new ObservableCollection<ProductInfo>();
           
            for (int i = 0; i < ProductNames.Length; i++)
            {
                var details = new ProductInfo()
                {
                    ProductName = ProductNames[i],
                    Quantity = ProductQuantity[i],
                    ProductImage = ProductImages[i],
                };
                Items?.Add(details);
            }
        }

        readonly string[] ProductNames = new string[]
        {
            "Watches",
            "Crocs",
            "Bags",
            "Shoes",
            "Cooling glasses",
            "Cosmetics",
            "Cameras",
            "Headsets",
            "Jewelry",
            "Pen drives",
            "Memory cards",
            "Monitors",
            "Printers",
            "Laptops",
            "Sound bars",
            "Desktops",
            "Storage devices",
            "Router & networking devices",
            "Musical instruments",
            "Camera accessories",
            "Computer accessories",
            "Smart watches",
            "Mobiles",
            "Telephone & accessories",
            "Power accessories",

        };
        readonly int[] ProductQuantity = new int[]
        {
            5,
            2,
            5,
            6,
            12,
            10,
            4,
            10,
            12,
            10,
            12,
            10,
            5,
            10,
            5,
            2,
            5,
            6,
            5,
            10,
            5,
            4,
            5,
            4,
            10,
        };

        readonly string[] ProductImages = new string[]
        {
            "watchimage.png",
            "crocs.png",
            "bags.png",
            "shoes.png",
            "coolingglasses.png",
            "cosmetics.png",
            "camera.png",
            "headsets.png",
            "jewelries.png",
            "pendrives.png",
            "memorycards.png",
            "monitors.png",
            "printers.png",
            "laptops.png",
            "soundbars.png",
            "desktops.png",
            "storagedevices.png",
            "router.png",
            "musical.png",
            "cameraaccessories.png",
            "computeraccessories.png",
            "smartwatches.png",
            "mobiles.png",
            "telephone.png",
            "poweraccessories.png",
        };
      

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

    public enum ListViewSortOptions
    {
        None,
        Ascending,
        Descending
    }
}
