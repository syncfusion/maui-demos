#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Essentials;
using Microsoft.Maui.Graphics;
using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Gauges;
using System;
using System.Linq;

namespace SampleBrowser.Maui.SfLinearGauge
{
    public partial class Showcase : SampleView
    {
        #region Constructor
        public Showcase()
        {
            InitializeComponent();
            GenerateActiveHoursPointers();

            if (graphicsView.Drawable is GraphicsDrawable graphicsDrawable)
                graphicsDrawable.WaterLevelFactor = waterLevelGauge.ValueToFactor(150);

#if MACCATALYST
            temperatureMeterGauge.Margin = new Thickness(50, 0, 50, 0);
            activeHoursVerticalLayout.Margin = new Thickness(50, 0, 50, 0);
#else
            temperatureMeterGauge.WidthRequest = 340;
            activeHoursVerticalLayout.WidthRequest = 340;

            if (DeviceInfo.Platform == DevicePlatform.iOS && DeviceInfo.Idiom == DeviceIdiom.Tablet)
            {
                activeHoursLayout.WidthRequest = temperatureLayout.WidthRequest = 
                    taskTrackerLayout.WidthRequest = progressBarLayout.WidthRequest = 340;
                taskTrackerLayout.HorizontalOptions = progressBarLayout.HorizontalOptions = LayoutOptions.Center;
            }
#endif
        }
        #endregion

        #region Thermometer
        private void thermoMeterShape_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (e.NewValue > 50 || e.NewValue < -18)
                e.Cancel = true;

            if (e.NewValue > 38)
                thermoMeterShape.Fill = new SolidColorBrush(Color.FromRgb(255, 123, 123));
            else
                thermoMeterShape.Fill = new SolidColorBrush(Color.FromRgb(0, 116, 227));

        }
        #endregion

        #region Height calculator

        private void SfLinearGauge_LabelCreated(object sender, LabelCreatedEventArgs e)
        {
            e.Text = e.Text + " cm";
        }

        private void Pointer1_ValueChanged(object sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
        {
            heightAnnotationLabel.Text = (int)e.Value + " cm";
        }
        #endregion

        #region Water level indicator

        private void waterLevelShapePointer_ValueChanged(object sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
        {
            waterLevelLabel.Text = (int)e.Value + " ml";
            if (graphicsView.Drawable is GraphicsDrawable graphicsDrawable)
                graphicsDrawable.WaterLevelFactor = waterLevelGauge.ValueToFactor(e.Value);
            graphicsView.Invalidate();

        }

        #endregion

        #region Volume settings

        private void BarPointer_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (e.NewValue > 100)
            {
                e.Cancel = true;
                return;
            }

            if (sender is BarPointer pointer)
            {
                string text = AutomationProperties.GetHelpText(pointer);

                double value = e.NewValue;
                value = value > 50 ? Math.Ceiling(value) : Math.Floor(value);

                if (text == "firstVolumeLabel")
                {
                    firstVolumeLabel.Text = value.ToString() + "%";

                    firstLabelIcon.Text = value == 0 ? "\uE70A" : "\uE706";
                }
                else if (text == "secondVolumeLabel")
                {
                    secondVolumeLabel.Text = value.ToString() + "%";

                    secondLabelIcon.Text = value == 0 ? "\uE70B" : "\uE709";
                }
                else if (text == "thirdVolumeLabel")
                {
                    thirdVolumeLabel.Text = value.ToString() + "%";

                    thirdLabelIcon.Text = value == 0 ? "\uE70C" : "\uE707";
                }
            }
        }
        #endregion

        #region Temperature meter
        private void SfLinearGauge_LabelCreated_1(object sender, LabelCreatedEventArgs e)
        {
            e.Text = e.Text + "°C";
        }
        #endregion

        #region Active hours

        private void GenerateActiveHoursPointers()
        {
            double[] inActiveHours = { 2, 3, 4, 6, 7, 9 };
            for (int i = 0; i <= 18; i++)
            {
                LinearContentPointer contentPointer = new();
                contentPointer.Value = i;
                RoundRectangle roundRectangle = new()
                {
                    WidthRequest = 10,
                    CornerRadius = new CornerRadius(5),
                    HeightRequest = 70,
                };
                if (inActiveHours.Contains(i))
                    roundRectangle.Fill = new SolidColorBrush(Color.FromArgb("#6405C3DD"));
                else
                    roundRectangle.Fill = new SolidColorBrush(Color.FromArgb("#FF05C3DD"));

                contentPointer.Content = roundRectangle;

                activeHoursGauge.MarkerPointers.Add(contentPointer);
            }
        }

        #endregion

        #region Override methods

        public override void OnExpandedViewDisappearing(Microsoft.Maui.Controls.View view)
        {
            base.OnExpandedViewDisappearing(view);
            view.Handler?.DisconnectHandler();

            sleepScoreLayout.HeightRequest = progressBarLayout.HeightRequest = taskTrackerLayout.HeightRequest =
                temperatureLayout.HeightRequest = batteryIndicatorLayout.HeightRequest = activeHoursLayout.HeightRequest = 350d;
        }
        public override void OnExpandedViewAppearing(View view)
        {
            base.OnExpandedViewAppearing(view);
            sleepScoreLayout.HeightRequest = progressBarLayout.HeightRequest = taskTrackerLayout.HeightRequest =
                temperatureLayout.HeightRequest = batteryIndicatorLayout.HeightRequest = activeHoursLayout.HeightRequest = double.NaN;
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            thermometerGauge1.Handler?.DisconnectHandler();
            thermometerGauge2.Handler?.DisconnectHandler();
            heightCalculatorGauge.Handler?.DisconnectHandler();
            waterLevelGauge.Handler?.DisconnectHandler();
            volumeSettingsGauge1.Handler?.DisconnectHandler();
            volumeSettingsGauge2.Handler?.DisconnectHandler();
            volumeSettingsGauge3.Handler?.DisconnectHandler();
            progressBarGauge.Handler?.DisconnectHandler();
            taskTrackerGauge.Handler?.DisconnectHandler();
            sleepWatchScoreGauge1.Handler?.DisconnectHandler();
            sleepWatchScoreGauge2.Handler?.DisconnectHandler();
            temperatureMeterGauge.Handler?.DisconnectHandler();
            batteryIndicatorGauge.Handler?.DisconnectHandler();
            activeHoursGauge.Handler?.DisconnectHandler();
        }

        #endregion
    }
}
