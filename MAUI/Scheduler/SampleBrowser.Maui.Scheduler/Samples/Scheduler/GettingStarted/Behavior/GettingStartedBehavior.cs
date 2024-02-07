#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    using System;
    using Microsoft.Maui.Controls;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Scheduler;

    /// <summary>
    /// Getting started Behavior class
    /// </summary>
    internal class GettingStartedBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// schedule initialize
        /// </summary>
        private SfScheduler? scheduler;

        /// <summary>
        /// The visible days switch.
        /// </summary>
        private Switch? visibleDaysSwitch;

        /// <summary>
        /// The currenttime indicator switch.
        /// </summary>
        private Switch? currentTimeIndicatorSwitch;

        /// <summary>
        /// The weekNumber switch.
        /// </summary>
        private Switch? weekNumberSwitch;

        /// <summary>
        /// The allow view navigation switch
        /// </summary>
        private Switch? allowViewNavigationSwitch;

        /// <summary>
        /// The visible days grid.
        /// </summary>
        private Grid? visibleDaysGrid;

        /// <summary>
        /// The current time indicator grid.
        /// </summary>
        private Grid? currentTimeIndicatorGrid;

        /// <summary>
        /// The week number grid.
        /// </summary>
        private Grid? weekNumberGrid;

        /// <summary>
        /// The trailing dates grid.
        /// </summary>
        private Grid? trailingDatesGrid;

        /// <summary>
        /// The trailing dates switch.
        /// </summary>
        private Switch? trailingDatesSwitch;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);

            this.scheduler = bindable.Content.FindByName<SfScheduler>("Scheduler");
            this.visibleDaysSwitch = bindable.FindByName<Switch>("visibleDaysSwitch");
            this.weekNumberSwitch = bindable.Content.FindByName<Switch>("weekNumberSwitch");
            this.currentTimeIndicatorSwitch = bindable.Content.FindByName<Switch>("currentTimeIndicatorSwitch");
            this.allowViewNavigationSwitch = bindable.Content.FindByName<Switch>("allowViewNavigationSwitch");
            this.visibleDaysGrid = bindable.Content.FindByName<Grid>("visibleDaysGrid");
            this.currentTimeIndicatorGrid = bindable.Content.FindByName<Grid>("currentTimeIndicatorGrid");
            this.weekNumberGrid = bindable.Content.FindByName<Grid>("weekNumberGrid");
            this.trailingDatesGrid = bindable.Content.FindByName<Grid>("trailingDatesGrid");
            this.trailingDatesSwitch = bindable.Content.FindByName<Switch>("trailingDatesSwitch");

            if (this.currentTimeIndicatorSwitch != null)
            {
                this.currentTimeIndicatorSwitch.Toggled += CurrentTimeIndicatorSwitch_Toggled;
            }

            if (this.weekNumberSwitch != null)
            {
                this.weekNumberSwitch.Toggled += WeekNumberSwitch_Toggled;
            }

            if (this.trailingDatesSwitch != null)
            {
                this.trailingDatesSwitch.Toggled += TrailingDatesSwitch_Toggled;
            }

            if (this.visibleDaysSwitch != null)
            {
                this.visibleDaysSwitch.Toggled += VisibleDaysSwitch_Toggled;
            }

            if (this.allowViewNavigationSwitch != null)
            {
                this.allowViewNavigationSwitch.Toggled += AllowViewNavigationSwitch_Toggled;
            }

            if (this.scheduler != null)
            {
                this.scheduler.ViewChanged += Scheduler_ViewChanged;
            }
        }

        /// <summary>
        /// Method for visible days swicth toggle changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void VisibleDaysSwitch_Toggled(object? sender, ToggledEventArgs e)
        {
            if (this.scheduler == null)
            {
                return;
            } 
            
            if (e.Value)
            {
                this.scheduler.DaysView.NumberOfVisibleDays = 3;
                this.scheduler.TimelineView.NumberOfVisibleDays = 3;
            }
            else
            {
                this.scheduler.DaysView.NumberOfVisibleDays = -1;
                this.scheduler.TimelineView.NumberOfVisibleDays = -1;
            }
        }

        /// <summary>
        /// Method for scheduler view changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void Scheduler_ViewChanged(object? sender, SchedulerViewChangedEventArgs e)
        {
            if (e.NewView == SchedulerView.Month)
            {
                if (this.visibleDaysGrid != null)
                {
                    this.visibleDaysGrid.IsVisible = false;
                }

                if (this.currentTimeIndicatorGrid != null)
                {
                    this.currentTimeIndicatorGrid.IsVisible = false;
                }

                if (this.weekNumberGrid != null)
                {
                    this.weekNumberGrid.IsVisible = true;
                }

                if (this.trailingDatesGrid != null)
                {
                    this.trailingDatesGrid.IsVisible = true;
                }
            }
            else if (e.NewView == SchedulerView.Day ||
                e.NewView == SchedulerView.Week ||
                e.NewView == SchedulerView.WorkWeek ||
                e.NewView == SchedulerView.TimelineDay ||
                e.NewView == SchedulerView.TimelineWeek ||
                e.NewView == SchedulerView.TimelineWorkWeek)
            {
                this.IsVisiblePropertyView(true);
            }
            else
            {
                this.IsVisiblePropertyView(false);
            }
        }

        /// <summary>
        /// Method to handle property view visible or not based on the scheduler view.
        /// </summary>
        /// <param name="isVisible">The bool value</param>
        private void IsVisiblePropertyView(bool isVisible)
        {
            if (this.visibleDaysGrid != null)
            {
                this.visibleDaysGrid.IsVisible = isVisible;
            }

            if (this.currentTimeIndicatorGrid != null)
            {
                this.currentTimeIndicatorGrid.IsVisible = isVisible;
            }

            if (this.weekNumberGrid != null)
            {
                this.weekNumberGrid.IsVisible = isVisible;
            }

            if (this.trailingDatesGrid != null)
            {
                this.trailingDatesGrid.IsVisible = false;
            }
        }

        /// <summary>
        /// Method for allow view navigation swicth toggle changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void AllowViewNavigationSwitch_Toggled(object? sender, ToggledEventArgs e)
        {
            if (this.scheduler != null)
            {
                this.scheduler.AllowViewNavigation = e.Value;
            }
        }

        /// <summary>
        /// Method for week numer swicth toggle changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void WeekNumberSwitch_Toggled(object? sender, ToggledEventArgs e)
        {
            if (this.scheduler != null)
            {
                this.scheduler.ShowWeekNumber = e.Value;
            }
        }

        /// <summary>
        /// Method for trailing dates swicth toggle changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void TrailingDatesSwitch_Toggled(object? sender, ToggledEventArgs e)
        {
            if (this.scheduler != null)
            {
                this.scheduler.MonthView.ShowLeadingAndTrailingDates = e.Value;
            }
        }

        /// <summary>
        /// Method for current timeindicator swicth toggle changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void CurrentTimeIndicatorSwitch_Toggled(object? sender, ToggledEventArgs e)
        {
            if (this.scheduler != null)
            {
                this.scheduler.DaysView.ShowCurrentTimeIndicator = e.Value;
                this.scheduler.TimelineView.ShowCurrentTimeIndicator = e.Value;
            }
        }

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);

            if (this.visibleDaysGrid != null)
            {
                this.visibleDaysGrid = null;
            }

            if (this.currentTimeIndicatorGrid != null)
            {
                this.currentTimeIndicatorGrid = null;
            }

            if (this.weekNumberGrid != null)
            {
                this.weekNumberGrid = null;
            }

            if (this.weekNumberSwitch != null)
            {
                this.weekNumberSwitch.Toggled -= WeekNumberSwitch_Toggled;
                this.weekNumberSwitch = null;
            }

            if (this.trailingDatesGrid != null)
            {
                this.trailingDatesGrid = null;
            }

            if (this.trailingDatesSwitch != null)
            {
                this.trailingDatesSwitch.Toggled -= TrailingDatesSwitch_Toggled;
                this.trailingDatesSwitch = null;
            }

            if (this.visibleDaysSwitch != null)
            {
                this.visibleDaysSwitch.Toggled -= VisibleDaysSwitch_Toggled;
                this.visibleDaysSwitch = null;
            }

            if (this.currentTimeIndicatorSwitch != null)
            {
                this.currentTimeIndicatorSwitch.Toggled -= CurrentTimeIndicatorSwitch_Toggled;
                this.currentTimeIndicatorSwitch = null;
            }

            if (this.allowViewNavigationSwitch != null)
            {
                this.allowViewNavigationSwitch.Toggled -= AllowViewNavigationSwitch_Toggled;
                this.allowViewNavigationSwitch = null;
            }

            if (this.scheduler != null)
            {
                this.scheduler.ViewChanged -= Scheduler_ViewChanged;
                this.scheduler = null;
            }
        }
    }
}