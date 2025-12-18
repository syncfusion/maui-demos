#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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
namespace SampleBrowser.Maui.DataGrid
{
    public class ProductReviewInfoViewModel
    {
        private readonly ProductReviewInfoRepository reviewInfoRepository;
		private Random random = new Random();
		public ObservableCollection<ProductReviewInfo> ReviewInfo { get; set; }

        public ProductReviewInfoViewModel()
        {
            reviewInfoRepository = new ProductReviewInfoRepository();
            ReviewInfo = new ObservableCollection<ProductReviewInfo>();

            GenerateSource(100);
        }
        private void GenerateSource(int value)
        {
            
			for (int i = 0; i < value; i++)
            {
				int index = this.random.Next(17);
				var name = reviewInfoRepository.AuthorNames[index];
                var p = new ProductReviewInfo()
                {
                    AuthorName = name,
                    Comments = reviewInfoRepository.AuthorComments[index],
                    AuthorImage = reviewInfoRepository.AuthorImages[index],
                    Rating = reviewInfoRepository.Ratings[index],
                };

                ReviewInfo.Add(p);

            }
        }
    }
}