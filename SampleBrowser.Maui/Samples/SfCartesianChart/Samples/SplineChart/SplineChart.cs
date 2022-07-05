#region Copyright Syncfusion Inc. 2001-2022.
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
    public partial class SplineChart : SampleView
    {
        public SplineChart()
        {
            InitializeComponent();

            if (!RunTimeDevice.IsMobileDevice())
                viewModel1.StartTimer();
        }

        public override void OnExpandedViewAppearing(View view)
        {
            base.OnExpandedViewAppearing(view);

            var content = view as Chart.SfCartesianChart;
            if (RunTimeDevice.IsMobileDevice() && content != null && content.BindingContext is DynamicAnimationViewModel)
            {
                viewModel1.StopTimer();
                viewModel1.StartTimer();
            }
        }

        public override void OnExpandedViewDisappearing(View view)
        {
            base.OnExpandedViewDisappearing(view);
            var content = view as Chart.SfCartesianChart;
            if (RunTimeDevice.IsMobileDevice() && content != null && content.BindingContext is DynamicAnimationViewModel)
            {
                viewModel1.StopTimer();
            }

            view.Handler?.DisconnectHandler();
        }


        public override void OnScrollingToNewCardViewExt(CardViewExt cardViewExt)
        {
            if (RunTimeDevice.IsMobileDevice())
            {
                if (cardViewExt.Title == "Dynamic update animation")
                {
                    viewModel1.StopTimer();
                    viewModel1.StartTimer();
                }
                else
                {
                    viewModel1.StopTimer();
                    var content = cardViewExt.MainContent as Syncfusion.Maui.Charts.SfCartesianChart;
                    content?.AnimateSeries();
                }
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            if (viewModel1 != null)
                viewModel1.StopTimer();

            Chart.Handler?.DisconnectHandler();
            Chart1.Handler?.DisconnectHandler();
            Chart2.Handler?.DisconnectHandler();
        }
    }
}
