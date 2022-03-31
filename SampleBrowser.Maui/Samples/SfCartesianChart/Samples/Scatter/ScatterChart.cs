﻿#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Core;
using Chart = Syncfusion.Maui.Charts;

namespace SampleBrowser.Maui.SfCartesianChart
{
    public partial class ScatterChart : SampleView
    {
        public ScatterChart()
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

            Chart.Handler?.DisconnectHandler();
            Chart1.Handler?.DisconnectHandler();
        }
    }
}
