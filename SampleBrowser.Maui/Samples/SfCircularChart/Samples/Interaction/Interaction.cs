#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
# endregion

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
