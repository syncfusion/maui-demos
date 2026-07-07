using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.FunnelChart.SfFunnelChart
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
