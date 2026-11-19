namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    using Microsoft.Maui.Controls;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Buttons;
    using Syncfusion.Maui.Scheduler;
    using System.Collections.ObjectModel;

    internal class HolidayCalendarBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// the scheduler object.
        /// </summary>
        private SfScheduler? scheduler;

        /// <summary>
        /// the scheduler view model.
        /// </summary>
        private SchedulerViewModel? viewModel;

        /// <summary>
        /// the holiday dates list.
        /// </summary>
        private HashSet<DateTime>? holidayDates;

        /// <summary>
        /// The holiday switch.
        /// </summary>
        private SfSwitch? holidaySwitch;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value.</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.scheduler = bindable.Content.FindByName<SfScheduler>("Scheduler");
            this.holidaySwitch = bindable.Content.FindByName<SfSwitch>("holidaySwitch");
            this.viewModel = bindable.BindingContext as SchedulerViewModel;
            this.holidayDates = this.viewModel?.GetIndianHolidayDates(2026);
            this.scheduler.AppointmentDragStarting += this.Scheduler_AppointmentDragStarting;
            this.scheduler.AppointmentResizeStart += this.Scheduler_AppointmentResizeStart;
            this.scheduler.AppointmentEditorOpening += this.Scheduler_AppointmentEditorOpening;
            this.scheduler.AppointmentDrop +=  this.OnSchedulerAppointmentDrop;
            this.scheduler.AppointmentResizing +=  this.OnSchedulerAppointmentResizing;
            this.scheduler.AppointmentEditorClosing +=  this.OnSchedulerAppointmentEditorClosing;
        }

        /// <summary>
        /// Invokes on scheduler appointment editor opening.
        /// </summary>
        /// <param name="sender">The scheduler object.</param>
        /// <param name="e">The appointment editor opening event args.</param>
        private void Scheduler_AppointmentEditorOpening(object? sender, AppointmentEditorOpeningEventArgs e)
        {
            if (e.Appointment?.IsReadOnly == true)
            {
                e.Cancel = true;
            }

            if (this.holidayDates == null || this.holidaySwitch?.IsOn == true)
            {
                return;
            }

            // Cancel if editing appointment is a holiday
            if (e.Appointment != null)
            {
                if (IsHolidayRange(e.Appointment.StartTime, e.Appointment.EndTime))
                {
                    e.Cancel = true;
                }
            }
            else if (e.DateTime != null)
            {
                if (this.holidayDates.Contains(e.DateTime.Value))
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Invokes on scheduler appointment resize start.
        /// </summary>
        /// <param name="sender">The scheduler object.</param>
        /// <param name="e">The appointment resize start event args.</param>
        private void Scheduler_AppointmentResizeStart(object? sender, AppointmentResizeStartEventArgs e)
        {
            if (e.Appointment?.IsReadOnly == true)
            {
                e.Cancel = true;
            }

            if (e.Appointment != null && this.holidaySwitch?.IsOn == false)
            {
                if (IsHolidayRange(e.Appointment.StartTime, e.Appointment.EndTime))
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Invokes on scheduler appointment drag start.
        /// </summary>
        /// <param name="sender">The scheduler object.</param>
        /// <param name="e">The appointment drag start event args.</param>
        private void Scheduler_AppointmentDragStarting(object? sender, AppointmentDragStartingEventArgs e)
        {
            if (e.Appointment?.IsReadOnly == true)
            {
                e.Cancel = true;
            }

            if (e.Appointment != null && this.holidaySwitch?.IsOn == false)
            {
                if (IsHolidayRange(e.Appointment.StartTime, e.Appointment.EndTime))
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Invokes on scheduler view appointment drop.
        /// </summary>
        /// <param name="sender">The scheduler object.</param>
        /// <param name="e">The appointment editor event args.</param>
        private void  OnSchedulerAppointmentEditorClosing(object? sender, AppointmentEditorClosingEventArgs e)
        {
            if (e.Appointment?.IsReadOnly == true)
            {
                e.Cancel = true;
            }

            if (!string.IsNullOrEmpty(e.Appointment?.RecurrenceRule) && (e.Action == AppointmentEditorAction.Edit || e.Action == AppointmentEditorAction.Add))
            {
                e.Handled = true;
                var appointment = e.Appointment;
                if (this.scheduler?.AppointmentsSource is ObservableCollection<SchedulerAppointment> schedulerAppointments)
                {
                    appointment.RecurrenceExceptionDates = new ObservableCollection<DateTime>(this.holidayDates ?? new HashSet<DateTime>());
                    schedulerAppointments.Add(appointment);
                }
            }

            if (e.Appointment == null || this.holidaySwitch?.IsOn == true)
            {
                return;
            }

            // Cancel if editing appointment is a holiday
            if (IsHolidayRange(e.Appointment.StartTime, e.Appointment.EndTime))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Invokes on scheduler appointment resize end.
        /// </summary>
        /// <param name="sender">The scheduler object.</param>
        /// <param name="e">The appointment resize event args.</param>
        private void OnSchedulerAppointmentResizing(object? sender, AppointmentResizingEventArgs e)
        {
            if (e.Appointment?.IsReadOnly == true)
            {
                e.Cancel = true;
            }

            if (this.holidayDates == null || this.holidaySwitch?.IsOn == true)
            {
                return;
            }

            // Cancel if resized appointment is a holiday
            if (e.Appointment != null && IsHolidayRange(e.Appointment.StartTime.Date, e.ResizingTime.Date) || this.holidayDates.Contains(e.ResizingTime.Date))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Invokes on scheduler view appointment drop.
        /// </summary>
        /// <param name="sender">The scheduler object.</param>
        /// <param name="e">The appointment drop event args.</param>
        private void  OnSchedulerAppointmentDrop(object? sender, AppointmentDropEventArgs e)
        {
            if (e.Appointment?.IsReadOnly == true)
            {
                e.Cancel = true;
            }

            if (this.holidayDates == null || this.holidaySwitch?.IsOn == true)
            {
                return;
            }

            // Cancel if dropped appointment is a holiday
            if (this.holidayDates.Contains(e.DropTime.Date))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Checks whether the specified date range contains any holiday date.
        /// </summary>
        /// <param name="start">The start date of the range.</param>
        /// <param name="end">The end date of the range.</param>
        /// <returns>
        /// <c>true</c> if at least one holiday exists between the start and end dates (inclusive);
        /// otherwise, <c>false</c>.
        /// </returns>
        private bool IsHolidayRange(DateTime start, DateTime end)
        {
            if (this.holidayDates == null)
            {
                return false;
            }

            DateTime current = start.Date;
            DateTime last = end.Date;

            while (current <= last)
            {
                if (this.holidayDates.Contains(current))
                {
                    return true;
                }

                current = current.AddDays(1);
            }

            return false;
        }

        /// <summary>
        /// Begins when the behavior detaching from the view. 
        /// </summary>
        /// <param name="bindable">bindable value.</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.scheduler != null)
            {
                this.scheduler.AppointmentDragStarting -= this.Scheduler_AppointmentDragStarting;
                this.scheduler.AppointmentResizeStart -= this.Scheduler_AppointmentResizeStart;
                this.scheduler.AppointmentEditorOpening -= this.Scheduler_AppointmentEditorOpening;
                this.scheduler.AppointmentDrop -= this.OnSchedulerAppointmentDrop;
                this.scheduler.AppointmentResizing -= this.OnSchedulerAppointmentResizing;
                this.scheduler.AppointmentEditorClosing -= this.OnSchedulerAppointmentEditorClosing;
                this.scheduler = null;
            }

            if (this.holidaySwitch != null)
            {
                this.holidaySwitch = null;
            }
        }
    }
}
