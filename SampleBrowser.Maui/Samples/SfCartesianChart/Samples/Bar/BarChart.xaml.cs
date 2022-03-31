#region Copyright Syncfusion Inc. 2001-2022.
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
using Chart = Syncfusion.Maui.Charts;
using mauiColor = Microsoft.Maui.Graphics.Color;

namespace SampleBrowser.Maui.SfCartesianChart
{
    public partial class BarChart : SampleView
    {
        public BarChart()
        {
            InitializeComponent();

            if (!RunTimeDevice.IsMobileDevice())
                viewModel.StartTimer();
        }

        public override void OnExpandedViewAppearing(View view)
        {
            base.OnExpandedViewAppearing(view);
            var content = view as Chart.SfCartesianChart;
            if (RunTimeDevice.IsMobileDevice() && content != null && content.BindingContext is DynamicAnimationViewModel)
            {
                viewModel.StopTimer();
                viewModel.StartTimer();
            }
        }

        public override void OnExpandedViewDisappearing(View view)
        {
            base.OnExpandedViewDisappearing(view);
            var content = view as Chart.SfCartesianChart;
            if (RunTimeDevice.IsMobileDevice() && content != null && content.BindingContext is DynamicAnimationViewModel)
            {
                viewModel.StopTimer();
            }

            view.Handler?.DisconnectHandler();
        }


        public override void OnScrollingToNewCardViewExt(CardViewExt cardViewExt)
        {
            if (RunTimeDevice.IsMobileDevice())
            {
                if (cardViewExt.Title == "Dynamic update animation")
                {
                    viewModel.StopTimer();
                    viewModel.StartTimer();
                }
                else
                {
                    viewModel.StopTimer();
                    var content = cardViewExt.MainContent as Syncfusion.Maui.Charts.SfCartesianChart;
                    content?.AnimateSeries();
                }
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            if (viewModel != null)
                viewModel.StopTimer();

            Chart1.Handler?.DisconnectHandler();
            Chart2.Handler?.DisconnectHandler();
            Chart3.Handler?.DisconnectHandler();
            Chart4.Handler?.DisconnectHandler();
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
        RectF trackRect = RectF.Zero;

        protected override void OnLayout()
        {
            base.OnLayout();

            if (Series is CartesianSeries series && series.ActualYAxis is NumericalAxis yAxis)
            {
                var top = yAxis.ValueToPoint(Convert.ToDouble(yAxis.Maximum ?? double.NaN));
                trackRect = new RectF() { Left = Left, Top = Top, Right = (float)top, Bottom = Bottom };
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
