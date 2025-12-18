#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartScheduler
{
    using Syncfusion.Maui.Scheduler;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents the smart scheduler view model class.
    /// </summary>
    public class SmartSchedulerViewModel
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
        private ObservableCollection<SchedulerAppointment>? events;

        /// <summary>
        /// name collection
        /// </summary>

        private List<string> employeeNames;

        /// <summary>
        /// resources
        /// </summary>
        private ObservableCollection<SchedulerResourceExt>? resources;

        /// <summary>
        /// Holds the assist view suggestions.
        /// </summary>
        private List<string>? suggestedPrompts;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartSchedulerViewModel" /> class.
        /// </summary>
        public SmartSchedulerViewModel()
        {
            this.employeeNames = new List<string>();
            this.colors = new List<Brush>();
            this.startHours = new List<int>();
            this.Events = new ObservableCollection<SchedulerAppointment>();
            this.currentDayMeetings = new List<string>();
            this.Resources = new ObservableCollection<SchedulerResourceExt>();
            this.SuggestedPrompts = new List<string>() { "Summarize today's appointments", "Find today's free timeslots", "Conflict detection" };
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
                SchedulerResourceExt resource = new SchedulerResourceExt();
                resource.Name = employeeNames[i];
                resource.Background = this.colors[random.Next(0, 9)];
                resource.Foreground = (resource.Background as SolidColorBrush)?.Color.GetLuminosity() > 0.7 ? Colors.Black : Colors.White;
                resource.Id = i.ToString();

                if (resource.Name == "Robert")
                {
                    resource.ImageName = "people_circle8.png";
                }
                else if (resource.Name == "Sophia")
                {
                    resource.ImageName = "people_circle1.png";
                }
                else if (resource.Name == "Stephen")
                {
                    resource.ImageName = "people_circle12.png";
                }
                else if (resource.Name == "Emilia")
                {
                    resource.ImageName = "people_circle3.png";
                }
                else if (resource.Name == "Max")
                {
                    resource.ImageName = "people_circle4.png";
                }
                else if (resource.Name == "William")
                {
                    resource.ImageName = "people_circle5.png";
                }

                Resources?.Add(resource);
            }
        }

        #region ListOfMeeting

        /// <summary>
        /// Gets or sets appointments.
        /// </summary>
        public ObservableCollection<SchedulerAppointment>? Events
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

        public ObservableCollection<SchedulerResourceExt>? Resources
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
        /// Gets or sets the assist view suggestions.
        /// </summary>
        public List<string>? SuggestedPrompts
        {
            get
            {
                return this.suggestedPrompts;
            }

            set
            {
                this.suggestedPrompts = value;
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
            Random random = new Random();
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddMonths(1);

            if (resources == null || this.Events == null)
                return;

            for (DateTime date = startDate; date < endDate; date = date.AddDays(1))
            {
                HashSet<int> usedHours = new HashSet<int>();
                for (int i = 0; i < 20; i++) 
                {
                    int hour;
                    int attempts = 0;
                    do
                    {
                        hour = this.startHours[random.Next(this.startHours.Count)];
                        attempts++;
                    } while (usedHours.Contains(hour) && attempts < 20);

                    if (usedHours.Contains(hour))
                        continue;

                    usedHours.Add(hour);

                    DateTime startTime = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                    DateTime endTime = startTime.AddHours(1);

                    // Pick a random resource
                    SchedulerResource? resource = resources[random.Next(resources.Count)] as SchedulerResource;
                    if (resource == null || resource.Id == null)
                        continue;

                    // Check for conflicts
                    bool hasConflict = this.Events.OfType<SchedulerAppointment>().Any(e => e.ResourceIds != null &&
                        e.ResourceIds.Contains(resource.Id) &&
                        startTime < e.EndTime && endTime > e.StartTime);

                    if (hasConflict)
                        continue;

                    // Create appointment
                    SchedulerAppointment meeting = new SchedulerAppointment
                    {
                        StartTime = startTime,
                        EndTime = endTime,
                        Subject = this.currentDayMeetings[random.Next(this.currentDayMeetings.Count)],
                        Background = this.colors[random.Next(this.colors.Count)],
                        ResourceIds = new ObservableCollection<object> { resource.Id }
                    };

                    this.Events.Add(meeting);
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
            this.currentDayMeetings.Add("General check-up");
            this.currentDayMeetings.Add("Therapy Session");
            this.currentDayMeetings.Add("Specialist Appointment");
            this.currentDayMeetings.Add("Consulting");
            this.currentDayMeetings.Add("Treatment Planning");
            this.currentDayMeetings.Add("Yoga Therapy");
            this.currentDayMeetings.Add("Wellness Review");
            this.currentDayMeetings.Add("Health Checkup");
            this.currentDayMeetings.Add("Medical Review");

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
            this.employeeNames.Add("Sophia");
            this.employeeNames.Add("Stephen");
            this.employeeNames.Add("Emilia");
            this.employeeNames.Add("William");
            this.employeeNames.Add("Max");

            this.startHours = new List<int>();
            this.startHours.Add(8);
            this.startHours.Add(9);
            this.startHours.Add(10);
            this.startHours.Add(11);
            this.startHours.Add(12);
            this.startHours.Add(13);
            this.startHours.Add(14);
            this.startHours.Add(15);
            this.startHours.Add(16);
            this.startHours.Add(17);
            this.startHours.Add(18);
            this.startHours.Add(19);
            this.startHours.Add(20);
        }

        #endregion InitializeDataForBookings
    }

    public class SchedulerResourceExt : SchedulerResource
    {
        public string? ImageName { get; set; }
    }
}