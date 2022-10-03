#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.ListView.SfListView
{
    public class ListViewInboxInfoRepository
    {
        #region Fields

        private Random random = new Random();

        #endregion

        #region Constructor

        public ListViewInboxInfoRepository()
        {

        }

        #endregion

        #region GetEmployeeInfo

        internal ObservableCollection<ListViewInboxInfo> GetInboxInfo()
        {
            var empInfo = new ObservableCollection<ListViewInboxInfo>();
            int k = 0;
            for (int i = 0; i < Subject.Count(); i++)
            {
                if(k>5)
                {
                    k = 0;
                }
                var record = new ListViewInboxInfo()
                {
                    ProfileName = ProfileList[i],
                    Subject = Subject[i],
                    Description = Descriptions[i],
                    Date = Month[i] + " " + (i + 8).ToString(),
                    Image = Images[k],
                    IsAttached = Attachments[i],
                    IsOpened = false,
                };
                record.IsFavorite = (i < 7 && i % 2 == 0) ? true : false;
                empInfo.Add(record);
                k++;
            }
            return empInfo;
        }

        #endregion

        #region Employee Info

        string[] ProfileList = new string[]
        {
            "JL",
            "DC",
            "HS",
            "JO",
            "FM",
            "RJ",
            "NR",
            "LC",
            "BA",
            "BK",
            "LT",
            "NT",
            "DT",
            "RS",
            "XB"

        };

        string[] Images = new string[]
        {
            "bluecircle.png",
            "greencircle.png",
            "lightbluecircle.png",
            "redcircle.png",
            "violetcircle.png",
            "yellowcircle.png",
        };

        bool[] Attachments = new bool[]
        {
            false,
            false,
            false,
            true,
            false,
            true,
            false,
            true,
            true,
            false,
            false,
            false,
            true,
            false,
            true,
        };


        string[] Month = new string[]
        {
            "Jan",
            "Jan",
            "Feb",
            "Mar",
            "Mar",
            "Apr",
            "May",
            "May",
            "May",
            "June",
            "July",
            "Aug",
            "Aug",
            "Sep",
            "Sep"
        };

        string[] Subject = new string[] 
        {
            "Happy birthday to an amazing employee!",
            "Like a vintage auto, your value increases...",
            "Happy Anniversary! Happy Anniversary!",
            "We wish you an amazing year with accomplishment...",
            "No one could do a better job than...",
            "GET WELL SOON!!",
            "A cheery Christmas hold lots of happiness for you!",
            "BOO!!! Happy Halloween! Happy Halloween!",
            "Happy Turkey Day!!",
            "Wishing you Happy St Pat's Day!",
            "Congratulations on the move!",
            "Enjoy the new greener pastures!",
            "Happy Thanksgiving Day!",
            "Never doubt yourself. You’re always...",
            "The warmest wishes to a great member of our team...",
        };

        string[] Descriptions = new string[] {
            "Wishing you great achievements in this career, And I hope that you have a great day today!",
            "Happy birthday to one of the best and most loyal employees ever!",
            "Congrats! May your life continue to be filled with love, laughter and happiness.",
            "We wish you an amazing year with accomplishment of the great goals that you have set!",
            "No one could do a better job than the job you do. We thank you for sticking with us!",
            "Card messages aren't my thing. Get well soon!",
            "Wishing you a happy Christmas. May it be all that you hope it will be! All the best",
            "Wishing you a killer Halloween, Don't forget to give us treat or else..",
            "Happy Turkey Day!. Don't forget to give thanks for being so blessed.",
            "It's all green which means its all good! HAPPY ST PAT'S",
            "Congratulations! May you find great happiness at your new address.",
            "Good luck with the new job and your career aspirations. All the best",
            "May you enjoy this special day. Happy Thanksgiving to you and your whole family!",
            "Never doubt yourself. You’re always the best! Just continue to be like that!",
            "Warmest wishes! May your special day be full of good emotions, fun and cheer!"
        };

        #endregion
    }
}
