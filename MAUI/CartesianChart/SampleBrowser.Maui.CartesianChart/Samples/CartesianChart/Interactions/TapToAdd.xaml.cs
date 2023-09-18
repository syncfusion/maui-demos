#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
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
    public partial class TapToAdd : SampleView
    {
       
        public TapToAdd()
        {
            InitializeComponent();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
        }
        
        public override void OnDisappearing()
        {
            base.OnDisappearing();
            categoryChart.Handler?.DisconnectHandler();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            realTimeViewModel?.ResetChartData();
        }
    }

    public class ChartInteractionExt : ChartInteractiveBehavior
    {
        protected override void OnTouchUp(ChartBase chart, float pointX, float pointY)
        {
            base.OnTouchUp(chart, pointX, pointY);
            
            if(chart is Syncfusion.Maui.Charts.SfCartesianChart cartesianChart)
            {
                var x = cartesianChart.PointToValue(cartesianChart.XAxes[0], pointX, pointY);
                var y = cartesianChart.PointToValue(cartesianChart.YAxes[0], pointX, pointY);

                if (cartesianChart.BindingContext is TapToAddViewModel viewModel)
                {
                    viewModel.LiveChartData.Add(new ChartDataModel(x, y));
                }
            }
        }
    }

}
