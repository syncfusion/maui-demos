using System;
using Microsoft.Maui.Controls.Xaml;
using SampleBrowser.Maui.Core;

namespace SampleBrowser.Maui.SfCartesianChart
{
	public partial class SplineAreaChart : SampleView
	{
		public SplineAreaChart()
		{
			InitializeComponent();
            
        }

		public override void OnScrollingToNewCardViewExt(CardViewExt cardViewExt)
		{
			var content = cardViewExt.MainContent as Syncfusion.Maui.Charts.SfCartesianChart;

			content.AnimateSeries();
		}
	}
}
