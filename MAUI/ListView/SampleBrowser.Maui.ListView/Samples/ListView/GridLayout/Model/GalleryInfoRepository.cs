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
            var random = new Random();
            for (int i = 1; i <= 30; i++)
            {
                var gallery = new ListViewGalleryInfo()
                {
                    Image = "image" + i + ".png",
                    ImageTitle = random.Next(1242, 5383) + ".jpg",
                    IsFavorite = i % 2 == 0 || i % 3 == 0 ? true : false
                };
                galleryInfo.Add(gallery);
            }
            return galleryInfo;
        }

        #endregion
    }
}
