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
    public partial class LinearGaugeLabels : SampleView
    {
        /// <summary>
        /// Check the application theme is light or dark.
        /// </summary>
        private bool isLightTheme = Application.Current?.RequestedTheme == AppTheme.Light;

        public LinearGaugeLabels()
        {
            InitializeComponent();
#if IOS
            contentView.HeightRequest = 550;
#endif
        }

        #region Events
        private void Button_Clicked(object sender, EventArgs e)
        {
            contentView.Content = new LinearGaugeLabelsHorizontal();
            horizontalRectangle.Fill = new SolidColorBrush(Color.FromRgb(0, 116, 227));
            horizontalLabel.TextColor = isLightTheme ? Colors.White : Colors.Black;

            verticalLabel.TextColor = isLightTheme ? Colors.Black : Colors.White;
            verticalRectangle.Fill = isLightTheme ? new SolidColorBrush(Colors.White) : Color.FromArgb("#25232A");
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            contentView.Content = new LinearGaugeLabelsVertical();
            verticalRectangle.Fill = new SolidColorBrush(Color.FromRgb(0, 116, 227));
            verticalLabel.TextColor = isLightTheme ? Colors.White : Colors.Black;

            horizontalLabel.TextColor = isLightTheme ? Colors.Black : Colors.White;
            horizontalRectangle.Fill = isLightTheme ? new SolidColorBrush(Colors.White) : Color.FromArgb("#25232A");
        }
        #endregion
    }
}
