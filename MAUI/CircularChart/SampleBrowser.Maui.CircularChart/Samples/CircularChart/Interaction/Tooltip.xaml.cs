using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.CircularChart.SfCircularChart
{ 
    public partial class Tooltip : SampleView
    {
        public Tooltip()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Chart.Handler?.DisconnectHandler();
        }
    }
}
