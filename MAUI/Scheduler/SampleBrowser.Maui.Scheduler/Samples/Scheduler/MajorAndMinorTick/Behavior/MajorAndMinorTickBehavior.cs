namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    using System;
    using System.Globalization;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Inputs;
    using Syncfusion.Maui.Scheduler;
    using Syncfusion.Maui.Sliders;

    /// <summary>
    /// Major and minor tick Behavior class.
    /// </summary>
    internal class MajorAndMinorTickBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// The pixels-per-slot constant used to size each major tick.
        /// slot height (DaysView) and slot width (TimelineView) = 28 * TimeSlotCount.
        /// </summary>
        private const double PixelsPerSlot = 28d;

        /// <summary>
        /// The scheduler instance.
        /// </summary>
        private SfScheduler? scheduler;

        /// <summary>
        /// The time slot count picker.
        /// </summary>
        private SfSlider? timeSlotCountSlider;

        /// <inheritdoc/>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);

            this.scheduler = bindable.Content.FindByName<SfScheduler>("Scheduler");
            this.timeSlotCountSlider = bindable.Content.FindByName<SfSlider>("timeSlotCountSlider");

            if (this.timeSlotCountSlider != null)
            {
                this.timeSlotCountSlider.ValueChanged += TimeSlotCountValueChanged;
            }

            if (this.scheduler != null)
            {
                this.scheduler.DaysView.TimeSlotCount = 2;
                this.scheduler.TimelineView.TimeSlotCount = 2;
                this.scheduler.ViewChanged += OnSchedulerViewChanged;
            }
        }

        /// <summary>
        /// Method to apply the chosen time slot count to both the days and timeline views.
        /// </summary>
        /// <param name="timeSlotCount">The selected time slot count.</param>
        private void ApplyTimeSlotCount(int timeSlotCount)
        {
            if (this.scheduler == null)
            {
                return;
            }

            if (timeSlotCount < 2)
            {
                this.scheduler.DaysView.TimeSlotCount = timeSlotCount;
                this.scheduler.DaysView.TimeIntervalHeight = 50;

                this.scheduler.TimelineView.TimeSlotCount = timeSlotCount;
                this.scheduler.TimelineView.TimeIntervalWidth = double.NaN;
                return;
            }

            double majorSize = PixelsPerSlot * timeSlotCount;

            this.scheduler.DaysView.TimeSlotCount = timeSlotCount;
            this.scheduler.DaysView.TimeIntervalHeight = majorSize;

            this.scheduler.TimelineView.TimeSlotCount = timeSlotCount;
            this.scheduler.TimelineView.TimeIntervalWidth = majorSize;
        }

        /// <summary>
        /// Method for time slot count slider value changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void TimeSlotCountValueChanged(object? sender, SliderValueChangedEventArgs e)
        {
            int timeSlotCount = (int)e.NewValue;
            this.ApplyTimeSlotCount(timeSlotCount);
        }

        /// <summary>
        /// Method for scheduler view changed - keeps the slot sizing consistent across view switches.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="args">Event Args</param>
        private void OnSchedulerViewChanged(object? sender, SchedulerViewChangedEventArgs args)
        {
            if (args.OldView == args.NewView || this.timeSlotCountSlider == null || this.scheduler == null)
            {
                return;
            }

            int timeSlotCount = (int)this.timeSlotCountSlider.Value;
            this.ApplyTimeSlotCount(timeSlotCount);
        }

        /// <inheritdoc/>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);

            if (this.timeSlotCountSlider != null)
            {
                this.timeSlotCountSlider.ValueChanged -= TimeSlotCountValueChanged;
                this.timeSlotCountSlider = null;
            }

            if (this.scheduler != null)
            {
                this.scheduler.ViewChanged -= OnSchedulerViewChanged;
                this.scheduler = null;
            }
        }
    }
}