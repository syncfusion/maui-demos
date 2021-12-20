using Microsoft.Maui;
using SampleBrowser.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SampleBrowser.Maui.SfRadialGauge
{
    public partial class Animation : SampleView
    {
        public Animation()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            linearAnimationGauge.Handler?.DisconnectHandler();
            springInAnimationGauge.Handler?.DisconnectHandler();
            springOutAnimationGauge.Handler?.DisconnectHandler();
            bounceInAnimationGauge.Handler?.DisconnectHandler();
            bounceOutAnimationGauge.Handler?.DisconnectHandler();
        }

        public override void OnExpandedViewDisappearing(Microsoft.Maui.Controls.View view)
        {
            base.OnExpandedViewDisappearing(view);

            view.Handler?.DisconnectHandler();
        }
    }
}