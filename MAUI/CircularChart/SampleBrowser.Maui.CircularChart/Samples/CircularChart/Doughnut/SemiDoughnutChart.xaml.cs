using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Graphics.Internals;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.CircularChart.SfCircularChart
{
    public partial class SemiDoughnutChart : SampleView
    {
        public SemiDoughnutChart()
        {
            InitializeComponent();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
#if IOS
            if (IsCardView)
            {
                Chart1.WidthRequest = 350;
                Chart1.HeightRequest = 400;
                Chart1.VerticalOptions = LayoutOptions.Start;
            }
#endif
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Chart1.Handler?.DisconnectHandler();
        }
    }
}