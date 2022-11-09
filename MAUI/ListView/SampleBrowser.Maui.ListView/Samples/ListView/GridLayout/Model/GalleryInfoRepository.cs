#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class ListViewGalleryInfoRepository
    {
        #region Constructor

        public ListViewGalleryInfoRepository()
        {

        }

        #endregion

        #region GetGalleryInfo

        internal ObservableCollection<ListViewGalleryInfo> GetGalleryInfo()
        {
            var galleryInfo = new ObservableCollection<ListViewGalleryInfo>();          
            for (int i = 0; i < ImageTitles.Length; i++)
            {
                var gallery = new ListViewGalleryInfo()
                {
                    Image = Images[i],
                    ImageTitle = ImageTitles[i],
                    ImageCount = ImagesCount[i],
                    IsFavorite = i % 2 == 0 || i % 3 == 0 ? true : false
                };
                galleryInfo.Add(gallery);
            }
            return galleryInfo;
        }

        #endregion
        #region GalleryInfo

        readonly string[] ImageTitles = new string[]
        {
            "Places",
            "Food",
            "Art",
            "Fashion",
            "Books",
            "Footwear",
            "Animals",
            "Jewelry",
            "Watches",
            "Kids",
            "Sports",
            "Electronics",
            "Music",
            "Scenery",
            "Birds",
            "Mobiles & accessories",
            "Paintings",
            "Wallpapers",
            "Babies",
            "Vehicles",
            "Flowers",
            "Logos",
            "Designs",
            "Houses",
            "Kitchen",
            "Cars",
            "Sculptures",
            "Masonry",
            "Museums",
            "Artists",

        };
        readonly string[] ImagesCount = new string[]
        {
            "10 images",
            "15 images",
            "13 images",
            "134 images",
            "10 images",
            "20 images",
            "10 images",
            "30 images",
            "10 images",
            "50 images",
            "20 images",
            "15 images",
            "10 images",
            "30 images",
            "20 images",
            "20 images",
            "10 images",
            "20 images",
            "10 images",
            "25 images",
            "30 images",
            "10 images",
            "10 images",
            "30 images",
            "50 images",
            "20 images",
            "15 images",
            "10 images",
            "20 images",
            "10 images",
        };

        readonly string[] Images = new string[]
        {
            "places.png",
            "food.png",
            "art.png",
            "fashion.png",
            "books.png",
            "footware.png",
            "animals.png",
            "jewelry.png",
            "watches.png",
            "kids.png",
            "sports.png",
            "electronics.png",
            "music.png",
            "naturals.png",
            "birds.png",
            "mobile.png",
            "paintings.png",
            "wallpaper.png",
            "baby.png",
            "vehicles.png",
            "flowers.png",
            "logos.png",
            "designs.png",
            "house.png",
            "kitchen.png",
            "cars.png",
            "sculpture.png",
            "masonry.png",
            "museum.png",
            "artist.png",
        };
        #endregion
    }
}
