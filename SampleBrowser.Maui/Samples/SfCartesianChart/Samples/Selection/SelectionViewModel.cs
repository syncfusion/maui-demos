using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfCartesianChart
{
    public class SelectionViewModel : BaseViewModel
    {
        public ObservableCollection<ChartDataModel> SelectionData { get; set; }

        public SelectionViewModel()
        {
            DateTime date = new DateTime(2017, 3, 01);
            Random r = new Random();
            SelectionData = new ObservableCollection<ChartDataModel>();
            for (int i = 0; i < 20; i++)
            {
                SelectionData.Add(new ChartDataModel(date, r.Next(10, 65)));
                date = date.AddHours(1);
            }
        }
    }
}
