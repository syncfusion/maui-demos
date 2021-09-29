using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Graphics.Internals;

namespace SampleBrowser.Maui.SfCircularChart
{
	public partial class PieChart : SampleView
	{
		public PieChart()
		{
			InitializeComponent();
		}

		public override void OnScrollingToNewCardViewExt(CardViewExt cardViewExt)
		{
			//var content = cardViewExt.MainContent as Syncfusion.Maui.Charts.SfCircularChart;

			//content.AnimateSeries();
		}
	}

	

}
