using System.Collections.ObjectModel;
using System.Windows.Input;
using Syncfusion.Maui.Scheduler;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    /// <summary>
    /// View model for the mobile Adaptive Hierarchical Resource demo (Android/iOS). Builds
    /// the same two-level Projects / Tasks hierarchy as the desktop view but with
    /// hamburger-icon drawer support on the resource header.
    /// </summary>
    public class HierarchicalAdaptiveResourceViewModel
    {
        #region Fields

        private List<string> subjects;
        private ObservableCollection<object>? resources;
        private ObservableCollection<Meeting>? events;
        private List<Brush> colors;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalAdaptiveResourceViewModel"/> class.
        /// </summary>
        public HierarchicalAdaptiveResourceViewModel()
        {
            // Hamburger command toggles the adaptive resource drawer.
            this.HamburgerIconCommand = new Command(this.OnHamburgerIconTapped);
            this.Resources = new ObservableCollection<object>();
            this.Events = new ObservableCollection<Meeting>();
            this.subjects = new List<string>();
            this.colors = new List<Brush>();
            this.DisplayDate = DateTime.Now.Date.AddHours(8).AddMinutes(50);
            this.InitializeDataForBookings();
            this.InitializeResources();
            this.BookingAppointments();
        }

        #region Properties

        /// <summary>
        /// Gets or sets the hamburger icon command used by the adaptive resource header.
        /// </summary>
        public ICommand HamburgerIconCommand { get; set; }

        /// <summary>
        /// Gets or sets the events shown by the scheduler.
        /// </summary>
        public ObservableCollection<Meeting>? Events
        {
            get { return this.events; }
            set { this.events = value; }
        }

        /// <summary>
        /// Gets or sets the resource collection (parent + child resources).
        /// </summary>
        public ObservableCollection<object>? Resources
        {
            get { return this.resources; }
            set { this.resources = value; }
        }

        /// <summary>
        /// Gets or sets the display date for the scheduler.
        /// </summary>
        public DateTime DisplayDate { get; set; }

        #endregion

        /// <summary>
        /// Toggles the resource drawer view when the hamburger icon is tapped.
        /// </summary>
        /// <param name="obj">The <see cref="SchedulerAdaptiveResource"/> instance.</param>
        private void OnHamburgerIconTapped(object obj)
        {
            (obj as SchedulerAdaptiveResource)?.ToggleResourceDrawerView();
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