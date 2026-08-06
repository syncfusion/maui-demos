using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart;

    public partial class StackedAreaChart : SampleView
    {
        public StackedAreaChart()
        {
            InitializeComponent();
        }
        public override void OnAppearing()
        {
            base.OnAppearing();           
#if IOS
            if (IsCardView)
            {
                Chart.WidthRequest = 350;
                Chart.HeightRequest = 400;
                Chart.VerticalOptions = LayoutOptions.Start;
            }
#endif
            if (!IsCardView)
            {
                XAxis.Title = new ChartAxisTitle() { Text = "Year" };
                YAxis.Title = new ChartAxisTitle() { Text = "Spends" };
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Chart.Handler?.DisconnectHandler();
        }
    }