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

        public override void OnExpandedViewAppearing(View view)
        {
            base.OnExpandedViewAppearing(view);

			if (RunTimeDevice.IsMobileDevice())
			{
				pieSeries1.CircularCoefficient = pieSeries.CircularCoefficient = 0.6;
			}
        }

        public override void OnExpandedViewDisappearing(View view)
        {
            base.OnExpandedViewDisappearing(view);

			if (RunTimeDevice.IsMobileDevice())
			{
				pieSeries1.CircularCoefficient = pieSeries.CircularCoefficient = 0.8;
			}

			view.Handler?.DisconnectHandler();
		}

        public override void OnDisappearing()
        {
            base.OnDisappearing();

			Chart.Handler?.DisconnectHandler();
			Chart1.Handler?.DisconnectHandler();
        }
    }

	

}
