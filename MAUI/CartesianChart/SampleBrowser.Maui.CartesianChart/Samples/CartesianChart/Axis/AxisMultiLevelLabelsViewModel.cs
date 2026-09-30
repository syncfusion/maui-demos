using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public class AxisMultiLevelLabelsViewModel : BaseViewModel
    {
        public List<ChartDataModel> DataCollection { get; set; }

        public AxisMultiLevelLabelsViewModel()
        {

            DataCollection = new List<ChartDataModel>()
            {
                new ChartDataModel("Jan", 60),
                new ChartDataModel("Feb", 80),
                new ChartDataModel("Mar", 70),
                new ChartDataModel("Apr", 60),
                new ChartDataModel("May", 90),
                new ChartDataModel("Jun", 60),
                new ChartDataModel("Jul", 120),
                new ChartDataModel("Aug", 150),
                new ChartDataModel("Sep", 110),
                new ChartDataModel("Oct", 140),
                new ChartDataModel("Nov", 180), 
                new ChartDataModel("Dec", 150),
            };
        }
    }
}
