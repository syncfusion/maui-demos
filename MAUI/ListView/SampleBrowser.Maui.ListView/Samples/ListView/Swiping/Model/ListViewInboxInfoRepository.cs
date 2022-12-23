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
            "FM",
            "JR",
            "NR",
            "LC",
            "BA",
            "BK",
            "LT",
            "RS",
            "XB",

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
        };

        string[] Subject = new string[] 
        {
            "Happy birthday to an amazing employee!",
            "Happy New Year!",
            "Get well soon!!",
            "Merry Christmas!",
            "Happy Halloween!",
            "Happy Thanksgiving!!",
            "Happy St Patrick's Day!",
            "Congratulations on the move!",
            "Never doubt yourself. You’re always...",
            "Warmest wishes...",
        };

        string[] Descriptions = new string[] {
            "Happy birthday to one of the best and most loyal employees ever!",
            "May you be blessed with health, wealth, and happiness this new year.",
            "Wishing you a speedy recovery. Get well soon!",
            "Wishing you a happy Christmas. May your Christmas be filled with love, happiness, and prosperity.",
            "Wishing you a night full of frights and a bag full of delights..",
            "Wishing you hope, joy, peace, good health, favor, and love on this Thanksgiving Day!",
            "May you find lots 'o' gold at the end of your rainbow this St. Patrick's Day!",
            "Congratulations! May you find great happiness in your new home.",
            "Never doubt yourself. You’re always the best! Just continue to be like that!",
            "Happy wedding anniversary to you both. You are special.",
        };

        #endregion
    }
}
