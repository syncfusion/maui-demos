using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfCartesianChart
{
    public class ZoomViewModel : BaseViewModel
    {
        public ObservableCollection<ChartDataModel> ZoomData { get; set; }

        public ZoomViewModel()
        {
            DateTime date = new DateTime(1950, 3, 01);
            Random r = new Random();
            ZoomData = new ObservableCollection<ChartDataModel>();
            for (int i = 0; i < 20; i++)
            {
                ZoomData.Add(new ChartDataModel(date, r.Next(45, 75)));
                date = date.AddDays(5);
            }
        }
    }
}
