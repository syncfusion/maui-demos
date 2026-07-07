namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    using System;
    using Microsoft.Maui.Controls;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Buttons;
    using Syncfusion.Maui.Inputs;
    using Syncfusion.Maui.Scheduler;

    /// <summary>
    /// Appointment tooltip Behavior class
    /// </summary>
    internal class AppointmentTooltipBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// schedule initialize
        /// </summary>
        private SfScheduler? scheduler;

        /// <summary>
        /// The appointment tooltip position picker
        /// </summary>
        private SfComboBox? toolTipPositionPicker;

        /// <inheritdoc/>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);

            this.scheduler = bindable.Content.FindByName<SfScheduler>("Scheduler");
            this.toolTipPositionPicker = bindable.Content.FindByName<SfComboBox>("ToolTipPositionPicker");

            if (this.toolTipPositionPicker != null)
            {
                this.toolTipPositionPicker.SelectionChanged += OnAppointmentToolTipPositionChanged;
            }
        }

        /// <summary>
        /// Method for appointment tooltip position picker changed
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void OnAppointmentToolTipPositionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.scheduler == null || this.toolTipPositionPicker == null)
            {
                return;
            }

            var selectedToolTipPosition = this.toolTipPositionPicker.SelectedItem?.ToString();

            if (selectedToolTipPosition == "Auto")
            {
                this.scheduler.AppointmentToolTipSettings.ToolTipPosition = SchedulerToolTipPosition.Auto;
            }
            else if (selectedToolTipPosition == "Left")
            {
                this.scheduler.AppointmentToolTipSettings.ToolTipPosition = SchedulerToolTipPosition.Left;
            }
            else if (selectedToolTipPosition == "Top")
            {
                this.scheduler.AppointmentToolTipSettings.ToolTipPosition = SchedulerToolTipPosition.Top;
            }
            else if (selectedToolTipPosition == "Right")
            {
                this.scheduler.AppointmentToolTipSettings.ToolTipPosition = SchedulerToolTipPosition.Right;
            }
            else
            {
                this.scheduler.AppointmentToolTipSettings.ToolTipPosition = SchedulerToolTipPosition.Bottom;
            }
        }

        /// <inheritdoc/>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);

            if (this.toolTipPositionPicker != null)
            {
                this.toolTipPositionPicker.SelectionChanged -= OnAppointmentToolTipPositionChanged;
                this.toolTipPositionPicker = null;
            }

            if (this.scheduler != null)
            {
                this.scheduler = null;
            }
        }
    }
}