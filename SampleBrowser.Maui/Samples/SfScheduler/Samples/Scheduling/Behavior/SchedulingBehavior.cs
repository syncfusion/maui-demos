#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Core;
using System;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SampleBrowser.Maui.SfScheduler
{
    internal class SchedulingBehavior : Behavior<SampleView>
    {
        #region Fields

        /// <summary>
        /// The scheduler initialize.
        /// </summary>
        private Syncfusion.Maui.Scheduler.SfScheduler schedule;

        /// <summary>
        /// The appointment editor view, used for Android and iOS platforms.
        /// </summary>
        private AppointmentEditorView appointmentEditorView;

        /// <summary>
        /// The appointment editor window used for Windows platform.
        /// </summary>
        private AppointmentEditorWindow appointmentEditorWindow;

        /// <summary>
        /// The IsNew appointment.
        /// </summary>
        private bool isNewAppointment;

        /// <summary>
        /// Editing recurrence appointment.
        /// </summary>
        private Meeting recurrenceAppointment;

#if WINDOWS
        /// <summary>
        /// The recurrence editing option view.
        /// </summary>
        private StackLayout recurrenceEditingOptionView;

        /// <summary>
        /// The single radio button.
        /// </summary>
        private RadioButton singleRadioButton;

        /// <summary>
        /// The series radio button.
        /// </summary>
        private RadioButton seriesRadioButton;

        /// <summary>
        /// The recurring editor option view ok button.
        /// </summary>
        private Button recurringEditorOptionViewOkButton;

        /// <summary>
        /// The recurring editor option view cancel button.
        /// </summary>
        private Button recurringEditorOptionViewCancelButton;
#endif

        #endregion

        #region Methods

        /// <summary>
        /// Begins when the behavior attached to the view. 
        /// </summary>
        /// <param name="bindable">The bindable values.</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            if (bindable == null)
            {
                return;
            }

            base.OnAttachedTo(bindable);
            var type = bindable.GetType();
            this.schedule = bindable.Content.FindByName<Syncfusion.Maui.Scheduler.SfScheduler>("Schedule");
            this.appointmentEditorView = bindable.Content.FindByName<AppointmentEditorView>("AppointmentEditorView");
            this.appointmentEditorWindow = bindable.Content.FindByName<AppointmentEditorWindow>("AppointmentEditorWindow");

            //// Faced framework issue on Picker, DatePicker and TimePicker.
            //// this.schedule.DoubleTapped += this.OnScheduleDoubleTapped;
#if WINDOWS
            (this.appointmentEditorWindow.BindingContext as SchedulerDataBindingViewModel).AppointmentEditorClosed += this.OnAppointmentEditorClosed;
            this.recurrenceEditingOptionView = bindable.Content.FindByName<RecurrenceEditingOptionView>("RecurrenceEditingOptionView");

            if (this.recurrenceEditingOptionView != null)
            {
                this.singleRadioButton = this.recurrenceEditingOptionView.FindByName<RadioButton>("SingleRadioButton");
                this.seriesRadioButton = this.recurrenceEditingOptionView.FindByName<RadioButton>("SeriesRadioButton");
                this.recurringEditorOptionViewOkButton = this.recurrenceEditingOptionView.FindByName<Button>("RecurrenceOkButton");
                this.recurringEditorOptionViewCancelButton = this.recurrenceEditingOptionView.FindByName<Button>("RecurrenceCancelButton");
                if (this.recurringEditorOptionViewOkButton != null)
                {
                    this.recurringEditorOptionViewOkButton.Clicked += this.OnRecurringEditorOptionViewOkButtonClicked;
                }

                if (this.recurringEditorOptionViewCancelButton != null)
                {
                    this.recurringEditorOptionViewCancelButton.Clicked += this.OnRecurringEditorOptionViewCancelButtonClicked;
                }
            }
#else
            (this.appointmentEditorView.BindingContext as SchedulerDataBindingViewModel).AppointmentEditorClosed += this.OnAppointmentEditorClosed;
#endif
        }

        /// <summary>
        /// Begins when the behavior de-attached from the view. 
        /// </summary>
        /// <param name="bindable">The bindable values.</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
#if WINDOWS
            if (this.appointmentEditorWindow != null)
            {
                (this.appointmentEditorWindow.BindingContext as SchedulerDataBindingViewModel).AppointmentEditorClosed -= this.OnAppointmentEditorClosed;
                this.appointmentEditorWindow = null;
            }

            if (this.singleRadioButton != null)
            {
                this.singleRadioButton = null;
            }

            if (this.seriesRadioButton != null)
            {
                this.seriesRadioButton = null;
            }

            if (this.recurringEditorOptionViewOkButton != null)
            {
                this.recurringEditorOptionViewOkButton.Clicked -= this.OnRecurringEditorOptionViewOkButtonClicked;
                this.recurringEditorOptionViewOkButton = null;
            }

            if (this.recurringEditorOptionViewCancelButton != null)
            {
                this.recurringEditorOptionViewCancelButton.Clicked -= this.OnRecurringEditorOptionViewCancelButtonClicked;

                this.recurringEditorOptionViewCancelButton = null;
            }

            if (this.recurrenceEditingOptionView != null)
            {
                this.recurrenceEditingOptionView = null;
            }
#else
            if (this.appointmentEditorView != null)
            {
                (this.appointmentEditorView.BindingContext as SchedulerDataBindingViewModel).AppointmentEditorClosed -= this.OnAppointmentEditorClosed;
                this.appointmentEditorView = null;
            }
#endif

            if (this.recurrenceAppointment != null)
            {
                this.recurrenceAppointment = null;
            }

            if (this.schedule != null)
            {
                this.schedule.DoubleTapped -= this.OnScheduleDoubleTapped;
                this.schedule = null;
            }

            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        /// Occurs when the appoinment properties modified.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The appointment modified event args.</param>
        private void OnAppointmentEditorClosed(object sender, AppointmentEditorClosedEventArgs e)
        {
            this.appointmentEditorView.IsVisible = false;
            var appointment = e.Appointment;
            var appointmentsSource = this.schedule.AppointmentsSource as ObservableCollection<Meeting>;
#if !WINDOWS
            var appointmentEditorBehavior = this.appointmentEditorView.Behaviors[0] as AppointmentEditorBehavior;
#endif

            if (isNewAppointment)
            {
                appointmentsSource.Add(appointment);
            }
            else if (e.IsEdit)
            {
#if WINDOWS
                if (this.recurrenceAppointment != null)
                {
                    var parentAppointment = this.GetParentAppointment(appointment.Id) as Meeting;
                    if (parentAppointment == null)
                    {
                        return;
                    }

                    if (this.singleRadioButton.IsChecked && appointment.RecurrenceId == null)
                    {
                        appointment.RecurrenceRule = string.Empty;
                        appointment.RecurrenceId = parentAppointment.Id;
                        parentAppointment.RecurrenceExceptions.Add(appointment.From);
                        appointmentsSource.Add(appointment);
                    }
                    else if (this.seriesRadioButton.IsChecked)
                    {
                        if (!parentAppointment.From.Equals(appointment.From) || !parentAppointment.To.Equals(appointment.To) || !parentAppointment.StartTimeZone.Equals(appointment.StartTimeZone) || !parentAppointment.EndTimeZone.Equals(appointment.EndTimeZone))
                        {
                            parentAppointment.RecurrenceExceptions?.Clear();
                            this.UpdateParentAppointment(parentAppointment, appointment);
                            parentAppointment.From = appointment.IsAllDay ? appointment.From : this.ConvertTimeToAppointmentTimeZone(appointment.From, appointment.StartTimeZone, this.schedule.TimeZone);
                            parentAppointment.To = appointment.IsAllDay ? appointment.To : this.ConvertTimeToAppointmentTimeZone(appointment.To, appointment.EndTimeZone, this.schedule.TimeZone);
                        }
                        else
                        {
                            this.UpdateParentAppointment(parentAppointment, appointment);
                        }
                    }
                }
#else
                if (this.recurrenceAppointment != null)
                {
                    var parentAppointment = this.GetParentAppointment(appointment.Id) as Meeting;
                    if (parentAppointment == null)
                    {
                        return;
                    }

                    if (appointmentEditorBehavior.SingleRadioButton.IsChecked)
                    {
                        appointment.RecurrenceRule = string.Empty;
                        if (appointment.RecurrenceId == null)
                        {
                            appointment.RecurrenceId = parentAppointment.Id;
                            parentAppointment.RecurrenceExceptions.Add(appointment.From);
                            appointmentsSource.Add(appointment);
                        }
                        else
                        {
                            this.UpdateParentAppointment(this.recurrenceAppointment, appointment);
                        }
                    }
                    else if (appointmentEditorBehavior.SeriesRadioButton.IsChecked)
                    {
                        DateTime appStartDate = parentAppointment.From.Date.Add(appointment.From.TimeOfDay);
                        DateTime appEndDate = parentAppointment.To.Date.Add(appointment.To.TimeOfDay);
                        if (!this.recurrenceAppointment.From.Equals(appointment.From) || !this.recurrenceAppointment.To.Equals(appointment.To) || !this.recurrenceAppointment.StartTimeZone.Equals(appointment.StartTimeZone) || !this.recurrenceAppointment.EndTimeZone.Equals(appointment.EndTimeZone))
                        {
                            parentAppointment.RecurrenceExceptions?.Clear();
                            this.UpdateParentAppointment(parentAppointment, appointment);
                            parentAppointment.From = appointment.IsAllDay ? appStartDate : this.ConvertTimeToAppointmentTimeZone(appStartDate, appointment.StartTimeZone, this.schedule.TimeZone);
                            parentAppointment.To = appointment.IsAllDay ? appEndDate : this.ConvertTimeToAppointmentTimeZone(appEndDate, appointment.EndTimeZone, this.schedule.TimeZone);
                        }
                        else
                        {
                            appointment.From = appStartDate;
                            appointment.To = appEndDate;
                            this.UpdateParentAppointment(parentAppointment, appointment);
                        }
                    }
                }
#endif
            }
            else if (e.IsDelete)
            {
                var parentAppointment = this.GetParentAppointment(e.Appointment.Id) as Meeting;
                if (parentAppointment != null && this.recurrenceAppointment != null)
                {
#if WINDOWS
                    if (this.singleRadioButton.IsChecked)
#else
                    if (appointmentEditorBehavior.SingleRadioButton.IsChecked)
#endif
                    {
                        parentAppointment.RecurrenceExceptions.Add(appointment.From);
                    }
                    else
                    {
                        parentAppointment.RecurrenceExceptions.Clear();
                        appointmentsSource.Remove(parentAppointment);
                    }
                }
                else
                {
                    appointmentsSource.Remove(appointment);
                }
            }
        }

        /// <summary>
        /// Method to convert actual date time from the specified time zone.
        /// </summary>
        /// <param name="date">The data.</param>
        /// <param name="appTimeZone">The appointment time zone id.</param>
        /// <param name="schedulerTimeZone">The scheduler time zone id.</param>
        /// <returns>The date time.</returns>
        internal DateTime ConvertTimeToAppointmentTimeZone(DateTime date, TimeZoneInfo appTimeZone, TimeZoneInfo schedulerTimeZone)
        {
            DateTime newdate = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Unspecified);
            if (appTimeZone == TimeZoneInfo.Local && schedulerTimeZone == TimeZoneInfo.Local)
            {
                return newdate;
            }

            if (appTimeZone != TimeZoneInfo.Local)
            {
                newdate = TimeZoneInfo.ConvertTime(newdate, appTimeZone, schedulerTimeZone);
            }
            else if (schedulerTimeZone != TimeZoneInfo.Local)
            {
                newdate = TimeZoneInfo.ConvertTime(newdate, TimeZoneInfo.Utc, schedulerTimeZone);
            }

            return newdate;
        }

        /// <summary>
        /// Method to clone selected appointment.
        /// </summary>
        /// <param name="parentAppointment">Parent appointment instance.</param>
        /// <param name="appointment">The selected appointment.</param>
        /// <returns>The cloned appointment.</returns>
        private void UpdateParentAppointment(Meeting parentAppointment, Meeting appointment)
        {
            if (!parentAppointment.From.Equals(appointment.From))
            {
                parentAppointment.From = appointment.From;
            }

            if (!parentAppointment.To.Equals(appointment.To))
            {
                parentAppointment.To = appointment.To;
            }

            if (parentAppointment.EventName == null || !parentAppointment.EventName.Equals(appointment.EventName))
            {
                parentAppointment.EventName = appointment.EventName;
            }

            if (parentAppointment.Background == null || !parentAppointment.Background.Equals(appointment.Background))
            {
                parentAppointment.Background = appointment.Background;
            }

            if (parentAppointment.StartTimeZone == null || !parentAppointment.StartTimeZone.Equals(appointment.StartTimeZone))
            {
                parentAppointment.StartTimeZone = appointment.StartTimeZone;
            }

            if (parentAppointment.EndTimeZone == null || !parentAppointment.EndTimeZone.Equals(appointment.EndTimeZone))
            {
                parentAppointment.EndTimeZone = appointment.EndTimeZone;
            }

            if (parentAppointment.RecurrenceRule == null || !parentAppointment.RecurrenceRule.Equals(appointment.RecurrenceRule))
            {
                parentAppointment.RecurrenceRule = appointment.RecurrenceRule;
            }

            if (parentAppointment.Notes == null || !parentAppointment.Notes.Equals(appointment.Notes))
            {
                parentAppointment.Notes = appointment.Notes;
            }

            parentAppointment.IsAllDay = appointment.IsAllDay;
        }

        /// <summary>
        /// Occues when tap on scheduler cell or appointment.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnScheduleDoubleTapped(object sender, SchedulerDoubleTappedEventArgs e)
        {
            bool isAllDay = false;
#if WINDOWS
            var appointmentEditorWindowBehavior = this.appointmentEditorWindow.Behaviors[0] as AppointmentEditorWindowBehavior;
            if (appointmentEditorWindowBehavior == null || this.appointmentEditorWindow.IsVisible)
            {
                return;
            }
#else
            var appointmentEditorBehavior = this.appointmentEditorView.Behaviors[0] as AppointmentEditorBehavior;
            if (appointmentEditorBehavior == null || this.appointmentEditorView.IsVisible)
            {
                return;
            }
#endif

            this.recurrenceAppointment = null;
            if (e.Appointments != null && e.Appointments.Count != 0 && e.Element == SchedulerElement.Appointment)
            {
                var app = e.Appointments[0] as Meeting;
                this.isNewAppointment = false;

#if WINDOWS
                if (!string.IsNullOrEmpty(app.RecurrenceRule) || (app.RecurrenceId != null && app.Id != null))
                {
                    this.singleRadioButton.IsChecked = true;
                    this.seriesRadioButton.IsChecked = false;
                    this.recurrenceAppointment = app;
                    this.recurrenceEditingOptionView.IsVisible = true;
                }
                else
                {
                    appointmentEditorWindowBehavior.EditAppointment(app);
                }
#else
                if (!string.IsNullOrEmpty(app.RecurrenceRule) || (app.RecurrenceId != null && app.Id != null))
                {
                    this.recurrenceAppointment = app;
                    app = AppointmentHelper.CloneAppointment(app);
                }

                appointmentEditorBehavior.EditAppointment(app);
#endif
            }
            else if (e.Element == SchedulerElement.SchedulerCell)
            {
                if (this.schedule.View == SchedulerView.Month)
                {
                    isAllDay = true;
                }

                this.isNewAppointment = true;

#if WINDOWS
                appointmentEditorWindowBehavior.AddNewAppointment((DateTime)e.Date, isAllDay);
#else
                appointmentEditorBehavior.AddNewAppointment((DateTime)e.Date, isAllDay);
#endif

            }
        }

        /// <summary>
        /// Method to get the parent appointment of recurrence appointment.
        /// </summary>
        /// <param name="recurrenceId">The recurrence id.</param>
        /// <returns>The parent appointment.</returns>
        internal object GetParentAppointment(object recurrenceId)
        {
            var appointments = new List<Meeting>();

            foreach (var app in (this.schedule.AppointmentsSource as ObservableCollection<Meeting>))
            {
                appointments.Add(app);
            }

            if (recurrenceId == null)
            {
                return null;
            }

            var appointment = appointments.Find(app => (app as Meeting).Id.Equals(recurrenceId));
            if (appointment == null)
            {
                return null;
            }

            return appointment;
        }


#if WINDOWS
        /// <summary>
        /// Occurs when button clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnRecurringEditorOptionViewCancelButtonClicked(object sender, EventArgs e)
        {
            this.recurrenceEditingOptionView.IsVisible = false;
            this.recurrenceAppointment = null;
        }

        /// <summary>
        /// Occurs when button clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnRecurringEditorOptionViewOkButtonClicked(object sender, EventArgs e)
        {
            this.recurrenceEditingOptionView.IsVisible = false;
            Meeting app = this.singleRadioButton.IsChecked ? this.recurrenceAppointment : AppointmentHelper.CloneAppointment(this.GetParentAppointment(this.recurrenceAppointment.Id) as Meeting);
            var appointmentEditorWindowBehavior = this.appointmentEditorWindow.Behaviors[0] as AppointmentEditorWindowBehavior;
            if (appointmentEditorWindowBehavior == null || app == null)
            {
                this.recurrenceAppointment = null;
                return;
            }

            appointmentEditorWindowBehavior.EditAppointment(app);
        }
#endif
#endregion
    }
}
