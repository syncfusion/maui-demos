#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge
{
    public partial class LinearGaugeContentPointerHorizontal : ContentView
    {
        public LinearGaugeContentPointerHorizontal()
        {
            InitializeComponent();
        }

        private void Pointer1_ValueChanged(object sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
        {
            textPointerLabel.Text = ((int)e.Value).ToString();
        }

        private void Pointer2_ValueChanged(object sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
        {
            multiPointerLabel.Text = ((int)e.Value).ToString();
        }
    }
}