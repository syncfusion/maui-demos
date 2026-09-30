using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class PlotBandRecurrence : SampleView
    {
        public PlotBandRecurrence()
        {
            InitializeComponent();
            InitializePlatBandValues();

        }

        #region InitializePlatBandValues
        private void InitializePlatBandValues()
        {
            dateTimePlotBand.IsVisible = false;
            numericalPlotBand.IsVisible = true;
        }
        #endregion

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            plotBandRecurrenceChart.Handler?.DisconnectHandler();
        }

        private void axis_CheckedChanged(object? sender, CheckedChangedEventArgs e)
        {
            if (sender is not CheckBox checkbox)
                return;

            bool isChecked = e.Value;
            var styleId = checkbox.StyleId;
            if (styleId is null)
                return;

            switch (styleId)
            {
                case "xAxis":
                    if (dateTimePlotBand is not null)
                        dateTimePlotBand.IsVisible = isChecked;
                    break;
                case "yAxis":
                    if (numericalPlotBand is not null)
                        numericalPlotBand.IsVisible = isChecked;
                    break;
            }
        }
    }
}