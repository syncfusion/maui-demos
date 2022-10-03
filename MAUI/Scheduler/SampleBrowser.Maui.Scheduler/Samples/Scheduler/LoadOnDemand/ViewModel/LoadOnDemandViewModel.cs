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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SampleBrowser.Maui.Scheduler.SfScheduler;

/// <summary>
/// Load on demand view model.
/// </summary>
public class LoadOnDemandViewModel : INotifyPropertyChanged
{
    #region Fields

    private bool showBusyIndicator;
    private ObservableCollection<Meeting>? events;

    #endregion

    #region Properties

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

    /// <summary>
    /// Gets or sets load on demand command.
    /// </summary>
    public ICommand QueryAppointmentsCommand { get; set; }

    /// <summary>
    /// Gets or sets the scheduler view type.
    /// </summary>
    public SchedulerView SchedulerView { get; set; }

    /// <summary>
    /// Gets or sets the event collection.
    /// </summary>
    public ObservableCollection<Meeting>? Events
    {
        get { return events; }
        set
        {
            events = value;
            this.RaiseOnPropertyChanged(nameof(Events));
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the busy indicator.
    /// </summary>
    public bool ShowBusyIndicator
    {
        get { return showBusyIndicator; }
        set
        {
            showBusyIndicator = value;
            this.RaiseOnPropertyChanged(nameof(ShowBusyIndicator));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="LoadOnDemandViewModel" /> class.
    /// </summary>
    public LoadOnDemandViewModel()
    {
        this.SchedulerView = SchedulerView.Week;
        this.DisplayDate = DateTime.Now.Date.AddHours(8).AddMinutes(50);
        this.MinDateTime = DateTime.Now.Date.AddMonths(-5);
        this.MaxDateTime = DateTime.Now.AddMonths(5);
        this.QueryAppointmentsCommand = new Command<object>(LoadMoreAppointments, CanLoadMoreAppointments);
    }

    #endregion

    #region Methods

    private void RaiseOnPropertyChanged(string propertyName)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool CanLoadMoreAppointments(object obj)
    {
        return true;
    }
    private async void LoadMoreAppointments(object obj)
    {
        if (obj == null || !(obj is SchedulerQueryAppointmentsEventArgs))
        {
            return;
        }

        this.ShowBusyIndicator = true;
        await Task.Delay(1500);
        var eventCollection = this.GenerateSchedulerAppointments(((SchedulerQueryAppointmentsEventArgs)obj).VisibleDates);
        if (this.SchedulerView != SchedulerView.Agenda)
        {
            this.Events = eventCollection;
        }
        else
        {
            foreach (var meeting in eventCollection)
            {
                this.Events?.Add(meeting);
            }
        }

        this.ShowBusyIndicator = false;
    }

    /// <summary>
    /// Method to generate scheduler appointments based on current visible date range.
    /// </summary>
    /// <param name="visibleDates">Current visible date range.</param>
    private ObservableCollection<Meeting> GenerateSchedulerAppointments(List<DateTime> visibleDates)
    {
        var brush = new ObservableCollection<Brush>
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

        var subjects = new ObservableCollection<string>
        {
            "Business Meeting",
            "Conference",
            "Medical check up",
            "Performance Check",
            "Consulting",
            "Project Status Discussion",
            "Client Meeting",
            "General Meeting",
            "Yoga Therapy",
            "GoToMeeting",
            "Plan Execution",
            "Project Plan"
        };

        Random ran = new();
        int daysCount = visibleDates.Count;
        DateTime visibleStartDate = visibleDates.FirstOrDefault();
        var appointments = new ObservableCollection<Meeting>();
        for (int i = 0; i < 10; i++)
        {
            var startTime = visibleStartDate.AddDays(ran.Next(0, daysCount + 1)).AddHours(ran.Next(9, 16));
            Meeting meeting = new();
            meeting.From = startTime;
            meeting.To = startTime.AddHours(1);
            meeting.EventName = subjects[ran.Next(0, subjects.Count)];
            meeting.Background = brush[ran.Next(0, brush.Count)];
            appointments.Add(meeting);
        }

        return appointments;
    }

    #endregion
}