using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfListView
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
                    Image = "image"+ i +".png",
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
