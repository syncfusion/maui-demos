#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge
{
    public partial class LinearGaugeLabelsHorizontal : ContentView
    {
        public LinearGaugeLabelsHorizontal()
        {
            InitializeComponent();
        }

        #region Events
        private void SfLinearGauge_LabelCreated(object sender, Syncfusion.Maui.Gauges.LabelCreatedEventArgs e)
        {
            e.Text = "$" + e.Text;
        }

        private void SfLinearGauge_LabelCreated_1(object sender, Syncfusion.Maui.Gauges.LabelCreatedEventArgs e)
        {
            if (e.Text == "5")
                e.Text = "Ordered";
            else if (e.Text == "10")
                e.Text = "Packed";
            else if (e.Text == "15")
                e.Text = "Shipped";
            else if (e.Text == "20")
                e.Text = "Delivered";
        }

        #endregion
    }
}