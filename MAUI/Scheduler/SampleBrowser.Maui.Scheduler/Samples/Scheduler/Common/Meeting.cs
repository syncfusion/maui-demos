#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    public class Meeting
    {
        public Meeting()
        {
            this.From = DateTime.Now;
            this.To = DateTime.Now;
            this.EventName = string.Empty;
            this.IsAllDay = false;
            this.Background = Brush.Transparent;
            this.RecurrenceRule = string.Empty;
            this.Notes = string.Empty;
            this.Location = string.Empty;
            this.StartTimeZone = TimeZoneInfo.Local;
            this.EndTimeZone = TimeZoneInfo.Local;
            this.RecurrenceExceptions = new ObservableCollection<DateTime>();
            this.Resources = new ObservableCollection<object>();
        }

        /// <summary>
        /// Gets or sets the value to display the start date.
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// Gets or sets the value to display the end date.
        /// </summary>
        public DateTime To { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether the appointment is all-day or not.
        /// </summary>
        public bool IsAllDay { get; set; }

        /// <summary>
        /// Gets or sets the value to display the subject.
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets the value to display the notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the value to display the start time zone.
        /// </summary>
        public TimeZoneInfo StartTimeZone { get; set; }

        /// <summary>
        /// Gets or sets the value to display the end time zone.
        /// </summary>
        public TimeZoneInfo EndTimeZone { get; set; }

        /// <summary>
        /// Gets or sets the value to display the background.
        /// </summary>
        public Brush Background { get; set; }

        /// <summary>
        /// Gets or sets the value to display the recurrence id.
        /// </summary>
        public object? RecurrenceId { get; set; }

        /// <summary>
        /// Gets or sets the value to display the id.
        /// </summary>
        public object? Id { get; set; }

        /// <summary>
        /// Gets or sets the value to display the RRule.
        /// </summary>
        public string RecurrenceRule { get; set; }

        /// <summary>
        /// Gets or sets the value to display the location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the value to display the recurrence exceptions.
        /// </summary>
        public ObservableCollection<DateTime> RecurrenceExceptions { get; set; }

        /// <summary>
        /// Gets or sets the value to display the resource collection.
        /// </summary>
        public ObservableCollection<object> Resources { get; set; }
    }
}