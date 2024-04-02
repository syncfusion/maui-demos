#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Base;

namespace SampleBrowser.Maui.CircularChart.SfCircularChart
{ 
    public partial class Selection : SampleView
    {
        readonly SelectionViewModel? model;

        public Selection()
        {
            InitializeComponent();
            dataPointSelection.SelectionChanging += Chart_SelectionChanging;
            model = chart1.BindingContext as SelectionViewModel;
        }

        private void Chart_SelectionChanging(object? sender, Syncfusion.Maui.Charts.ChartSelectionChangingEventArgs e)
        {
            if (model == null) return;

            if (e.OldIndexes.Count > 0 && e.NewIndexes.Count == 0)
            {
                series1.PaletteBrushes = model.ThemePaletteBrushes;
            }

            foreach (var index in e.NewIndexes)
            {
                series1.PaletteBrushes = model.SelectionBrushes;
                if (model.ThemePaletteBrushes[index] is SolidColorBrush brush)
                    dataPointSelection.SelectionBrush = brush;
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            chart1.Handler?.DisconnectHandler();
        }
    }
}
