#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public class StackedLine100ViewModel : BaseViewModel
    {
        public ObservableCollection<ChartDataModel> StackedLineData { get; set; }
        public StackedLine100ViewModel()
        {
            StackedLineData = new ObservableCollection<ChartDataModel>()
            {
               new ChartDataModel("US",37.40,35.70,8.50,18.40),
               new ChartDataModel("Switzerland",26,37,13,24),
               new ChartDataModel("Austria",30,33,12,25),
               new ChartDataModel("Bulgaria",28,37,13,22),
               new ChartDataModel("Poland",26.60,31.26,15.73,26.41),
               new ChartDataModel("Myanmar",35.70,23.80,32.70,7.80),
            };
        }
    }
}