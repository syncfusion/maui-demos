#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    using System;
    using Microsoft.Maui.Controls;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Buttons;
    using Syncfusion.Maui.Inputs;
    using Syncfusion.Maui.Scheduler;

    /// <summary>
    /// Appointment editor Behavior class
    /// </summary>
    internal class AppointmentEditorBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// schedule initialize
        /// </summary>
        private SfScheduler? scheduler;

        /// <summary>
        /// The appointment edit mode picker
        /// </summary>
        private SfComboBox? appointmentEditorModePicker;

        /// <inheritdoc/>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);

            this.scheduler = bindable.Content.FindByName<SfScheduler>("Scheduler");
            this.appointmentEditorModePicker = bindable.Content.FindByName<SfComboBox>("AppointmentEditorModePicker");

            if (this.appointmentEditorModePicker != null)
            {
                this.appointmentEditorModePicker.SelectionChanged += OnAppointmentEditorModeChanged;
            }
        }

        /// <summary>
        /// Method for appointment editor mode picker changed
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void OnAppointmentEditorModeChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.scheduler == null || this.appointmentEditorModePicker == null)
            {
                return;
            }

            var selectedEditorMode = this.appointmentEditorModePicker.SelectedItem?.ToString();

            if (selectedEditorMode == "Add")
            {
                this.scheduler.AppointmentEditorMode = AppointmentEditorMode.Add;
            }
            else if (selectedEditorMode == "Edit")
            {
                this.scheduler.AppointmentEditorMode = AppointmentEditorMode.Edit;
            }
            else if (selectedEditorMode == "None")
            {
                this.scheduler.AppointmentEditorMode = AppointmentEditorMode.None;
            }
            else
            {
                this.scheduler.AppointmentEditorMode = AppointmentEditorMode.Add | AppointmentEditorMode.Edit;
            }
        }

        /// <inheritdoc/>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);

            if (this.appointmentEditorModePicker != null)
            {
                this.appointmentEditorModePicker.SelectionChanged -= OnAppointmentEditorModeChanged;
                this.appointmentEditorModePicker = null;
            }

            if (this.scheduler != null)
            {
                this.scheduler = null;
            }
        }
    }
}