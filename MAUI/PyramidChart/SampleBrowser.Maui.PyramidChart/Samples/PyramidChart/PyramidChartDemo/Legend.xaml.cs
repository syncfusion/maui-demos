using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.PyramidChart.SfPyramidChart
{
    public partial class Legend : SampleView
    {
        public Legend()
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
