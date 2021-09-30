#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
# endregion

using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using Chart = Syncfusion.Maui.Charts;
using Syncfusion.Maui.Graphics.Internals;
using sfChart = Syncfusion.Maui.Charts;

namespace SampleBrowser.Maui.SfCartesianChart
{
	public partial class ColumnChart : SampleView
	{
		public ColumnChart()
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

	public class ColumnSeries_Rounded : Syncfusion.Maui.Charts.ColumnSeries
	{
        protected override void DrawSeries(ICanvas canvas, RectangleF clipRect)
        {
            base.DrawSeries(canvas, clipRect);

			var x = ActualXAxis.ValueToPoint(0);
			var y = ActualYAxis.ValueToPoint(50);

			canvas.SaveState();
			var color = Color.FromArgb("#F06C64");
			var text = "Overflow";
			var size = text.Measure(12);
			var texty = y - (float)size.Height;
			var textx = x + (float)size.Width + (float)size.Width / 2;

			canvas.StrokeColor = color;
			canvas.StrokeSize = 2;
			canvas.StrokeDashPattern = new float[] { 15, 6, 5, 3 };

			canvas.FontColor = color;
			canvas.SetToBoldSystemFont();
			canvas.FontSize = 15;
			canvas.DrawString(text, textx, texty, HorizontalAlignment.Center);
			canvas.DrawLine(x, y, clipRect.Right, y);
			canvas.RestoreState();

		}
	}

}
