using SampleBrowser.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SampleBrowser.Maui.SfRadialGauge
{
    public partial class Annotation : SampleView
    {
        public Annotation()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            compassGauge.Handler?.DisconnectHandler();
            imageAnnotationGauge.Handler?.DisconnectHandler();
        }

        public override void OnExpandedViewDisappearing(Microsoft.Maui.Controls.View view)
        {
            base.OnExpandedViewDisappearing(view);

            view.Handler?.DisconnectHandler();
        }
    }
}