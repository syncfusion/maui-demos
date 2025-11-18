#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Gauges;
namespace SampleBrowser.Maui.RadialSliders.RadialRangeSlider
{
    public partial class LabelsAndTicksRadialRangeSlider : SampleView
    {
        public LabelsAndTicksRadialRangeSlider()
        {
            InitializeComponent();
        }

        #region Events

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            clockGauge.Handler?.DisconnectHandler();
        }

        private void clockMarkerPointer1_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (Math.Abs(e.NewValue - e.OldValue) > 2.4 || e.NewValue > clockMarkerPointer2.Value ||
            Math.Abs(e.NewValue - clockMarkerPointer1.Value) > 1.2)
                e.Cancel = true;
            else
                UpdateClockAnnotationLabel(e.NewValue, true);
        }

        private void clockMarkerPointer2_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (Math.Abs(e.NewValue - e.OldValue) > 2.4 || e.NewValue < clockMarkerPointer1.Value ||
            Math.Abs(e.NewValue - clockMarkerPointer2.Value) > 1.2)
                e.Cancel = true;
            else
                UpdateClockAnnotationLabel(e.NewValue, false);
        }

        private void UpdateClockAnnotationLabel(double newValue, bool isFirstMarker)
        {
            double value = newValue - (int)newValue;
            value = value > 0.5 ? Math.Ceiling(newValue) : Math.Floor(newValue);

            if (isFirstMarker)
                clockAnnotationLabel1.Text = (value == 0 ? 12 : value) + " AM";
            else
                clockAnnotationLabel3.Text = (value == 0 ? 12 : value) + " AM";
        }


        bool isStartPointer;
        private void clockMarkerPointer1_ValueChangeStarted(object sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
        {
            if (AnimationExtensions.AnimationIsRunning(this.clockGauge, "DragStartAnimation"))
            {
                this.OnAnimationFinished(0, true);
            }

            isStartPointer = true;
            this.DragAnimation(false);
        }

        private void clockMarkerPointer2_ValueChangeStarted(object sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
        {
            if (AnimationExtensions.AnimationIsRunning(this.clockGauge, "DragStartAnimation"))
            {
                this.OnAnimationFinished(0, true);
            }
            isStartPointer = false;
            this.DragAnimation(false);
        }

        private void clockMarkerPointer1_ValueChangeCompleted(object sender, Syncfusion.Maui.Gauges.ValueChangedEventArgs e)
        {
            this.DragAnimation(true);
        }

        private void DragAnimation(bool isCompleted)
        {
            double annotationFadeAnimationStart = isCompleted ? 0 : 1;
            double annotationFadeAnimationEnd = isCompleted ? 1 : 0;
            double annotationPositionAnimationStart = isCompleted ? 0 : 0.25;
            double annotationPositionAnimationEnd = isCompleted ? 0.25 : 0;
            double annotationScaleAnimationStart = isCompleted ? 1.8 : 1;
            double annotationScaleAnimationEnd = isCompleted ? 1 : 1.8;

#if ANDROID
            GaugeAnnotation gaugeAnnotation = isStartPointer ? StartValueAnnotation : EndValueAnnotation;
            gaugeAnnotation.Content.AnchorX = 0.49;
            gaugeAnnotation.Content.AnchorY = 0.49;

#endif
            var parentAnimation = new Animation();

            var animation = new Animation(this.OnAnnotationFadeOutUpdate, annotationFadeAnimationStart, annotationFadeAnimationEnd, Easing.Linear, null);
            parentAnimation.Add(0, 1, animation);

            animation = new Animation(this.OnAnnotationPositionFactorUpdate, annotationPositionAnimationStart, annotationPositionAnimationEnd, Easing.Linear, null);
            parentAnimation.Add(0, 1, animation);

            animation = new Animation(this.OnAnnotationScaleUpdate, annotationScaleAnimationStart, annotationScaleAnimationEnd, Easing.Linear, null);
            parentAnimation.Add(0, 1, animation);

            parentAnimation.Commit(this.clockGauge, "DragStartAnimation", 16, (uint)300,
                Easing.Linear, OnAnimationFinished, null);
        }

        private void OnAnnotationFadeOutUpdate(double value)
        {
            Label label = isStartPointer ? clockAnnotationLabel3 : clockAnnotationLabel1;
            label.Opacity = value;
            clockAnnotationLabel2.Opacity = value;
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

        private void OnAnimationFinished(double value, bool isCompleted)
        {
            AnimationExtensions.AbortAnimation(this.clockGauge, "DragStartAnimation");
        }

        #endregion
    }
}