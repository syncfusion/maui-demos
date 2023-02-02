#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Devices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    /// <summary>
    /// The agenda view View Model.
    /// </summary>
    internal class AgendaViewModel
    {
        #region Fields

        /// <summary>
        /// team management
        /// </summary>
        private List<string> subjects;

        /// <summary>
        /// color collection
        /// </summary>
        private List<Brush> colors;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AgendaViewModel" /> class.
        /// </summary>
        public AgendaViewModel()
        {
            this.subjects = new List<string>();
            this.colors = new List<Brush>();
            this.Events = new ObservableCollection<Meeting>();
            this.CreateSubjectCollection();
            this.CreateColorCollection();
            this.IntializeAppoitments();
            this.DisplayDate = DateTime.Now.Date.AddHours(8).AddMinutes(50);
            this.MinDateTime = DateTime.Now.Date.AddMonths(-5);
            this.MaxDateTime = DateTime.Now.AddMonths(5);
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets appointments.
        /// </summary>
        public ObservableCollection<Meeting> Events { get; set; }

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
        /// Method to initialize the appointments.
        /// </summary>
        private void IntializeAppoitments()
        {
            List<Point> randomTimeCollection = this.GettingTimeRanges();
            Random randomTime = new();

            DateTime date;
            DateTime dateFrom = DateTime.Now.AddDays(-50);
            DateTime dateTo = DateTime.Now.AddDays(50);

            for (date = dateFrom; date < dateTo; date = date.AddDays(1))
            {
                for (int additionalAppointmentIndex = 0; additionalAppointmentIndex < 2; additionalAppointmentIndex++)
                {
                    var meeting = new Meeting();
                    int hour = randomTime.Next((int)randomTimeCollection[additionalAppointmentIndex].X, (int)randomTimeCollection[additionalAppointmentIndex].Y);
                    meeting.From = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                    meeting.To = meeting.From.AddHours(randomTime.Next(1, 4));
                    meeting.EventName = this.subjects[randomTime.Next(15)];
                    meeting.Background = this.colors[randomTime.Next(10)];
                    meeting.IsAllDay = false;
                    meeting.StartTimeZone = TimeZoneInfo.Local;
                    meeting.EndTimeZone = TimeZoneInfo.Local;
                    this.Events.Add(meeting);
                }
            }

            for (date = dateFrom; date < dateTo; date = date.AddDays(3))
            {
                for (int additionalAppointmentIndex = 0; additionalAppointmentIndex < 1; additionalAppointmentIndex++)
                {
                    var meeting = new Meeting();
                    int hour = randomTime.Next((int)randomTimeCollection[additionalAppointmentIndex].X, (int)randomTimeCollection[additionalAppointmentIndex].Y);
                    meeting.From = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                    meeting.To = meeting.From.AddHours(1);
                    meeting.EventName = this.subjects[randomTime.Next(15)];
                    meeting.Background = this.colors[randomTime.Next(10)];
                    meeting.IsAllDay = true;
                    meeting.StartTimeZone = TimeZoneInfo.Local;
                    meeting.EndTimeZone = TimeZoneInfo.Local;
                    this.Events.Add(meeting);
                }
            }

            for (date = dateFrom; date < dateTo; date = date.AddDays(7))
            {
                for (int additionalAppointmentIndex = 0; additionalAppointmentIndex < 1; additionalAppointmentIndex++)
                {
                    var meeting = new Meeting();
                    int hour = randomTime.Next((int)randomTimeCollection[additionalAppointmentIndex].X, (int)randomTimeCollection[additionalAppointmentIndex].Y);
                    meeting.From = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                    meeting.To = meeting.From.AddHours(1).AddDays(randomTime.Next(1, 3));
                    meeting.EventName = this.subjects[randomTime.Next(15)];
                    meeting.Background = this.colors[randomTime.Next(10)];
                    meeting.IsAllDay = true;
                    meeting.StartTimeZone = TimeZoneInfo.Local;
                    meeting.EndTimeZone = TimeZoneInfo.Local;
                    this.Events.Add(meeting);
                }
            }

            var currentDate = DateTime.Now.AddMonths(-5);
            var dailyEvent = new Meeting
            {
                EventName = "Daily recurring meeting",
                From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 9, 0, 0),
                To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 10, 0, 0),
                Background = this.colors[randomTime.Next(10)],
                RecurrenceRule = "FREQ=DAILY;INTERVAL=1;COUNT=100",
                RecurrenceExceptions = new ObservableCollection<DateTime>()
            };

            this.Events.Add(dailyEvent);

            var weeklyEvent = new Meeting
            {
                EventName = "Weekly recurring meeting",
                From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 11, 0, 0),
                To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 12, 0, 0),
                Background = this.colors[randomTime.Next(10)],
                RecurrenceRule = "FREQ=WEEKLY;BYDAY=MO,WE,FR;INTERVAL=1;COUNT=20",
                RecurrenceExceptions = new ObservableCollection<DateTime>()
            };

            this.Events.Add(weeklyEvent);

            var monthlyEvent = new Meeting
            {
                EventName = "Monthly recurring Meeting",
                From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 12, 0, 0),
                To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 13, 0, 0),
                Background = this.colors[randomTime.Next(10)],
                RecurrenceRule = "FREQ=MONTHLY;BYDAY=TU;BYSETPOS=1;INTERVAL=1;COUNT=15",
                RecurrenceExceptions = new ObservableCollection<DateTime>()
            };

            this.Events.Add(monthlyEvent);

            var yearlyEvent = new Meeting
            {
                EventName = "Yearly recurring Meeting",
                From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 2, 0, 0),
                To = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 3, 0, 0),
                Background = this.colors[randomTime.Next(10)],
                RecurrenceRule = "FREQ=YEARLY;BYMONTHDAY=3;BYMONTH=5;INTERVAL=1;COUNT=10",
                RecurrenceExceptions = new ObservableCollection<DateTime>()
            };

            this.Events.Add(yearlyEvent);

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

            if (DeviceInfo.Idiom == DeviceIdiom.Desktop || DeviceInfo.Idiom == DeviceIdiom.Tablet)
            {
                List<string> locations = new();
                locations.Add(" - Austin, Texas - USA.");
                locations.Add(" - Albany, New York - USA.");
                locations.Add(" - Carson City, Nevada - USA.");

                foreach (Meeting meeting in this.Events)
                {
                    meeting.EventName += locations[randomTime.Next(3)];
                }
            }
        }

        /// <summary>
        /// Method to create the subject collection.
        /// </summary>
        private void CreateSubjectCollection()
        {
            this.subjects.AddRange(new List<string>()
            {
                "General Meeting",
                "Plan Execution",
                "Project Plan",
                "Consulting",
                "Support",
                "Development Meeting",
                "Scrum",
                "Project Completion",
                "Release updates",
                "GoToMeeting",
                "Business Meeting",
                "Conference",
                "Project Status Discussion",
                "Auditing",
                "Client Meeting",
                "Generate Report"
            });
        }

        /// <summary>
        /// Method to create the color collection.
        /// </summary>
        private void CreateColorCollection()
        {
            this.colors.AddRange(new List<Brush>()
            {
                new SolidColorBrush(Color.FromArgb("#FF8B1FA9")),
                new SolidColorBrush(Color.FromArgb("#FFD20100")),
                new SolidColorBrush(Color.FromArgb("#FFFC571D")),
                new SolidColorBrush(Color.FromArgb("#FF36B37B")),
                new SolidColorBrush(Color.FromArgb("#FF3D4FB5")),
                new SolidColorBrush(Color.FromArgb("#FFE47C73")),
                new SolidColorBrush(Color.FromArgb("#FF636363")),
                new SolidColorBrush(Color.FromArgb("#FF85461E")),
                new SolidColorBrush(Color.FromArgb("#FF0F8644")),
                new SolidColorBrush(Color.FromArgb("#FF01A1EF"))
            });
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

        #endregion
    }
}
