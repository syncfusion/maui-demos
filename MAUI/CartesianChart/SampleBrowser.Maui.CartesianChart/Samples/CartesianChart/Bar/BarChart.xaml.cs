using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using System;
using Chart = Syncfusion.Maui.Charts;
using mauiColor = Microsoft.Maui.Graphics.Color;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class BarChart : SampleView
    {
        public BarChart()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Chart1.Handler?.DisconnectHandler();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            hyperLinkLayout.IsVisible = !IsCardView;
        }
    }
}
