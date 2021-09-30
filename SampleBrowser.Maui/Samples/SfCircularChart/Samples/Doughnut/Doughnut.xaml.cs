#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
# endregion

using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Graphics.Internals;

namespace SampleBrowser.Maui.SfCircularChart
{
	public partial class DoughnutChart : SampleView
	{
		public DoughnutChart()
		{
			InitializeComponent();
			chart.SelectionChanged += Chart_SelectionChanged;

		}

        private void Chart_SelectionChanged(object sender, SelectionEventArgs e)
        {
            if (e.CurrentIndex == e.PreviousIndex)
            {
                series.ExplodeIndex = -1;
            }
            else if (e.CurrentIndex != -1)
            {
                series.ExplodeIndex = e.CurrentIndex;
            }
        }
		public override void OnScrollingToNewCardViewExt(CardViewExt cardViewExt)
		{
			//var content = cardViewExt.MainContent as Syncfusion.Maui.Charts.SfCircularChart;

			//content.AnimateSeries();
		}
	}

	public class DoughnutSeriesExt : DoughnutSeries
	{
		ChartDataModel selectedModel;
		protected override void DrawSeries(ICanvas canvas, RectangleF clipRect)
		{
			base.DrawSeries(canvas, clipRect);
			var center = clipRect.Center;
			var datas = ItemsSource as ObservableCollection<ChartDataModel>;
			if (SelectedIndex != -1)
			{
				selectedModel = (ItemsSource as ObservableCollection<ChartDataModel>)[SelectedIndex];
			}

			if (selectedModel != null)
			{
				canvas.FontSize = 12;
				canvas.SetToBoldSystemFont();
				var txt1 = selectedModel.Name;
				var size = txt1.Measure(12);

				canvas.DrawString(txt1, center.X, center.Y - (float)size.Height, HorizontalAlignment.Center);

				var sum = datas.Sum(item => item.Value);
				var SelectedItemsPercentage = selectedModel.Value / sum * 100;
				SelectedItemsPercentage = Math.Floor(SelectedItemsPercentage * 100) / 100;
				var txt2 = SelectedItemsPercentage.ToString("0'%");
				size = txt2.Measure(12);

				canvas.DrawString(txt2, center.X, center.Y + (float)size.Height, HorizontalAlignment.Center);
			}
		}
	}

}
