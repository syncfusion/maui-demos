using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;

namespace SampleBrowser.Maui.PyramidChart.SfPyramidChart
{
    public partial class DefaultPyramid : SampleView
    {
        public DefaultPyramid()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Chart.Handler?.DisconnectHandler();
        }

        private void Inversed_CheckedChanged(object? sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
        {
            if (sender is Syncfusion.Maui.Buttons.SfCheckBox checkBox)
            {
                bool isChecked = checkBox.IsChecked ?? false;
                Chart.Orientation = isChecked
                    ? ChartOrientation.Horizontal
                    : ChartOrientation.Vertical;
            }
        }
    }
}
