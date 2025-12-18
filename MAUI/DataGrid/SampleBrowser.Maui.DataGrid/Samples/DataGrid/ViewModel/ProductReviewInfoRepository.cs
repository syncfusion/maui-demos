#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.DataGrid
{
    public class ProductReviewInfoRepository
    {
        #region Properties

        internal ObservableCollection<ProductReviewInfo> GetReviewInfo()
        {
            var reviewInfo = new ObservableCollection<ProductReviewInfo>();
            for (int i = 0; i < AuthorNames.Length; i++)
            {
                var review = new ProductReviewInfo()
                {
                    AuthorName = AuthorNames[i],
                    Comments = AuthorComments[i],
                    AuthorImage = AuthorImages[i],
                    Rating = Ratings[i],
                };
                reviewInfo.Add(review);
            }
            return reviewInfo;
        }

        #endregion

        #region BookInfo

        internal readonly string[] AuthorNames = new string[]
        {
            "Alexandar",
            "Gabriella",
            "Clara",
            "Tye",
            "Ami",
            "Daisy",
            "Lita",
            "Jackson",
            "Liam",
            "Gina",
            "Irene",
            "Watson",
            "Jennifer",
            "Torrey",
            "Howard",
            "Daniel",
            "Zara",
            "Lucas",

        };
        internal readonly string[] AuthorComments = new string[]
        {
            "Excellent online content and knowledge for product review page use.",
            "Found product while searching for issues. Live Chat with Kate helped quickly.",
            "Product impressed me. Signed up, built testimonial page with sliders easily.",
            "Seems promising. We've been looking for such a tool here.",
            "Found product for Facebook galleries. Excellent tool, can't do without it.",
            "Company product and services excel. Simplified my business life greatly.",
            "Prominent on homepage, compiles reviews. Feature-rich, easy, great price.",
            "Fantastic support and great plugin. Tested others, yours is best.",
            "Excellent human support for awesome items. Sending good vibes.",
            "They develop programs solving problems. Great business indeed.",
            "Excellent application. Useful, easy to install, well-made, sincere compliments.",
            "Amazing. Looked for businesses, none respond fast like yours.",
            "Great product and amazing support team. Continue wonderful effort.",
            "Excellent product and outstanding customer service. Keep up great work.",
            "Customer service disgusts me. Acknowledged problem but ignored free service.",
            "Helpful for web marketing and overall business purposes.",
            "Ease of working with team. Prompt responses to requests.",
            "Our product use is excellent. Reliable, fast loading, nice appearance."
        };

        internal readonly int[] Ratings = new int[]
        {
            5,
            5,
            4,
            4,
            5,
            5,
            4,
            4,
            5,
            4,
            5,
            4,
            5,
            5,
            3,
            5,
            4,
            4,
        };

        internal readonly string[] AuthorImages = new string[]
        {
            "people_circle12.png",
            "people_circle0.png",
            "people_circle1.png",
            "people_circle14.png",
            "people_circle3.png",
            "people_circle4.png",
            "people_circle20.png",
            "people_circle26.png",
            "people_circle21.png",
            "people_circle6.png",
            "people_circle16.png",
            "people_circle5.png",
            "people_circle9.png",
            "people_circle27.png",
            "people_circle18.png",
            "people_circle23.png",
            "people_circle2.png",
            "people_circle8.png",
        };

        #endregion
    }
}
