#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
	public partial class DateTimeCategoryAxisViewModel : BaseViewModel
	{
		public ObservableCollection<ChartDataModel> SalesRevenue { get; set; }
		public ObservableCollection<Brush> ChartColors { get; set; } 
		public DateTimeCategoryAxisViewModel() 
		{
			SalesRevenue =
			[
				new ChartDataModel() { Date = new DateTime(2017, 12, 24), Value = 40 },
				new ChartDataModel() { Date = new DateTime(2017, 12, 25), Value = 60 },
				new ChartDataModel() { Date = new DateTime(2017, 12, 31), Value = 57 },
				new ChartDataModel() { Date = new DateTime(2018, 01, 01), Value = 64 },
				new ChartDataModel() { Date = new DateTime(2018, 01, 02), Value = 60 },
				new ChartDataModel() { Date = new DateTime(2018, 01, 14), Value = 50 },
			];

			ChartColors =
			[
				new SolidColorBrush(Color.FromArgb("#95DB9C")),
				new SolidColorBrush(Color.FromArgb("#B95375")),
				new SolidColorBrush(Color.FromArgb("#56BBAF")),
				new SolidColorBrush(Color.FromArgb("#606D7F")),
				new SolidColorBrush(Color.FromArgb("#E99941")),
				new SolidColorBrush(Color.FromArgb("#327DBE")),
			];
		}
	}
}
