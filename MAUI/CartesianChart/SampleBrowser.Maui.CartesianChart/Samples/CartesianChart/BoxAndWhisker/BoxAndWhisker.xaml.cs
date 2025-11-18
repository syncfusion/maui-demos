#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Platform;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Inputs;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoxAndWhisker : SampleView
    {
        public BoxAndWhisker()
        {
            InitializeComponent();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            hyperLinkLayout.IsVisible = !IsCardView;
            if (!IsCardView)
            {
                Chart.Title = (Label)layout.Resources["title"];
                xAxis.Title = new ChartAxisTitle() { Text = "Machine" };
                yAxis.Title = new ChartAxisTitle() { Text = "Energy" };
            }
        }

        private void Mode_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var comboBox = (SfComboBox)sender;
            int selectedIndex = comboBox.SelectedIndex;

            if (selectedIndex == 0)
            {
                BoxAndWhisker1.BoxPlotMode = BoxPlotMode.Exclusive;
            }
            else
            {
                BoxAndWhisker1.BoxPlotMode = BoxPlotMode.Inclusive;
            }
        }
    }
}