using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class CategoryAxisChart : SampleView
    {
        public CategoryAxisChart()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            categoryChart.Handler?.DisconnectHandler();
        }
    }
}
