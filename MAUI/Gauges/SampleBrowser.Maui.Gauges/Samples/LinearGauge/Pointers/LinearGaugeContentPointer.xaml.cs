#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Base;
using System;

namespace SampleBrowser.Maui.Gauges.SfLinearGauge
{
    public partial class LinearGaugeContentPointer : SampleView
    {
        public LinearGaugeContentPointer()
        {
            InitializeComponent();
        }

        #region Events
        private void SegmentedControl_SelectionChanged(object sender, Syncfusion.Maui.Buttons.SelectionChangedEventArgs e)
        {
            if (e.NewIndex == 0)
            {
                contentView.Content = new LinearGaugeContentPointerHorizontal();
            }
            else if (e.NewIndex == 1)
            {
                contentView.Content = new LinearGaugeContentPointerVertical();
            }
        }

        #endregion
    }
}
