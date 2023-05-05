#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.CircularChart.SfCircularChart
{
    public class DoughnutSeriesViewModel : BaseViewModel
    {
        public ObservableCollection<ChartDataModel> DoughnutSeriesData { get; set; }
        public ObservableCollection<ChartDataModel> SemiCircularData { get; set; }
        public ObservableCollection<ChartDataModel> CenterElevationData { get; set; }

        private int selectedIndex = 1;        
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                UpdateIndex(value);
                base.OnPropertyChanged("SelectedIndex");
            }
        }
        private string name = "";
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                base.OnPropertyChanged("Name");
            }
        }
        private int value1;
        public int Value
        {
            get { return value1; }
            set
            {
                value1 = value;
                base.OnPropertyChanged("Value");
            }
        }

        private int total = 357580;
        public int Total
        {
            get { return total; }
            set
            {
                total = value;
            }
        }

        private void UpdateIndex(int value)
        {
            if (value >= 0 && value < DoughnutSeriesData.Count)
            {
                var model = DoughnutSeriesData[value];
                if(model != null && model.Name != null)
                {
                    Name = model.Name;
                    double sum = DoughnutSeriesData.Sum(item => item.Value);
                    double SelectedItemsPercentage = model.Value / sum * 100;
                    SelectedItemsPercentage = Math.Floor(SelectedItemsPercentage * 100) / 100;
                    Value = (int)SelectedItemsPercentage;
                }               
            }
        }

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

            CenterElevationData = new ObservableCollection<ChartDataModel>
            {
                new ChartDataModel("Agriculture",51),
                new ChartDataModel("Forest",30),
                new ChartDataModel("Water",5.2),
                new ChartDataModel("Others",14),
            };
        }

    }
}
