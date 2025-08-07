#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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
    public partial class LinearGaugeShapePointer : SampleView
    {
        public LinearGaugeShapePointer()
        {
            InitializeComponent();
#if IOS
            contentView.HeightRequest = 550;
#endif
        }

        #region Events
        private void SegmentedControl_SelectionChanged(object sender, Syncfusion.Maui.Buttons.SelectionChangedEventArgs e)
        {
            if (e.NewIndex == 0)
            {
                contentView.Content = new LinearGaugeShapePointerHorizontal();
            }
            else if (e.NewIndex == 1)
            {
                contentView.Content = new LinearGaugeShapePointerVertical();
            }
        }

        #endregion
    }
}
