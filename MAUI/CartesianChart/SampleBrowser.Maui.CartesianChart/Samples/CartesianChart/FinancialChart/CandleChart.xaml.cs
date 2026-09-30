using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CandleChart : SampleView
    {
        public CandleChart()
        {
            InitializeComponent();
        }

        int month = int.MaxValue;

        private void Primary_LabelCreated(object? sender, ChartAxisLabelEventArgs e)
        {
            DateTime baseDate = new(1899, 12, 30);
            var date = baseDate.AddDays(e.Position);
            if (date.Month != month)
            {
                ChartAxisLabelStyle labelStyle = new();
                labelStyle.LabelFormat = "MMM-dd";
                labelStyle.FontAttributes = FontAttributes.Bold;
                e.LabelStyle = labelStyle;

                month = date.Month;
            }
            else
            {
                ChartAxisLabelStyle labelStyle = new();
                labelStyle.LabelFormat = "dd";
                e.LabelStyle = labelStyle;
            }
        }
      
        public override void OnAppearing()
        {
            base.OnAppearing();

            hyperLinkLayout.IsVisible = !IsCardView;

            if(!IsCardView)
            {
                Chart.Title = (Label)layout.Resources["title"];
                xAxis.Title = new ChartAxisTitle() { Text = "Year 2020" };
                YAxis.Title = new ChartAxisTitle() { Text = "Index Price" };
            }
        }

        private void checkBox_CheckedChanged(object? sender, CheckedChangedEventArgs e)
        {
            if (sender is not CheckBox checkBox || Candle is null)
                return;

            if (ViewModel != null)
            {
                Candle.EnableSolidCandle = checkBox.IsChecked;
            }
        }
    }
}