﻿#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfCartesianChart
{
    public class BarSeriesViewModel : BaseViewModel
    {
        public ObservableCollection<ChartDataModel> BarData1 { get; set; }

        public ObservableCollection<ChartDataModel> BarData2 { get; set; }
        public ObservableCollection<ChartDataModel> BarData3 { get; set; }
        public ObservableCollection<ChartDataModel> RundedBarData { get; set; }

        public BarSeriesViewModel()
        {
            EnableAnimation = true;

            RundedBarData = new ObservableCollection<ChartDataModel>()
            {
                new ChartDataModel("Boat", 9.872),
                new ChartDataModel("Walk", 5.237),
                new ChartDataModel("Plane", 9.0437),
                new ChartDataModel("Bike", 4.11),
                new ChartDataModel("Car", 8.43),
            };

            BarData1 = new ObservableCollection<ChartDataModel>
            {
                new ChartDataModel("Facebook", 4.119),
                new ChartDataModel("FB Messenger", 3.408),
                new ChartDataModel("WhatsApp", 2.979),
                new ChartDataModel("Instagram", 1.843),
                new ChartDataModel("Skype", 1.039),
                new ChartDataModel("Subway Surfers", 1.025),

            };

            BarData2 = new ObservableCollection<ChartDataModel>
            {
                new ChartDataModel("Egg", 2.2),
                new ChartDataModel("Fish", 2.4),
                new ChartDataModel("Misc", 3),
                new ChartDataModel("Tea", 3.1),
            };

            BarData3 = new ObservableCollection<ChartDataModel>
            {
                new ChartDataModel("Egg", 1.2),
                new ChartDataModel("Fish", 1.3),
                new ChartDataModel("Misc", 1.5),
                new ChartDataModel("Tea", 2.2),
            };
        }
    }
}
