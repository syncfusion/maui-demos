#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;



namespace SampleBrowser.Maui.ListView.SfListView
{

    public class ListViewProductReviewInfoRepository
    {
        #region Constructor

        public ListViewProductReviewInfoRepository()
        {

        }

        #endregion

        #region Properties

        internal ObservableCollection<ListViewProductReviewInfo> GetReviewInfo()
        {
            var reviewInfo = new ObservableCollection<ListViewProductReviewInfo>();            
            for (int i = 0; i < AuthorNames.Length; i++)
            {
                var review = new ListViewProductReviewInfo()
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
            "Excellent online content and the knowledge of how to use my product review page! Your YouTube how-to videos are also a tremendous help!",
            "I found the product while searching for answers to some issues I had about the product pages. I started a Live Chat with Kate and she quickly provided me the information I needed without requiring me to register or pay anything. Definitely going to check out the tools you have to offer.",
            "The product quite impressed me. I just signed up for a trial, and within a few hours, our website had a brand-new customer testimonial page constructed along with the sliders positioned thoughtfully around it. Well done for making the app so user-friendly.",
            "So far, it seems pretty promising! We've been looking for a tool just like this. We anticipate finding out more information about it.",
            "Found the product when looking for a way to insert the photo galleries from my Facebook business page on my website. I have not turned around. It's an excellent tool. I'm unsure of what I would do without it. Continue your excellent work.",
            "The company product and their services are excellent, especially their functionalities . My business life has been so much simpler as a result! 5 stars!",
            "It commands a prominent position on our homepage and compiles reviews from many sources. For a fantastic price, it is incredibly feature-rich and simple to use. Messenger is used for customer service and onsite chat, and they respond quickly to messages and take care of the infrequent problems we have encountered. I heartily endorse your merchandise.",
            "Fantastic support and a great plugin. I tested a number of plugins that aim to achieve the same goal. None of them managed to make it as elegantly and simply as your products.",
            "Excellent human support for awesome items. I send them good vibes.",
            "They develop programs that actually solve problems and take care of concerns. Great business!",
            "Excellent application. It is useful, simple to install, and well made. Sincere compliments.",
            "Amazing. I've looked for a lot of businesses to accomplish what they do, but I've never come across any that are as quick to respond and incorporate fresh concepts. Incredibly impressed.",
            "Great product and an amazing support team. Continue your wonderful effort!",
            "Excellent product and outstanding customer service. Continue your wonderful effort!",
            "The customer service provided by this firm truly disgusts me. They acknowledged that we had a problem, , but they flat out told us that since we were using the free service, they would not be spending time on it. If you don't think a free service will be useful, don't offer it. You just guaranteed that we would never spend money on upgrading with your firm because it's a horrible business strategy.",
            "Helpful for web marketing and business.",
            "Working with the product team is a breeze. Any specific requests receive a prompt response, and they act quickly to implement account modifications. In addition to having excellent customer service, their goods are also incredibly user-friendly and provide a lot of freedom that is unavailable with other comparable items. I’d definitely recommend.",
            "Our use of the product has been excellent. It is reliable, loads quickly, and has a nice appearance.",

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
