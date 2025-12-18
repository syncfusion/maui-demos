#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Buttons;
using Syncfusion.Maui.Calendar;
using Syncfusion.Maui.DataSource.Extensions;
using Syncfusion.Maui.Inputs;
using Syncfusion.Maui.Sliders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SampleBrowser.Maui.LiquidGlass.SfCalendar
{
    internal class CalendarPageBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Calendar view 
        /// </summary>
        private Syncfusion.Maui.Calendar.SfCalendar? calendar;

        /// <summary>
        /// The current time indicator switch.
        /// </summary>
        private Syncfusion.Maui.Buttons.SfSwitch? enableDatesSwitch;

        /// <summary>
        /// The enable swipe selection switch
        /// </summary>
        private Syncfusion.Maui.Buttons.SfSwitch? enableSwipeSelectionSwitch;

        /// <summary>
        /// The weekNumber switch.
        /// </summary>
        private Syncfusion.Maui.Buttons.SfSwitch? weekNumberSwitch;

        /// <summary>
        /// The allow view navigation switch
        /// </summary>
        private Syncfusion.Maui.Buttons.SfSwitch? allowViewNavigationSwitch;

        /// <summary>
        /// The week number grid.
        /// </summary>
        private Grid? weekNumberGrid;

        /// <summary>
        /// The trailing dates switch.
        /// </summary>
        private Syncfusion.Maui.Buttons.SfSwitch? trailingDatesSwitch;

        /// <summary>
        /// The combo box is a text box component that allows users to type a value or choose
        /// an option from the list of predefined options.
        /// </summary>
        private Syncfusion.Maui.Inputs.SfComboBox? comboBox;

        /// <summary>
        /// This combo box is used to choose the selection mode of the calendar
        /// </summary>
        private Syncfusion.Maui.Inputs.SfComboBox? selectionComboBox;

        /// <summary>
        /// This combo box is used to choose the selection shape of the calendar
        /// </summary>
        private Syncfusion.Maui.Inputs.SfComboBox? selectionShapeComboBox;

        /// <summary>
        /// This combo box is used to choose the selection direction of the calendar
        /// </summary>
        private Syncfusion.Maui.Inputs.SfComboBox? directionComboBox;

        /// <summary>
        /// Grid for SelectionDirection, TrailingDates and EnableSwipeSelection
        /// </summary>
        private Grid? selectionDirectionGrid, trailingDatesGrid, enableSwipeSelectionGrid;

        /// <summary>
        /// The datePicker used to choose the display date
        /// </summary>
        private DatePicker? datePicker;

        /// <summary>
        /// The show action buttons switch.
        /// </summary>
        private Syncfusion.Maui.Buttons.SfSwitch? enableActionButtonsSwitch;

        /// <summary>
        /// The show today buttons switch.
        /// </summary>
        private Syncfusion.Maui.Buttons.SfSwitch? enableTodayButtonSwitch;

        /// <summary>
        /// Navigate to adjacent month switch.
        /// </summary>
        private Syncfusion.Maui.Buttons.SfSwitch? navigationToAdjacentMonth;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.calendar = bindable.Content.FindByName<Syncfusion.Maui.Calendar.SfCalendar>("Calendar");
            this.datePicker = bindable.Content.FindByName<DatePicker>("datePicker");
            this.weekNumberSwitch = bindable.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("weekNumberSwitch");
            this.enableDatesSwitch = bindable.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("enableDatesSwitch");
            this.allowViewNavigationSwitch = bindable.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("allowViewNavigationSwitch");
            this.weekNumberGrid = bindable.Content.FindByName<Grid>("weekNumberGrid");

            this.enableSwipeSelectionSwitch = bindable.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("enableSwipeSelectionSwitch");
            this.enableSwipeSelectionGrid = bindable.Content.FindByName<Grid>("enableSwipeSelectionGrid");

            this.trailingDatesGrid = bindable.Content.FindByName<Grid>("trailingDatesGrid");
            this.trailingDatesSwitch = bindable.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("trailingDatesSwitch");

            this.comboBox = bindable.Content.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("comboBox");
            comboBox.ItemsSource = Enum.GetValues(typeof(CalendarView)).ToList<CalendarView>();
            comboBox.SelectedIndex = 0;
            comboBox.SelectionChanged += ComboBox_SelectionChanged;

            this.selectionComboBox = bindable.Content.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("selectionComboBox");
            selectionComboBox.ItemsSource = Enum.GetValues(typeof(CalendarSelectionMode)).ToList<CalendarSelectionMode>();
            selectionComboBox.SelectedIndex = 0;
            selectionComboBox.SelectionChanged += ComboBox_SelectionTypeChanged;

            this.selectionShapeComboBox = bindable.Content.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("selectionShapeComboBox");
            selectionShapeComboBox.ItemsSource = Enum.GetValues(typeof(CalendarSelectionShape)).ToList<CalendarSelectionShape>();
            selectionShapeComboBox.SelectedIndex = 0;
            selectionShapeComboBox.SelectionChanged += ComboBox_SelectionShapeChanged;

            this.selectionDirectionGrid = bindable.Content.FindByName<Grid>("selectionDirectionGrid");
            this.directionComboBox = bindable.Content.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("directionComboBox");
            directionComboBox.ItemsSource = Enum.GetValues(typeof(CalendarRangeSelectionDirection)).ToList<CalendarRangeSelectionDirection>();
            directionComboBox.SelectedIndex = 0;
            directionComboBox.SelectionChanged += DirectionComboBox_SelectionChanged;

            this.enableActionButtonsSwitch = bindable.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("showActionButtonsSwitch");
            this.enableTodayButtonSwitch = bindable.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("showTodayButtonSwitch");
            this.navigationToAdjacentMonth = bindable.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("navigationToAdjacentMonthSwitch");
            this.calendar.FooterView = new CalendarFooterView()
            {
                ShowActionButtons = true,
                ShowTodayButton = true,
            };

            this.calendar.MinimumDate = new DateTime(1900, 01, 01);
            this.calendar.MaximumDate = new DateTime(2100, 12, 31);
            this.calendar.SelectedDate = DateTime.Now.AddDays(-3);
            ObservableCollection<DateTime> selectedDates = new();
            Random random = new();
            for (int i = 0; i < 6; i++)
            {
                selectedDates.Add(new DateTime(DateTime.Now.Year, DateTime.Now.Month, random.Next(1, 28)));
            }

            this.calendar.SelectedDates = selectedDates;
            this.calendar.SelectedDateRange = new CalendarDateRange(DateTime.Now.AddDays(2), DateTime.Now.AddDays(6));
            this.calendar.SelectedDateRanges = new ObservableCollection<CalendarDateRange> { new CalendarDateRange(DateTime.Now.AddDays(2), DateTime.Now.AddDays(6)), new CalendarDateRange(DateTime.Now.AddDays(8), DateTime.Now.AddDays(12)) };

            if (this.enableDatesSwitch != null)
            {
                this.enableDatesSwitch.StateChanged += EnablePastDates_Toggled;
            }

            if (this.enableSwipeSelectionSwitch != null)
            {
                this.enableSwipeSelectionSwitch.StateChanged += EnableSwipeSelection_Toggled;
            }

            if (this.weekNumberSwitch != null)
            {
                this.weekNumberSwitch.StateChanged += WeekNumberSwitch_Toggled;
            }

            if (this.trailingDatesSwitch != null)
            {
                this.trailingDatesSwitch.StateChanged += TrailingDatesSwitch_Toggled;
            }

            if (this.allowViewNavigationSwitch != null)
            {
                this.allowViewNavigationSwitch.StateChanged += AllowViewNavigationSwitch_Toggled;
            }

            if (this.enableActionButtonsSwitch != null)
            {
                this.enableActionButtonsSwitch.StateChanged += EnableActionButtonsSwitch_Toggled;
            }

            if (this.enableTodayButtonSwitch != null)
            {
                this.enableTodayButtonSwitch.StateChanged += EnableTodayButtonSwitch_Toggled;
            }

            if (this.navigationToAdjacentMonth != null)
            {
                this.navigationToAdjacentMonth.StateChanged += NavigationToAdjacentMonth_StateChanged;
            }

            if (this.calendar != null && this.datePicker != null)
            {
                this.calendar.ViewChanged += Calendar_ViewChanged;
                if (calendar.SelectedDate != null)
                    this.datePicker.Date = (DateTime)calendar.SelectedDate;
                this.datePicker.DateSelected += DatePicker_DateSelected;
            }
        }

        /// <summary>
        /// Method for Display date selected in DatePicker is changed.
        /// </summary>
        /// <param name="sender">Return the object</param>
        /// <param name="e">Event Arguments</param>
        private void DatePicker_DateSelected(object? sender, DateChangedEventArgs e)
        {
            if (this.calendar != null && e.NewDate != null)
            {
                this.calendar!.DisplayDate = e.NewDate.Value;
            }

            this.datePicker!.Date = e.NewDate;
        }

        /// <summary>
        /// Method for Combo box selection direction changed.
        /// </summary>
        /// <param name="sender">Return the object</param>
        /// <param name="e">Event Arguments</param>
        private void DirectionComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (calendar != null && e.AddedItems != null)
            {
                string? selectionDirection = e.AddedItems[0].ToString();
                if (selectionDirection != null)
                    calendar.RangeSelectionDirection = Enum.Parse<CalendarRangeSelectionDirection>(selectionDirection);
            }
        }

        /// <summary>
        /// Method for Calendar view Changed.
        /// </summary>
        /// <param name="sender">Return the object</param>
        /// <param name="e">Event Arguments</param>
        private void Calendar_ViewChanged(object? sender, CalendarViewChangedEventArgs e)
        {
            if (this.weekNumberGrid != null)
            {
                this.weekNumberGrid.IsVisible = e.NewView == CalendarView.Month;
            }

            if (this.trailingDatesGrid != null)
            {
                this.trailingDatesGrid.IsVisible = e.NewView != CalendarView.Year;
            }

            if (this.comboBox != null && e.OldView != e.NewView)
            {
                this.comboBox.SelectedIndex = Enum.GetValues(typeof(CalendarView)).ToList<CalendarView>().ToList().IndexOf(e.NewView);
            }

            if (this.datePicker != null && this.calendar != null)
            {
                DateTime visibleStartDate = e.NewVisibleDates[0];
                DateTime visibleEndDate = e.NewVisibleDates[e.NewVisibleDates.Count - 1];
                if (this.calendar.View == CalendarView.Month)
                {
                    visibleStartDate = e.NewVisibleDates[e.NewVisibleDates.Count / 2];
                    visibleStartDate = new DateTime(visibleStartDate.Year, visibleStartDate.Month, 1);
                    int days = DateTime.DaysInMonth(visibleStartDate.Year, visibleStartDate.Month);
                    visibleEndDate = new DateTime(visibleStartDate.Year, visibleStartDate.Month, days);
                }
                else if (this.calendar.View != CalendarView.Year)
                {
                    visibleEndDate = e.NewVisibleDates[e.NewVisibleDates.Count - 3];
                }

                if (this.datePicker.Date >= visibleStartDate.Date && this.datePicker.Date <= visibleEndDate.Date)
                {
                    return;
                }

                this.datePicker.Date = visibleStartDate.Date;
            }
        }

        /// <summary>
        /// Method for Combo box calendar selection mode changed.
        /// </summary>
        /// <param name="sender">Return the object</param>
        /// <param name="e">Event Arguments</param>
        private void ComboBox_SelectionTypeChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (calendar != null && e.AddedItems != null)
            {
                string? selection = e.AddedItems[0].ToString();
                if (selection != null)
                    calendar.SelectionMode = Enum.Parse<CalendarSelectionMode>(selection);

                if (calendar.SelectionMode == CalendarSelectionMode.Range)
                {
                    if (this.selectionDirectionGrid != null)
                    {
                        this.selectionDirectionGrid.IsVisible = true;
                    }

                    if (this.enableSwipeSelectionGrid != null)
                    {
                        this.enableSwipeSelectionGrid.IsVisible = true;
                    }
                }
                else if (calendar.SelectionMode == CalendarSelectionMode.MultiRange)
                {
                    if (this.selectionDirectionGrid != null)
                    {
                        this.selectionDirectionGrid.IsVisible = false;
                    }

                    if (this.enableSwipeSelectionGrid != null)
                    {
                        this.enableSwipeSelectionGrid.IsVisible = true;
                    }
                }
                else
                {
                    if (this.selectionDirectionGrid != null)
                    {
                        this.selectionDirectionGrid.IsVisible = false;
                    }

                    if (this.enableSwipeSelectionGrid != null)
                    {
                        this.enableSwipeSelectionGrid.IsVisible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Method for Combo box selection shape changed.
        /// </summary>
        /// <param name="sender">Return the object</param>
        /// <param name="e">Event Arguments</param>
        private void ComboBox_SelectionShapeChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (calendar != null && e.AddedItems != null)
            {
                string? shape = e.AddedItems[0].ToString();
                if (shape != null)
                    calendar.SelectionShape = Enum.Parse<CalendarSelectionShape>(shape);
            }
        }

        /// <summary>
        /// Method for Combo box selection view changed.
        /// </summary>
        /// <param name="sender">Return the object</param>
        /// <param name="e">Event Arguments</param>
        private void ComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (calendar != null && e.AddedItems != null)
            {
                string? view = e.AddedItems[0].ToString();
                if (view != null)
                    calendar.View = Enum.Parse<CalendarView>(view);

                if (this.trailingDatesGrid != null)
                {
                    this.trailingDatesGrid.IsVisible = calendar.View != CalendarView.Year;
                }

                if (this.weekNumberGrid != null)
                {
                    this.weekNumberGrid.IsVisible = calendar.View == CalendarView.Month;
                }
            }
        }

        /// <summary>
        /// Method for allow view navigation switch toggle changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Arguments</param>
        private void AllowViewNavigationSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.calendar != null && e.NewValue.HasValue)
            {
                this.calendar.AllowViewNavigation = e.NewValue.Value;
            }
        }

        /// <summary>
        /// Method for week number switch toggle changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Arguments</param>
        private void WeekNumberSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.calendar != null && e.NewValue.HasValue)
            {
                this.calendar.MonthView.ShowWeekNumber = e.NewValue.Value;
            }
        }

        /// <summary>
        /// Method for trailing dates switch toggle changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Arguments</param>
        private void TrailingDatesSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.calendar != null && e.NewValue.HasValue)
            {
                this.calendar.ShowTrailingAndLeadingDates = e.NewValue.Value;
            }
        }

        /// <summary>
        /// Method for current time indicator switch toggle changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Arguments</param>
        private void EnablePastDates_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.calendar != null && e.NewValue.HasValue)
            {
                this.calendar.EnablePastDates = e.NewValue.Value;
            }
        }

        /// <summary>
        /// Method for enable swipe selection switch toggle changed
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Arguments</param>
        private void EnableSwipeSelection_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.calendar != null && e.NewValue.HasValue)
            {
                this.calendar.EnableSwipeSelection = e.NewValue.Value;
            }
        }

        /// <summary>
        /// Method for enable today button switch toggle changed
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Arguments</param>
        private void EnableTodayButtonSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.calendar != null && e.NewValue.HasValue)
            {
                this.calendar.FooterView.ShowTodayButton = e.NewValue.Value;
            }
        }

        /// <summary>
        /// Method for navigation month switch toggle changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigationToAdjacentMonth_StateChanged(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.calendar != null && e.NewValue.HasValue)
            {
                this.calendar.NavigateToAdjacentMonth = e.NewValue.Value;
            }
        }

        /// <summary>
        /// Method for enable action buttons switch toggle changed
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Arguments</param>
        private void EnableActionButtonsSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.calendar != null && e.NewValue.HasValue)
            {
                this.calendar.FooterView.ShowActionButtons = e.NewValue.Value;
            }
        }

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.weekNumberGrid != null)
            {
                this.weekNumberGrid = null;
            }

            if (this.weekNumberSwitch != null)
            {
                this.weekNumberSwitch.StateChanged -= WeekNumberSwitch_Toggled;
                this.weekNumberSwitch = null;
            }

            if (this.trailingDatesSwitch != null)
            {
                this.trailingDatesSwitch.StateChanged -= TrailingDatesSwitch_Toggled;
                this.trailingDatesSwitch = null;
            }

            if (this.enableDatesSwitch != null)
            {
                this.enableDatesSwitch.StateChanged -= EnablePastDates_Toggled;
                this.enableDatesSwitch = null;
            }

            if (this.enableSwipeSelectionSwitch != null)
            {
                this.enableSwipeSelectionSwitch.StateChanged -= EnableSwipeSelection_Toggled;
                this.enableDatesSwitch = null;
            }

            if (this.allowViewNavigationSwitch != null)
            {
                this.allowViewNavigationSwitch.StateChanged -= AllowViewNavigationSwitch_Toggled;
                this.allowViewNavigationSwitch = null;
            }

            if (this.enableActionButtonsSwitch != null)
            {
                this.enableActionButtonsSwitch.StateChanged -= EnableActionButtonsSwitch_Toggled;
                this.enableActionButtonsSwitch = null;
            }

            if (this.enableTodayButtonSwitch != null)
            {
                this.enableTodayButtonSwitch.StateChanged -= EnableTodayButtonSwitch_Toggled;
                this.enableTodayButtonSwitch = null;
            }

            if (this.navigationToAdjacentMonth != null)
            {
                this.navigationToAdjacentMonth.StateChanged -= NavigationToAdjacentMonth_StateChanged;
                this.navigationToAdjacentMonth = null;
            }

            if (this.calendar != null)
            {
                this.calendar.ViewChanged -= Calendar_ViewChanged;
                this.calendar = null;
            }

            if (this.datePicker != null)
            {
                this.datePicker.DateSelected -= DatePicker_DateSelected;
                this.datePicker = null;
            }
        }
    }
}
