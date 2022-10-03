#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Graphics.Internals;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.CircularChart.SfCircularChart
{
    public partial class DoughnutChart : SampleView
    {
        public DoughnutChart()
        {
            InitializeComponent();
            chart.SelectionChanged += Chart_SelectionChanged;
        }

        private void Chart_SelectionChanged(object? sender, ChartSelectionChangedEventArgs e)
        {
            if (e.CurrentIndex == e.PreviousIndex)
            {
                series.ExplodeIndex = -1;
            }
            else if (e.CurrentIndex != -1)
            {
                series.SelectionBrush = doughnutViewModel.PaletteBrushes[e.CurrentIndex % 5];
                series.ExplodeIndex = e.CurrentIndex;
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            chart.Handler?.DisconnectHandler();
        }
    }

    public class DoughnutSeriesExt : DoughnutSeries
    {
        ChartDataModel? selectedModel;

        protected override void DrawSeries(ICanvas canvas, ReadOnlyObservableCollection<ChartSegment> segments, RectF clipRect)
        {
            base.DrawSeries(canvas, segments, clipRect);
            var center = clipRect.Center;
            var datas = ItemsSource as ObservableCollection<ChartDataModel>;
            if (datas == null) return;
            if (ExplodeIndex != -1)
            {
                selectedModel = datas[ExplodeIndex];
            }

            if (selectedModel != null)
            {
                var txt1 = selectedModel.Name ?? "";
                var size = txt1.Measure(12);

                var labelStyle = new ChartLabelStyle();
                labelStyle.FontSize = 12;
                labelStyle.TextColor = Colors.Black;

                canvas.DrawText(txt1, center.X - (float)size.Width / 2, center.Y - (float)size.Height, labelStyle);
                var sum = datas.Sum(item => item.Value);
                var SelectedItemsPercentage = selectedModel.Value / sum * 100;
                SelectedItemsPercentage = Math.Floor(SelectedItemsPercentage * 100) / 100;
                var txt2 = SelectedItemsPercentage.ToString("0'%");
                size = txt2.Measure(12);

                canvas.DrawText(txt2, center.X - (float)size.Width / 2, center.Y + (float)size.Height / 2, labelStyle);
            }
        }
    }
}