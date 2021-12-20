using System;
using Microsoft.Maui.Controls;
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

        public override void OnDisappearing()
        {
            base.OnDisappearing();

			Chart.Handler?.DisconnectHandler();
        }

        public override void OnExpandedViewDisappearing(View view)
        {
            base.OnExpandedViewDisappearing(view);

            view.Handler?.DisconnectHandler();
        }
    }
}
