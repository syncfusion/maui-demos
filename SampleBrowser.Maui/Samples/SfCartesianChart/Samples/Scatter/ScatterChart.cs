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

			Chart.Handler?.DisconnectHandler();
			Chart1.Handler?.DisconnectHandler();
        }
    }
}
