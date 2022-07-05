#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public class TooltipViewModel : BaseViewModel
    {
        public ObservableCollection<ChartDataModel> ChartData1 { get; set; }

        public TooltipViewModel()
        {
            ChartData1 = new ObservableCollection<ChartDataModel>()
            {
                new ChartDataModel(2004, 42.630000, 34.730000),
                new ChartDataModel( 2005,43.320000, 40.400000),
                new ChartDataModel( 2006,43.660000, 38.090000),
                new ChartDataModel( 2007,43.540000, 45.710000),
                new ChartDataModel( 2008,43.600000, 46.320000),
                new ChartDataModel( 2009,43.500000, 47.200000),
                new ChartDataModel( 2010,43.350000, 47.990000),
                new ChartDataModel( 2011,43.620000, 50.170000),
                new ChartDataModel( 2012,43.930000, 51.640000),
            };
        }
    }
}
