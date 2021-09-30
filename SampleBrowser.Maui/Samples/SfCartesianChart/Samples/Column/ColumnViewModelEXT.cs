using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfCartesianChart
{
    public class ColumnViewModelEXT : BaseViewModel
    {
        public ObservableCollection<ChartDataModel> ColumnData { get; set; }

        public ColumnViewModelEXT()
        {
            ColumnData = new ObservableCollection<ChartDataModel>()
            {
                //new ChartDataModel("HP Inc", 12.54),
                //new ChartDataModel("Lenovo", 13.46),
                new ChartDataModel("Apple", 4.56),
                new ChartDataModel("Dell" ,9.18),
                new ChartDataModel("ASUS", 5.29),
                //new ChartDataModel("Like", 38000),
                //new ChartDataModel("View", 65000),
                //new ChartDataModel("Share", 25900),
            };
        }
    }
}
