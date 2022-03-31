﻿#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Gauges;
using System;
using System.Globalization;
using System.Threading.Tasks;
using ValueChangedEventArgs = Syncfusion.Maui.Gauges.ValueChangedEventArgs;


namespace SampleBrowser.Maui.SfRadialGauge
{
    public partial class Showcase : SampleView
    {
        private double seconds;
        private bool canStopTimer;
        public Showcase()
        {
            InitializeComponent();
            this.seconds = 0;
            StartTimer();
        }

        private void StopTimer()
        {
            canStopTimer = true;
        }

        private async void StartTimer()
        {
            await Task.Delay(500);
#pragma warning disable CS0618 // Type or member is obsolete
            Device.StartTimer(new TimeSpan(0, 0, 0, 1, 0), UpdateTick);
#pragma warning restore CS0618 // Type or member is obsolete
            canStopTimer = false;
        }

        private bool UpdateTick()
        {
            if (canStopTimer) return false;
            this.seconds += 0.2;
            if (this.seconds > 12)
            {
                this.secondsPointer.EnableAnimation = false;
                this.seconds = 0;
                this.secondsPointer.Value = seconds;
                this.secondsPointer.EnableAnimation = true;
                this.seconds += 0.2;
                this.secondsPointer.Value = seconds;
            }
            else
            {
                this.secondsPointer.Value = seconds;
            }
            return true;
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            StopTimer();

            sleepTracker.Handler?.DisconnectHandler();
            clockGauge.Handler?.DisconnectHandler();
            temperatureMonitorGauge.Handler?.DisconnectHandler();
            distanceTrackerGauge.Handler?.DisconnectHandler();
        }

        public override void OnExpandedViewDisappearing(Microsoft.Maui.Controls.View view)
        {
            base.OnExpandedViewDisappearing(view);
            StopTimer();

            view.Handler?.DisconnectHandler();
        }
        public override void OnExpandedViewAppearing(View view)
        {
            base.OnExpandedViewAppearing(view);
#if __MACCATALYST__
            if (view is Syncfusion.Maui.Gauges.SfRadialGauge gauge && gauge.Axes.Count > 0 &&
                gauge.Axes[0].Pointers.Count > 1 && gauge.Axes[0].Pointers[1].Value == 138)
                gauge.Axes[0].Pointers[1].Value = 140;
#endif
        }

        #region Sleep Tracker

        bool isStartPointer;
        private void StartPointer_ValueChangeStarted(object sender, ValueChangedEventArgs e)
        {
            if (AnimationExtensions.AnimationIsRunning(this.sleepTracker, "DragStartAnimation"))
            {
                this.OnAnimationFinished(0, true);
            }

            isStartPointer = true;
            if (startPointer.Content != null)
                startPointer.Content.HeightRequest = startPointer.Content.WidthRequest = 30d;
            this.DragAnimation(false);
        }

        private void StartPointer_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            this.range.StartValue = e.NewValue;
            this.CalculateSleepTime();
        }

        private void ValueChangeCompleted(object sender, ValueChangedEventArgs e)
        {
            this.DragAnimation(true);
        }

        private void EndPointer_ValueChangeStarted(object sender, ValueChangedEventArgs e)
        {
            if (AnimationExtensions.AnimationIsRunning(this.sleepTracker, "DragStartAnimation"))
            {
                this.OnAnimationFinished(0, true);
            }
            isStartPointer = false;

            if (endPointer.Content != null)
                endPointer.Content.HeightRequest = endPointer.Content.WidthRequest = 30d;
            this.DragAnimation(false);
        }

