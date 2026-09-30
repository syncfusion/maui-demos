using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public class TapToAddViewModel : BaseViewModel
    {
        private ObservableCollection<ChartDataModel> _items = new ObservableCollection<ChartDataModel>();
        public ObservableCollection<ChartDataModel> LiveChartData
        {
            get { return _items; }
            set
            {
                _items = value;

                OnPropertyChanged("LiveChartData");
            }
        }

        public TapToAddViewModel()
        {
            ResetChartData();
        }

        public void ResetChartData()
        {
            LiveChartData = new ObservableCollection<ChartDataModel>() {
            new ChartDataModel(1, 5),
            new ChartDataModel(2, 8),
            new ChartDataModel(3, 6),
            new ChartDataModel(4, 8),
            new ChartDataModel(5, 7.5)
        };
        }
    }
}
