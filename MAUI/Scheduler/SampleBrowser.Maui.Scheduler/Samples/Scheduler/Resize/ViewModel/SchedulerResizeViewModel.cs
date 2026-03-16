#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    /// <summary>
    /// The resize view model for scheduler.
    /// </summary>
    public class SchedulerResizeViewModel
    {
        #region Fields

        /// <summary>
        /// Team management
        /// </summary>
        private List<string> subjects;

        /// <summary>
        /// The color collection
        /// </summary>
        private List<Brush> colors;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerResizeViewModel" /> class.
        /// </summary>
        public SchedulerResizeViewModel()
        {
            this.subjects = SchedulerHelper.GetSubjects();
            this.colors = SchedulerHelper.GetColors();
            this.IntializeAppoitments();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets appointments.
        /// </summary>
        public ObservableCollection<SchedulerAppointment>? Events { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Method to initialize the appointments.
        /// </summary>
        private void IntializeAppoitments()
        {
            this.Events = new ObservableCollection<SchedulerAppointment>();
            Random randomTime = new();
            List<Point> randomTimeCollection = this.GettingTimeRanges();

            DateTime date;
            DateTime dateFrom = DateTime.Now.AddDays(-50);
            DateTime dateTo = DateTime.Now.AddDays(50);

            for (date = dateFrom; date < dateTo; date = date.AddDays(1))
            {
                for (int additionalAppointmentIndex = 0; additionalAppointmentIndex < 1; additionalAppointmentIndex++)
                {
                    var appointment = new SchedulerAppointment();
                    int hour = randomTime.Next((int)randomTimeCollection[additionalAppointmentIndex].X, (int)randomTimeCollection[additionalAppointmentIndex].Y);
                    appointment.StartTime = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                    appointment.EndTime = appointment.StartTime.AddHours(1);
                    appointment.Subject = this.subjects[randomTime.Next(9)];
                    appointment.Background = this.colors[randomTime.Next(10)];
                    this.Events.Add(appointment);
                }
            }
        }

        /// <summary>
        /// Method for get timing range.
        /// </summary>
        /// <returns>return time collection</returns>
        private List<Point> GettingTimeRanges()
        {
            List<Point> randomTimeCollection = new();
            randomTimeCollection.Add(new Point(9, 11));
            randomTimeCollection.Add(new Point(12, 14));
            randomTimeCollection.Add(new Point(15, 17));

            return randomTimeCollection;
        }

        #endregion
    }
}