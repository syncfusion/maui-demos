#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Calendar.SfCalendar
{
    using System;
    using System.Collections.ObjectModel;
    using Microsoft.Maui.Controls;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Calendar;
    using Syncfusion.Maui.Inputs;

    /// <summary>
    /// Getting started Behavior class
    /// </summary>
    internal class DateSelectionBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Calendar view 
        /// </summary>
        private SfCalendar? calendar;

        /// <summary>
        /// The combo box that allows users to choose to whether to select date or a range.
        /// </summary>
        private SfComboBox? comboBox;

        /// <summary>
        /// The label to display the selected date or range.
        /// </summary>
        private Label? label;

        /// <summary>
        /// The selected date which is to be displayed on the label.
        /// </summary>
        private DateTime date = DateTime.Now;

        /// <summary>
        /// The label shows the selection type based on what users choose.
        /// </summary>
        private Label? selectionLabel;

        /// <summary>
        /// The selected date range which is to be displayed on the label.
        /// </summary>
        private CalendarDateRange dateRange = new CalendarDateRange(DateTime.Now, DateTime.Now.AddDays(3));

        /// <summary>
        /// Check the application theme is light or dark.
        /// </summary>
        private bool isLightTheme = Application.Current?.RequestedTheme == AppTheme.Light;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
#if IOS || MACCATALYST
            Border border = bindable.Content.FindByName<Border>("border");
            border.IsVisible = true;
#if MACCATALYST
            border.Stroke = isLightTheme ? Color.FromArgb("#CAC4D0") : Color.FromArgb("#49454F");
#else
            border.Stroke = Colors.Transparent;
#endif
            this.calendar = bindable.Content.FindByName<SfCalendar>("dateSelection1");
            this.label = bindable.Content.FindByName<Label>("label1");
            this.selectionLabel = bindable.Content.FindByName<Label>("selectionLabel1");
#else
            Frame frame = bindable.Content.FindByName<Frame>("frame");
            frame.IsVisible = true;
#if ANDROID
            frame.BorderColor = Colors.Transparent;
#else
            frame.BorderColor = isLightTheme ? Color.FromArgb("#CAC4D0") : Color.FromArgb("#49454F");
#endif
            this.calendar = bindable.Content.FindByName<SfCalendar>("dateSelection");
            this.label = bindable.Content.FindByName<Label>("label");
            this.selectionLabel = bindable.Content.FindByName<Label>("selectionLabel");
#endif
            this.comboBox = bindable.Content.FindByName<SfComboBox>("comboBox");
            comboBox.ItemsSource = new List<string>() { "Date", "Range" };
            comboBox.SelectedIndex = 0;
            comboBox.SelectionChanged += ComboBox_SelectionChanged;
            calendar.SelectedDate = date;
            calendar.SelectedDateRange = new CalendarDateRange(dateRange.StartDate, dateRange.EndDate);
            calendar.SelectionChanged += Calendar_SelectionChanged;
            this.UpdateSelectionText();
        }

        private void Calendar_SelectionChanged(object? sender, CalendarSelectionChangedEventArgs e)
        {
            this.UpdateSelectionText();
        }

        /// <summary>
        /// Method to update the selection text.
        /// </summary>
        private void UpdateSelectionText()
        {
            if (this.label != null && this.calendar != null && this.calendar.SelectionMode == CalendarSelectionMode.Single && this.calendar.SelectedDate != null)
            {
                this.label.Text = this.calendar.SelectedDate.Value.ToString("ddd") + ", " + this.calendar.SelectedDate.Value.ToString("MMM") + " " + this.calendar.SelectedDate.Value.Day.ToString();
            }
            else if (this.label != null && this.calendar != null && this.calendar.SelectionMode == CalendarSelectionMode.Range && this.calendar.SelectedDateRange != null)
            {
                string startDateText = string.Empty;
                string EndDateText = string.Empty;
                if (this.calendar.SelectedDateRange.StartDate != null)
                {
                    startDateText = this.calendar.SelectedDateRange.StartDate.Value.ToString("MMM") + " " + this.calendar.SelectedDateRange.StartDate.Value.Day.ToString();
                }

                if (this.calendar.SelectedDateRange.EndDate != null)
                {
                    EndDateText = this.calendar.SelectedDateRange.EndDate.Value.ToString("MMM") + " " + this.calendar.SelectedDateRange.EndDate.Value.Day.ToString();
                }

                if (EndDateText == string.Empty)
                {
                    this.label.Text = startDateText;
                }
                else
                {
                    this.label.Text = startDateText + " - " + EndDateText;
                }
            }
        }

        /// <summary>
        /// Method for Combo box selection type changed.
        /// </summary>
        /// <param name="sender">Return the object</param>
        /// <param name="e">Event Arguments</param>
        private void ComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (calendar != null && e.AddedItems != null)
            {
                string? selectionMode = e.AddedItems[0].ToString();
                if (selectionMode != null)
                {
                    calendar.SelectionMode = selectionMode == "Date" ? CalendarSelectionMode.Single : CalendarSelectionMode.Range;
                    if (calendar.SelectionMode == CalendarSelectionMode.Single)
                    {
                        if (this.selectionLabel != null)
                        {
                            this.selectionLabel.Text = "Select date";
                        }

                        if (calendar.View == CalendarView.Month)
                        {
                            DateTime visibleStartDate = new DateTime(this.calendar.DisplayDate.Year, this.calendar.DisplayDate.Month, 1);
                            int days = DateTime.DaysInMonth(visibleStartDate.Year, visibleStartDate.Month);
                            DateTime visibleEndDate = new DateTime(visibleStartDate.Year, visibleStartDate.Month, days);
                            if (!this.IsDateWithInRange(this.calendar.SelectedDate, visibleStartDate.Date, visibleEndDate.Date))
                            {
                                this.calendar.SelectedDate = visibleStartDate.Date;
                                this.date = this.calendar.SelectedDate.Value;
                            }
                        }
                    }
                    else if (calendar.SelectionMode == CalendarSelectionMode.Range)
                    {
                        if (this.selectionLabel != null)
                        {
                            this.selectionLabel.Text = "Select range";
                        }

                        if (calendar.View == CalendarView.Month)
                        {
                            DateTime visibleStartDate = new DateTime(this.calendar.DisplayDate.Year, this.calendar.DisplayDate.Month, 1);
                            int days = DateTime.DaysInMonth(visibleStartDate.Year, visibleStartDate.Month);
                            DateTime visibleEndDate = new DateTime(visibleStartDate.Year, visibleStartDate.Month, days);
                            if (this.calendar.SelectedDateRange == null || (!this.IsDateWithInRange(this.calendar.SelectedDateRange.StartDate, visibleStartDate, visibleEndDate) &&
                                !this.IsDateWithInRange(this.calendar.SelectedDateRange.EndDate, visibleStartDate.Date, visibleEndDate.Date) && !this.IsDateWithInRange(visibleStartDate, this.calendar.SelectedDateRange.StartDate, this.calendar.SelectedDateRange.EndDate)))
                            {
                                this.calendar.SelectedDateRange = new CalendarDateRange(visibleStartDate.Date, visibleStartDate.Date.AddDays(3));
                                this.dateRange = new CalendarDateRange(calendar.SelectedDateRange.StartDate, calendar.SelectedDateRange.EndDate);
                            }
                        }
                    }

                    this.UpdateSelectionText();
                }
            }
        }

        /// <summary>
        /// Return the date with in date range.
        /// </summary>
        /// <param name="date">Date value.</param>
        /// <param name="startDate">Start date of the range.</param>
        /// <param name="endDate">End date of the range.</param>
        /// <returns>Return true, the date with in date range.</returns>
        private bool IsDateWithInRange(DateTime? date, DateTime? startDate, DateTime? endDate)
        {
            if (date == null || startDate == null || endDate == null)
            {
                return false;
            }

            return date.Value.Date >= startDate.Value.Date && date.Value.Date <= endDate.Value.Date;
        }

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);

            if (this.calendar != null)
            {
                this.calendar.SelectionChanged -= this.Calendar_SelectionChanged;
                this.calendar = null;
            }

            if (this.comboBox != null)
            {
                this.comboBox.SelectionChanged -= this.ComboBox_SelectionChanged;
                this.comboBox = null;
            }

            if (this.label != null)
            {
                this.label = null;
            }
        }
    }
}