#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Buttons;
using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Inputs;

namespace SampleBrowser.Maui.FunnelChart.SfFunnelChart
{
    public partial class DefaultFunnel : SampleView
    {
        public DefaultFunnel()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Chart.Handler?.DisconnectHandler();
        }

        private void Inversed_CheckedChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
        {
            var checkBox = ((SfCheckBox)sender).IsChecked ?? false; 
            if (checkBox)
            {
                Chart.Orientation = ChartOrientation.Horizontal;
            }
            else
            {
                Chart.Orientation = ChartOrientation.Vertical;
            }
        }
    }
}
