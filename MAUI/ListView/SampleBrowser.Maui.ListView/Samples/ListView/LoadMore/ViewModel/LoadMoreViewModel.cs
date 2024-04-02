#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using SampleBrowser.Maui.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

#nullable disable
namespace SampleBrowser.Maui.ListView.SfListView
{
    public class LoadMoreViewModel
    {
        private readonly int totalItems = 18;
        private readonly ListViewProductReviewInfoRepository reviewInfoRepository;
        public ObservableCollection<ListViewProductReviewInfo> ReviewInfo { get; set; }
        public Command<object> LoadMoreItemsCommand { get; set; }

        public LoadMoreViewModel()
        {
            reviewInfoRepository = new ListViewProductReviewInfoRepository();
            ReviewInfo = new ObservableCollection<ListViewProductReviewInfo>();

            if(DeviceInfo.Platform == DevicePlatform.MacCatalyst)
            {
                GenerateSource(11);
            }
            else
            {
                GenerateSource(7);
            }
            LoadMoreItemsCommand = new Command<object>(LoadMoreItems, CanLoadMoreItems);
        }
        private void GenerateSource(int value)
        {
            for (int i = 0; i < value; i++)
            {
                var name = reviewInfoRepository.AuthorNames[i];
                var p = new ListViewProductReviewInfo()
                {
                    AuthorName = name,
                    Comments = reviewInfoRepository.AuthorComments[i],
                    AuthorImage = reviewInfoRepository.AuthorImages[i],
                    Rating = reviewInfoRepository.Ratings[i],
                };

                ReviewInfo.Add(p);

            }
        }
        private async void LoadMoreItems(object obj)
        {
            var listview = obj as Syncfusion.Maui.ListView.SfListView;
            if (listview != null)
            {
                listview.IsLazyLoading = true;
                await Task.Delay(2500);
                var index = ReviewInfo.Count;
                var count = index + 2 >= totalItems ? totalItems - index : 2;
                AddProducts(index, count);

                listview.IsLazyLoading = false;
            }
        }

        private bool CanLoadMoreItems(object obj)
        {
            if (ReviewInfo.Count >= totalItems)
                return false;
            return true;
        }
        private void AddProducts(int index, int count)
        {
            for (int i = index; i < index + count; i++)
            {
                var name = reviewInfoRepository.AuthorNames[i];
                var p = new ListViewProductReviewInfo()
                {
                    AuthorName = name,
                    Comments = reviewInfoRepository.AuthorComments[i],
                    AuthorImage = reviewInfoRepository.AuthorImages[i],
                    Rating = reviewInfoRepository.Ratings[i],
                };
                ReviewInfo.Add(p);
            }
        }

    }
}