namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    using System;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Scheduler;
    using Syncfusion.Maui.Sliders;

    /// <summary>
    /// Major and minor tick Behavior class.
    /// </summary>
    internal class LimitConcurrentEventBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// The scheduler instance.
        /// </summary>
        private SfScheduler? scheduler;

        /// <summary>
        /// The maximum-events-per-slot slider.
        /// </summary>
        private SfSlider? maxEventStackSlider;

        /// <inheritdoc/>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);

            this.scheduler = bindable.Content.FindByName<SfScheduler>("Scheduler");
            this.maxEventStackSlider = bindable.Content.FindByName<SfSlider>("maxEventStackSlider");
            ApplyMaxEventStack(1);
            if (this.maxEventStackSlider != null)
            {
                this.maxEventStackSlider.ValueChanged += OnMaxEventStackSliderValueChanged;
            }
        }

        ///// <summary>
        ///// Pushes the slider value onto <see cref="SchedulerDaysView.MaxEventStack"/>.
        ///// </summary>
        ///// <param name="sender">return the object</param>
        ///// <param name="e">Event Args</param>
        private void OnMaxEventStackSliderValueChanged(object? sender, SliderValueChangedEventArgs e)
        {
            if (this.maxEventStackSlider == null)
            {
                return;
            }

            int maxEventStack = (int)this.maxEventStackSlider.Value;
            ApplyMaxEventStack(maxEventStack);
        }

        /// <summary>
        /// Writes the supplied cap to the scheduler's days view.
        /// </summary>
        /// <param name="maxEventStack">The maximum number of events visible per time slot.</param>
        private void ApplyMaxEventStack(int maxEventStack)
        {
            if (this.scheduler == null)
            {
                return;
            }

            this.scheduler.DaysView.MaxEventStack = maxEventStack;
        }

        /// <inheritdoc/>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);

            if (this.maxEventStackSlider != null)
            {
                this.maxEventStackSlider.ValueChanged -= OnMaxEventStackSliderValueChanged;
                this.maxEventStackSlider = null;
            }

            if (this.scheduler != null)
            {
                this.scheduler = null;
            }
        }
    }
}