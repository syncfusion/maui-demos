using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    public class MonthResourceViewModel
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
        /// Initializes a new instance of the <see cref="MonthResourceViewModel" /> class.
        /// </summary>
        public MonthResourceViewModel()
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
            for (int i = 0; i < 6; i++)
            {
                Employee employees = new Employee();
                employees.Name = employeeNames[i];
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
            Random randomTime = new Random();
            DateTime today = DateTime.Now;
            DateTime dateFrom = today.AddMonths(-2);
            DateTime dateTo = today.AddMonths(3);

            // Early return if no resources available
            if (resources == null || resources.Count == 0 || Events == null)
            {
                return;
            }

            ObservableCollection<DateTime> workWeekDays = new ObservableCollection<DateTime>();
            ObservableCollection<DateTime> nonWorkingDays = new ObservableCollection<DateTime>();
            ObservableCollection<string> specialEventNames = new ObservableCollection<string>() 
            { 
                "Birthday Celebration", "Anniversary Event", "Team Lunch", 
                "Office Party", "Workshop Session", "Conference Day" 
            };

            // Categorize days into work week and non-working days
            int dayCounter = 0;
            for (DateTime date = dateFrom; date <= dateTo; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    nonWorkingDays.Add(date);
                }
                else
                {
                    workWeekDays.Add(date);
                }
                dayCounter++;
            }

            // Create work week appointments (2-4 per day across all resources)
            for (int i = 0; i < workWeekDays.Count; i++)
            {
                DateTime date = workWeekDays[i];
                int appointmentCount = randomTime.Next(2, 5);
                
                for (int index = 0; index < appointmentCount; index++)
                {
                    // Assign to rotating resources
                    Employee? employee = resources[index % resources.Count] as Employee;
                    if (employee?.Id != null)
                    {
                        int startHour = 8 + (index * 3) + randomTime.Next(0, 2);
                        DateTime startDate = date.AddHours(startHour);
                        
                        Meeting workMeeting = new Meeting();
                        workMeeting.From = startDate;
                        workMeeting.To = startDate.AddHours(1);
                        workMeeting.EventName = this.currentDayMeetings[randomTime.Next(Math.Min(9, this.currentDayMeetings.Count - 1))];
                        workMeeting.Background = this.colors[randomTime.Next(Math.Min(9, this.colors.Count - 1))];
                        workMeeting.IsAllDay = false;
                        workMeeting.StartTimeZone = TimeZoneInfo.Local;
                        workMeeting.EndTimeZone = TimeZoneInfo.Local;

                        ObservableCollection<object> meetingResources = new ObservableCollection<object>();
                        meetingResources.Add(employee.Id);
                        workMeeting.Resources = meetingResources;

                        this.Events.Add(workMeeting);
                    }
                }
            }

            // Create special events on random non-working days for all resources
            int specialEventCount = 5;
            for (int i = 0; i < specialEventCount && nonWorkingDays.Count > 0; i++)
            {
                DateTime date = nonWorkingDays[randomTime.Next(0, nonWorkingDays.Count)];
                
                for (int resIdx = 0; resIdx < resources.Count; resIdx++)
                {
                    Employee? employee = resources[resIdx] as Employee;
                    if (employee?.Id != null)
                    {
                        Meeting specialMeeting = new Meeting();
                        specialMeeting.From = date.AddHours(randomTime.Next(9, 18));
                        specialMeeting.To = specialMeeting.From.AddHours(1);
                        specialMeeting.EventName = specialEventNames[i % specialEventNames.Count];
                        specialMeeting.Background = this.colors[i % this.colors.Count];
                        specialMeeting.IsAllDay = false;
                        specialMeeting.StartTimeZone = TimeZoneInfo.Local;
                        specialMeeting.EndTimeZone = TimeZoneInfo.Local;

                        ObservableCollection<object> eventResources = new ObservableCollection<object>();
                        eventResources.Add(employee.Id);
                        specialMeeting.Resources = eventResources;

                        this.Events.Add(specialMeeting);
                    }
                }
            }

            // Create all-day appointments for all resources
            for (int index = 0; index < 8; index++)
            {
                for (int resIdx = 0; resIdx < resources.Count; resIdx++)
                {
                    Employee? employee = resources[resIdx] as Employee;
                    if (employee?.Id != null)
                    {
                        DateTime allDayDate = today.AddDays(randomTime.Next(1, 30));
                        
                        Meeting allDayMeeting = new Meeting();
                        allDayMeeting.From = allDayDate.Date;
                        allDayMeeting.To = allDayDate.Date.AddHours(23).AddMinutes(59);
                        allDayMeeting.EventName = this.currentDayMeetings[randomTime.Next(Math.Min(9, this.currentDayMeetings.Count - 1))];
                        allDayMeeting.Background = this.colors[randomTime.Next(Math.Min(9, this.colors.Count - 1))];
                        allDayMeeting.IsAllDay = true;
                        allDayMeeting.StartTimeZone = TimeZoneInfo.Local;
                        allDayMeeting.EndTimeZone = TimeZoneInfo.Local;

                        ObservableCollection<object> allDayResources = new ObservableCollection<object>();
                        allDayResources.Add(employee.Id);
                        allDayMeeting.Resources = allDayResources;

                        this.Events.Add(allDayMeeting);
                    }
                }
            }

            // Create multi-day span appointments for all resources
            for (int resIdx = 0; resIdx < resources.Count; resIdx++)
            {
                Employee? employee = resources[resIdx] as Employee;
                if (employee?.Id != null)
                {
                    // Span appointment 1
                    Meeting spanMeeting1 = new Meeting();
                    spanMeeting1.From = today.Date.AddDays(2);
                    spanMeeting1.To = today.Date.AddDays(7);
                    spanMeeting1.EventName = this.currentDayMeetings[randomTime.Next(Math.Min(5, this.currentDayMeetings.Count - 1))];
                    spanMeeting1.Background = this.colors[resIdx % this.colors.Count];
                    spanMeeting1.StartTimeZone = TimeZoneInfo.Local;
                    spanMeeting1.EndTimeZone = TimeZoneInfo.Local;

                    ObservableCollection<object> spanResources1 = new ObservableCollection<object>();
                    spanResources1.Add(employee.Id);
                    spanMeeting1.Resources = spanResources1;

                    this.Events.Add(spanMeeting1);

                    // Span appointment 2
                    Meeting spanMeeting2 = new Meeting();
                    spanMeeting2.From = today.Date.AddDays(-5);
                    spanMeeting2.To = today.Date;
                    spanMeeting2.EventName = this.currentDayMeetings[randomTime.Next(Math.Min(5, this.currentDayMeetings.Count - 1))];
                    spanMeeting2.Background = this.colors[(resIdx + 1) % this.colors.Count];
                    spanMeeting2.StartTimeZone = TimeZoneInfo.Local;
                    spanMeeting2.EndTimeZone = TimeZoneInfo.Local;

                    ObservableCollection<object> spanResources2 = new ObservableCollection<object>();
                    spanResources2.Add(employee.Id);
                    spanMeeting2.Resources = spanResources2;

                    this.Events.Add(spanMeeting2);
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