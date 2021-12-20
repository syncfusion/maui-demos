using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SampleBrowser.Maui.SfRadialGauge
{
    public partial class Pointers : SampleView
    {
        public Pointers()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            rangePointerGauge.Handler?.DisconnectHandler();
            multipleNeedleGauge.Handler?.DisconnectHandler();
            markerPointerGauge.Handler?.DisconnectHandler();
        }

        public override void OnExpandedViewDisappearing(View view)
        {
            base.OnExpandedViewDisappearing(view);

            view.Handler?.DisconnectHandler();
        }
    }
}