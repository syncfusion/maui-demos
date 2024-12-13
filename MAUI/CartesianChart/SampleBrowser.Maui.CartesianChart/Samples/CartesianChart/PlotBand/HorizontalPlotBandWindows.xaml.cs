#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Inputs;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class HorizontalPlotBandWindows : SampleView
    {
        public HorizontalPlotBandWindows()
        {
            InitializeComponent();
            YAxis.PlotBands = ViewModel.NumericalPlotBandFillCollection;
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            horizontalPlotBandWindowsChart.Handler?.DisconnectHandler();
        }

        private void ComboBox_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var comboBox = sender as SfComboBox;
            if (comboBox!.SelectedIndex == 0)
            {
                YAxis.PlotBands = ViewModel.NumericalPlotBandFillCollection;
            }
            else if (comboBox.SelectedIndex == 1)
            {
                YAxis.PlotBands = ViewModel.NumericalPlotBandLineCollection;
                YAxis.PlotOffsetEnd = 5;
            }
            else if (comboBox.SelectedIndex == 2)
            {
                YAxis.PlotBands = ViewModel.SegmentPlotBandCollectionWindows;
            }
        }
    }
}
