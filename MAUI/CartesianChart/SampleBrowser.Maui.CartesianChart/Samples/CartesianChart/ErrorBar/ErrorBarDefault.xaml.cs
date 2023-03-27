#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class ErrorBarDefault : SampleView
    {

        #region Constructor

        #region  Public Constructor

        public ErrorBarDefault()
        {
            InitializeComponent();
        }
        
        public override void OnAppearing()
        {
            base.OnAppearing();
            hyperLinkLayout.IsVisible = !IsCardView;
            if (!IsCardView)
            {
                errorBarChart.Title = (Label)layout.Resources["title"];
                xAxis.Title = new ChartAxisTitle() { Text = "Materials" };
                yAxis.Title = new ChartAxisTitle() { Text = "Thermal Coefficient" };
            }

        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            errorBarChart.Handler?.DisconnectHandler();
        }

        #endregion

        #endregion

    }
}
