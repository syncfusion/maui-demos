#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using PickerControl = Microsoft.Maui.Controls.Picker;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class HorizontalPlotBandWindows : SampleView
    {
        public HorizontalPlotBandWindows()
        {
            InitializeComponent();
            YAxis.PlotBands = ViewModel.NumericalPlotBandFillCollection;
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as PickerControl;
            if (picker!.SelectedIndex == 0)
            {       
                YAxis.PlotBands = ViewModel.NumericalPlotBandFillCollection;
            }
            else if (picker.SelectedIndex == 1)
            {
                YAxis.PlotBands = ViewModel.NumericalPlotBandLineCollection;
                YAxis.PlotOffsetEnd = 5;
            }
            else if (picker.SelectedIndex == 2)
            {                
                YAxis.PlotBands = ViewModel.SegmentPlotBandCollectionWindows;
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            horizontalPlotBandWindowsChart.Handler?.DisconnectHandler();
        }
    }
}
