#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.SfCircularChart
{
    public class PieSeriesViewModel : BaseViewModel
    {
        public ObservableCollection<ChartDataModel> PieSeriesData { get; set; }
        public ObservableCollection<ChartDataModel> SemiCircularData { get; set; }
        public PieSeriesViewModel()
        {
            PieSeriesData = new ObservableCollection<ChartDataModel>
            {
                new ChartDataModel("David", 16.6),
                new ChartDataModel("Steve", 14.6),
                new ChartDataModel("Jack", 18.6),
                new ChartDataModel("John", 20.5),
                new ChartDataModel("Regev", 39.5),
                //new ChartDataModel("Alise", 17.6),
                //new ChartDataModel("Stephen", 15),
           };

            SemiCircularData = new ObservableCollection<ChartDataModel>
            {
                new ChartDataModel("Product A", 750),
                new ChartDataModel("Product B", 463),
                new ChartDataModel("Product C", 389),
                new ChartDataModel("Product D", 697),
                new ChartDataModel("Product E", 251)
            };
        }
    }
}
