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

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public partial class CartesianTrackball : SampleView
    {
        public CartesianTrackball()
        {
            InitializeComponent();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            trackballChart.Handler?.DisconnectHandler();
        }

        private void Switch_Toggled(object sender, SwitchStateChangedEventArgs e)
        {
            var state = e.NewValue;

            if (state == true)
            {
                foreach (CartesianSeries series in trackballChart.Series)
                {
                    if (series != null)
                    {
                        if (trackballChart.Resources["trackballLabelTemplate"] is DataTemplate dataTemplate)
                        {
                            series.TrackballLabelTemplate = dataTemplate;
                        }
                    }
                }
            }
            else
            {
                foreach (CartesianSeries series in trackballChart.Series)
                {
                    if (series != null)
                    {
                        series.TrackballLabelTemplate = null!;
                    }
                }
            }
        }
        

        private void trackballChart_TrackballCreated(object sender, TrackballEventArgs e)
        {
            foreach (var info in e.TrackballPointsInfo)
            {
                if (trackball.DisplayMode == LabelDisplayMode.GroupAllPoints)
                {
                    if (!string.IsNullOrEmpty(info.Label))
                    {
                        info.Label = info.Series.Label + " : " + $" {info.Label}M";
                    }
                }
                else if (trackball.DisplayMode != LabelDisplayMode.GroupAllPoints)
                {
                    if (info.Series.TrackballLabelTemplate == null)
                    {
                        if (!string.IsNullOrEmpty(info.Label))
                        {
                            info.Label = $"{info.Label}M";
                        }
                    } 
                } 
            }
        }

        private void ComboBox_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            var comboBox = (SfComboBox)sender;
            int selectedIndex = comboBox.SelectedIndex;
            if (selectedIndex == 0)
            {
                trackball.DisplayMode = LabelDisplayMode.FloatAllPoints;
                TemplateSwitch.IsEnabled = true;
                label.IsEnabled = true;
                label.Opacity = 1;
            }
            else if (selectedIndex == 1)
            {
                trackball.DisplayMode = LabelDisplayMode.NearestPoint;
                TemplateSwitch.IsEnabled = true;
                label.IsEnabled = true;
                label.Opacity = 1;
            }
            else if (selectedIndex == 2)
            {
                trackball.DisplayMode = LabelDisplayMode.GroupAllPoints;
                TemplateSwitch.IsEnabled = false;
                label.IsEnabled = false;
                label.Opacity = 0.5;
            }
        }
    }
}
