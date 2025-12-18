#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
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

        private void axis_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            if (ViewModel != null)
            {
                var input = checkBox.StyleId;
                switch(input)
                {
                    case "xAxis":
                        dateTimePlotBand.IsVisible = checkBox.IsChecked;
                        break;
                    case "yAxis":
                        numericalPlotBand.IsVisible = checkBox.IsChecked;
                            break;

                }
            }
        }
    }
}