using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.SfScheduler
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
        private List<string> subjectCollection;

        /// <summary>
        /// Notes Collection.
        /// </summary>
        private List<string> noteCollection;

        /// <summary>
        /// color collection
        /// </summary>
        private List<Brush> colorCollection;

        /// <summary>
        /// Event handler.
        /// </summary>
        internal event EventHandler<AppointmentEditorClosedEventArgs> AppointmentEditorClosed;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerDataBindingViewModel" /> class.
        /// </summary>
        public SchedulerDataBindingViewModel()
        {
            this.CreateSubjectCollection();
            this.CreateColorCollection();
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
        public ObservableCollection<Meeting> Events { get; set; }

        /// <summary>
        /// Gets or sets recursive appointments.
        /// </summary>
        public ObservableCollection<Meeting> RecursiveEvents { get; set; }

        /// <summary>
        /// Gets or sets  the timeline appointments.
        /// </summary>
        public ObservableCollection<Meeting> TimelineEvents { get; set; }

        /// <summary>
        /// Gets or sets  the special region appointments.
        /// </summary>
        public ObservableCollection<Meeting> SpecialRegionEvents { get; set; }

        /// <summary>
        /// Gets or sets time regions.
        /// </summary>
        public ObservableCollection<SchedulerTimeRegion> TimeRegions { get; set; }

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
        public Brush DisabledBackground { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Method to create the note collection.
        /// </summary>
        private void CreateNoteCollection()
        {
            this.noteCollection = new List<string>();
            this.noteCollection.Add("Consulting firm laws with business advisers");
            this.noteCollection.Add("Execute Project Scope");
            this.noteCollection.Add("Project Scope & Deliverables");
            this.noteCollection.Add("Executive summary");
            this.noteCollection.Add("Try to reduce the risks");
            this.noteCollection.Add("Encourages the integration of mind, body, and spirit");
            this.noteCollection.Add("Execute Project Scope");
            this.noteCollection.Add("Project Scope & Deliverables");
            this.noteCollection.Add("Executive summary");
            this.noteCollection.Add("Try to reduce the risk");
        }

        /// <summary>
        /// Method to initialize the appointments.
        /// </summary>
        private void IntializeAppoitments()
        {
            this.Events = new ObservableCollection<Meeting>();
            Random randomTime = new Random();
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
                    meeting.EventName = this.subjectCollection[randomTime.Next(9)];
                    meeting.Background = this.colorCollection[randomTime.Next(10)];
                    meeting.IsAllDay = false;
                    meeting.Notes = this.noteCollection[randomTime.Next(10)];
                    meeting.StartTimeZone = TimeZoneInfo.Local;
                    meeting.EndTimeZone = TimeZoneInfo.Local;
                    this.Events.Add(meeting);
                }
            }
        }

        /// <summary>
        /// Method to create the subject collection.
        /// </summary>
        private void CreateSubjectCollection()
        {
            this.subjectCollection = new List<string>();
            this.subjectCollection.Add("General Meeting");
            this.subjectCollection.Add("Plan Execution");
            this.subjectCollection.Add("Project Plan");
            this.subjectCollection.Add("Consulting");
            this.subjectCollection.Add("Performance Check");
            this.subjectCollection.Add("Support");
            this.subjectCollection.Add("Development Meeting");
            this.subjectCollection.Add("Scrum");
            this.subjectCollection.Add("Project Completion");
            this.subjectCollection.Add("Release updates");
            this.subjectCollection.Add("Performance Check");
        }

        /// <summary>
        /// Method for get timing range.
        /// </summary>
        /// <returns>return time collection</returns>
        private List<Point> GettingTimeRanges()
        {
            List<Point> randomTimeCollection = new List<Point>();
            randomTimeCollection.Add(new Point(9, 11));
            randomTimeCollection.Add(new Point(12, 14));
            randomTimeCollection.Add(new Point(15, 17));

            return randomTimeCollection;
        }

        /// <summary>
        /// Method to create the color collection.
        /// </summary>
        private void CreateColorCollection()
        {
            this.colorCollection = new List<Brush>();

            this.colorCollection.Add(new SolidColorBrush(Color.FromArgb("#FF8B1FA9")));
            this.colorCollection.Add(new SolidColorBrush(Color.FromArgb("#FFD20100")));
            this.colorCollection.Add(new SolidColorBrush(Color.FromArgb("#FFFC571D")));
            this.colorCollection.Add(new SolidColorBrush(Color.FromArgb("#FF36B37B")));
            this.colorCollection.Add(new SolidColorBrush(Color.FromArgb("#FF3D4FB5")));
            this.colorCollection.Add(new SolidColorBrush(Color.FromArgb("#FFE47C73")));
            this.colorCollection.Add(new SolidColorBrush(Color.FromArgb("#FF636363")));
            this.colorCollection.Add(new SolidColorBrush(Color.FromArgb("#FF85461E")));
            this.colorCollection.Add(new SolidColorBrush(Color.FromArgb("#FF0F8644")));
            this.colorCollection.Add(new SolidColorBrush(Color.FromArgb("#FF01A1EF")));
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
            Random randomTime = new Random();
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
                    if(additionalAppointmentIndex != 0){
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
                    meeting.EventName = this.subjectCollection[randomTime.Next(9)];
                    meeting.Background = this.colorCollection[randomTime.Next(10)];
                    meeting.IsAllDay = false;
                    meeting.Notes = this.noteCollection[randomTime.Next(10)];
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
            Random ran = new Random();
            DateTime currentDate = DateTime.Now.AddMonths(-1);

            var dailyEvent = new Meeting
            {
                EventName = "Daily recurring meeting",
                From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 9, 0, 0),
                To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 10, 0, 0),
                Background = this.colorCollection[ran.Next(10)],
                RecurrenceRule = "FREQ=DAILY;INTERVAL=1;COUNT=100",
                RecurrenceExceptions = new ObservableCollection<DateTime>()
            };

            this.RecursiveEvents.Add(dailyEvent);

            var weeklyEvent = new Meeting
            {
                EventName = "Weekly recurring meeting",
                From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 11, 0, 0),
                To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 12, 0, 0),
                Background = this.colorCollection[ran.Next(10)],
                RecurrenceRule = "FREQ=WEEKLY;BYDAY=MO,WE,FR;INTERVAL=1;COUNT=20",
                RecurrenceExceptions = new ObservableCollection<DateTime>()
            };

            this.RecursiveEvents.Add(weeklyEvent);

            var monthlyEvent = new Meeting
            {
                EventName = "Monthly recurring Meeting",
                From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 12, 0, 0),
                To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 13, 0, 0),
                Background = this.colorCollection[ran.Next(10)],
                RecurrenceRule = "FREQ=MONTHLY;BYDAY=TU;BYSETPOS=1;INTERVAL=1;COUNT=50",
                RecurrenceExceptions = new ObservableCollection<DateTime>()
            };

            this.RecursiveEvents.Add(monthlyEvent);

            var yearlyEvent = new Meeting
            {
                EventName = "Yearly recurring Meeting",
                From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 2, 0, 0),
                To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 3, 0, 0),
                Background = this.colorCollection[ran.Next(10)],
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

            Random ran = new Random();
            DateTime today = DateTime.Now;
            if (today.Month == 12)
            {
                today = today.AddMonths(-1);
            }
            else if (today.Month == 1)
            {
                today = today.AddMonths(1);
            }

            DateTime startMonth = new DateTime(today.Year, today.Month - 1, 1, 0, 0, 0);
            int day = (int)startMonth.DayOfWeek;
            DateTime CurrentStart = startMonth.AddDays(-day);

            this.TimelineEvents = new ObservableCollection<Meeting>();
            this.TimelineEvents = this.RecursiveEvents;
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
                this.TimelineEvents.Add(new Meeting()
                {
                    From = date,
                    To = date.AddHours(1),
                    Background = this.colorCollection[ran.Next(10)],
                    EventName = WorkWeekSubjects[i % WorkWeekSubjects.Count]
                });
            }
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
