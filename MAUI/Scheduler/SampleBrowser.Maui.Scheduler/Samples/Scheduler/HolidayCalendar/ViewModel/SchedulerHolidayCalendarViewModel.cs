using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    /// <summary>
    /// Represents the view model for the scheduler.
    /// </summary>
    public class SchedulerViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Local cache of holiday appointments for restore when re-enabled.
        /// </summary>
        private HashSet<SchedulerAppointment> holidayAppointmentsCache = new HashSet<SchedulerAppointment>();

        /// <summary>
        /// Gets or sets a value indicating whether to show holiday events in the scheduler.
        /// </summary>
        private bool showHolidayEvents;

        /// <summary>
        /// Gets or sets a value indicating whether to schedule events on holidays in the scheduler.
        /// </summary>
        private bool scheduleEventsOnHoliday;

        /// <summary>
        /// Gets or sets the event that is raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

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
        /// Gets or sets the collection of appointments in the scheduler.
        /// </summary>
        public ObservableCollection<SchedulerAppointment> Appointments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show holiday events in the scheduler.
        /// </summary>
        public bool ShowHolidayEvents
        {
            get => showHolidayEvents;
            set
            {
                if (showHolidayEvents != value)
                {
                    showHolidayEvents = value;
                    UpdateHolidayEvents();
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to schedule events on holidays in the scheduler.
        /// </summary>
        public bool ScheduleEventsOnHoliday
        {
            get => scheduleEventsOnHoliday;
            set
            {
                if (scheduleEventsOnHoliday != value)
                {
                    scheduleEventsOnHoliday = value;
                    UpdateHolidaySchedulerEvents();
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerViewModel"/> class.
        /// </summary>
        public SchedulerViewModel()
        {
            this.Appointments = new ObservableCollection<SchedulerAppointment>();
            LoadYearlyAppointments();
            this.ShowHolidayEvents = true;
            this.ScheduleEventsOnHoliday = true;
            this.DisplayDate = new DateTime(2026, 8, 15);
            this.MinDateTime = new DateTime(2026, 1, 1);
            this.MaxDateTime = new DateTime(2026, 12, 31);
        }

        /// <summary>
        /// Loads the yearly appointments into the scheduler.
        /// </summary>
        private void LoadYearlyAppointments()
        {
            var subjects = new string[]
            {
                "Team Meeting",
                "Sprint Planning",
                "Client Demo",
                "Code Review",
                "Architecture Meeting",
                "Testing Session",
                "Product Planning",
                "Customer Call",
                "Release Planning",
                "Bug Triage",
                "Design Discussion",
                "Requirement Review",
                "Training Session",
                "Project Review",
                "Status Update",
                "Technical Discussion",
                "Knowledge Sharing",
                "Retrospective"
            };

            int subjectIndex = 0;

            // Holiday dates
            var holidayDates = GetIndianHolidays(2026).Select(x => x.StartTime.Date).ToHashSet();

            for (int month = 1; month <= 12; month++)
            {
                for (int day = 1; day <= 15; day++)
                {
                    int actualDay = Math.Min(day * 2, DateTime.DaysInMonth(2026, month));

                    DateTime appointmentDate = new DateTime(2026, month, actualDay);

                    // Skip holidays
                    if (holidayDates.Contains(appointmentDate.Date))
                    {
                        continue;
                    }

                    Appointments.Add(new SchedulerAppointment
                    {
                        Subject = subjects[subjectIndex++ % subjects.Length],
                        StartTime = appointmentDate.AddHours(9),
                        EndTime = appointmentDate.AddHours(10)
                    });

                    Appointments.Add(new SchedulerAppointment
                    {
                        Subject = subjects[subjectIndex++ % subjects.Length],
                        StartTime = appointmentDate.AddHours(14),
                        EndTime = appointmentDate.AddHours(15)
                    });
                }
            }
        }

        /// <summary>
        /// Update the holiday events.
        /// </summary>
        private void UpdateHolidayEvents()
        {
            RemoveHolidayEvents();

            if (!this.ShowHolidayEvents)
            {
                return;
            }
            var holidayAppointments = GetIndianHolidays(2026);
            foreach (var holidayEvent in holidayAppointments)
            {
                Appointments.Add(holidayEvent);
            }
        }

        /// <summary>
        /// Removes the holiday events from the scheduler.
        /// </summary>
        private void RemoveHolidayEvents()
        {
            var holidays = Appointments.Where(x => x.IsReadOnly).ToList();

            foreach (var holiday in holidays)
            {
                Appointments.Remove(holiday);
            }
        }

        /// <summary>
        /// Toggle holiday appointments on/off.
        /// </summary>
        private void UpdateHolidaySchedulerEvents()
        {
            HashSet<DateTime> holidayDates = GetIndianHolidayDates(2026);

            if (ScheduleEventsOnHoliday)
            {
                foreach (var holiday in holidayAppointmentsCache)
                {
                    if (!Appointments.Any(a => a.Id == holiday.Id))
                    {
                        Appointments.Add(holiday);
                    }
                }
            }
            else
            {
                var holidayEvents = Appointments.Where(a => !a.IsReadOnly && holidayDates.Any(date => a.ActualStartTime.Date == date.Date || (a.ActualEndTime.Date == date.Date && (a.IsAllDay || a.ActualEndTime.TimeOfDay.TotalMilliseconds > 0)) || (a.ActualStartTime.Date < date.Date && a.ActualEndTime.Date > date.Date))).ToList();
                holidayAppointmentsCache.Clear();
                foreach (var appt in holidayEvents)
                {
                    holidayAppointmentsCache.Add(appt);
                    Appointments.Remove(appt);
                }
            }
        }

        /// <summary>
        /// Gets the list of Indian holidays for the specified year.
        /// </summary>
        /// <param name="year">The year for which to get the holidays.</param>
        /// <returns>A list of holiday appointments for the specified year.</returns>
        private List<SchedulerAppointment> GetIndianHolidays(int year)
        {
            return new List<SchedulerAppointment>
            {
                CreateHoliday("Republic Day", new DateTime(year, 1, 26)),
                CreateHoliday("Maha Shivaratri", new DateTime(year, 2, 26)),
                CreateHoliday("Holi", new DateTime(year, 3, 14)),
                CreateHoliday("Ram Navami", new DateTime(year, 4, 6)),
                CreateHoliday("Good Friday", new DateTime(year, 4, 18)),
                CreateHoliday("Independence Day", new DateTime(year, 8, 15)),
                CreateHoliday("Janmashtami", new DateTime(year, 8, 16)),
                CreateHoliday("Gandhi Jayanti", new DateTime(year, 10, 2)),
                CreateHoliday("Dussehra", new DateTime(year, 10, 22)),
                CreateHoliday("Diwali", new DateTime(year, 11, 8)),
                CreateHoliday("Guru Nanak Jayanti", new DateTime(year, 11, 24)),
                CreateHoliday("Christmas", new DateTime(year, 12, 25))
            };
        }

        /// <summary>
        /// Creates a holiday appointment with the specified name and date.
        /// </summary>
        /// <param name="name">The name of the holiday.</param>
        /// <param name="date">The date of the holiday.</param>
        /// <returns>A holiday appointment with the specified name and date.</returns>
        private SchedulerAppointment CreateHoliday(string name, DateTime date)
        {
            return new SchedulerAppointment
            {
                Subject = name,
                StartTime = date,
                EndTime = date,
                IsAllDay = true,
                IsReadOnly = true,
                Background = Colors.Green
            };
        }

        /// <summary>
        /// Returns a set of fixed Indian holiday dates for the given year.
        /// </summary>
        /// <param name="year">The year for which to get the holidays.</param>
        /// <returns></returns>
        internal HashSet<DateTime> GetIndianHolidayDates(int year)
        {
            return new HashSet<DateTime>
            {
                new DateTime(year, 1, 26),   // Republic Day
                new DateTime(year, 2, 26),   // Maha Shivaratri
                new DateTime(year, 3, 14),   // Holi
                new DateTime(year, 4, 6),    // Ram Navami
                new DateTime(year, 4, 18),   // Good Friday
                new DateTime(year, 8, 15),   // Independence Day
                new DateTime(year, 8, 16),   // Janmashtami
                new DateTime(year, 10, 2),   // Gandhi Jayanti
                new DateTime(year, 10, 22),  // Dussehra
                new DateTime(year, 11, 8),   // Diwali
                new DateTime(year, 11, 24),  // Guru Nanak Jayanti
                new DateTime(year, 12, 25)   // Christmas
            };
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
