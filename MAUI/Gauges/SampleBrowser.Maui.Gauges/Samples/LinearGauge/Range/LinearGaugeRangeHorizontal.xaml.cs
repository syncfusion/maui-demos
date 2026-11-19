using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Gauges;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge
{
    public partial class LinearGaugeRangeHorizontal : ContentView
    {
        public LinearGaugeRangeHorizontal()
        {
            InitializeComponent();
        }
    }

    public class CustomLinearRange : LinearRange
    {
        protected override void UpdateMidRangePath(PathF pathF, PointF startPoint, PointF midPoint, PointF endPoint)
        {
            pathF.CurveTo(startPoint, midPoint, endPoint);
        }
    }
}