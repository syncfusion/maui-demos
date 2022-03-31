#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.SfScheduler
{
    public class Meeting : INotifyPropertyChanged
    {
        private DateTime from, to;
        private string eventName;
        private bool isAllDay;
        private TimeZoneInfo startTimeZone, endTimeZone;
        private Brush background;
        private ObservableCollection<DateTime> recurrenceExceptionDates;
        private string rRUle;
        private string notes;
        private string location;
        private object? recurrenceId;
        private object? id;

        public Meeting()
        {
            this.from = DateTime.Now;
            this.to = DateTime.Now;
            this.eventName = string.Empty;
            this.isAllDay = false;
            this.background = Brush.Transparent;
            this.rRUle = string.Empty;
            this.notes = string.Empty;
            this.location = string.Empty;
            this.startTimeZone = TimeZoneInfo.Local;
            this.endTimeZone = TimeZoneInfo.Local;
            this.recurrenceExceptionDates = new ObservableCollection<DateTime>();
        }

        /// <summary>
        /// Property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets or sets the value to display the start date.
        /// </summary>
        public DateTime From
        {
            get { return from; }
            set
            {
                from = value;
                this.RaiseOnPropertyChanged(nameof(From));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the end date.
        /// </summary>
        public DateTime To
        {
            get { return to; }
            set
            {
                to = value;
                this.RaiseOnPropertyChanged(nameof(To));
            }
        }

        /// <summary>
        /// Gets or sets the value indicating whether the appointment is all-day or not.
        /// </summary>
        public bool IsAllDay
        {
            get { return isAllDay; }
            set
            {
                isAllDay = value;
                this.RaiseOnPropertyChanged(nameof(IsAllDay));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the subject.
        /// </summary>
        public string EventName
        {
            get { return eventName; }
            set
            {
                eventName = value;
                this.RaiseOnPropertyChanged(nameof(EventName));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the notes.
        /// </summary>
        public string Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                this.RaiseOnPropertyChanged(nameof(Notes));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the start time zone.
        /// </summary>
        public TimeZoneInfo StartTimeZone
        {
            get { return startTimeZone; }
            set
            {
                startTimeZone = value;
                this.RaiseOnPropertyChanged(nameof(StartTimeZone));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the end time zone.
        /// </summary>
        public TimeZoneInfo EndTimeZone
        {
            get { return endTimeZone; }
            set
            {
                endTimeZone = value;
                this.RaiseOnPropertyChanged(nameof(EndTimeZone));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the background.
        /// </summary>
        public Brush Background
        {
            get { return background; }
            set
            {
                background = value;
                this.RaiseOnPropertyChanged(nameof(Background));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the recurrence id.
        /// </summary>
        public object? RecurrenceId
        {
            get { return recurrenceId; }
            set
            {
                recurrenceId = value;
                this.RaiseOnPropertyChanged(nameof(RecurrenceId));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the id.
        /// </summary>
        public object? Id
        {
            get { return id; }
            set
            {
                id = value;
                this.RaiseOnPropertyChanged(nameof(Id));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the RRule.
        /// </summary>
        public string RecurrenceRule
        {
            get { return rRUle; }
            set
            {
                rRUle = value;
                this.RaiseOnPropertyChanged(nameof(RecurrenceRule));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the location.
        /// </summary>
        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                this.RaiseOnPropertyChanged(nameof(Location));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the recurrence exceptions.
        /// </summary>
        public ObservableCollection<DateTime> RecurrenceExceptions
        {
            get { return recurrenceExceptionDates; }
            set
            {
                recurrenceExceptionDates = value;
                this.RaiseOnPropertyChanged(nameof(RecurrenceExceptions));
            }
        }

        /// <summary>
        /// Invoke method when property changed.
        /// </summary>
        /// <param name="propertyName">property name</param>
        private void RaiseOnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}