#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge
{
    public class ThermometerGauge1 : Syncfusion.Maui.Gauges.SfLinearGauge
    {
        public override System.Collections.Generic.List<GaugeLabelInfo> GenerateVisibleLabels()
        {
            System.Collections.Generic.List<GaugeLabelInfo> customLabels = new();
            customLabels.Add(new GaugeLabelInfo { Value = -20, Text = "-20" });
            customLabels.Add(new GaugeLabelInfo { Value = -10, Text = "-10" });
            customLabels.Add(new GaugeLabelInfo { Value = 0, Text = "0" });
            customLabels.Add(new GaugeLabelInfo { Value = 10, Text = "10" });
            customLabels.Add(new GaugeLabelInfo { Value = 20, Text = "20" });
            customLabels.Add(new GaugeLabelInfo { Value = 30, Text = "30" });
            customLabels.Add(new GaugeLabelInfo { Value = 40, Text = "40" });
            customLabels.Add(new GaugeLabelInfo { Value = 50, Text = "50" });

            return customLabels;

        }
    }

    public class ThermometerGauge2 : Syncfusion.Maui.Gauges.SfLinearGauge
    {
        public override System.Collections.Generic.List<GaugeLabelInfo> GenerateVisibleLabels()
        {
            System.Collections.Generic.List<GaugeLabelInfo> customLabels = new();

            customLabels.Add(new GaugeLabelInfo { Value = -20, Text = "0" });
            customLabels.Add(new GaugeLabelInfo { Value = -8.5, Text = "20" });
            customLabels.Add(new GaugeLabelInfo { Value = 3, Text = "40" });
            customLabels.Add(new GaugeLabelInfo { Value = 14.5, Text = "60" });
            customLabels.Add(new GaugeLabelInfo { Value = 26, Text = "80" });
            customLabels.Add(new GaugeLabelInfo { Value = 37.5, Text = "100" });
            customLabels.Add(new GaugeLabelInfo { Value = 50, Text = "120" });
            return customLabels;
        }
    }

    public class TaskTrackerGauge : Syncfusion.Maui.Gauges.SfLinearGauge
    {
        public override System.Collections.Generic.List<GaugeLabelInfo> GenerateVisibleLabels()
        {
            System.Collections.Generic.List<GaugeLabelInfo> customLabels = new();

            customLabels.Add(new GaugeLabelInfo { Value = 0, Text = "Apr 19" });
            customLabels.Add(new GaugeLabelInfo { Value = 20, Text = "Jul 19" });
            customLabels.Add(new GaugeLabelInfo { Value = 40, Text = "Oct 19" });
            customLabels.Add(new GaugeLabelInfo { Value = 60, Text = "Jan 20" });
            customLabels.Add(new GaugeLabelInfo { Value = 80, Text = "Apr 20" });
            customLabels.Add(new GaugeLabelInfo { Value = 100, Text = "Jul 20" });

            return customLabels;
        }
    }

    public class SleepWatchScoreGauge : Syncfusion.Maui.Gauges.SfLinearGauge
    {
        public override System.Collections.Generic.List<GaugeLabelInfo> GenerateVisibleLabels()
        {
            System.Collections.Generic.List<GaugeLabelInfo> customLabels = new();

            customLabels.Add(new GaugeLabelInfo { Value = 150, Text = "Poor" });
            customLabels.Add(new GaugeLabelInfo { Value = 250, Text = "Fair" });
            customLabels.Add(new GaugeLabelInfo { Value = 350, Text = "Good" });
            customLabels.Add(new GaugeLabelInfo { Value = 450, Text = "Excellent" });

            return customLabels;
        }
    }

    public class ActiveHoursGauge : Syncfusion.Maui.Gauges.SfLinearGauge
    {
        public override System.Collections.Generic.List<GaugeLabelInfo> GenerateVisibleLabels()
        {
            System.Collections.Generic.List<GaugeLabelInfo> customLabels = new();

            customLabels.Add(new GaugeLabelInfo { Value = 0, Text = "00:00" });
            customLabels.Add(new GaugeLabelInfo { Value = 6, Text = "06:00" });
            customLabels.Add(new GaugeLabelInfo { Value = 12, Text = "12:00" });
            customLabels.Add(new GaugeLabelInfo { Value = 18, Text = "18:00" });

            return customLabels;
        }
    }

    public class GraphicsDrawable : IDrawable
    {

        public double WaterLevelFactor { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();
            float height = dirtyRect.Height;
            float width = dirtyRect.Width;
            PathF pathF = new();
            pathF.LineTo(width - 2, 2);
            pathF.LineTo(width * 0.75f, height - 15);
            pathF.QuadTo(width * 0.74f, height, width * 0.67f, height);
            pathF.LineTo(width * 0.33f, height);
            pathF.QuadTo(width * 0.26f, height, width * 0.25f, height - 15);
            pathF.LineTo(2, 2);
            pathF.Close();

            canvas.StrokeColor = Colors.Gray;
            canvas.DrawPath(pathF);
            canvas.RestoreState();

            canvas.SaveState();

            float y = (float)WaterLevelFactor * height;

            canvas.ClipRectangle(0, y, width, height);
            canvas.FillColor = Color.FromArgb("00a2ed");
            canvas.FillPath(pathF);
            canvas.RestoreState();
        }
    }
}
