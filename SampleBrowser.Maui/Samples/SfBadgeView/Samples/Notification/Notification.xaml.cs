﻿#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls.Xaml;
using SampleBrowser.Maui.Core;
using System.Threading.Tasks;


namespace SampleBrowser.Maui.SfBadgeView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notification : SampleView
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification" /> class.
        /// </summary>
        public Notification()
        {
            InitializeComponent();

            this.DynamicUpdate();

            //TODO: Badge renders far away from the icons in Windows. Below is the workaround for that.
            if (!RunTimeDevice.IsMobileDevice())
            {
                statusBadgeGrid.WidthRequest = 100;
                callsBadgeGrid.WidthRequest = 100;
                chatBadgeGrid.WidthRequest = 100;
            }
        }

        #endregion

        #region Methods

        private async void DynamicUpdate()
        {
            double badgeText = 1;
            while (true)
            {
                badgeText += 1;
                this.chatBadge.BadgeText = badgeText.ToString();
                await Task.Delay(2000);
            }
        }

        #endregion
    }
}