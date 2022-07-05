#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Core;


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