#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
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
    public class Meeting : INotifyPropertyChanged
    {
        #region Fields

        private DateTime from, to;
        private bool isAllDay;
        private string eventName;
        private string notes;
        private TimeZoneInfo startTimeZone;
        private TimeZoneInfo endTimeZone;
        private Brush background;
        private object? recurrenceId;
        private object? id;
        private string recurrenceRule;
        private string location;
        private ObservableCollection<DateTime> recurrenceExceptions;
        private ObservableCollection<object> resources;

        #endregion

        #region Constructor

        public Meeting()
        {
            this.from = DateTime.Now;
            this.to = DateTime.Now;
            this.eventName = string.Empty;
            this.isAllDay = false;
            this.background = Brush.Transparent;
            this.recurrenceRule = string.Empty;
            this.notes = string.Empty;
            this.location = string.Empty;
            this.startTimeZone = TimeZoneInfo.Local;
            this.endTimeZone = TimeZoneInfo.Local;
            this.recurrenceExceptions = new ObservableCollection<DateTime>();
            this.resources = new ObservableCollection<object>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the value to display the start date.
        /// </summary>
        public DateTime From
        {
            get { return this.from; }
            set
            {
                this.from = value;
                this.OnPropertyChanged(nameof(From));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the end date.
        /// </summary>
        public DateTime To
        {
            get { return this.to; }
            set
            {
                this.to = value;
                this.OnPropertyChanged(nameof(To));
            }
        }

        /// <summary>
        /// Gets or sets the value indicating whether the appointment is all-day or not.
        /// </summary>
        public bool IsAllDay
        {
            get { return this.isAllDay; }
            set
            {
                this.isAllDay = value;
                this.OnPropertyChanged(nameof(IsAllDay));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the subject.
        /// </summary>
        public string EventName
        {
            get { return this.eventName; }
            set
            {
                this.eventName = value;
                this.OnPropertyChanged(nameof(EventName));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the notes.
        /// </summary>
        public string Notes
        {
            get { return this.notes; }
            set
            {
                this.notes = value;
                this.OnPropertyChanged(nameof(Notes));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the start time zone.
        /// </summary>
        public TimeZoneInfo StartTimeZone
        {
            get { return this.startTimeZone; }
            set
            {
                this.startTimeZone = value;
                this.OnPropertyChanged(nameof(StartTimeZone));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the end time zone.
        /// </summary>
        public TimeZoneInfo EndTimeZone
        {
            get { return this.endTimeZone; }
            set
            {
                this.endTimeZone = value;
                this.OnPropertyChanged(nameof(EndTimeZone));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the background.
        /// </summary>
        public Brush Background
        {
            get { return this.background; }
            set
            {
                this.background = value;
                this.OnPropertyChanged(nameof(Background));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the recurrence id.
        /// </summary>
        public object? RecurrenceId
        {
            get { return this.recurrenceId; }
            set
            {
                this.recurrenceId = value;
                this.OnPropertyChanged(nameof(RecurrenceId));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the id.
        /// </summary>
        public object? Id
        {
            get { return this.id; }
            set
            {
                this.id = value;
                this.OnPropertyChanged(nameof(Id));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the RRule.
        /// </summary>
        public string RecurrenceRule
        {
            get { return this.recurrenceRule; }
            set
            {
                this.recurrenceRule = value;
                this.OnPropertyChanged(nameof(RecurrenceRule));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the location.
        /// </summary>
        public string Location
        {
            get { return this.location; }
            set
            {
                this.location = value;
                this.OnPropertyChanged(nameof(Location));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the recurrence exceptions.
        /// </summary>
        public ObservableCollection<DateTime> RecurrenceExceptions
        {
            get { return this.recurrenceExceptions; }
            set
            {
                this.recurrenceExceptions = value;
                this.OnPropertyChanged(nameof(RecurrenceExceptions));
            }
        }

        /// <summary>
        /// Gets or sets the value to display the resource collection.
        /// </summary>
        public ObservableCollection<object> Resources
        {
            get { return this.resources; }
            set
            {
                this.resources = value;
                this.OnPropertyChanged(nameof(Resources));
            }
        }

        #endregion

        #region event

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Invokes the event when the value of a property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}