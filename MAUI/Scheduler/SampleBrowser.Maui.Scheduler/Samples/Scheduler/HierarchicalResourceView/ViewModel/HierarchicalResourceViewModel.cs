using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    public class HierarchicalResourceViewModel
    {
        /// <summary>
        /// current day meetings 
        /// </summary>
        private List<string> subjects;

        /// <summary>
        /// color collection
        /// </summary>
        private List<Brush> colors;

        /// <summary>
        /// list of meeting
        /// </summary>
        private ObservableCollection<Meeting>? events;

        /// <summary>
        /// resources
        /// </summary>
        private ObservableCollection<object>? resources;

        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalResourceViewModel" /> class.
        /// </summary>
        public HierarchicalResourceViewModel()
        {
            this.colors = new List<Brush>();
            this.Events = new ObservableCollection<Meeting>();
            this.subjects = new List<string>();
            this.Resources = new ObservableCollection<object>();
            this.DisplayDate = DateTime.Now.Date.AddHours(8).AddMinutes(50);
            this.InitializeDataForBookings();
            this.InitializeResources();
            this.BookingAppointments();
        }

        private void InitializeResources()
        {
            var project1 = new TeamMember
            {
                Name = "PROJECT 1",
                Id = "1",
            };

            var project2 = new TeamMember
            {
                Name = "PROJECT 2",
                Id = "2",
            };

            var development1 = new TeamMember
            {
                Name = "Development",
                Id = "100",
                GroupId = project1.Id,
            };

            var testing1 = new TeamMember
            {
                Name = "Testing",
                Id = "101",
                GroupId = project1.Id,
            };

            var development2 = new TeamMember
            {
                Name = "Development",
                Id = "102",
                GroupId = project2.Id,
            };

            var testing2 = new TeamMember
            {
                Name = "Testing",
                Id = "103",
                GroupId = project2.Id,
            };

            if (Resources != null)
            {
                Resources.Add(project1);
                Resources.Add(project2);
                Resources.Add(development1);
                Resources.Add(testing1);
                Resources.Add(development2);
                Resources.Add(testing2);
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
            DateTime today = DateTime.Now;

            DateTime startMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-2);
            DateTime endMonth = startMonth.AddMonths(5);
            var childResources = this.resources?.Skip(2).OfType<TeamMember>().ToList();
            if (childResources == null || childResources.Count == 0)
            {
                return;
            }
            Random random = new();
            for (DateTime month = startMonth; month < endMonth; month = month.AddMonths(1))
            {
                int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);
                int skipDay = random.Next(1, daysInMonth + 1);
                int allDay;
                do
                {
                    allDay = random.Next(1, daysInMonth + 1);
                }
                while (allDay == skipDay);

                int spanDay;
                do
                {
                    spanDay = random.Next(1, daysInMonth + 1);
                }
                while (spanDay == skipDay || spanDay == allDay);

                for (int day = 1; day <= daysInMonth; day++)
                {
                    if (day == skipDay)
                    {
                        continue;
                    }

                    DateTime appointmentDate = new DateTime(month.Year, month.Month, day);

                    foreach (TeamMember resource in childResources)
                    {
                        

                        Meeting appointment;
                        string subject = subjects[random.Next(subjects.Count)];
                        var color = colors[random.Next(this.colors.Count)];
                        var res = new ObservableCollection<object>();
                        if (resource != null && resource.Id != null)
                        {
                            res.Add(resource.Id);
                        }

                        if (day == allDay)
                        {
                            appointment = new Meeting
                            {
                                EventName = subject,
                                Background = color,
                                Resources = res,
                                From = appointmentDate,
                                To = appointmentDate.AddDays(1),
                                IsAllDay = true,
                                StartTimeZone = TimeZoneInfo.Local,
                                EndTimeZone = TimeZoneInfo.Local
                            };
                        }
                        else if (day == spanDay)
                        {
                            appointment = new Meeting
                            {
                                EventName = subject,
                                Background = color,
                                Resources = res,
                                From = appointmentDate,
                                To = appointmentDate.AddDays(3),
                                IsAllDay = false,
                                StartTimeZone = TimeZoneInfo.Local,
                                EndTimeZone = TimeZoneInfo.Local
                            };
                        }
                        else
                        {
                            int startHour = random.Next(8, 18);
                            double[] durations = { 1.0, 1.5, 2.0 };
                            double duration = durations[random.Next(durations.Length)];
                            DateTime startTime = new DateTime(appointmentDate.Year, appointmentDate.Month, appointmentDate.Day, startHour, 0, 0);
                            appointment = new Meeting
                            {
                                EventName = subject,
                                Background = color,
                                Resources = res,
                                From = startTime,
                                To = startTime.AddHours(duration),
                                IsAllDay = false,
                                StartTimeZone = TimeZoneInfo.Local,
                                EndTimeZone = TimeZoneInfo.Local
                            };
                        }

                        this.Events?.Add(appointment);
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
            this.subjects = new List<string>();
            this.subjects.Add("General Meeting");
            this.subjects.Add("Plan Execution");
            this.subjects.Add("Project Plan");
            this.subjects.Add("Consulting");
            this.subjects.Add("Performance Check");
            this.subjects.Add("Yoga Therapy");
            this.subjects.Add("Plan Execution");
            this.subjects.Add("Project Plan");
            this.subjects.Add("Consulting");
            this.subjects.Add("Performance Check");

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
        }

        #endregion InitializeDataForBookings
    }

}