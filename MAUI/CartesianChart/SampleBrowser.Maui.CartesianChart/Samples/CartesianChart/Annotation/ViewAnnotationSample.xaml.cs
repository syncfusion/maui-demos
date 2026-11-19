using SampleBrowser.Maui.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
   
    public partial class ViewAnnotationSample : SampleView
    {
        public ViewAnnotationSample()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Chart.Handler?.DisconnectHandler();
        }
    }
}