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
    public class StackedLineViewModel: BaseViewModel
    {
        public ObservableCollection<ChartDataModel> StackedLineData { get; set; }
        public StackedLineViewModel()
        {
            StackedLineData = new ObservableCollection<ChartDataModel>()
            {
                new ChartDataModel("2014",3.2,2.5,5.7,7.4),
                new ChartDataModel("2015",2.2,2.9,5.7,7),
                new ChartDataModel("2016",1.9,1.8,4.5,6.8),
                new ChartDataModel("2017",2.7,2.5,3.5,6.9),
                new ChartDataModel("2018",1.4,3,4,6.7),
                new ChartDataModel("2019",1.6,2.5,3.5,6),
            };
        }
    }
}