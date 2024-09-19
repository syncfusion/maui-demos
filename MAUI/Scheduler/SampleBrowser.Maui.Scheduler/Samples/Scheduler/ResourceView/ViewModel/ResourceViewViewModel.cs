#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    public class ResourceViewViewModel
    {
        /// <summary>
        /// current day meetings 
        /// </summary>
        private List<string> currentDayMeetings;

        /// <summary>
        /// color collection
        /// </summary>
        private List<Brush> colors;

        /// <summary>
        /// Appointment start hours.
        /// </summary>
        private List<int> startHours;

        /// <summary>
        /// list of meeting
        /// </summary>
        private ObservableCollection<Meeting>? events;

        /// <summary>
        /// name collection
        /// </summary>

        private List<string> employeeNames;

        /// <summary>
        /// resources
        /// </summary>
        private ObservableCollection<object>? resources;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceViewViewModel" /> class.
        /// </summary>
        public ResourceViewViewModel()
        {
            this.employeeNames = new List<string>();
            this.colors = new List<Brush>();
            this.startHours = new List<int>();
            this.Events = new ObservableCollection<Meeting>();
            this.currentDayMeetings = new List<string>();
            this.Resources = new ObservableCollection<object>();
            this.DisplayDate = DateTime.Now.Date.AddHours(8).AddMinutes(50);
            this.InitializeDataForBookings();
            this.InitializeResources();
            this.BookingAppointments();
        }

        private void InitializeResources()
        {
            Random random = new Random();
            for (int i = 0; i < 9; i++)
            {
                Employee employees = new Employee();
                employees.Name = employeeNames[i];
                employees.Background = this.colors[random.Next(0, 9)];
                employees.Foreground = (employees.Background as SolidColorBrush)?.Color.GetLuminosity() > 0.7 ? Colors.Black : Colors.White;
                employees.Id = i.ToString();

                if (employees.Name == "Brooklyn")
                {
                    employees.ImageName = "people_circle8.png";
                }
                else if (employees.Name == "Sophia")
                {
                    employees.ImageName = "people_circle1.png";
                }
                else if (employees.Name == "Stephen")
                {
                    employees.ImageName = "people_circle12.png";
                }
                else if (employees.Name == "Zoey Addison")
                {
                    employees.ImageName = "people_circle2.png";
                }
                else if (employees.Name == "Daniel")
                {
                    employees.ImageName = "people_circle14.png";
                }
                else if (employees.Name == "Emilia")
                {
                    employees.ImageName = "people_circle3.png";
                }
                else if (employees.Name == "Adeline Ruby")
                {
                    employees.ImageName = "people_circle4.png";
                }
                else if (employees.Name == "James William")
                {
                    employees.ImageName = "people_circle5.png";
                }
                else if (employees.Name == "Kinsley Elena")
                {
                    employees.ImageName = "people_circle6.png";
                }

                Resources?.Add(employees);
            }
        }

        #region ListOfMeeting

        /// <summary>
        /// Gets or sets appointments.
        /// </summary>
        public ObservableCollection<Meeting>? Events
        {
            get
            {
                return this.events;
            }

            set
            {
                this.events = value;
            }
        }
        #endregion

        public ObservableCollection<object>? Resources
        {
            get
            {
                return resources;
            }

            set
            {
                resources = value;
            }
        }

        /// <summary>
        /// Gets or sets the schedule display date.
        /// </summary>
        public DateTime DisplayDate { get; set; }

        #region BookingAppointments

        /// <summary>
        /// Method for booking appointments.
        /// </summary>
        internal void BookingAppointments()
        {
            Random randomTime = new Random();
            List<Point> randomTimeCollection = this.GettingTimeRanges();
            DateTime date;
            DateTime today = DateTime.Now;
            DateTime dateFrom = DateTime.Now.AddDays(-80);
            DateTime dateTo = DateTime.Now.AddDays(80);
            DateTime dateRangeStart = DateTime.Now.AddDays(-70);
            DateTime dateRangeEnd = DateTime.Now.AddDays(70);

            if (resources == null)
            {
                return;
            }

            for (date = dateFrom; date < dateTo; date = date.AddDays(1))
            {
                for (int res = 0; res < 2; res++)
                {
                    Employee? resource = resources[randomTime.Next(resources.Count)] as Employee;
                    if ((DateTime.Compare(date, dateRangeStart) > 0) && (DateTime.Compare(date, dateRangeEnd) < 0))
                    {
                        for (int additionalAppointmentIndex = 0; additionalAppointmentIndex < 3; additionalAppointmentIndex++)
                        {
                            Meeting meeting = new Meeting();
                            meeting.From = new DateTime(date.Year, date.Month, date.Day, this.startHours[randomTime.Next(0, 2)], 0, 0);
                            meeting.To = meeting.From.AddHours(1);
                            meeting.EventName = this.currentDayMeetings[randomTime.Next(9)];
                            meeting.Background = this.colors[randomTime.Next(9)];
                            meeting.IsAllDay = false;
                            meeting.StartTimeZone = TimeZoneInfo.Local;
                            meeting.EndTimeZone = TimeZoneInfo.Local;
                            ObservableCollection<object> resources = new ObservableCollection<object>();
                            if (resource != null && resource.Id != null)
                            {
                                resources.Add(resource.Id);
                            }
                            meeting.Resources = resources;
                            this.Events?.Add(meeting);
                        }
                    }
                    else
                    {
                        Meeting meeting = new Meeting();
                        meeting.From = new DateTime(date.Year, date.Month, date.Day, randomTime.Next(9, 11), 0, 0);
                        meeting.To = meeting.From.AddDays(2).AddHours(4);
                        meeting.EventName = this.currentDayMeetings[randomTime.Next(9)];
                        meeting.Background = this.colors[randomTime.Next(9)];
                        meeting.IsAllDay = true;
                        meeting.StartTimeZone = TimeZoneInfo.Local;
                        meeting.EndTimeZone = TimeZoneInfo.Local;
                        ObservableCollection<object> resources = new ObservableCollection<object>();
                        if (resource != null && resource.Id != null)
                        {
                            resources.Add(resource.Id);
                        }
                        meeting.Resources = resources;
                        this.Events?.Add(meeting);
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                Employee? resourceView = resources[i] as Employee;
                //// Adding Span appointments
                Meeting meeting = new Meeting();
                meeting.From = today.Date.AddDays(1);
                meeting.To = today.Date.AddDays(8);
                meeting.EventName = this.currentDayMeetings[randomTime.Next(9)];
                meeting.Background = this.colors[randomTime.Next(10)];
                ObservableCollection<object> meetingResources = new ObservableCollection<object>();
                if (resourceView != null && resourceView.Id != null)
                {
                    meetingResources.Add(resourceView.Id);
                }
                meeting.Resources = meetingResources;

                Meeting planning = new Meeting();
                planning.EventName = this.currentDayMeetings[randomTime.Next(9)];
                planning.From = today.Date.AddDays(-6);
                planning.To = today.Date;
                planning.Background = this.colors[randomTime.Next(10)];
                ObservableCollection<object> planningResources = new ObservableCollection<object>();
                if (resourceView != null && resourceView.Id != null)
                {
                    planningResources.Add(resourceView.Id);
                }
                planning.Resources = planningResources;

                Meeting consulting = new Meeting();
                consulting.EventName = this.currentDayMeetings[randomTime.Next(9)];
                consulting.From = today.Date.AddHours(23);
                consulting.To = today.Date.AddDays(1).AddHours(2);
                consulting.IsAllDay = false;
                consulting.Background = this.colors[randomTime.Next(10)];
                ObservableCollection<object> consultingResources = new ObservableCollection<object>();
                if (resourceView != null && resourceView.Id != null)
                {
                    consultingResources.Add(resourceView.Id);
                }
                consulting.Resources = consultingResources;

                this.Events?.Add(meeting);
                this.Events?.Add(planning);
                this.Events?.Add(consulting);
            }
        }

        #endregion BookingAppointments

        #region GettingTimeRanges

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

        #endregion GettingTimeRanges

        #region InitializeDataForBookings

        /// <summary>
        /// Method for initialize data bookings.
        /// </summary>
        private void InitializeDataForBookings()
        {
            this.currentDayMeetings = new List<string>();
            this.currentDayMeetings.Add("General Meeting");
            this.currentDayMeetings.Add("Plan Execution");
            this.currentDayMeetings.Add("Project Plan");
            this.currentDayMeetings.Add("Consulting");
            this.currentDayMeetings.Add("Performance Check");
            this.currentDayMeetings.Add("Yoga Therapy");
            this.currentDayMeetings.Add("Plan Execution");
            this.currentDayMeetings.Add("Project Plan");
            this.currentDayMeetings.Add("Consulting");
            this.currentDayMeetings.Add("Performance Check");

            this.colors = new List<Brush>();
            this.colors.Add(Color.FromArgb("#FF8B1FA9"));
            this.colors.Add(Color.FromArgb("#FFD20100"));
            this.colors.Add(Color.FromArgb("#FFFC571D"));
            this.colors.Add(Color.FromArgb("#FF36B37B"));
            this.colors.Add(Color.FromArgb("#FF3D4FB5"));
            this.colors.Add(Color.FromArgb("#FF3D4FB5"));
            this.colors.Add(Color.FromArgb("#FF636363"));
            this.colors.Add(Color.FromArgb("#FF636363"));
            this.colors.Add(Color.FromArgb("#FF01A1EF"));
            this.colors.Add(Color.FromArgb("#FF0F8644"));
            this.colors.Add(Color.FromArgb("#FF00ABA9"));

            this.employeeNames = new List<string>();
            this.employeeNames.Add("Brooklyn");
            this.employeeNames.Add("James William");
            this.employeeNames.Add("Sophia");
            this.employeeNames.Add("Stephen");
            this.employeeNames.Add("Zoey Addison");
            this.employeeNames.Add("Daniel");
            this.employeeNames.Add("Emilia");
            this.employeeNames.Add("Adeline Ruby");
            this.employeeNames.Add("Kinsley Elena");
            this.employeeNames.Add("Danial William");
            this.employeeNames.Add("Stephen Addison");
            this.employeeNames.Add("Kinsley Ruby");
            this.employeeNames.Add("Adeline Elena");
            this.employeeNames.Add("Emilia William");
            this.employeeNames.Add("Danial Addison");

            this.startHours = new List<int>();
            this.startHours.Add(8);
            this.startHours.Add(17);
            this.startHours.Add(17);
        }

        #endregion InitializeDataForBookings
    }
}