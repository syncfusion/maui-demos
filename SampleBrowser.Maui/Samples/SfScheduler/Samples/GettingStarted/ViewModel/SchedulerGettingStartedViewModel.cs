#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.SfScheduler
{
    /// <summary>
    /// The getting started View Model.
    /// </summary>
    public class SchedulerGettingStartedViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerGettingStartedViewModel" /> class.
        /// </summary>
        public SchedulerGettingStartedViewModel()
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
            var WorkWeekDays = new ObservableCollection<DateTime>();
            var WorkWeekSubjects = new ObservableCollection<string>()
                                                           { "GoToMeeting", "Business Meeting", "Conference", "Project Status Discussion",
                                                             "Auditing", "Client Meeting", "Generate Report", "Target Meeting", "General Meeting" };

            var NonWorkingDays = new ObservableCollection<DateTime>();

            var YearlyOccurranceSubjects = new ObservableCollection<string>() { "Wedding Anniversary", "Sam's Birthday", "Jenny's Birthday" };
            var MonthlyOccurranceSubjects = new ObservableCollection<string>() { "Pay House Rent", "Car Service", "Medical Check Up" };
            var WeekEndOccurranceSubjects = new ObservableCollection<string>() { "FootBall Match", "TV Show" };
            var colorCollection = this.GetColorCollection();

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
                    //add weekend appointments
                    NonWorkingDays.Add(CurrentStart.AddDays(i));
                }
                else
                {
                    //Add Workweek appointment
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
            int j = 0;
            int k = 0;
            int l = 0;

            while (j < YearlyOccurranceSubjects.Count)
            {
                DateTime date = NonWorkingDays[ran.Next(0, NonWorkingDays.Count)];
                this.Events.Add(new SchedulerAppointment()
                {
                    StartTime = date,
                    EndTime = date.AddHours(1),
                    Background = colorCollection[1],
                    Subject = YearlyOccurranceSubjects[j]
                });

                j++;
            }

            while (k < MonthlyOccurranceSubjects.Count)
            {
                DateTime date = NonWorkingDays[ran.Next(0, NonWorkingDays.Count)].AddHours(ran.Next(9, 23));
                this.Events.Add(new SchedulerAppointment()
                {
                    StartTime = date,
                    EndTime = date.AddHours(1),
                    Background = colorCollection[k],
                    Subject = MonthlyOccurranceSubjects[k]
                });

                k++;
            }

            while (l < WeekEndOccurranceSubjects.Count)
            {
                DateTime date = NonWorkingDays[ran.Next(0, NonWorkingDays.Count)].AddHours(ran.Next(0, 23));
                this.Events.Add(new SchedulerAppointment()
                {
                    StartTime = date,
                    EndTime = date.AddHours(1),
                    Background = colorCollection[l],
                    Subject = WeekEndOccurranceSubjects[l]
                });

                l++;
            }
        }

        /// <summary>
        /// Method to get the color collection.
        /// </summary>
        /// <returns>The color collection.</returns>
        private ObservableCollection<Brush> GetColorCollection()
        {
            var colorCollection = new ObservableCollection<Brush>
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

            return colorCollection;
        }

        #endregion
    }
}