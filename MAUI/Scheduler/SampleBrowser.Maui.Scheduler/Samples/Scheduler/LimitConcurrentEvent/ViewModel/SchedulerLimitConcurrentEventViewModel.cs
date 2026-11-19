using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    // /// <summary>
    // /// The Limit Concurrent Event view model. Generates the same heavily-overlapping
    // /// appointment set as the Blazor GetConcurrentData sample so the +N indicator (driven by
    // /// <see cref="SchedulerDaysView.MaxEventStack"/>) has realistic coverage in Day, Week and
    // /// WorkWeek views. Dates are pinned to May 25 - 29 of the current year for reproducibility.
    // /// </summary>
    public class SchedulerLimitConcurrentEventViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerLimitConcurrentEventViewModel"/> class.
        /// </summary>
        public SchedulerLimitConcurrentEventViewModel()
        {
            this.Events = new ObservableCollection<SchedulerAppointment>();
            this.GenerateConcurrentAppointments();
            this.DisplayDate = new DateTime(DateTime.Now.Year, 5, 25).AddHours(9);
            this.MinDateTime = new DateTime(DateTime.Now.Year, 5, 1);
            this.MaxDateTime = new DateTime(DateTime.Now.Year, 6, 30);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the appointments shown by the Scheduler.
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

        #region Methods

        /// <summary>
        /// Builds the 35 overlapping appointments from the Blazor GetConcurrentData sample.
        /// Each record preserves subject, location, color hex and the all-day flag so the
        /// multi-day cluster (May 25 - 29) gives the +N indicator enough data to render.
        /// </summary>
        private void GenerateConcurrentAppointments()
        {
            int year = DateTime.Now.Year;

            // Tuples: (id, subject, location, start, end, category color, isAllDay)
            List<(int Id, string Subject, string Location, DateTime Start, DateTime End, string Color, bool IsAllDay)> appointments = new()
            {
                (1, "Explosion of Betelgeuse Star", "Space Centre USA", new DateTime(year, 5, 25, 9, 30, 0), new DateTime(year, 5, 25, 11, 30, 0), "#1aaa55", false),
                (2, "Thule Air Crash Report", "Newyork City", new DateTime(year, 5, 26, 12, 0, 0), new DateTime(year, 5, 26, 14, 0, 0), "#357cd2", true),
                (3, "Blue Moon Eclipse", "Space Centre USA", new DateTime(year, 5, 27, 9, 30, 0), new DateTime(year, 5, 27, 11, 0, 0), "#7fa900", false),
                (4, "Meteor Showers in 2022", "Space Centre USA", new DateTime(year, 5, 28, 13, 0, 0), new DateTime(year, 5, 28, 14, 30, 0), "#ea7a57", true),
                (5, "Milky Way as Melting Pot", "Space Centre USA", new DateTime(year, 5, 29, 12, 0, 0), new DateTime(year, 5, 29, 14, 0, 0), "#00bdae", false),
                (6, "Mysteries of Bermuda Triangle", "Bermuda", new DateTime(year, 5, 25, 9, 30, 0), new DateTime(year, 5, 25, 11, 0, 0), "#8e24aa", false),
                (7, "Glaciers and Snowflakes", "Himalayas", new DateTime(year, 5, 26, 11, 0, 0), new DateTime(year, 5, 26, 12, 30, 0), "#8e24aa", false),
                (8, "Life on Mars", "Space Centre USA", new DateTime(year, 5, 24, 9, 0, 0), new DateTime(year, 5, 24, 10, 0, 0), "#357cd2", false),
                (9, "Alien Civilization", "Space Centre USA", new DateTime(year, 5, 28, 11, 0, 0), new DateTime(year, 5, 28, 13, 0, 0), "#7fa900", false),
                (10, "Wildlife Galleries", "Africa", new DateTime(year, 5, 28, 16, 0, 0), new DateTime(year, 5, 28, 17, 0, 0), "#ea7a57", false),
                (11, "Best Photography 2022", "London", new DateTime(year, 5, 25, 9, 30, 0), new DateTime(year, 5, 25, 11, 0, 0), "#00bdae", false),
                (12, "Smarter Puppies", "Sweden", new DateTime(year, 5, 26, 10, 0, 0), new DateTime(year, 5, 26, 11, 30, 0), "#f57f17", false),
                (13, "Myths of Andromeda Galaxy", "Space Centre USA", new DateTime(year, 5, 27, 10, 30, 0), new DateTime(year, 5, 27, 12, 30, 0), "#1aaa55", false),
                (14, "Aliens vs Humans", "Research Centre of USA", new DateTime(year, 5, 28, 10, 0, 0), new DateTime(year, 5, 28, 11, 30, 0), "#357cd2", false),
                (15, "Facts of Humming Birds", "California", new DateTime(year, 5, 29, 9, 30, 0), new DateTime(year, 5, 29, 11, 0, 0), "#7fa900", false),
                (16, "Sky Gazers", "Alaska", new DateTime(year, 5, 25, 11, 0, 0), new DateTime(year, 5, 25, 13, 0, 0), "#ea7a57", false),
                (17, "The Cycle of Seasons", "Research Centre of USA", new DateTime(year, 5, 26, 5, 30, 0), new DateTime(year, 5, 26, 7, 30, 0), "#00bdae", false),
                (18, "Space Galaxies and Planets", "Space Centre USA", new DateTime(year, 5, 27, 17, 0, 0), new DateTime(year, 5, 27, 18, 30, 0), "#f57f17", false),
                (19, "Lifecycle of Bumblebee", "San Francisco", new DateTime(year, 5, 28, 6, 0, 0), new DateTime(year, 5, 28, 7, 30, 0), "#7fa900", false),
                (20, "Alien Civilization", "Space Centre USA", new DateTime(year, 5, 29, 16, 0, 0), new DateTime(year, 5, 29, 18, 0, 0), "#ea7a57", false),
                (21, "Alien Civilization", "Space Centre USA", new DateTime(year, 5, 25, 14, 0, 0), new DateTime(year, 5, 25, 16, 0, 0), "#ea7a57", false),
                (22, "The Cycle of Seasons", "Research Centre of USA", new DateTime(year, 5, 26, 14, 30, 0), new DateTime(year, 5, 26, 16, 0, 0), "#00bdae", false),
                (23, "Sky Gazers", "Greenland", new DateTime(year, 5, 27, 14, 30, 0), new DateTime(year, 5, 27, 16, 0, 0), "#ea7a57", false),
                (24, "Facts of Humming Birds", "California", new DateTime(year, 5, 28, 12, 30, 0), new DateTime(year, 5, 28, 14, 30, 0), "#7fa900", false),
                (25, "Solar Flare Observation", "Space Centre USA", new DateTime(year, 5, 29, 10, 0, 0), new DateTime(year, 5, 29, 12, 0, 0), "#ff5733", false),
                (26, "Astronomy Workshop", "Newyork City", new DateTime(year, 5, 25, 11, 0, 0), new DateTime(year, 5, 25, 13, 0, 0), "#33c1ff", false),
                (27, "Rocket Engine Testing", "Research Centre of USA", new DateTime(year, 5, 26, 6, 30, 0), new DateTime(year, 5, 26, 8, 0, 0), "#9c27b0", false),
                (28, "Satellite Communication Drill", "Space Centre USA", new DateTime(year, 5, 27, 7, 0, 0), new DateTime(year, 5, 27, 9, 0, 0), "#4caf50", false),
                (29, "Asteroid Tracking Session", "Bermuda", new DateTime(year, 5, 28, 10, 0, 0), new DateTime(year, 5, 28, 12, 0, 0), "#ff9800", false),
                (30, "Deep Space Signals Analysis", "Space Centre USA", new DateTime(year, 5, 28, 16, 30, 0), new DateTime(year, 5, 28, 17, 30, 0), "#3f51b5", false),
                (31, "Comet Observation", "Himalayas", new DateTime(year, 5, 25, 13, 30, 0), new DateTime(year, 5, 25, 15, 0, 0), "#795548", false),
                (32, "Space Debris Monitoring", "Space Centre USA", new DateTime(year, 5, 26, 14, 0, 0), new DateTime(year, 5, 26, 16, 0, 0), "#009688", false),
                (33, "Planetary Alignment Study", "Alaska", new DateTime(year, 5, 27, 8, 0, 0), new DateTime(year, 5, 27, 10, 0, 0), "#673ab7", false),
                (34, "Interstellar Research Briefing", "Space Centre USA", new DateTime(year, 5, 28, 9, 0, 0), new DateTime(year, 5, 28, 11, 30, 0), "#e91e63", false),
                (35, "Thule Air Crash Report", "Newyork City", new DateTime(year, 5, 25, 11, 0, 0), new DateTime(year, 5, 25, 13, 0, 0), "#33c1ff", false),
            };

            foreach (var entry in appointments)
            {
                this.Events.Add(new SchedulerAppointment()
                {
                    StartTime = entry.Start,
                    EndTime = entry.End,
                    Subject = entry.Subject,
                    Location = entry.Location,
                    IsAllDay = entry.IsAllDay,
                    Background = GetBrushFromHex(entry.Color)
                });
            }
        }

        /// <summary>
        /// Converts a CSS-style hex string (e.g. #1aaa55) to a SolidColorBrush.
        /// </summary>
        /// <param name="hex">The hex code including the leading # symbol.</param>
        /// <returns>A SolidColorBrush matching the supplied hex value.</returns>
        private static Brush GetBrushFromHex(string hex)
        {
            return new SolidColorBrush(Color.FromArgb(hex));
        }

        #endregion
    }
}