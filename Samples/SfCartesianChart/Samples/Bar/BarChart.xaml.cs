using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Charts;
using mauiColor = Microsoft.Maui.Graphics.Color;
using Chart = Syncfusion.Maui.Charts;
using System.Reflection;

namespace SampleBrowser.Maui.SfCartesianChart
{
	public partial class BarChart : SampleView
	{
		public BarChart()
		{
			InitializeComponent();
		}

        public override void OnExpandedViewAppearing(View view)
        {
            base.OnExpandedViewAppearing(view);
            var content = view as Chart.SfCartesianChart;
            if (content != null && content.BindingContext is DynamicAnimationViewModel)
            {
                viewModel.StopTimer();
                viewModel.StartTimer();
            }
        }

        public override void OnExpandedViewDisappearing(View view)
        {
            base.OnExpandedViewDisappearing(view);
            var content = view as Chart.SfCartesianChart;
            if (content != null && content.BindingContext is DynamicAnimationViewModel)
            {
                viewModel.StopTimer();
            }
        }


        public override void OnScrollingToNewCardViewExt(CardViewExt cardViewExt)
		{
			if (cardViewExt.Title == "Dynamic update animation" && viewModel != null)
			{
                viewModel.StopTimer();
                viewModel.StartTimer();
			}
            else
            {
                viewModel.StopTimer();
                var content = cardViewExt.MainContent as Syncfusion.Maui.Charts.SfCartesianChart;
                content.AnimateSeries();
            }
        }

		public override void OnDisappearing()
		{
			base.OnDisappearing();
			if (viewModel != null)
				viewModel.StopTimer();
		}
	}

    public class CustomBarChart : ColumnSeries
    {
        protected override ChartSegment CreateSegment()
        {
            return new BarSegmentExt();
        }
    }

    public class BarSegmentExt : ColumnSegment
    {
        RectangleF trackRect = RectangleF.Zero;

        protected override void OnLayout()
        {
            base.OnLayout();

            if (Series != null)
            {
                var yAxis = (Series as CartesianSeries).ActualYAxis as NumericalAxis;
                var top = yAxis.ValueToPoint((double)yAxis.Maximum);

                trackRect = new RectangleF() { Left = Left, Top = Top, Right = (float)top, Bottom = Bottom };
            }
        }

        protected override void Draw(ICanvas canvas)
        {
            canvas.SetFillPaint(new SolidColorBrush(mauiColor.FromArgb("#f7f7f7")), trackRect);
            canvas.FillRoundedRectangle(trackRect, 25);

            base.Draw(canvas);
        }
    }

}
