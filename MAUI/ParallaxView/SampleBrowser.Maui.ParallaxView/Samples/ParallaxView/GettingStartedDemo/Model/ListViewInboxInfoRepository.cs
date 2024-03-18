#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.ParallaxView.SfParallaxView
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
                    IsOpened = false,
                };
                record.IsFavorite = (i < 25 && i % 2 == 0) ? true : false;
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
            "M",
            "MV",
            "MV",
            "T",
            "M",
            "LI",
            "M",
            "M",
            "SO",
            "OT",
            "MO",
            "MA",
            "BT",
            "M",
            "M",
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

        string[] Month = new string[]
        {
            "Jan",
            "Jan",
            "Jan",
            "Jan",
            "Jan",
            "Feb",
            "Feb",
            "Feb",
            "Mar",
            "Mar",
            "Mar",
            "Mar",
            "Mar",
            "Apr",
            "Apr",
            "Apr",
            "May",
            "May",
            "May",
            "May",
            "May",
            "May",
            "May",
            "June",
            "June",
        };

        string[] Subject = new string[]
        {
            "Happy birthday to an amazing employee!",
            "Happy New Year!",
            "Dev Essentials: Learn about the future of .NET and celebrate Visual Studio's 25th anniversary",
            "Your daily briefing",
            "Your digest email",
            "Get well soon!!",
            "Merry Christmas!",
            "Be more recognizable",
            "Dev Essentials: Announcing that the .NET Multiplatform App UI is generally available",
            "You have two new messages",
            "Happy Halloween!",
            "Start learning .NET MAUI and discover a new AI pair programmer",
            "Happy Thanksgiving!!",
             "Dev Essentials: Learn how to code with Java",
            "Your friendly, fear-free guide to getting started",
            "Get to know what's new in Outlook",
            "Microsoft Outlook test message",
            "Happy St. Patrick's Day!",
            "Congratulations on the move!",
            "Never doubt yourself. You’re always...",
            "My Analytics | Collaboration Edition",
            "You've joined the Blog Team Site group",
            "Microsoft .NET News: Get started with .NET 6.0 and watch sessions from .NET Conf 2022 on demand",
            "Microsoft .NET News: Learn about the new tools and updates for .NET developers",
            "Warmest wishes...",
        };

        string[] Descriptions = new string[] {
            "Happy birthday to one of the best and most loyal employees ever!",
            "May you be blessed with health, wealth and happiness this new year.",
            "Developer news updates and training resources.",
            "Dear developer- It's almost the end of the week",
            "Dear developer- Discover trends in your work habits",
            "Wishing you a speedy recovery. Get well soon!",
            "Wishing you a happy Christmas. May your Christmas be filled with love, happiness, and prosperity.",
            "Stand out with a profile photo.",
            "One codebase, many platforms: .NET Multiplatform App UI is generally available.",
            "You have two new messages.",
            "Wishing you a night full of frights and a bag full of delights..",
            "Explore resources to get started with .NET MAUI.",
            "Wishing you hope, joy, peace, good health and love on this Thanksgiving Day!",
            "Get Started: Java for Beginners",
            "How to learn and get started with Stack Overflow.",
            "Hello and welcome to Outlook.",
            "This email message was sent automatically by Microsoft Outlook while testing the settings of your account.",
            "May you find lots o' gold at the end of your rainbow this St. Patrick's Day!",
            "Congratulations! May you find great happiness in your new home.",
            "Never doubt yourself. You’re always the best! Just continue to be like that!",
            "Discover your habits. Work smarter.",
            "Welcome to the Blog Team Site group.",
            "The Xamarin Newsletter is now .NET News.",
            "Now available: Visual Studio 2019 version 16.9.",
            "Happy wedding anniversary to you both. You are special.",
        };

        #endregion
    }
}
