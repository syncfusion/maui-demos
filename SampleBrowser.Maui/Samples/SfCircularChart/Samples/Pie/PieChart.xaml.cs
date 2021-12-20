#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

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
