#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    public class SchedulerHelper
    {
        public static void AddSpanAppointments(ObservableCollection<Meeting>? events, ObservableCollection<string> subjects, List<Brush> colors)
        {
            DateTime today = DateTime.Now;
            Random random = new Random();

            //// Adding Span appointments
            Meeting meeting = new Meeting();
            meeting.From = today.Date.AddDays(1);
            meeting.To = today.Date.AddDays(8);
            meeting.EventName = subjects[random.Next(8)];
            meeting.Background = colors[random.Next(9)];

            Meeting planning = new Meeting();
            planning.EventName = subjects[random.Next(8)];
            planning.From = today.Date.AddDays(-6);
            planning.To = today.Date;
            planning.Background = colors[random.Next(9)];

            Meeting consulting = new Meeting();
            consulting.EventName = subjects[random.Next(8)];
            consulting.From = today.Date.AddHours(23);
            consulting.To = today.Date.AddDays(1).AddHours(2);
            consulting.IsAllDay = false;
            consulting.Background = colors[random.Next(9)];

            events?.Add(meeting);
            events?.Add(planning);
            events?.Add(consulting);
        }

        /// <summary>
        /// Method to get the subjects
        /// </summary>
        /// <returns>Returns the subjects collection</returns>
        public static List<string> GetSubjects()
        {
             List<string> subjects = new List<string>();
            subjects.AddRange(new List<string>()
            {
                "General Meeting",
                "Plan Execution",
                "Project Plan",
                "Consulting",
                "Performance Check",
                "Support",
                "Development Meeting",
                "Scrum",
                "Project Completion",
                "Release updates",
                "Performance Check"
            });

            return subjects;
        }

        /// <summary>
        /// Method to get the colrs
        /// </summary>
        /// <returns>Returns the colors collection</returns>
        public static List<Brush> GetColors()
        {
            List<Brush> colors = new List<Brush>();
            colors.AddRange(new List<Brush>()
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
            });
            return colors;
        }
    }
}
