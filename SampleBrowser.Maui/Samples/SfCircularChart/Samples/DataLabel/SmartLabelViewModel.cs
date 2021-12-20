using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Graphics.Internals;

namespace SampleBrowser.Maui.SfCircularChart
{
	public class CenterViewDoughnut : PieSeries
	{

		protected override void DrawDataLabel(ICanvas canvas, Brush fillcolor, string label, PointF point, int index)
		{
			label = (((this.ItemsSource as ObservableCollection<IncomeAndExpenses>)[index].Value / 41893) * 100).ToString("#") + "%";

			base.DrawDataLabel(canvas, fillcolor, label, point, index);
		}
	}

	public class DoughnutChartExt : PieSeries
	{
		protected override void DrawDataLabel(ICanvas canvas, Brush fillcolor, string label, PointF point, int index)
		{
			var data = ((this.ItemsSource as ObservableCollection<IncomeAndExpenses>)[index]);
			label = data.Name + ":" + data.Percentage.ToString("#") + "%";

			base.DrawDataLabel(canvas, fillcolor, label, point, index);
		}
	}

	public class SmartLabelsViewModel
	{
		public ObservableCollection<IncomeAndExpenses> DataSource { get; set; }

		public ObservableCollection<IncomeAndExpenses> SubDataSource { get; set; }
		public ObservableCollection<Brush> CustomBrushes { get; set; }
		public ObservableCollection<Brush> SubCustomBrushes { get; set; }

		public SmartLabelsViewModel()
		{
			CustomBrushes = new ObservableCollection<Brush>()
			{
			   new SolidColorBrush(Color.FromArgb("#314A6E")),
				 new SolidColorBrush(Color.FromArgb("#48988B")),
				 new SolidColorBrush(Color.FromArgb("#5E498C")),
				 new SolidColorBrush(Color.FromArgb("#74BD6F")),
				 new SolidColorBrush(Color.FromArgb("#597FCA")),
			};

			DataSource = new ObservableCollection<IncomeAndExpenses>()
			{
				new IncomeAndExpenses("Food", 7923),
				new IncomeAndExpenses("Housing", 10927),
				new IncomeAndExpenses("Entertainment",4793),
				new IncomeAndExpenses("Healthcare", 3577),
				new IncomeAndExpenses("Others", 14673)
			};

			if (RunTimeDevice.IsMobileDevice())
			{
				SubDataSource = new ObservableCollection<IncomeAndExpenses>()
			{
				new IncomeAndExpenses("Food","At home", 4464, 7923),
				new IncomeAndExpenses("Food","Outside",3459, 7923),
				new IncomeAndExpenses("Housing","Owned",6678, 10927),
				new IncomeAndExpenses("Housing","Rented",4249 , 10927),

				new IncomeAndExpenses("Entertainment","Movie",3975, 4793),
				new IncomeAndExpenses("Entertainment","Toys",818,4793),
				new IncomeAndExpenses("Healthcare","Services",3405,3577),
				new IncomeAndExpenses("Healthcare","Supplies",172,3577),

				new IncomeAndExpenses("Others","Care products",768, 14673),
				new IncomeAndExpenses("Others","Reading",108, 14673),
				new IncomeAndExpenses("Others","Education",1407 ,14673),
				new IncomeAndExpenses("Others","Tobacco supplies",347, 14673),
				new IncomeAndExpenses("Others","Miscellaneous",993, 14673),
				new IncomeAndExpenses("Others","Cash contributions",1888 , 14673),
				new IncomeAndExpenses("Others","Insurance",7296, 14673),
				new IncomeAndExpenses("Others","Clothing",1866, 14673),
			};
			}
			else
			{
				SubDataSource = new ObservableCollection<IncomeAndExpenses>()
			{
				new IncomeAndExpenses("Food","At home", 4464, 7923),
				new IncomeAndExpenses("Food","Outside",3459, 7923),
				new IncomeAndExpenses("Housing","Owned",6678, 10927),
				new IncomeAndExpenses("Housing","Rented",4249 , 10927),

				new IncomeAndExpenses("Entertainment","Movie",3975, 4793),
				new IncomeAndExpenses("Entertainment","Toys",818,4793),
				new IncomeAndExpenses("Healthcare","Services",1609,3577),
				new IncomeAndExpenses("Healthcare","Supplies",1967,3577),

				new IncomeAndExpenses("Others","Miscellaneous",993, 14673),
				new IncomeAndExpenses("Others","Cash contributions",1888 , 14673),
				new IncomeAndExpenses("Others","Insurance",7296, 14673),
				new IncomeAndExpenses("Others","Clothing",1866, 14673),
				new IncomeAndExpenses("Others","Care products",768, 14673),
				new IncomeAndExpenses("Others","Reading",108, 14673),
				new IncomeAndExpenses("Others","Education",1407 ,14673),
				new IncomeAndExpenses("Others","Tobacco supplies",347, 14673),
			};
			}

			SubCustomBrushes = new ObservableCollection<Brush>();

			foreach (var item in SubDataSource)
			{
				switch (item.Category)
				{
					case "Food":
						SubCustomBrushes.Add(CustomBrushes[0]);
						break;
					case "Housing":
						SubCustomBrushes.Add(CustomBrushes[1]);
						break;
					case "Entertainment":
						SubCustomBrushes.Add(CustomBrushes[2]);
						break;
					case "Healthcare":
						SubCustomBrushes.Add(CustomBrushes[3]);
						break;
					case "Others":
						SubCustomBrushes.Add(CustomBrushes[4]);
						break;
				}
			}
		}
	}

	public class IncomeAndExpenses : ChartDataModel
	{
		public string Category { get; set; }

		public double Percentage { get { return (Value / Size) * 100; } }

		public IncomeAndExpenses(string category, string name, double value, double total) : base(name, value, total)
		{
			Category = category;
		}

		public IncomeAndExpenses(string category, double value)
		{
			Category = category;
			Value = value;
		}
	}
}
