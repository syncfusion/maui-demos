#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.CircularChart.SfCircularChart
{
    public class DoughnutSeriesViewModel : BaseViewModel
    {
        public ObservableCollection<ChartDataModel> DoughnutSeriesData { get; set; }
        public ObservableCollection<ChartDataModel> SemiCircularData { get; set; }

        public DoughnutSeriesViewModel()
        {
            DoughnutSeriesData = new ObservableCollection<ChartDataModel>
            {
                new ChartDataModel("Labor", 10),
                new ChartDataModel("Legal", 8),
                new ChartDataModel("Production", 7),
                new ChartDataModel("License", 5),
                new ChartDataModel("Facilities", 10),
                new ChartDataModel("Taxes", 6),
                new ChartDataModel("Insurance", 18)
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
