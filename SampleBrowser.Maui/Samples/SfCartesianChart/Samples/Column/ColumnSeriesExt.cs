﻿#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Charts;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using mauiColor = Microsoft.Maui.Graphics.Color;
namespace SampleBrowser.Maui.SfCartesianChart
{
    public class ColumnSeriesExt : ColumnSeries
    {
        protected override ChartSegment CreateSegment()
        {
            return new ColumnSegmentExt();
        }


        protected override void DrawDataLabel(ICanvas canvas, Brush? fillcolor, string label, PointF point, int index)
        {
            var items = ItemsSource as ObservableCollection<ChartDataModel>;
            if (items != null)
            {
                var text = items[index].Name ?? "";
                base.DrawDataLabel(canvas, new SolidColorBrush(Colors.Transparent), label, point, index);
                base.DrawDataLabel(canvas, new SolidColorBrush(Colors.Transparent), text, new PointF(point.X, point.Y - 30), index);
            }
        }
    }

    public class ColumnSegmentExt : ColumnSegment
    {
        float curveHeight;
        float curveXGape = 30;
        float curveYGape = 20;

        protected override void Draw(ICanvas canvas)
        {
            base.Draw(canvas);

            if (Series is CartesianSeries series && series.ActualYAxis is NumericalAxis yAxis)
            {
                var top = yAxis.ValueToPoint(Convert.ToDouble(yAxis.Maximum ?? double.NaN));

                var trackRect = new RectF() { Left = Left, Top = (float)top, Right = Right, Bottom = Bottom };
                curveHeight = (float)trackRect.Height / curveYGape;
                var width = (float)trackRect.Width + (float)Math.Sqrt((trackRect.Width * trackRect.Width) + (trackRect.Height * trackRect.Height));
                var waveLeft = trackRect.Left;
                var waveRight = waveLeft + width;
                var waveTop = trackRect.Bottom;
                var waveBottom = trackRect.Bottom + trackRect.Height;

                var waveRect = new Rect() { Left = waveLeft, Top = waveTop, Right = waveRight, Bottom = waveBottom };

                float freq = trackRect.Bottom - Top;

                canvas.SaveState();

                DrawTrackPath(canvas, trackRect);

                var color = (Fill is SolidColorBrush brush) ? brush.Color : Colors.Transparent;

                canvas.SetFillPaint(new SolidColorBrush(color.MultiplyAlpha(0.6f)), waveRect);
                DrawWave(canvas, new Rect(new Point(waveLeft - curveXGape - freq, waveTop - freq), waveRect.Size));

                canvas.SetFillPaint(Fill, waveRect);
                DrawWave(canvas, new Rect(new Point(waveLeft - freq, waveTop - freq), waveRect.Size));

                canvas.RestoreState();
            }
        }

        private void DrawTrackPath(ICanvas canvas, RectF trackRect)
        {
            PathF path = new();
            path.MoveTo(trackRect.Left, trackRect.Bottom);
            path.LineTo(trackRect.Left, trackRect.Top);
            path.LineTo(trackRect.Right, trackRect.Top);
            path.LineTo(trackRect.Right, trackRect.Bottom);

            path.Close();
            canvas.ClipPath(path);

            canvas.SetFillPaint(new SolidColorBrush(mauiColor.FromArgb("#f7f7f7")), trackRect);
            canvas.FillPath(path);
        }

        private void DrawWave(ICanvas canvas, RectF rectangle)
        {
            PathF path = new();

            path.MoveTo(rectangle.Left, rectangle.Bottom);
            path.LineTo(rectangle.Left, rectangle.Top);

            var top = rectangle.Top;
            var start = rectangle.Left;
            var split = rectangle.Width / 5;
            do
            {
                var next = start + split;

                var crX = start + (split / 2);
                var crY = top - curveHeight;
                var crY2 = top + curveHeight;

                path.CurveTo(crX, crY, crX, crY2, next, top);

                start = next;
            } while (start <= rectangle.Right);

            path.LineTo(rectangle.Right, rectangle.Bottom);
            path.Close();
            canvas.FillPath(path);
        }
    }
}
