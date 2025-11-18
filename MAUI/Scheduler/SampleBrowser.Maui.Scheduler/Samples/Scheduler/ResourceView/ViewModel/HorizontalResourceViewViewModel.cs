#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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
    public class HorizontalResourceViewViewModel
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
        /// Initializes a new instance of the <see cref="HorizontalResourceViewViewModel" /> class.
        /// </summary>
        public HorizontalResourceViewViewModel()
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
            for (int i = 0; i < 4; i++)
            {
                Employee employees = new Employee();
                employees.Name = employeeNames[i];
                employees.Background = this.colors[random.Next(0, 9)];
                employees.Foreground = (employees.Background as SolidColorBrush)?.Color.GetLuminosity() > 0.7 ? Colors.Black : Colors.White;
                employees.Id = i.ToString();

                if (employees.Name == "Robert")
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
            // Initialize required variables
            Random randomTime = new Random();
            DateTime today = DateTime.Now;
            DateTime dateFrom = today.AddDays(-30);
            DateTime dateTo = today.AddDays(30);

            // Early return if no resources available
            if (resources == null || resources.Count == 0)
            {
                return;
            }

            // Dictionary to track used time slots for each day and resource
            Dictionary<DateTime, Dictionary<object, HashSet<int>>> dateResourceTimeSlots =
                new Dictionary<DateTime, Dictionary<object, HashSet<int>>>();

            // Loop through each day in the date range
            for (DateTime date = dateFrom; date < dateTo; date = date.AddDays(1))
            {
                DateTime dateKey = date.Date;

                // Initialize the tracking for this date
                if (!dateResourceTimeSlots.ContainsKey(dateKey))
                {
                    dateResourceTimeSlots[dateKey] = new Dictionary<object, HashSet<int>>();
                }

                // Create one all-day appointment for each day
                Employee? allDayResource = resources[randomTime.Next(resources.Count)] as Employee;

                Meeting allDayMeeting = new Meeting();
                allDayMeeting.From = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                allDayMeeting.To = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
                allDayMeeting.EventName = this.currentDayMeetings[randomTime.Next(Math.Min(9, this.currentDayMeetings.Count - 1))];
                allDayMeeting.Background = this.colors[randomTime.Next(Math.Min(9, this.colors.Count - 1))];
                allDayMeeting.IsAllDay = true;
                allDayMeeting.StartTimeZone = TimeZoneInfo.Local;
                allDayMeeting.EndTimeZone = TimeZoneInfo.Local;

                ObservableCollection<object> allDayMeetingResources = new ObservableCollection<object>();
                if (allDayResource != null && allDayResource.Id != null)
                {
                    allDayMeetingResources.Add(allDayResource.Id);
                }
                allDayMeeting.Resources = allDayMeetingResources;

                this.Events?.Add(allDayMeeting);

                // Create appointments for each resource throughout the full day
                foreach (var resource in resources)
                {
                    Employee? employee = resource as Employee;
                    if (employee != null && employee.Id != null)
                    {
                        object resourceId = employee.Id;

                        // Initialize time slot tracking for this resource if needed
                        if (!dateResourceTimeSlots[dateKey].ContainsKey(resourceId))
                        {
                            dateResourceTimeSlots[dateKey][resourceId] = new HashSet<int>();
                        }

                        // Create early morning appointment (1 AM - 8 AM)
                        List<int> earlyMorningHours = new List<int>();
                        for (int hour = 1; hour <= 8; hour++)
                        {
                            if (!dateResourceTimeSlots[dateKey][resourceId].Contains(hour))
                            {
                                earlyMorningHours.Add(hour);
                            }
                        }

                        if (earlyMorningHours.Count > 0)
                        {
                            int selectedHour = earlyMorningHours[randomTime.Next(earlyMorningHours.Count)];
                            dateResourceTimeSlots[dateKey][resourceId].Add(selectedHour);

                            Meeting earlyMorningMeeting = new Meeting();
                            earlyMorningMeeting.From = new DateTime(date.Year, date.Month, date.Day, selectedHour, 0, 0);
                            earlyMorningMeeting.To = earlyMorningMeeting.From.AddHours(1);
                            earlyMorningMeeting.EventName = this.currentDayMeetings[randomTime.Next(Math.Min(9, this.currentDayMeetings.Count - 1))];
                            earlyMorningMeeting.Background = this.colors[randomTime.Next(Math.Min(9, this.colors.Count - 1))];
                            earlyMorningMeeting.IsAllDay = false;
                            earlyMorningMeeting.StartTimeZone = TimeZoneInfo.Local;
                            earlyMorningMeeting.EndTimeZone = TimeZoneInfo.Local;

                            ObservableCollection<object> earlyMorningMeetingResources = new ObservableCollection<object>();
                            earlyMorningMeetingResources.Add(employee.Id);
                            earlyMorningMeeting.Resources = earlyMorningMeetingResources;

                            this.Events?.Add(earlyMorningMeeting);
                        }

                        // Create business hours appointment (9 AM - 5 PM)
                        List<int> businessHours = new List<int>();
                        for (int hour = 9; hour <= 17; hour++)
                        {
                            if (!dateResourceTimeSlots[dateKey][resourceId].Contains(hour))
                            {
                                businessHours.Add(hour);
                            }
                        }

                        if (businessHours.Count > 0)
                        {
                            int selectedHour = businessHours[randomTime.Next(businessHours.Count)];
                            dateResourceTimeSlots[dateKey][resourceId].Add(selectedHour);

                            Meeting businessHoursMeeting = new Meeting();
                            businessHoursMeeting.From = new DateTime(date.Year, date.Month, date.Day, selectedHour, 0, 0);
                            businessHoursMeeting.To = businessHoursMeeting.From.AddHours(1);
                            businessHoursMeeting.EventName = this.currentDayMeetings[randomTime.Next(Math.Min(9, this.currentDayMeetings.Count - 1))];
                            businessHoursMeeting.Background = this.colors[randomTime.Next(Math.Min(9, this.colors.Count - 1))];
                            businessHoursMeeting.IsAllDay = false;
                            businessHoursMeeting.StartTimeZone = TimeZoneInfo.Local;
                            businessHoursMeeting.EndTimeZone = TimeZoneInfo.Local;

                            ObservableCollection<object> businessHoursMeetingResources = new ObservableCollection<object>();
                            businessHoursMeetingResources.Add(employee.Id);
                            businessHoursMeeting.Resources = businessHoursMeetingResources;

                            this.Events?.Add(businessHoursMeeting);
                        }

                        // Create evening appointment (6 PM - 11 PM)
                        List<int> eveningHours = new List<int>();
                        for (int hour = 18; hour <= 23; hour++)
                        {
                            if (!dateResourceTimeSlots[dateKey][resourceId].Contains(hour))
                            {
                                eveningHours.Add(hour);
                            }
                        }

                        if (eveningHours.Count > 0)
                        {
                            int selectedHour = eveningHours[randomTime.Next(eveningHours.Count)];
                            dateResourceTimeSlots[dateKey][resourceId].Add(selectedHour);

                            Meeting eveningMeeting = new Meeting();
                            eveningMeeting.From = new DateTime(date.Year, date.Month, date.Day, selectedHour, 0, 0);
                            eveningMeeting.To = eveningMeeting.From.AddHours(1);
                            eveningMeeting.EventName = this.currentDayMeetings[randomTime.Next(Math.Min(9, this.currentDayMeetings.Count - 1))];
                            eveningMeeting.Background = this.colors[randomTime.Next(Math.Min(9, this.colors.Count - 1))];
                            eveningMeeting.IsAllDay = false;
                            eveningMeeting.StartTimeZone = TimeZoneInfo.Local;
                            eveningMeeting.EndTimeZone = TimeZoneInfo.Local;

                            ObservableCollection<object> eveningMeetingResources = new ObservableCollection<object>();
                            eveningMeetingResources.Add(employee.Id);
                            eveningMeeting.Resources = eveningMeetingResources;

                            this.Events?.Add(eveningMeeting);
                        }
                    }
                }
            }
        }

        #endregion BookingAppointments

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
            this.employeeNames.Add("Robert");
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