        private void EndPointer_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            this.range.EndValue = e.NewValue;
            this.CalculateSleepTime();
        }

        private void DragAnimation(bool isCompleted)
        {
            MarkerPointer markerPointer = isStartPointer ? startPointer : endPointer;
            double markerSizeAnimationStart = isCompleted ? 40 : 30;
            double markerSizeAnimationEnd = isCompleted ? 30 : 40;
            double annotationFadeAnimationStart = isCompleted ? 0 : 1;
            double annotationFadeAnimationEnd = isCompleted ? 1 : 0;
            double annotationPositionAnimationStart = isCompleted ? 0 : 0.4;
            double annotationPositionAnimationEnd = isCompleted ? 0.4 : 0;
            double annotationScaleAnimationStart = isCompleted ? 1.8 : 1;
            double annotationScaleAnimationEnd = isCompleted ? 1 : 1.8;

#if ANDROID
            GaugeAnnotation gaugeAnnotation = isStartPointer ? StartValueAnnotation : EndValueAnnotation;
            gaugeAnnotation.Content.AnchorX = 0.49;
            gaugeAnnotation.Content.AnchorY = 0.49;

#endif
            var parentAnimation = new Microsoft.Maui.Controls.Animation();

            var animation = new Microsoft.Maui.Controls.Animation(this.OnMarkerSizeAnimationUpdate, markerSizeAnimationStart, markerSizeAnimationEnd, Easing.Linear, null);
            parentAnimation.Add(0, 1, animation);

            animation = new Microsoft.Maui.Controls.Animation(this.OnAnnotationFadeOutUpdate, annotationFadeAnimationStart, annotationFadeAnimationEnd, Easing.Linear, null);
            parentAnimation.Add(0, 1, animation);

            animation = new Microsoft.Maui.Controls.Animation(this.OnAnnotationPositionFactorUpdate, annotationPositionAnimationStart, annotationPositionAnimationEnd, Easing.Linear, null);
            parentAnimation.Add(0, 1, animation);

            animation = new Microsoft.Maui.Controls.Animation(this.OnAnnotationScaleUpdate, annotationScaleAnimationStart, annotationScaleAnimationEnd, Easing.Linear, null);
            parentAnimation.Add(0, 1, animation);

            parentAnimation.Commit(this.sleepTracker, "DragStartAnimation", 16, (uint)300,
                Easing.Linear, OnAnimationFinished, null);
        }

        private void OnMarkerSizeAnimationUpdate(double value)
        {
            ContentPointer pointer = isStartPointer ? startPointer : endPointer;

            if (pointer.Content != null)
                pointer.Content.HeightRequest = pointer.Content.WidthRequest = value;
        }

        private void OnAnnotationFadeOutUpdate(double value)
        {
            GaugeAnnotation gaugeAnnotation = isStartPointer ? EndValueAnnotation : StartValueAnnotation;
            gaugeAnnotation.Content.Opacity = value;
            IntermediateAnnotation.Content.Opacity = value;
        }

        private void OnAnnotationPositionFactorUpdate(double value)
        {
            GaugeAnnotation gaugeAnnotation = isStartPointer ? StartValueAnnotation : EndValueAnnotation;
            gaugeAnnotation.PositionFactor = value;
        }

        private void OnAnnotationScaleUpdate(double value)
        {
            GaugeAnnotation gaugeAnnotation = isStartPointer ? StartValueAnnotation : EndValueAnnotation;
            gaugeAnnotation.Content.Scale = value;
        }


        private void CalculateSleepTime()
        {
            if (this.range.StartValue < this.range.EndValue)
            {
                this.EndValueAnnotationDate.Text = "4 Apr";
            }
            else
            {
                this.EndValueAnnotationDate.Text = "5 Apr";
            }

            DateTime startDate = this.ConvertToDateTime(new DateTime(2021, 4, 4), this.range.StartValue);
            this.SleepTime.Text = this.GetDisplayString(startDate.Hour) + ":" + this.GetDisplayString(startDate.Minute);

            DateTime endDate = this.ConvertToDateTime(new DateTime(2021, 4, 5), this.range.EndValue);
            this.WakeUpTime.Text = this.GetDisplayString(endDate.Hour) + ":" + this.GetDisplayString(endDate.Minute);

            TimeSpan timeSpan = endDate.Subtract(startDate);
            this.sleepedTime.Text = this.GetDisplayString(timeSpan.Hours) + " hrs " + this.GetDisplayString(timeSpan.Minutes) + " min";
        }

        /// <summary>
        /// To get the display value.
        /// </summary>
        /// <param name="value">The minutes or hours value.</param>
        private string GetDisplayString(int value)
        {
            float digit = value / 10f;
            bool isSingleDigit = digit >= 1 ? false : true;
            return isSingleDigit ? "0" + value : value.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// To convert the pointer value to date.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="value">The pointer value.</param>
        /// <returns>Calculated date.</returns>
        private DateTime ConvertToDateTime(DateTime day, double value)
        {
            int hours = this.GetHours(value);
            day = day.AddHours(hours);
            day = day.AddMinutes(this.GetMinutes(value, hours));
            return day;
        }

        /// <summary>
        /// To get the hours from pointer value.
        /// </summary>
        /// <param name="value">The pointer value.</param>
        /// <returns>Calculated hours value.</returns>
        private int GetHours(double value)
        {
            return Convert.ToInt32(Math.Floor(value));
        }

        /// <summary>
        /// To get the minutes from pointer value.
        /// </summary>
        /// <param name="value">The pointer value.</param>
        /// <param name="hour">The hours.</param>
        /// <returns>The calculated minutes value.</returns>
        private int GetMinutes(double value, int hour)
        {
            return Convert.ToInt32(Math.Floor((value - hour) * 60));
        }

        /// <summary>
        /// Called when animation finished. 
        /// </summary>
        /// <param name="value">Represents animation value.</param>
        /// <param name="isCompleted">Represents animation complete state.</param>
        private void OnAnimationFinished(double value, bool isCompleted)
        {
            AnimationExtensions.AbortAnimation(this.sleepTracker, "DragStartAnimation");
        }
        #endregion
    }

    public class ClockViewModel
    {
        private readonly double hour;
        private readonly double minute;

        public ClockViewModel()
        {
            var date = DateTime.Now;
            hour = date.Hour > 12 ? date.Hour - 12 : date.Hour;
            hour = hour + date.Minute / 60d;
            minute = date.Minute / 60d * 12d;
        }

        public double Hour
        {
            get
            {
                return hour;
            }
        }

        public double Minute
        {
            get
            {
                return minute;
            }
        }
    }
}
