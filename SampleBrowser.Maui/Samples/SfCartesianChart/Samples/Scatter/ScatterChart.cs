#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
# endregion

using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Chart = Syncfusion.Maui.Charts;
using SampleBrowser.Maui.Core;

namespace SampleBrowser.Maui.SfCartesianChart
{
	public partial class ScatterChart : SampleView
	{
		public ScatterChart()
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
            if (cardViewExt.Title == "Dynamic update animation")
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
}
