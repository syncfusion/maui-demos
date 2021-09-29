using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfCircularChart
{
	public partial class Interaction : SampleView
	{
		SelectionViewModel model;

		public Interaction()
		{
			InitializeComponent();
			chart1.SelectionChanging += Chart_SelectionChanging;
			model = chart1.BindingContext as SelectionViewModel;
		}

		private void Chart_SelectionChanging(object sender, Syncfusion.Maui.Charts.SelectionChangingEventArgs e)
		{
			if (e.CurrentIndex == e.PreviousIndex || e.CurrentIndex == -1)
			{
				e.Cancel = true;
			}
			else if (e.CurrentIndex != -1)
			{
				series1.CustomBrushes = model.SelectionBrushes;
				series1.SelectionBrush = model.CustomBrushes[e.CurrentIndex] as SolidColorBrush;
			}
		}

		public override void OnScrollingToNewCardViewExt(CardViewExt cardViewExt)
		{

		}
	}
}
