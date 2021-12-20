#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace SampleBrowser.Maui.SfScheduler
{
    internal static class AppointmentHelper
    {
        internal static int GetTimeZoneIndex(TimeZoneInfo timeZone)
        {
            if (timeZone == null || timeZone == TimeZoneInfo.Local)
            {
                return 0;
            }

            for (int i = 0; i < TimeZoneList.Count; i++)
            {
                if (timeZone.Id.Equals(TimeZoneList[i]))
                {
                    return i;
                }
            }

            return 0;
        }

        /// <summary>
        /// Method to get the color index.
        /// </summary>
        /// <param name="background">The background color.</param>
        /// <returns>The color index.</returns>
        internal static int GetColorIndex(Brush background)
        {
            var selectedAppointmentbackground = background as SolidColorBrush;
            int i = 0;
            foreach (var backgroundBrush in BackgroundCollection.Values)
            {
                if (selectedAppointmentbackground.Color.Equals((backgroundBrush as SolidColorBrush).Color))
                {
                    return i;
                }

                i++;
            }

            return 0;
        }

        /// <summary>
        /// Method to get the selected recurrence properties.
        /// </summary>
        /// <returns>The scheduler recurrence properties.</returns>
        internal static SchedulerRecurrenceInfo GetRecurrenceInfo(int selectedIndex, DateTime selectedDate)
        {
            var recInfo = new SchedulerRecurrenceInfo();
            recInfo.Interval = 1;

            if (selectedIndex == 1)
            {
                recInfo.RecurrenceType = SchedulerRecurrenceType.Daily;
            }
            else if (selectedIndex == 2)
            {
                recInfo.RecurrenceType = SchedulerRecurrenceType.Weekly;
                recInfo.WeekDays = (SchedulerWeekDays)Enum.Parse(typeof(SchedulerWeekDays), ((DayOfWeek)selectedDate.DayOfWeek).ToString());
            }
            else if (selectedIndex == 3)
            {
                recInfo.RecurrenceType = SchedulerRecurrenceType.Monthly;
                recInfo.DayOfMonth = selectedDate.Day;
            }
            else if (selectedIndex == 4)
            {
                recInfo.RecurrenceType = SchedulerRecurrenceType.Yearly;
                recInfo.Month = selectedDate.Month;
                recInfo.DayOfMonth = selectedDate.Day;
            }

            return recInfo;
        }

        /// <summary>
        /// Clone appointment.
        /// </summary>
        /// <param name="parentAppointment">Appointment need to be cloned.</param>
        /// <returns>Cloned appointment.</returns>
        internal static Meeting CloneAppointment(Meeting parentAppointment)
        {
            return new Meeting
            {
                From = parentAppointment.From,
                To = parentAppointment.To,
                Background = parentAppointment.Background,
                IsAllDay = parentAppointment.IsAllDay,
                EventName = parentAppointment.EventName,
                Id = parentAppointment.Id,
                StartTimeZone = parentAppointment.StartTimeZone,
                EndTimeZone = parentAppointment.EndTimeZone,
                RecurrenceExceptions = parentAppointment.RecurrenceExceptions,
                Notes = parentAppointment.Notes,
                RecurrenceId = parentAppointment.RecurrenceId,
                RecurrenceRule = parentAppointment.RecurrenceRule,
            };
        }

        /// <summary>
        /// Gets or sets time zone list.
        /// </summary>
        internal static IList TimeZoneList { get; set; } = new List<string>()
        {
                "Default",
                "AUS Central Standard Time",
                "AUS Eastern Standard Time",
                "Afghanistan Standard Time",
                "Alaskan Standard Time",
                "Arab Standard Time",
                "Arabian Standard Time",
                "Arabic Standard Time",
                "Argentina Standard Time",
                "Atlantic Standard Time",
                "Azerbaijan Standard Time",
                "Azores Standard Time",
                "Bahia Standard Time",
                "Bangladesh Standard Time",
                "Belarus Standard Time",
                "Canada Central Standard Time",
                "Cape Verde Standard Time",
                "Caucasus Standard Time",
                "Cen. Australia Standard Time",
                "Central America Standard Time",
                "Central Asia Standard Time",
                "Central Brazilian Standard Time",
                "Central Europe Standard Time",
                "Central European Standard Time",
                "Central Pacific Standard Time",
                "Central Standard Time",
                "China Standard Time",
                "Dateline Standard Time",
                "E. Africa Standard Time",
                "E. Australia Standard Time",
                "E. South America Standard Time",
                "Eastern Standard Time",
                "Egypt Standard Time",
                "Ekaterinburg Standard Time",
                "FLE Standard Time",
                "Fiji Standard Time",
                "GMT Standard Time",
                "GTB Standard Time",
                "Georgian Standard Time",
                "Greenland Standard Time",
                "Greenwich Standard Time",
                "Hawaiian Standard Time",
                "India Standard Time",
                "Iran Standard Time",
                "Israel Standard Time",
                "Jordan Standard Time",
                "Kaliningrad Standard Time",
                "Korea Standard Time",
                "Libya Standard Time",
                "Line Islands Standard Time",
                "Magadan Standard Time",
                "Mauritius Standard Time",
                "Middle East Standard Time",
                "Montevideo Standard Time",
                "Morocco Standard Time",
                "Mountain Standard Time",
                "Mountain Standard Time (Mexico)",
                "Myanmar Standard Time",
                "N. Central Asia Standard Time",
                "Namibia Standard Time",
                "Nepal Standard Time",
                "New Zealand Standard Time",
                "Newfoundland Standard Time",
                "North Asia East Standard Time",
                "North Asia Standard Time",
                "Pacific SA Standard Time",
                "Pacific Standard Time",
                "Pacific Standard Time (Mexico)",
                "Pakistan Standard Time",
                "Paraguay Standard Time",
                "Romance Standard Time",
                "Russia Time Zone 10",
                "Russia Time Zone 11",
                "Russia Time Zone 3",
                "Russian Standard Time",
                "SA Eastern Standard Time",
                "SA Pacific Standard Time",
                "SA Western Standard Time",
                "SE Asia Standard Time",
                "Samoa Standard Time",
                "Singapore Standard Time",
                "South Africa Standard Time",
                "Sri Lanka Standard Time",
                "Syria Standard Time",
                "Taipei Standard Time",
                "Tasmania Standard Time",
                "Tokyo Standard Time",
                "Tonga Standard Time",
                "Turkey Standard Time",
                "US Eastern Standard Time",
                "US Mountain Standard Time",
                "UTC",
                "UTC+12",
                "UTC-02",
                "UTC-11",
                "Ulaanbaatar Standard Time",
                "Venezuela Standard Time",
                "Vladivostok Standard Time",
                "W. Australia Standard Time",
                "W. Central Africa Standard Time",
                "W. Europe Standard Time",
                "West Asia Standard Time",
                "West Pacific Standard Time",
                 "Yakutsk Standard Time"
        };

        /// <summary>
        /// Gets or sets recurrence type colloection..
        /// </summary>
        internal static IList RecurrenceTypesCollection { get; set; } = new List<string>()
        {
#if ANDROID || IOS
                 "Day",
                 "Week",
                 "Month",
                 "Year" ,
#else
                "Never",
                "Daily",
                "Weekly",
               "Monthly",
                "Yearly",
#endif
        };

        /// <summary>
        /// Gets or sets background colloection..
        /// </summary>
        internal static Dictionary<string, Brush> BackgroundCollection { get; set; } = new Dictionary<string, Brush>()
        {
            { "Blue", new SolidColorBrush(Color.FromArgb("#FF3D4FB5")) },
            { "Purple", new SolidColorBrush(Color.FromArgb("#FF8B1FA9")) },
            { "Red", new SolidColorBrush(Color.FromArgb("#FFD20100")) },
            { "Orange", new SolidColorBrush(Color.FromArgb("#FFFC571D")) },
            { "Light Green", new SolidColorBrush(Color.FromArgb("#FF36B37B"))},
            { "Peach",new SolidColorBrush(Color.FromArgb("#FFE47C73"))},
            { "Gray",new SolidColorBrush(Color.FromArgb("#FF636363")) },
            { "Green", new SolidColorBrush(Color.FromArgb("#FF0F8644")) },
            { "Caramel",new SolidColorBrush(Color.FromArgb("#FF85461E"))},
        };
    }
}
