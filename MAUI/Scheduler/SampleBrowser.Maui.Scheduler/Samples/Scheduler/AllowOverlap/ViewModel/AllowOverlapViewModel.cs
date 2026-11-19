using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    /// <summary>
    /// The agenda view View Model.
    /// </summary>
    internal class AllowOverlapViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerGettingStartedViewModel" /> class.
        /// </summary>
        public AllowOverlapViewModel()
        {
            this.Events = new ObservableCollection<SchedulerAppointment>();
            this.GenerateRandomAppointments();
            this.DisplayDate = DateTime.Now.Date.AddHours(8).AddMinutes(50);
            this.MinDateTime = DateTime.Now.Date.AddMonths(-3);
            this.MaxDateTime = DateTime.Now.AddMonths(3);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets appointments.
        /// </summary>
        public ObservableCollection<SchedulerAppointment> Events { get; set; }

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

        #region Method

        /// <summary>
        /// Method to generate the appointments.
        /// </summary>
        private void GenerateRandomAppointments()
        {
            ObservableCollection<DateTime> WorkWeekDays = new ObservableCollection<DateTime>();
            ObservableCollection<string> WorkWeekSubjects = new ObservableCollection<string>()
                                                   { "GoToMeeting", "Business Meeting", "Conference", "Project Status Discussion",
                                                     "Auditing", "Client Meeting", "Generate Report", "Target Meeting", "General Meeting" };

            ObservableCollection<DateTime> NonWorkingDays = new ObservableCollection<DateTime>();

            ObservableCollection<string> YearlyOccurranceSubjects = new ObservableCollection<string>() { "Wedding Anniversary", "Sam's Birthday", "Jenny's Birthday" };
            ObservableCollection<string> MonthlyOccurranceSubjects = new ObservableCollection<string>() { "Pay House Rent", "Car Service", "Medical Check Up" };
            ObservableCollection<string> WeekEndOccurranceSubjects = new ObservableCollection<string>() { "FootBall Match", "TV Show" };
            ObservableCollection<Brush> colorCollection = this.GetColorCollection();

            Random ran = new();
            DateTime today = DateTime.Now;
            if (today.Month == 12)
            {
                today = today.AddMonths(-1);
            }
            else if (today.Month == 1)
            {
                today = today.AddMonths(1);
            }

            DateTime startMonth = new(today.Year, today.Month - 1, 1, 0, 0, 0);
            int day = (int)startMonth.DayOfWeek;
            DateTime CurrentStart = startMonth.AddDays(-day);

            for (int i = 0; i < 90; i++)
            {
                if (i % 7 == 0 || i % 7 == 6)
                {
                    NonWorkingDays.Add(CurrentStart.AddDays(i));
                }
                else
                {
                    WorkWeekDays.Add(CurrentStart.AddDays(i));
                }
            }

            for (int i = 0; i < WorkWeekDays.Count; i++)
            {
                DateTime date = WorkWeekDays[i];
                int count = ran.Next(2, 4);
                for (int index = 0; index < count; index++)
                {
                    int startIndex = 8 + (index * 3) + ran.Next(0, 2);
                    DateTime startDate = date.AddHours(startIndex);
                    this.Events.Add(new SchedulerAppointment()
                    {
                        StartTime = startDate,
                        EndTime = startDate.AddHours(1),
                        Background = colorCollection[ran.Next(0, colorCollection.Count)],
                        Subject = WorkWeekSubjects[ran.Next(0, WorkWeekSubjects.Count)]
                    });
                }
            }

            for (int j = 0; j < YearlyOccurranceSubjects.Count; j++)
            {
                DateTime date = NonWorkingDays[ran.Next(0, NonWorkingDays.Count)].AddHours(ran.Next(9, 18));
                this.Events.Add(new SchedulerAppointment()
                {
                    StartTime = date,
                    EndTime = date.AddHours(1),
                    Background = colorCollection[1],
                    Subject = YearlyOccurranceSubjects[j]
                });
            }

            for (int k = 0; k < MonthlyOccurranceSubjects.Count; k++)
            {
                DateTime date = NonWorkingDays[ran.Next(0, NonWorkingDays.Count)].AddHours(ran.Next(9, 23));
                this.Events.Add(new SchedulerAppointment()
                {
                    StartTime = date,
                    EndTime = date.AddHours(1),
                    Background = colorCollection[k],
                    Subject = MonthlyOccurranceSubjects[k]
                });
            }

            for (int l = 0; l < WeekEndOccurranceSubjects.Count; l++)
            {
                DateTime date = NonWorkingDays[ran.Next(0, NonWorkingDays.Count)].AddHours(ran.Next(8, 22));
                this.Events.Add(new SchedulerAppointment()
                {
                    StartTime = date,
                    EndTime = date.AddHours(1),
                    Background = colorCollection[l],
                    Subject = WeekEndOccurranceSubjects[l]
                });
            }
        }

        /// <summary>
        /// Method to get the color collection.
        /// </summary>
        /// <returns>The color collection.</returns>
        private ObservableCollection<Brush> GetColorCollection()
        {
            ObservableCollection<Brush> colors = new ObservableCollection<Brush>
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
            };

            return colors;
        }

        #endregion
    }

}
