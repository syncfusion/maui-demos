﻿#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Core;

namespace SampleBrowser.Maui.SfCircularChart
{
    public partial class DataLabel : SampleView
    {
        public DataLabel()
        {
            InitializeComponent();
            if (RunTimeDevice.IsMobileDevice())
            {
                Chart.HeightRequest = 510;
            }
            else
            {
                Chart.VerticalOptions = LayoutOptions.Fill;
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            Chart.Handler?.DisconnectHandler();
        }

        public override void OnExpandedViewDisappearing(View view)
        {
            base.OnExpandedViewDisappearing(view);

            view.Handler?.DisconnectHandler();
        }
    }
}
