#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class AutoScrolling : SampleView
    {
        public AutoScrolling()
        {
            InitializeComponent();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            realTimeViewModel.StopTimer();
            realTimeViewModel.StartTimer();
        }
        
        public override void OnDisappearing()
        {
            base.OnDisappearing();
            if (realTimeViewModel != null)
            {
                realTimeViewModel.StopTimer();
            }

            categoryChart.Handler?.DisconnectHandler();
        }
    }
}
