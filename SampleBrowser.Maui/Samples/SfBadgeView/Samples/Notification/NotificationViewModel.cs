#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.SfBadgeView
{
    internal class NotificationViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationViewModel" /> class
        /// </summary>
        public NotificationViewModel()
        {
            this.Collection = new ObservableCollection<NotificationModel>
            {
                new NotificationModel()
                {
                    Image = "people_circle1.png",
                    Name = "Blessy",
                    Message = "Hi, I have sent you a photo",
                    Time = "Monday",
                    Count = string.Empty
                },
                new NotificationModel()
                {
                    Image = "people_circle5.png",
                    Name = "Aaron",
                    Message = "Family meeting tomorrow at 6:30 PM",
                    Time = "11:30 PM",
                    Count = "99+"
                },
                new NotificationModel()
                {
                    Image = "people_circle2.png",
                    Name = "Tara",
                    Message = "Hi, I am Tara, How are you?",
                    Time = "11:12 PM",
                    Count = "3"
                },
                new NotificationModel()
                {
                    Image = "people_circle3.png",
                    Name = "Jeni",
                    Message = "video",
                    Time = "07:53 PM",
                    Count = "137",
                },
                new NotificationModel()
                {
                    Image = "people_circle4.png",
                    Name = "Flora",
                    Message = "I have received your gift",
                    Time = "04:40 PM",
                    Count = string.Empty
                },
                new NotificationModel()
                {
                    Image = "people_circle6.png",
                    Name = "Sara",
                    Count = "47",
                    Message = "done thanks",
                    Time = "Yesterday"
                },
                new NotificationModel()
                {
                    Image = "people_circle8.png",
                    Name = "Stephan",
                    Count = string.Empty,
                    Time = "07.46 PM",
                    Message = "ok fine"
                },
                new NotificationModel()
                {
                    Image = "people_circle7.png",
                    Name = "Maria",
                    Count = string.Empty,
                    Time = "07.46 PM",
                    Message = "Hi, How are you?"
                },
                new NotificationModel()
                {
                    Image = "people_circle9.png",
                    Name = "Ancy",
                    Message = "Hi, i have sent you a photo",
                    Time = "Monday",
                    Count = "8"
                }
            };
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the collection of Badge Model
        /// </summary>
        public ObservableCollection<NotificationModel> Collection { get; set; }

        #endregion Properties
    }
}
