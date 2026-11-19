using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    /// <summary>
    /// View model for the ICS Export/Import sample.
    /// Builds a deliberately varied appointment set (normal one-off, multi-day span,
    /// all-day, recurring daily/weekly, and a recurring series with a changed occurrence)
    /// so that an exported / imported calendar contains every supported type.
    /// </summary>
    public class ICSExportImportViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ICSExportImportViewModel" /> class.
        /// </summary>
        public ICSExportImportViewModel()
        {
            this.Events = new ObservableCollection<SchedulerAppointment>();
            this.GenerateAppointments();
            this.DisplayDate = DateTime.Now.Date.AddHours(9);
            this.MinDateTime = DateTime.Now.Date.AddMonths(-2);
            this.MaxDateTime = DateTime.Now.AddMonths(6);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets appointments.
        /// </summary>
        public ObservableCollection<SchedulerAppointment> Events { get; set; }

        /// <summary>
        /// Gets or sets the schedule display date.
        /// </summary>
        public DateTime DisplayDate { get; set; }

        /// <summary>
        /// Gets or sets the schedule min date time.
        /// </summary>
        public DateTime MinDateTime { get; set; }

        /// <summary>
        /// Gets or sets the schedule max date time.
        /// </summary>
        public DateTime MaxDateTime { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Method to generate the appointments.
        /// </summary>
        private void GenerateAppointments()
        {
            ObservableCollection<string> subjects = new()
            {
                "GoToMeeting", "Business Meeting", "Conference", "Project Status Discussion",
                "Auditing", "Client Meeting", "Generate Report", "Target Meeting", "General Meeting",
                "Standup", "Quarterly Review", "Sprint Planning"
            };

            Random ran = new();
            DateTime today = DateTime.Now.Date;

            // 1) Normal one-off appointment for today
            DateTime normalStart = today.AddHours(9);
            this.Events.Add(new SchedulerAppointment()
            {
                Id = 1,
                StartTime = normalStart,
                EndTime = normalStart.AddHours(1),
                Subject = subjects[0]
            });

            // 2) Span (multi-day) appointment from yesterday into tomorrow
            this.Events.Add(new SchedulerAppointment()
            {
                Id = 2,
                StartTime = today.AddDays(-1).AddHours(10),
                EndTime = today.AddDays(1).AddHours(14),
                Subject = "Offsite Planning"
            });

            // 3) All-day appointment for tomorrow
            this.Events.Add(new SchedulerAppointment()
            {
                Id = 3,
                StartTime = today.AddDays(1),
                EndTime = today.AddDays(2),
                IsAllDay = true,
                Subject = "Company Holiday"
            });

            // 4) Recurring weekly meeting on Mon/Wed/Fri for 8 weeks
            this.Events.Add(new SchedulerAppointment()
            {
                Id = 4,
                StartTime = today.AddHours(11),
                EndTime = today.AddHours(12),
                Subject = "Team Sync",
                RecurrenceRule = "FREQ=WEEKLY;BYDAY=MO,WE,FR;INTERVAL=1;COUNT=24",
                RecurrenceExceptionDates = new ObservableCollection<DateTime>
                {
                    today.AddDays(2),
                    today.AddDays(6)
                }
            });

            // 5) Daily stand-up for the next 30 working days
            this.Events.Add(new SchedulerAppointment()
            {
                Id = 5,
                StartTime = today.AddHours(8),
                EndTime = today.AddHours(9),
                Subject = "Daily Standup",
                RecurrenceRule = "FREQ=DAILY;INTERVAL=1;COUNT=30"
            });

            // 6) Changed occurrence - exception for the recurring team sync.
            // The exception date matches one of the entries in RecurrenceExceptionDates
            // on parent #4, so the recurrent edit is exported as a separate VEVENT
            // with a RECURRENCE-ID pointing back to #4 (RFC 5545).
            this.Events.Add(new SchedulerAppointment()
            {
                Id = 6,
                StartTime = today.AddDays(2).AddHours(13),
                EndTime = today.AddDays(2).AddHours(14),
                Subject = "Team Sync - Rescheduled",
                RecurrenceId = 4
            });

            // 7) A couple of regular filler appointments around the visible range
            for (int offset = -3; offset <= 3; offset++)
            {
                if (offset == 0 || offset == 1)
                {
                    continue;
                }

                DateTime date = today.AddDays(offset);
                for (int index = 0; index < 2; index++)
                {
                    int startIndex = 10 + (index * 3) + ran.Next(0, 2);
                    DateTime startDate = date.AddHours(startIndex);
                    this.Events.Add(new SchedulerAppointment()
                    {
                        Id = 10 + offset * 2 + index,
                        StartTime = startDate,
                        EndTime = startDate.AddHours(1),
                        Subject = subjects[ran.Next(0, subjects.Count)]
                    });
                }
            }
        }

        #endregion
    }
}