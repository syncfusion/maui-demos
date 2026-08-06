using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.PyramidChart.SfPyramidChart
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
