#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
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
    using Syncfusion.Maui.Buttons;
    using Syncfusion.Maui.Inputs;
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
        private SfSwitch? visibleDaysSwitch;

        /// <summary>
        /// The currenttime indicator switch.
        /// </summary>
        private SfSwitch? currentTimeIndicatorSwitch;

        /// <summary>
        /// The weekNumber switch.
        /// </summary>
        private SfSwitch? weekNumberSwitch;

        /// <summary>
        /// The allow view navigation switch
        /// </summary>
        private SfSwitch? allowViewNavigationSwitch;

        /// <summary>
        /// The datePickerButton switch.
        /// </summary>
        private SfSwitch? datePickerButtonSwitch;

        /// <summary>
        /// The navigation direction Picker
        /// </summary>
        private SfComboBox? navigationDirectionPicker;

        /// <summary>
        /// The visible days grid.
        /// </summary>
        private Grid? visibleDaysGrid;

        /// <summary>
        /// The current time indicator grid.
        /// </summary>
        private Grid? currentTimeIndicatorGrid;

        /// <summary>
        /// The navigation direction grid
        /// </summary>
        private Grid? NavigationGrid;

        /// <summary>
        /// The week number grid.
        /// </summary>
        private Grid? weekNumberGrid;

        /// <summary>
        /// The date picker button grid.
        /// </summary>
        private Grid? datePickerButtonGrid;

        /// <summary>
        /// The trailing dates grid.
        /// </summary>
        private Grid? trailingDatesGrid;

        /// <summary>
        /// The trailing dates switch.
        /// </summary>
        private SfSwitch? trailingDatesSwitch;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);

            this.scheduler = bindable.Content.FindByName<SfScheduler>("Scheduler");
            this.visibleDaysSwitch = bindable.FindByName<SfSwitch>("visibleDaysSwitch");
            this.weekNumberSwitch = bindable.Content.FindByName<SfSwitch>("weekNumberSwitch");
            this.currentTimeIndicatorSwitch = bindable.Content.FindByName<SfSwitch>("currentTimeIndicatorSwitch");
            this.allowViewNavigationSwitch = bindable.Content.FindByName<SfSwitch>("allowViewNavigationSwitch");
            this.datePickerButtonSwitch = bindable.Content.FindByName<SfSwitch>("datePickerButtonSwitch");
            this.visibleDaysGrid = bindable.Content.FindByName<Grid>("visibleDaysGrid");
            this.visibleDaysGrid = bindable.Content.FindByName<Grid>("visibleDaysGrid");
            this.currentTimeIndicatorGrid = bindable.Content.FindByName<Grid>("currentTimeIndicatorGrid");
            this.NavigationGrid = bindable.Content.FindByName<Grid>("NavigationGrid");
            this.weekNumberGrid = bindable.Content.FindByName<Grid>("weekNumberGrid");
            this.datePickerButtonGrid = bindable.Content.FindByName<Grid>("datePickerButtonGrid");
            this.trailingDatesGrid = bindable.Content.FindByName<Grid>("trailingDatesGrid");
            this.trailingDatesSwitch = bindable.Content.FindByName<SfSwitch>("trailingDatesSwitch");
            this.navigationDirectionPicker = bindable.Content.FindByName<SfComboBox>("NavigationDirectionPicker");

            if (this.currentTimeIndicatorSwitch != null)
            {
                this.currentTimeIndicatorSwitch.StateChanged += OnCurrentTimeIndicatorSwitchStateChanged;
            }

            if (this.weekNumberSwitch != null)
            {
                this.weekNumberSwitch.StateChanged += OnWeekNumberSwitchStateChanged;
            }

            if (this.trailingDatesSwitch != null)
            {
                this.trailingDatesSwitch.StateChanged += OnTrailingDatesSwitchStateChanged;
            }

            if (this.visibleDaysSwitch != null)
            {
                this.visibleDaysSwitch.StateChanged += OnVisibleDaysSwitchStateChanged;
            }

            if (this.allowViewNavigationSwitch != null)
            {
                this.allowViewNavigationSwitch.StateChanged += OnAllowViewNavigationSwitchStateChanged;
            }

            if (this.datePickerButtonSwitch != null)
            {
                this.datePickerButtonSwitch.StateChanged += OnDatePickerButtonSwitchStateChanged;
            }

            if (this.navigationDirectionPicker != null)
            {
                this.navigationDirectionPicker.SelectionChanged += OnNavigationDirectionChanged;
            }

            if (this.scheduler != null)
            {
                this.scheduler.ViewChanged += OnSchedulerViewChanged;
            }
        }

        /// <summary>
        /// Method for visible days swicth toggle changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void OnVisibleDaysSwitchStateChanged(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.scheduler == null)
            {
                return;
            } 
            
            if (e.NewValue == true)
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
        private void OnSchedulerViewChanged(object? sender, SchedulerViewChangedEventArgs e)
        {
            if (this.NavigationGrid == null)
            {
                return;
            }

            if (e.NewView == SchedulerView.Month)
            {
                NavigationGrid.IsVisible = true;
            }
            else
            {
                NavigationGrid. IsVisible = false;
            }

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

                if (this.datePickerButtonGrid != null)
                {
                    this.datePickerButtonGrid.IsVisible = true;
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
        /// Method for navigation direction picker changed
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void OnNavigationDirectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.scheduler == null || this.navigationDirectionPicker == null)
            {
                return;
            }

            var selectedDirection = this.navigationDirectionPicker.SelectedItem?.ToString();

            if (selectedDirection == "Vertical")
            {
                this.scheduler.MonthView.NavigationDirection = SchedulerMonthNavigationDirection.Vertical;
            }
            else if (selectedDirection == "Horizontal")
            {
                this.scheduler.MonthView.NavigationDirection = SchedulerMonthNavigationDirection.Horizontal;
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

            if (this.datePickerButtonGrid != null)
            {
                this.datePickerButtonGrid.IsVisible = isVisible;
            }
        }

        /// <summary>
        /// Method for allow view navigation swicth toggle changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void OnAllowViewNavigationSwitchStateChanged(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.scheduler != null && e.NewValue.HasValue)
            {
                this.scheduler.AllowViewNavigation = e.NewValue.Value;
            }
        }

        /// <summary>
        /// Method for week numer swicth toggle changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void OnWeekNumberSwitchStateChanged(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.scheduler != null && e.NewValue != null)
            {
                this.scheduler.ShowWeekNumber = e.NewValue.Value;
            }
        }

        /// <summary>
        /// Method for date picker button switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object</param>
        /// <param name="e">Event Args</param>
        private void OnDatePickerButtonSwitchStateChanged(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.scheduler !=null && e.NewValue != null)
            {
                this.scheduler.ShowDatePickerButton = e.NewValue.Value;
            }
        }

        /// <summary>
        /// Method for trailing dates swicth toggle changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void OnTrailingDatesSwitchStateChanged(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.scheduler != null && e.NewValue != null)
            {
                this.scheduler.MonthView.ShowLeadingAndTrailingDates = e.NewValue.Value;
            }
        }

        /// <summary>
        /// Method for current timeindicator swicth toggle changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void OnCurrentTimeIndicatorSwitchStateChanged(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.scheduler != null && e.NewValue != null)
            {
                this.scheduler.DaysView.ShowCurrentTimeIndicator = e.NewValue.Value;
                this.scheduler.TimelineView.ShowCurrentTimeIndicator = e.NewValue.Value;
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
                this.weekNumberSwitch.StateChanged -= OnWeekNumberSwitchStateChanged;
                this.weekNumberSwitch = null;
            }

            if(this.datePickerButtonGrid != null)
            {
                this.datePickerButtonGrid = null;
            }

            if (this.trailingDatesGrid != null)
            {
                this.trailingDatesGrid = null;
            }

            if (this.trailingDatesSwitch != null)
            {
                this.trailingDatesSwitch.StateChanged -= OnTrailingDatesSwitchStateChanged;
                this.trailingDatesSwitch = null;
            }

            if (this.visibleDaysSwitch != null)
            {
                this.visibleDaysSwitch.StateChanged -= OnVisibleDaysSwitchStateChanged;
                this.visibleDaysSwitch = null;
            }

            if (this.currentTimeIndicatorSwitch != null)
            {
                this.currentTimeIndicatorSwitch.StateChanged -= OnCurrentTimeIndicatorSwitchStateChanged;
                this.currentTimeIndicatorSwitch = null;
            }

            if (this.allowViewNavigationSwitch != null)
            {
                this.allowViewNavigationSwitch.StateChanged -= OnAllowViewNavigationSwitchStateChanged;
                this.allowViewNavigationSwitch = null;
            }

            if (this.navigationDirectionPicker != null)
            {
                this.navigationDirectionPicker.SelectionChanged -= OnNavigationDirectionChanged;
                this.navigationDirectionPicker = null;
            }

            if (this.scheduler != null)
            {
                this.scheduler.ViewChanged -= OnSchedulerViewChanged;
                this.scheduler = null;
            }

            if (this.datePickerButtonSwitch != null)
            {
                this.datePickerButtonSwitch.StateChanged -= OnDatePickerButtonSwitchStateChanged;
                this.datePickerButtonSwitch = null;
            }
        }
    }
}