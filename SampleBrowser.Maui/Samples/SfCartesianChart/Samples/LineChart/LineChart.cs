using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Chart = Syncfusion.Maui.Charts;
using SampleBrowser.Maui.Core;

namespace SampleBrowser.Maui.SfCartesianChart
{
	public partial class LineChart : SampleView
	{
		public LineChart()
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
			if (cardViewExt.Title == "Dynamic update animation")
			{
				viewModel1.StopTimer();
				viewModel1.StartTimer();
			}
            else
            {
				viewModel1.StopTimer();
				var content = cardViewExt.MainContent as Syncfusion.Maui.Charts.SfCartesianChart;
				content.AnimateSeries();
			}
		}

		public override void OnDisappearing()
		{
			base.OnDisappearing();
			if (viewModel1 != null)
			{
				viewModel1.StopTimer();
			}

			Chart.Handler?.DisconnectHandler();
			Chart1.Handler?.DisconnectHandler();
			Chart2.Handler?.DisconnectHandler();
		}
	}
}
