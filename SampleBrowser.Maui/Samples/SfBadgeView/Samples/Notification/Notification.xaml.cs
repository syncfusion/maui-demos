#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using SampleBrowser.Maui.Core;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.TabView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


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
            
            //TODO: Badge renders far away from the icons in Windows. Below is the workaround for that.
            if (!RunTimeDevice.IsMobileDevice())
            {
                statusBadgeGrid.WidthRequest = 100;
                callsBadgeGrid.WidthRequest = 100;
                chatBadgeGrid.WidthRequest = 100;
            }
        }

    #endregion
    }
}