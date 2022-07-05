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

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class Column_RoundedEdges : SampleView
    {
        public Column_RoundedEdges()
        {
            InitializeComponent();
        }
        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Chart1.Handler?.DisconnectHandler();
        }
    }

    public class RoundedColumnSeries : ColumnSeries
    {
        protected override void DrawSeries(ICanvas canvas, ReadOnlyObservableCollection<ChartSegment> segments, RectF clipRect)
        {
            base.DrawSeries(canvas, segments, clipRect);
            if (ActualXAxis == null || ActualYAxis == null) return;
            var x = ActualXAxis.ValueToPoint(0);
            var y = ActualYAxis.ValueToPoint(50);

            canvas.SaveState();
            var color = Color.FromArgb("#F06C64");
            var text = "Overflow";
            var size = text.Measure(12);
            var texty = y - (float)size.Height;
            var textx = x + (float)size.Width + (float)size.Width / 2;

            if (!(BaseConfig.RunTimeDeviceLayout == SBLayout.Mobile))
            {
                textx = clipRect.Left + (2 * (float)size.Width);
            }

            canvas.StrokeColor = color;
            canvas.StrokeSize = 2;
            canvas.StrokeDashPattern = new float[] { 15, 6, 5, 3 };

            canvas.FontColor = color;
            //canvas.SetToBoldSystemFont();
            canvas.FontSize = 15;
            canvas.DrawString(text, textx, texty, HorizontalAlignment.Center);
            canvas.DrawLine(x, y, clipRect.Right, y);
            canvas.RestoreState();
        }
    }
}
