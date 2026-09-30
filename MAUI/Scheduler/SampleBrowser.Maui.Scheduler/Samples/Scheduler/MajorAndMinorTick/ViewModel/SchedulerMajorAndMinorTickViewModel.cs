using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    /// <summary>
    /// The major and minor tick View Model.
    /// </summary>
    public class SchedulerMajorAndMinorTickViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerMajorAndMinorTickViewModel" /> class.
        /// </summary>
        public SchedulerMajorAndMinorTickViewModel()
        {
            this.Events = new ObservableCollection<SchedulerAppointment>();
            this.GenerateAppointments();
            this.DisplayDate = DateTime.Now.Date.AddHours(9);
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
        private void GenerateAppointments()
        {
            ObservableCollection<string> subjects = new ObservableCollection<string>()
            {
                "GoToMeeting", "Business Meeting", "Conference", "Project Status Discussion",
                "Auditing", "Client Meeting", "Generate Report", "Target Meeting", "General Meeting"
            };

            ObservableCollection<Brush> colorCollection = this.GetColorCollection();
            Random ran = new();
            DateTime today = DateTime.Now.Date;

            for (int offset = -10; offset <= 10; offset++)
            {
                DateTime date = today.AddDays(offset);
                int appointmentCount = ran.Next(1, 4);

                for (int i = 0; i < appointmentCount; i++)
                {
                    int startHour = ran.Next(9, 17);
                    DateTime start = date.AddHours(startHour);
                    this.Events.Add(new SchedulerAppointment()
                    {
                        StartTime = start,
                        EndTime = start.AddHours(1),
                        Background = colorCollection[ran.Next(colorCollection.Count)],
                        Subject = subjects[ran.Next(subjects.Count)]
                    });
                }
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