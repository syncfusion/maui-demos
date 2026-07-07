using System;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public class SelectionViewModel : BaseViewModel
    {
        public ObservableCollection<ChartDataModel> SelectionData { get; set; }

        public SelectionViewModel()
        {
            DateTime date = new(2017, 3, 01);
            Random r = new();
            SelectionData = new ObservableCollection<ChartDataModel>();
            for (int i = 0; i < 20; i++)
            {
                SelectionData.Add(new ChartDataModel(date, r.Next(10, 65)));
                date = date.AddHours(1);
            }
        }
    }
}
