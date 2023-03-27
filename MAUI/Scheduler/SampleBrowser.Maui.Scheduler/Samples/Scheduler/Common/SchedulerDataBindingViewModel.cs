#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{

    /// <summary>
    /// The data binding View Model.
    /// </summary>
    public class SchedulerDataBindingViewModel
    {
        #region Fields

        /// <summary>
        /// team management
        /// </summary>
        private List<string> subjects;

        /// <summary>
        /// Notes Collection.
        /// </summary>
        private List<string> notes;

        /// <summary>
        /// color collection
        /// </summary>
        private List<Brush> colors;

        /// <summary>
        /// Event handler.
        /// </summary>
        internal event EventHandler<AppointmentEditorClosedEventArgs>? AppointmentEditorClosed;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerDataBindingViewModel" /> class.
        /// </summary>
        public SchedulerDataBindingViewModel()
        {
            this.subjects = SchedulerHelper.GetSubjects();
            this.colors = SchedulerHelper.GetColors();
            this.notes = new List<string>();
            this.CreateNoteCollection();
            this.IntializeAppoitments();
            this.GenerateTimeRegions();
            this.GenerateSpecialRegionAppoitments();
            this.GenerateRecurrsiveAppointments();
            this.GenerateTimelineViewAppointments();
            this.DisplayDate = DateTime.Now.Date.AddHours(8).AddMinutes(50);
            this.MinDateTime = DateTime.Now.Date.AddMonths(-3);
            this.MaxDateTime = DateTime.Now.AddMonths(3);
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets appointments.
        /// </summary>
        public ObservableCollection<Meeting>? Events { get; set; }

        /// <summary>
        /// Gets or sets recursive appointments.
        /// </summary>
        public ObservableCollection<Meeting>? RecursiveEvents { get; set; }

        /// <summary>
        /// Gets or sets  the timeline appointments.
        /// </summary>
        public ObservableCollection<Meeting>? TimelineEvents { get; set; }

        /// <summary>
        /// Gets or sets  the special region appointments.
        /// </summary>
        public ObservableCollection<Meeting>? SpecialRegionEvents { get; set; }

        /// <summary>
        /// Gets or sets time regions.
        /// </summary>
        public ObservableCollection<SchedulerTimeRegion>? TimeRegions { get; set; }

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

        /// <summary>
        /// Gets or sets the schedule disabled background.
        /// </summary>
        public Brush? DisabledBackground { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Method to create the note collection.
        /// </summary>
        private void CreateNoteCollection()
        {
            this.notes.AddRange(new List<string>()
            {
                "Consulting firm laws with business advisers",
                "Execute Project Scope",
                "Project Scope & Deliverables",
                "Executive summary",
                "Try to reduce the risks",
                "Encourages the integration of mind, body, and spirit",
                "Execute Project Scope",
                "Project Scope & Deliverables",
                "Executive summary",
                "Try to reduce the risk"
            });
        }

        /// <summary>
        /// Method to initialize the appointments.
        /// </summary>
        private void IntializeAppoitments()
        {
            this.Events = new ObservableCollection<Meeting>();
            Random randomTime = new();
            List<Point> randomTimeCollection = this.GettingTimeRanges();

            DateTime date;
            DateTime dateFrom = DateTime.Now.AddDays(-50);
            DateTime dateTo = DateTime.Now.AddDays(50);

            for (date = dateFrom; date < dateTo; date = date.AddDays(1))
            {
                for (int additionalAppointmentIndex = 0; additionalAppointmentIndex < 1; additionalAppointmentIndex++)
                {
                    var meeting = new Meeting();
                    int hour = randomTime.Next((int)randomTimeCollection[additionalAppointmentIndex].X, (int)randomTimeCollection[additionalAppointmentIndex].Y);
                    meeting.From = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                    meeting.To = meeting.From.AddHours(1);
                    meeting.EventName = this.subjects[randomTime.Next(9)];
                    meeting.Background = this.colors[randomTime.Next(10)];
                    meeting.IsAllDay = false;
                    meeting.Notes = this.notes[randomTime.Next(10)];
                    meeting.StartTimeZone = TimeZoneInfo.Local;
                    meeting.EndTimeZone = TimeZoneInfo.Local;
                    this.Events.Add(meeting);
                }
            }
        }

        /// <summary>
        /// Method for get timing range.
        /// </summary>
        /// <returns>return time collection</returns>
        private List<Point> GettingTimeRanges()
        {
            List<Point> randomTimeCollection = new();
            randomTimeCollection.Add(new Point(9, 11));
            randomTimeCollection.Add(new Point(12, 14));
            randomTimeCollection.Add(new Point(15, 17));

            return randomTimeCollection;
        }

        /// <summary>
        /// Method to get special time regions.
        /// </summary>
        private void GenerateTimeRegions()
        {
            var currentDate = DateTime.Now.AddMonths(-5);
            this.TimeRegions = new ObservableCollection<SchedulerTimeRegion>();
            Brush backgroundColor = new SolidColorBrush(Colors.LightGray.WithAlpha(0.3f));
            this.DisabledBackground = backgroundColor;
            this.TimeRegions.Add(new SchedulerTimeRegion()
            {
                StartTime = new DateTime(currentDate.Year, currentDate.Month, 1, 0, 0, 0),
                Background = backgroundColor,
                EndTime = new DateTime(currentDate.Year, currentDate.Month, 1, 9, 0, 0),
                EnablePointerInteraction = false,
                RecurrenceRule = "FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU,WE,TH,FR",
            });

            this.TimeRegions.Add(new SchedulerTimeRegion()
            {
                StartTime = new DateTime(currentDate.Year, currentDate.Month, 1, 18, 0, 0),
                Background = backgroundColor,
                EndTime = new DateTime(currentDate.Year, currentDate.Month, 1, 23, 59, 59),
                EnablePointerInteraction = false,
                RecurrenceRule = "FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU,WE,TH,FR",
            });

            this.TimeRegions.Add(new SchedulerTimeRegion()
            {
                StartTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 10, 0, 0),
                EndTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 11, 0, 0),
                EnablePointerInteraction = false,
                Background = backgroundColor,
                Text = "Break",
                RecurrenceRule = "FREQ=WEEKLY;INTERVAL=1;BYDAY=TU",
            });

            this.TimeRegions.Add(new SchedulerTimeRegion()
            {
                StartTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 15, 0, 0),
                EndTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 16, 0, 0),
                EnablePointerInteraction = false,
                Background = backgroundColor,
                Text = "Break",
                RecurrenceRule = "FREQ=WEEKLY;INTERVAL=1;BYDAY=WE",
            });

            this.TimeRegions.Add(new SchedulerTimeRegion()
            {
                StartTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 13, 0, 0),
                EndTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 14, 0, 0),
                EnablePointerInteraction = false,
                Background = backgroundColor,
                Text = "Lunch",
                RecurrenceRule = "FREQ=WEEKLY;INTERVAL=1;BYDAY=MO,TU,WE,TH,FR",
            });
        }

        /// <summary>
        /// Method to generate the special region appointments.
        /// </summary>
        private void GenerateSpecialRegionAppoitments()
        {
            this.SpecialRegionEvents = new ObservableCollection<Meeting>();
            Random randomTime = new();
            DateTime date;
            DateTime dateFrom = DateTime.Now.AddDays(-50);
            DateTime dateTo = DateTime.Now.AddDays(50);

            for (date = dateFrom; date < dateTo; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                {
                    continue;
                }

                for (int additionalAppointmentIndex = 0; additionalAppointmentIndex < 2; additionalAppointmentIndex++)
                {
                    var meeting = new Meeting();
                    int hour = 0;
                    if (additionalAppointmentIndex != 0)
                    {
                        //// Skip the appointment on in between break time region.
                        if (date.DayOfWeek == DayOfWeek.Wednesday)
                        {
                            continue;
                        }

                        hour = randomTime.Next(14, 17);
                    }
                    else
                    {
                        //// Skip the appointment on in between break time region.
                        if (date.DayOfWeek == DayOfWeek.Tuesday)
                        {
                            continue;
                        }

                        hour = randomTime.Next(9, 12);
                    }

                    meeting.From = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                    meeting.To = meeting.From.AddHours(1);
                    meeting.EventName = this.subjects[randomTime.Next(9)];
                    meeting.Background = this.colors[randomTime.Next(10)];
                    meeting.IsAllDay = false;
                    meeting.Notes = this.notes[randomTime.Next(10)];
                    meeting.StartTimeZone = TimeZoneInfo.Local;
                    meeting.EndTimeZone = TimeZoneInfo.Local;
                    this.SpecialRegionEvents.Add(meeting);
                }
            }
        }

        /// <summary>
        /// Generate the recursive appointments.
        /// </summary>
        private void GenerateRecurrsiveAppointments()
        {
            this.RecursiveEvents = new ObservableCollection<Meeting>();
            Random ran = new();
            DateTime currentDate = DateTime.Now.AddMonths(-1);

            var dailyEvent = new Meeting
            {
                EventName = "Daily recurring meeting",
                From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 9, 0, 0),
                To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 10, 0, 0),
                Background = this.colors[ran.Next(10)],
                RecurrenceRule = "FREQ=DAILY;INTERVAL=1;COUNT=100",
                Id = 1,
                RecurrenceExceptions = new ObservableCollection<DateTime>()
            };

            this.RecursiveEvents.Add(dailyEvent);

            var weeklyEvent = new Meeting
            {
                EventName = "Weekly recurring meeting",
                From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 11, 0, 0),
                To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 12, 0, 0),
                Background = this.colors[ran.Next(10)],
                RecurrenceRule = "FREQ=WEEKLY;BYDAY=MO,WE,FR;INTERVAL=1;COUNT=20",
                RecurrenceExceptions = new ObservableCollection<DateTime>()
            };

            this.RecursiveEvents.Add(weeklyEvent);

            var monthlyEvent = new Meeting
            {
                EventName = "Monthly recurring Meeting",
                From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 12, 0, 0),
                To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 13, 0, 0),
                Background = this.colors[ran.Next(10)],
                RecurrenceRule = "FREQ=MONTHLY;BYDAY=TU;BYSETPOS=1;INTERVAL=1;COUNT=50",
                RecurrenceExceptions = new ObservableCollection<DateTime>()
            };

            this.RecursiveEvents.Add(monthlyEvent);

            var monthlyEndEvent = new Meeting
            {
                EventName = "Monthly review meeting",
                From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 7, 0, 0),
                To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 8, 0, 0),
                Background = this.colors[ran.Next(10)],
                RecurrenceRule = "FREQ=MONTHLY;BYMONTHDAY=-1;INTERVAL=1;COUNT=50",
                RecurrenceExceptions = new ObservableCollection<DateTime>()
            };

            this.RecursiveEvents.Add(monthlyEndEvent);

            var yearlyEvent = new Meeting
            {
                EventName = "Yearly recurring Meeting",
                From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 2, 0, 0),
                To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 3, 0, 0),
                Background = this.colors[ran.Next(10)],
                RecurrenceRule = "FREQ=YEARLY;BYMONTHDAY=3;BYMONTH=5;INTERVAL=1;COUNT=50",
                RecurrenceExceptions = new ObservableCollection<DateTime>()
            };

            this.RecursiveEvents.Add(yearlyEvent);

            //Add ExceptionDates to avoid occurrence on specific dates.
            DateTime changedExceptionDate1 = DateTime.Now.AddDays(-1).Date;
            DateTime changedExceptionDate2 = DateTime.Now.Date.AddDays(4).Date;
            DateTime changedExceptionDate3 = DateTime.Now.Date.AddDays(-3).Date;
            DateTime deletedExceptionDate1 = DateTime.Now.Date.AddDays(2);
            DateTime deletedExceptionDate2 = DateTime.Now.Date.AddDays(6);

            dailyEvent.RecurrenceExceptions = new ObservableCollection<DateTime>()
            {
                changedExceptionDate1,
                changedExceptionDate2,
                changedExceptionDate3,
                deletedExceptionDate1,
                deletedExceptionDate2,
            };

            var changedEvent1 = new Meeting
            {
                EventName = "Scrum meeting - Changed Occurrence",
                From = new DateTime(changedExceptionDate1.Year, changedExceptionDate1.Month, changedExceptionDate1.Day, 9, 0, 0),
                To = new DateTime(changedExceptionDate1.Year, changedExceptionDate1.Month, changedExceptionDate1.Day, 10, 0, 0),
                Background = this.colors[ran.Next(10)],
                Id = 2,
                RecurrenceId =1,
            };

            this.RecursiveEvents.Add(changedEvent1);

            var changedEvent2 = new Meeting
            {
                EventName = "Scrum meeting - Changed Occurrence",
                From = new DateTime(changedExceptionDate2.Year, changedExceptionDate2.Month, changedExceptionDate2.Day, 9, 0, 0),
                To = new DateTime(changedExceptionDate2.Year, changedExceptionDate2.Month, changedExceptionDate2.Day, 10, 0, 0),
                Background = this.colors[ran.Next(10)],
                Id = 3,
                RecurrenceId = 1,
            };

            this.RecursiveEvents.Add(changedEvent2);
        }

        /// <summary>
        /// Method to generate the timeline view appointments.
        /// </summary>
        private void GenerateTimelineViewAppointments()
        {
            var WorkWeekDays = new ObservableCollection<DateTime>();
            var WorkWeekSubjects = new ObservableCollection<string>()
                                                           { "GoToMeeting", "Business Meeting", "Conference", "Project Status Discussion",
                                                             "Auditing", "Client Meeting", "Generate Report", "Target Meeting", "General Meeting" };

            var NonWorkingDays = new ObservableCollection<DateTime>();

            Random ran = new();
            DateTime today = DateTime.Now;
            if (today.Month == 12)
            {
                today = today.AddMonths(-1);
            }
            else if (today.Month == 1)
            {
                today = today.AddMonths(1);
            }

            DateTime startMonth = new(today.Year, today.Month - 1, 1, 0, 0, 0);
            int day = (int)startMonth.DayOfWeek;
            DateTime CurrentStart = startMonth.AddDays(-day);
            this.TimelineEvents = new ObservableCollection<Meeting>(this.RecursiveEvents!);

            for (int i = 0; i < 90; i++)
            {
                if (i % 7 == 0 || i % 7 == 6)
                {
                    //add weekend appointments
                    NonWorkingDays.Add(CurrentStart.AddDays(i));
                }
                else
                {
                    //Add Workweek appointment
                    WorkWeekDays.Add(CurrentStart.AddDays(i));
                }
            }

            for (int i = 0; i < 50; i++)
            {
                DateTime date = WorkWeekDays[ran.Next(0, WorkWeekDays.Count)].AddHours(ran.Next(9, 17));
                this.TimelineEvents?.Add(new Meeting()
                {
                    From = date,
                    To = date.AddHours(1),
                    Background = this.colors[ran.Next(10)],
                    EventName = WorkWeekSubjects[i % WorkWeekSubjects.Count]
                });
            }

            SchedulerHelper.AddSpanAppointments(this.TimelineEvents, WorkWeekSubjects, this.colors);
        }

        /// <summary>
        /// method for appointment editor closed.
        /// </summary>
        /// <param name="e">The Appointment editor closed Event Args</param>
        internal virtual void RaiseAppointmentEditorClosedEvent(AppointmentEditorClosedEventArgs e)
        {
            if (this.AppointmentEditorClosed != null)
            {
                this.AppointmentEditorClosed.Invoke(this, e);
            }
        }

        #endregion
    }
}
