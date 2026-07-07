using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Platform;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;

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

        private void Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is not Picker value || BoxAndWhisker1 is null)
                return;

            int selectedIndex = value.SelectedIndex;
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