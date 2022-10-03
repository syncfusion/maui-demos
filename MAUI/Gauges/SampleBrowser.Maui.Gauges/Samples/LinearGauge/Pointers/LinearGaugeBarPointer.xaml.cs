#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
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
    public partial class LinearGaugeBarPointer : SampleView
    {

        public LinearGaugeBarPointer()
        {
            InitializeComponent();
#if IOS
            contentView.HeightRequest = 550;
#endif
        }

        #region Events
        private void Button_Clicked(object sender, EventArgs e)
        {
            contentView.Content = new LinearGaugeBarPointerHorizontal();
            horizontalRectangle.Fill = new SolidColorBrush(Color.FromRgb(0, 116, 227));
            horizontalLabel.TextColor = Colors.White;

            verticalLabel.TextColor = Colors.Black;
            verticalRectangle.Fill = new SolidColorBrush(Colors.White);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            contentView.Content = new LinearGaugeBarPointerVertical();
            verticalRectangle.Fill = new SolidColorBrush(Color.FromRgb(0, 116, 227));
            verticalLabel.TextColor = Colors.White;

            horizontalLabel.TextColor = Colors.Black;
            horizontalRectangle.Fill = new SolidColorBrush(Colors.White);
        }

        #endregion
    }
}
