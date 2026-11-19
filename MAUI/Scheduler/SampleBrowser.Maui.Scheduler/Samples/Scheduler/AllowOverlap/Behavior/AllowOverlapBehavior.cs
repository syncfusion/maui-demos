namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    using Microsoft.Maui.Controls;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Buttons;
    using Syncfusion.Maui.Scheduler;

    /// <summary>
    /// Allow overlap Behavior class.
    /// </summary>
    internal class AllowOverlapBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// The scheduler instance.
        /// </summary>
        private SfScheduler? scheduler;

        /// <summary>
        /// The allow overlap switch.
        /// </summary>
        private SfSwitch? allowOverlapSwitch;

        /// <inheritdoc/>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);

            this.scheduler = bindable.Content.FindByName<SfScheduler>("Scheduler");
            this.allowOverlapSwitch = bindable.Content.FindByName<SfSwitch>("allowOverlapSwitch");

            if (this.allowOverlapSwitch != null)
            {
                this.allowOverlapSwitch.StateChanged += OnAllowOverlapSwitchStateChanged;
            }
        }

        /// <summary>
        /// Method for allow overlap switch state changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void OnAllowOverlapSwitchStateChanged(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.scheduler != null && e.NewValue != null)
            {
                this.scheduler.AllowOverlap = e.NewValue.Value;
            }
        }

        /// <inheritdoc/>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);

            if (this.allowOverlapSwitch != null)
            {
                this.allowOverlapSwitch.StateChanged -= OnAllowOverlapSwitchStateChanged;
                this.allowOverlapSwitch = null;
            }

            if (this.scheduler != null)
            {
                this.scheduler = null;
            }
        }
    }
}