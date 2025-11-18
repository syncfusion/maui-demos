#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using Syncfusion.Maui.Inputs;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class ChartZooming : SampleView
    {
        public ChartZooming()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            chart.Handler?.DisconnectHandler();
        }

        private void checkbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            if (ViewModel != null && (DeviceInfo.Platform==DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.Android))
            {
                zoomingBehavior.EnableDirectionalZooming = checkBox.IsChecked;
            }
        }
        
        private void ZoomModeChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var comboBox = (SfComboBox)sender;
            int selectedIndex = comboBox.SelectedIndex;
            if (selectedIndex == 0)
            {
                zoomingBehavior.ZoomMode = ZoomMode.X;
            }
            else if (selectedIndex == 1)
            {
                zoomingBehavior.ZoomMode = ZoomMode.Y;
            }
            else if (selectedIndex == 2)
            {
                zoomingBehavior.ZoomMode = ZoomMode.XY;
            }
        }
    }
}
