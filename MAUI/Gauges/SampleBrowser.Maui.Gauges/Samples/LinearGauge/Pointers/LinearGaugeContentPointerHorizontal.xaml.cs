using Microsoft.Maui.Controls;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge
{
    public partial class LinearGaugeContentPointerHorizontal : ContentView
    {
        public LinearGaugeContentPointerHorizontal()
        {
            InitializeComponent();
        }

        private void Pointer1_ValueChanged(object? sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
        {
            textPointerLabel.Text = ((int)e.Value).ToString();
        }

        private void Pointer2_ValueChanged(object? sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
        {
            multiPointerLabel.Text = ((int)e.Value).ToString();
        }
    }
}