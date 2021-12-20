#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfCircularChart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Selection : SampleView
    {
        SelectionViewModel model;
        public Selection()
        {
            InitializeComponent();
            chart.SelectionChanging += Chart_SelectionChanging;
            model = chart.BindingContext as SelectionViewModel;
        }

        public override void OnScrollingToNewCardViewExt(CardViewExt cardViewExt)
        {
            var content = cardViewExt.MainContent as Syncfusion.Maui.Charts.SfCircularChart;

            content.AnimateSeries();
        }

        private void Chart_SelectionChanging(object sender, Syncfusion.Maui.Charts.ChartSelectionChangingEventArgs e)
        {
            if (e.CurrentIndex == e.PreviousIndex || e.CurrentIndex == -1)
            {
                e.Cancel = true;
            }
            else if (e.CurrentIndex != -1)
            {
                series.CustomBrushes = model.SelectionBrushes;
                series.SelectionBrush = model.CustomBrushes[e.CurrentIndex] as SolidColorBrush;
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            chart.Handler?.DisconnectHandler();
        }

        public override void OnExpandedViewDisappearing(View view)
        {
            base.OnExpandedViewDisappearing(view);

            view.Handler?.DisconnectHandler();
        }
    }
}
