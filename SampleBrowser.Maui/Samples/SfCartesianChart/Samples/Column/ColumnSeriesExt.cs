#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
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

        
        protected override void DrawDataLabel(ICanvas canvas, Brush fillcolor, string label, PointF point, int index)
        {
            var items = ItemsSource as ObservableCollection<ChartDataModel>;
            if(items != null)
            {
                var text = items[index].Name;
                base.DrawDataLabel(canvas, new SolidColorBrush(Colors.Transparent), label, point, index);
                base.DrawDataLabel(canvas, new SolidColorBrush(Colors.Transparent), text, new PointF(point.X, point.Y - 30), index);
            }
        }
    }

    public class ColumnSegmentExt : ColumnSegment
    {
        RectangleF trackRect = RectangleF.Zero;
        RectangleF waveRect = RectangleF.Zero;
        float curveHeight;
        float waveLeft;
        float waveRight;
        float waveTop;
        float waveBottom;
        float freq;
        protected override void OnLayout()
        {
            base.OnLayout();

            if (Series != null)
            {
                var yAxis = (Series as CartesianSeries).ActualYAxis as NumericalAxis;
                var top = yAxis.ValueToPoint((double)yAxis.Maximum);

                trackRect = new RectangleF() { Left = Left, Top = (float)top, Right = Right, Bottom = Bottom };
                curveHeight = (float)trackRect.Height / 20;
                var width = (float)trackRect.Width + (float)Math.Sqrt((trackRect.Width * trackRect.Width) + (trackRect.Height * trackRect.Height));
                waveLeft = trackRect.Left;
                waveRight = waveLeft + width;
                waveTop = trackRect.Bottom;
                waveBottom = trackRect.Bottom + trackRect.Height;

                waveRect = new Rectangle() { Left = waveLeft, Top = waveTop, Right = waveRight, Bottom = waveBottom };

                Animate();
            }
        }

        protected override void Draw(ICanvas canvas)
        {
            canvas.SaveState();

            if ((bool)(InvokeInternalMethod(typeof(ChartSeries), Series, "CanAnimate", null)))
            {
                OnLayout();
            }

            DrawTrackPath(canvas, trackRect);

            var color = (Background as SolidColorBrush).Color;

            canvas.SetFillPaint(new SolidColorBrush(color.MultiplyAlpha(0.6f)), waveRect);
            DrawWave(canvas, new Rectangle(new Point(waveLeft - 30 - freq, waveTop - freq), waveRect.Size));

            canvas.SetFillPaint(Background, waveRect);
            DrawWave(canvas, new Rectangle(new Point(waveLeft - freq, waveTop - freq), waveRect.Size));

            canvas.RestoreState();
        }

        private void DrawTrackPath(ICanvas canvas, RectangleF trackRect)
        {
            PathF path = new PathF();
            path.MoveTo(trackRect.Left, trackRect.Bottom);
            path.LineTo(trackRect.Left, trackRect.Top);
            path.LineTo(trackRect.Right, trackRect.Top);
            path.LineTo(trackRect.Right, trackRect.Bottom);

            path.Close();
            canvas.ClipPath(path);

            canvas.SetFillPaint(new SolidColorBrush(mauiColor.FromArgb("#f7f7f7")), trackRect);
            canvas.FillPath(path);
        }

        private void DrawWave(ICanvas canvas, RectangleF rectangle)
        {
            PathF path = new PathF();

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

        private void Animate()
        {
            freq = trackRect.Bottom - Top;
            freq = freq * AnimatedValue;
        }

        public static object InvokeInternalMethod(Type type, object obj, string methodName, params object[] args)
        {
            var method = type.GetTypeInfo().GetDeclaredMethod(methodName);
            return method != null ? method.Invoke(obj, args) : null;
        }
    }
}
