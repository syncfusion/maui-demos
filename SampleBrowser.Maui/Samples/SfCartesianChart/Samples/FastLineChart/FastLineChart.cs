using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Chart = Syncfusion.Maui.Charts;
using SampleBrowser.Maui.Core;

namespace SampleBrowser.Maui.SfCartesianChart
{
	public partial class FastLineChart : SampleView
	{
		public FastLineChart()
		{
			InitializeComponent();
		}

		public override void OnExpandedViewAppearing(View view)
		{
			base.OnExpandedViewAppearing(view);

			var content = view as Chart.SfCartesianChart;
			if (content != null)
			{
				if (content.BindingContext is RealTimeVerticalChartViewModel viewModel)
				{
					viewModel.StartVerticalTimer();
				}
				else if (content.BindingContext is RealTimeChartViewModel realTimeViewModel)
				{
					realTimeViewModel.StopTimer();
					realTimeViewModel.StartTimer();
				}
			}
		}

		public override void OnExpandedViewDisappearing(View view)
		{
			base.OnExpandedViewDisappearing(view);
			var content = view as Chart.SfCartesianChart;
			if (content != null && content.BindingContext is RealTimeChartViewModel viewModel)
			{
				viewModel.StopTimer();
			}

			view.Handler?.DisconnectHandler();
		}

        public override void OnAppearing()
        {
            base.OnAppearing();

			realTimeVerticalChartViewModel.StartVerticalTimer();

			realTimeChartViewModel.StopTimer();
			realTimeChartViewModel.StartTimer();

		}

        public override void OnDisappearing()
		{
			base.OnDisappearing();
            if (realTimeChartViewModel != null)
            {
				realTimeChartViewModel.StopTimer();
            }

			fastLineChart.Handler?.DisconnectHandler();
			realTimeChart.Handler?.DisconnectHandler();
			verticalLiveChart.Handler?.DisconnectHandler();
        }
	}
}
