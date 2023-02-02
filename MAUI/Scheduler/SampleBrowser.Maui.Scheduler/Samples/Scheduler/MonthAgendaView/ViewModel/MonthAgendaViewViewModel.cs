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
using System.ComponentModel;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{

    /// <summary>
    /// The data binding View Model.
    /// </summary>
    public class MonthAgendaViewViewModel : INotifyPropertyChanged
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
        /// The selected date meetings.
        /// </summary>
        private ObservableCollection<Meeting>? selectedDateMeetings;

        /// <summary>
        /// The selected date
        /// </summary>
        private DateTime selectedDate = DateTime.Now.Date;

        /// <summary>
        /// The bool value.
        /// </summary>
        private bool isToday = true;

        /// <summary>
        /// The date text color.
        /// </summary>
        private Color dateTextColor = Colors.White;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerDataBindingViewModel" /> class.
        /// </summary>
        public MonthAgendaViewViewModel()
        {
            this.subjects = new List<string>();
            this.colors = new List<Brush>();
            this.notes = new List<string>();
            this.selectedDateMeetings = new ObservableCollection<Meeting>();
            this.CreateSubjects();
            this.CreateColors();
            this.CreateNotes();
            this.IntializeAppoitments();
            this.selectedDateMeetings = this.GetSelectedDateAppointments(this.selectedDate);
            this.DisplayDate = DateTime.Now.Date.AddHours(8).AddMinutes(50);
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets appointments.
        /// </summary>
        public ObservableCollection<Meeting>? Events { get; set; }

        /// <summary>
        /// Gets or sets the selected date meetings.
        /// </summary>
        public ObservableCollection<Meeting>? SelectedDateMeetings
        {
            get { return selectedDateMeetings; }
            set
            {
                selectedDateMeetings = value;
                RaiseOnPropertyChanged("SelectedDateMeetings");
            }
        }

        /// <summary>
        /// Gets or sets the schedule selected date.
        /// </summary>
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                RaiseOnPropertyChanged("SelectedDate");
            }
        }

        /// <summary>
        /// Gets or sets the date is today or not.
        /// </summary>
        public bool IsToday
        {
            get { return isToday; }
            set
            {
                isToday = value;
                RaiseOnPropertyChanged("IsToday");
            }
        }

        /// <summary>
        /// Gets or sets the date text color.
        /// </summary>
        public Color DateTextColor
        {
            get { return dateTextColor; }
            set
            {
                dateTextColor = value;
                RaiseOnPropertyChanged("DateTextColor");
            }
        }

        /// <summary>
        /// Gets or sets the schedule display date.
        /// </summary>
        public DateTime DisplayDate { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Method to create the notes.
        /// </summary>
        private void CreateNotes()
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
            DateTime dateFrom = DateTime.Now.AddDays(-25);
            DateTime dateTo = DateTime.Now.AddDays(25);

            for (date = dateFrom; date < dateTo; date = date.AddDays(1))
            {
                for (int additionalAppointmentIndex = 0; additionalAppointmentIndex < randomTime.Next(4, 7); additionalAppointmentIndex++)
                {
                    var meeting = new Meeting();
                    var randomTimeIndex = randomTime.Next(2);
                    int hour = randomTime.Next((int)randomTimeCollection[randomTimeIndex].X, (int)randomTimeCollection[randomTimeIndex].Y);
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
        /// Metho to get selected date appointments.
        /// </summary>
        /// <param name="date">The selected date</param>
        public ObservableCollection<Meeting> GetSelectedDateAppointments(DateTime date)
        {
            ObservableCollection<Meeting> selectedAppiointments = new ObservableCollection<Meeting>();

            for (int i = 0; i < this.Events?.Count; i++)
            {
                DateTime startTime = this.Events[i].From;

                if (date.Day == startTime.Day && date.Month == startTime.Month && date.Year == startTime.Year)
                {
                    selectedAppiointments.Add(this.Events[i]);
                }
            }

            return selectedAppiointments;
        }

        /// <summary>
        /// Method to create the subject.
        /// </summary>
        private void CreateSubjects()
        {
            this.subjects.AddRange(new List<string>()
            {
                "General Meeting",
                "Plan Execution",
                "Project Plan",
                "Consulting",
                "Performance Check",
                "Support",
                "Development Meeting",
                "Scrum",
                "Project Completion",
                "Release updates",
                "Performance Check"
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

        /// <summary>
        /// Method to create the colors.
        /// </summary>
        private void CreateColors()
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

        #endregion

        #region Property Changed Event

        /// <summary>
        /// Property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Invoke method when property changed
        /// </summary>
        /// <param name="propertyName">property name</param>
        private void RaiseOnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
