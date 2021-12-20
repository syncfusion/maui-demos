using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.Generic;

namespace SampleBrowser.Maui.SfScheduler
{
    /// <summary>
    /// The appointment editor behavior.
    /// </summary>
    internal class AppointmentEditorBehavior : Behavior<StackLayout>
    {
        #region Fields

        /// <summary>
        /// The save button.
        /// </summary>
        private Button saveButton;

        /// <summary>
        /// The cancel button.
        /// </summary>
        private Button cancelButton;

        /// <summary>
        /// The delete button.
        /// </summary>
        private Button deleteButton;

        /// <summary>
        /// The appointment editor frame.
        /// </summary>
        private Frame frame;

        /// <summary>
        /// The end date picker.
        /// </summary>
        private DatePicker endDatePicker;

        private Grid timeZoneGrid;

        /// <summary>
        /// The end time picker.
        /// </summary>
        private TimePicker endTimePicker;

        /// <summary>
        ///The selected appointment.
        /// </summary>
        private Meeting selectedAppointment;

        /// <summary>
        /// The selected date.
        /// </summary>
        private DateTime selectedDate;

        /// <summary>
        /// The start date picker.
        /// </summary>
        private DatePicker startDatePicker;

        /// <summary>
        /// The start time picker.
        /// </summary>
        private TimePicker startTimePicker;

        /// <summary>
        /// The appointment editor view.
        /// </summary>
        private StackLayout appointmenEditorView;

        /// <summary>
        /// The recurrence editing option view.
        /// </summary>
        private StackLayout recurrenceEditingOptionView;

        /// <summary>
        /// The all day switch.
        /// </summary>
        private CheckBox switchAllDay;

        /// <summary>
        /// The title text.
        /// </summary>
        private Entry subjectText;

        /// <summary>
        /// The organizer text.
        /// </summary>
        private Entry descriptionText;

        /// <summary>
        /// The time zone picker.
        /// </summary>
        private Picker TimeZonePicker;

        /// <summary>
        /// The color picker.
        /// </summary>
        private Picker colorPicker;

        /// <summary>
        /// The Rrule picker.
        /// </summary>
        private Picker rRulePicker;

        /// <summary>
        /// The recurring editor option view ok button.
        /// </summary>
        private Button recurringEditorOptionViewOkButton;

        /// <summary>
        /// The recurring editor option view cancel button.
        /// </summary>
        private Button recurringEditorOptionViewCancelButton;

        /// <summary>
        /// The recurrence text block
        /// </summary>
        private Label recurrenceTextBlock;

        /// <summary>
        /// The rRule picker collection.
        /// </summary>
        private List<string> rRulePickerCollection;

        /// <summary>
        /// The isDelete recurrence appointment.
        /// </summary>
        private bool isDeleteRecurrenceAppointment;

        /// <summary>
        /// The isDelete recurrence appointment.
        /// </summary>
        internal bool IsRecursiveAppointment { get; set; }

        /// <summary>
        /// The single radio button.
        /// </summary>
#if __IOS__ || __MACCATALYST__
        internal CheckBox
#else
internal RadioButton
#endif
            SingleRadioButton { get; set; }

        /// <summary>
        /// The series radio button.
        /// </summary>
#if __IOS__ || __MACCATALYST__
        internal CheckBox
#else
internal RadioButton
#endif
            SeriesRadioButton { get; set; }

        #endregion

        #region Methods 

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnAttachedTo(StackLayout bindable)
        {
            base.OnAttachedTo(bindable);
            this.appointmenEditorView = bindable;

            this.recurrenceEditingOptionView = bindable.FindByName<RecurrenceEditingOptionView>("RecurrenceEditingOptionView");

            this.subjectText = bindable.FindByName<Entry>("SubjectText");
            this.descriptionText = bindable.FindByName<Entry>("DescriptionText");
            this.saveButton = bindable.FindByName<Button>("SaveButton");
            this.cancelButton = bindable.FindByName<Button>("CancelButton");
            this.deleteButton = bindable.FindByName<Button>("DeleteButton");
            this.endDatePicker = bindable.FindByName<DatePicker>("EndDatePicker");
            this.endTimePicker = bindable.FindByName<TimePicker>("EndTimePicker");
            this.startDatePicker = bindable.FindByName<DatePicker>("StartDatePicker");
            this.startTimePicker = bindable.FindByName<TimePicker>("StartTimePicker");
            this.switchAllDay = bindable.FindByName<CheckBox>("SwitchAllDay");
            this.TimeZonePicker = bindable.FindByName<Picker>("TimeZonePicker");
            this.colorPicker = bindable.FindByName<Picker>("ColorPicker");
            this.rRulePicker = bindable.FindByName<Picker>("RrulePicker");
            this.frame = bindable.FindByName<Frame>("frame");
            this.timeZoneGrid = bindable.FindByName<Grid>("TimeZoneGrid");

            if (this.recurrenceEditingOptionView != null)
            {
                this.recurringEditorOptionViewOkButton = this.recurrenceEditingOptionView.FindByName<Button>("RecurrenceOkButton");
                this.recurringEditorOptionViewCancelButton = this.recurrenceEditingOptionView.FindByName<Button>("RecurrenceCancelButton");
#if __IOS__ || __MACCATALYST__
                this.SingleRadioButton = this.recurrenceEditingOptionView.FindByName<CheckBox>("SingleRadioCheckBox");
                this.SeriesRadioButton = this.recurrenceEditingOptionView.FindByName<CheckBox>("SeriesRadioCheckBox");
#else
                this.SingleRadioButton = this.recurrenceEditingOptionView.FindByName<RadioButton>("SingleRadioButton");
                this.SeriesRadioButton = this.recurrenceEditingOptionView.FindByName<RadioButton>("SeriesRadioButton");
#endif
                this.recurrenceTextBlock = this.recurrenceEditingOptionView.FindByName<Label>("RecurrenceTextBlock");
            }

            this.rRulePickerCollection = new List<string>();
            this.TimeZonePicker.ItemsSource = AppointmentHelper.TimeZoneList;
            this.TimeZonePicker.SelectedIndex = 0;
            this.AddpickerControlsCollection();
            WireEvents();
        }

        /// <summary>
        /// Method to Wire editor events.
        /// </summary>
        private void WireEvents()
        {
            if (this.colorPicker != null)
            {
                this.colorPicker.SelectedIndexChanged += this.OnColorPickerSelectedIndexChanged;
            }

            if (this.saveButton != null)
            {
                this.saveButton.Clicked += this.OnSaveButtonClicked;
            }

            if (this.cancelButton != null)
            {
                this.cancelButton.Clicked += this.OnCancelButtonClicked;
            }

            if (this.deleteButton != null)
            {
                this.deleteButton.Clicked += this.OnDeleteButtonClicked;
            }

            if (this.recurringEditorOptionViewOkButton != null)
            {
                this.recurringEditorOptionViewOkButton.Clicked += this.OnRecurringEditorOptionViewOkButtonClicked;
            }

            if (this.recurringEditorOptionViewCancelButton != null)
            {
                this.recurringEditorOptionViewCancelButton.Clicked += this.OnRecurringEditorOptionViewCancelButtonClicked;
            }

            if (this.switchAllDay != null)
            {
                this.switchAllDay.CheckedChanged += OnSwitchAllDayCheckedChanged;
            }

#if __IOS__ || __MACCATALYST__
            if (this.SingleRadioButton != null)
            {
                this.SingleRadioButton.CheckedChanged += OnSingleRadioButton_CheckedChanged;
            }

            if (this.SeriesRadioButton != null)
            {
                this.SeriesRadioButton.CheckedChanged += OnSeriesRadioButton_CheckedChanged;
            }

#endif
        }

        /// <summary>
        /// Method to add background and rrule pickers collection.
        /// </summary>
        private void AddpickerControlsCollection()
        {
            // Dictionary to get RRule.
            rRulePickerCollection = new List<string>
            {
                { "Does not repeat" },
                { "Every day" },
                { "Every week" },
                { "Every month"},
                { "Every year" },
                //{ "Custom" },
            };

            if (this.colorPicker != null && this.colorPicker.Items != null)
            {
                foreach (string colorName in AppointmentHelper.BackgroundCollection.Keys)
                {
                    this.colorPicker.Items.Add(colorName);
                }
            }

            if (this.rRulePicker != null && this.rRulePicker.Items != null)
            {
                foreach (string rrule in rRulePickerCollection)
                {
                    this.rRulePicker.Items.Add(rrule);
                }
            }
        }

        /// <summary>
        /// Occurs when button clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnRecurringEditorOptionViewCancelButtonClicked(object sender, EventArgs e)
        {
            this.recurrenceEditingOptionView.IsVisible = false;
            this.appointmenEditorView.IsVisible = true;
        }

        /// <summary>
        /// Occurs when button clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnRecurringEditorOptionViewOkButtonClicked(object sender, EventArgs e)
        {
            if (this.isDeleteRecurrenceAppointment)
            {
                var args = new AppointmentEditorClosedEventArgs();
                args.Appointment = this.selectedAppointment;
                args.IsEdit = false;
                args.IsDelete = true;
                (this.appointmenEditorView.BindingContext as SchedulerDataBindingViewModel).RaiseAppointmentEditorClosedEvent(args);
            }
            else
            {
                var args = new AppointmentEditorClosedEventArgs();
                args.Appointment = this.selectedAppointment;
                args.IsEdit = true;
                args.IsDelete = false;
                (this.appointmenEditorView.BindingContext as SchedulerDataBindingViewModel).RaiseAppointmentEditorClosedEvent(args);
            }

            this.recurrenceEditingOptionView.IsVisible = false;
            this.appointmenEditorView.IsVisible = false;
            this.selectedAppointment = null;
        }

        /// <summary>
        /// Method to Unwire the appointment editor events.
        /// </summary>
        private void UnWireEvents()
        {
            if (this.TimeZonePicker != null)
            {
                this.TimeZonePicker = null;
            }

            if (this.colorPicker != null)
            {
                this.colorPicker.SelectedIndexChanged -= this.OnColorPickerSelectedIndexChanged;
                this.colorPicker = null;
            }

            if (this.TimeZonePicker != null)
            {
                this.TimeZonePicker = null;
            }

            if (this.rRulePicker != null)
            {
                this.rRulePicker = null;
            }

            if (this.saveButton != null)
            {
                this.saveButton.Clicked -= this.OnSaveButtonClicked;
                this.saveButton = null;
            }

            if (this.cancelButton != null)
            {
                this.cancelButton.Clicked -= this.OnCancelButtonClicked;
                this.cancelButton = null;
            }

            if (this.deleteButton != null)
            {
                this.deleteButton.Clicked -= this.OnDeleteButtonClicked;
                this.deleteButton = null;
            }

            if (this.switchAllDay != null)
            {
                this.switchAllDay.CheckedChanged -= OnSwitchAllDayCheckedChanged;
                this.switchAllDay = null;
            }

            if (this.recurringEditorOptionViewOkButton != null)
            {
                this.recurringEditorOptionViewOkButton.Clicked -= this.OnRecurringEditorOptionViewOkButtonClicked;
                this.recurringEditorOptionViewOkButton = null;
            }

            if (this.recurringEditorOptionViewCancelButton != null)
            {
                this.recurringEditorOptionViewCancelButton.Clicked -= this.OnRecurringEditorOptionViewCancelButtonClicked;
                this.recurringEditorOptionViewCancelButton = null;
            }
        }

        /// <summary>
        /// Begins when the behavior de-attached from the view. 
        /// </summary>
        /// <param name="bindable">The bindable values.</param>
        protected override void OnDetachingFrom(StackLayout bindable)
        {
            this.UnWireEvents();
            if (this.subjectText != null)
            {
                this.subjectText = null;
            }

            if (this.descriptionText != null)
            {
                this.descriptionText = null;
            }

            if (this.startDatePicker != null)
            {
                this.startDatePicker = null;
            }

            if (this.startTimePicker != null)
            {
                this.startTimePicker = null;
            }

            if (this.frame != null)
            {
                this.frame = null;
            }

            if (this.SingleRadioButton != null)
            {
                this.SingleRadioButton = null;
            }

            if (this.SeriesRadioButton != null)
            {
                this.SeriesRadioButton = null;
            }

            if (this.recurrenceTextBlock != null)
            {
                this.recurrenceTextBlock = null;
            }

            if (this.endDatePicker != null)
            {
                this.endDatePicker = null;
            }

            if (this.endTimePicker != null)
            {
                this.endTimePicker = null;
            }
        }

        /// <summary>
        /// Methid to add the new appointment.
        /// </summary>
        /// <param name="selectedDate">The selected date.</param>
        /// <param name="isAllDay">The all day.</param>
        internal void AddNewAppointment(DateTime selectedDate, bool isAllDay)
        {
            this.selectedDate = selectedDate;
            this.SetNewAppointmentEditorProperties(isAllDay);
        }

        /// <summary>
        /// Method to edit an selected appointment.
        /// </summary>
        /// <param name="app">The selected appointment.</param>
        internal void EditAppointment(Meeting app)
        {
            this.selectedAppointment = new Meeting();
            this.selectedAppointment = app;
            this.UpdateAppointmentEditorProperties();
        }

        /// <summary>
        /// Method to set the new appointment editor properties.
        /// </summary>
        /// <param name="isAllDay">The All day.</param>
        private void SetNewAppointmentEditorProperties(bool isAllDay)
        {
            this.subjectText.Placeholder = "Add title";
            this.descriptionText.Placeholder = "Add description";
            this.subjectText.PlaceholderColor = Colors.LightGray;
            this.descriptionText.PlaceholderColor = Colors.LightGray;
            this.deleteButton.IsVisible = false;
            this.subjectText.Text = string.Empty;
            this.descriptionText.Text = string.Empty;
            this.switchAllDay.IsChecked = isAllDay;
            this.startDatePicker.Date = this.selectedDate;
            this.startTimePicker.Time = new TimeSpan(this.selectedDate.Hour, this.selectedDate.Minute, this.selectedDate.Second);
            this.frame.Background = Brush.Blue;
            if (this.selectedDate.Hour == 23)
            {
                this.endTimePicker.Time = new TimeSpan(this.selectedDate.Hour, this.selectedDate.Minute + 59, this.selectedDate.Second + 59);
            }
            else
            {
                this.endTimePicker.Time = new TimeSpan(this.selectedDate.Hour + 1, this.selectedDate.Minute, this.selectedDate.Second);
            }

            this.endDatePicker.Date = this.selectedDate;
            this.colorPicker.SelectedIndex = 0;
            this.rRulePicker.SelectedIndex = 0;
            this.TimeZonePicker.SelectedIndex = 0;
            this.selectedAppointment = null;
            this.recurrenceEditingOptionView.IsVisible = false;
            this.appointmenEditorView.IsVisible = true;
        }

        /// <summary>
        /// Method to update the appointment editor properties.
        /// </summary>
        private void UpdateAppointmentEditorProperties()
        {
            if (this.selectedAppointment != null)
            {
                this.subjectText.Text = this.selectedAppointment.EventName;
                this.descriptionText.Text = this.selectedAppointment.Notes;
                this.startDatePicker.Date = this.selectedAppointment.From;
                this.endDatePicker.Date = this.selectedAppointment.To;
                this.TimeZonePicker.SelectedIndex = AppointmentHelper.GetTimeZoneIndex(this.selectedAppointment.StartTimeZone);
                this.frame.Background = this.selectedAppointment.Background;
                this.colorPicker.SelectedIndex = AppointmentHelper.GetColorIndex(this.selectedAppointment.Background);
                this.deleteButton.IsVisible = true;
                if (!this.selectedAppointment.IsAllDay)
                {
                    this.startTimePicker.Time = new TimeSpan(this.selectedAppointment.From.Hour, this.selectedAppointment.From.Minute, this.selectedAppointment.From.Second);
                    this.endTimePicker.Time = new TimeSpan(this.selectedAppointment.To.Hour, this.selectedAppointment.To.Minute, this.selectedAppointment.To.Second);
                    this.switchAllDay.IsChecked = false;
                }
                else
                {
                    this.startTimePicker.Time = new TimeSpan(12, 0, 0);
                    this.startTimePicker.IsVisible = false;
                    this.endTimePicker.Time = new TimeSpan(12, 0, 0);
                    this.endTimePicker.IsVisible = false;
                    this.switchAllDay.IsChecked = true;
                }

                if (!string.IsNullOrEmpty(this.selectedAppointment.RecurrenceRule))
                {
                    this.IsRecursiveAppointment = true;
                }
                else
                {
                    this.IsRecursiveAppointment = false;
                }
            }

            this.recurrenceEditingOptionView.IsVisible = false;
            this.appointmenEditorView.IsVisible = true;
        }

        /// <summary>
        /// Occurs when color picker selected index changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnColorPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            Brush background;
            AppointmentHelper.BackgroundCollection.TryGetValue(this.colorPicker.SelectedItem.ToString(), out background);
            this.frame.Background = background;

            if (this.selectedAppointment != null)
            {
                this.selectedAppointment.Background = background;
            }
        }

        /// <summary>
        /// Occurs when check box checked changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The checked changes event args.</param>
        private void OnSwitchAllDayCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (this.switchAllDay.IsChecked)
            {
                this.startTimePicker.Time = new TimeSpan(12, 0, 0);
                this.endTimePicker.Time = new TimeSpan(12, 0, 0);
                this.startTimePicker.IsVisible = false;
                this.endTimePicker.IsVisible = false;
                this.timeZoneGrid.IsVisible = false;
            }
            else
            {
                this.timeZoneGrid.IsVisible = true;
                this.startTimePicker.IsVisible = true;
                this.endTimePicker.IsVisible = true;
                this.switchAllDay.IsChecked = false;
            }
        }

        /// <summary>
        /// Occurs when check box checked changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The checked changes event args.</param>
        private void OnSeriesRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (this.SeriesRadioButton.IsChecked)
            {
                this.SingleRadioButton.IsEnabled = false;
            }
            else
            {
                this.SingleRadioButton.IsEnabled = true;
            }
        }

        /// <summary>
        /// Occurs when check box checked changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The checked changes event args.</param>
        private void OnSingleRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (this.SingleRadioButton.IsChecked)
            {
                this.SeriesRadioButton.IsEnabled = false;
            }
            else
            {
                this.SeriesRadioButton.IsEnabled = true;
            }
        }

        /// <summary>
        /// Occurs when cancel button clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnCancelButtonClicked(object sender, EventArgs e)
        {
            this.appointmenEditorView.IsVisible = false;
            this.selectedAppointment = null;
        }

        /// <summary>
        /// Occurs when delete button clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.selectedAppointment.RecurrenceRule) && this.IsRecursiveAppointment)
            {
                this.isDeleteRecurrenceAppointment = true;
                this.recurrenceTextBlock.Text = "Delete recurring event";
                this.recurringEditorOptionViewOkButton.Text = "Delete";
                this.SingleRadioButton.IsChecked = true;
#if __IOS__ || __MACCATALYST__
                this.SeriesRadioButton.IsEnabled = false;
                this.SeriesRadioButton.IsChecked = false;
#endif
                this.recurrenceEditingOptionView.IsVisible = true;
            }
            else
            {
                var args = new AppointmentEditorClosedEventArgs();
                args.Appointment = this.selectedAppointment;
                args.IsEdit = false;
                args.IsDelete = true;
                (this.appointmenEditorView.BindingContext as SchedulerDataBindingViewModel).RaiseAppointmentEditorClosedEvent(args);
                this.appointmenEditorView.IsVisible = false;
                this.isDeleteRecurrenceAppointment = true;
                this.selectedAppointment = null;
            }
        }

        /// <summary>
        /// Occurs when save button clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var endDate = this.endDatePicker.Date;
            var startDate = this.startDatePicker.Date;
            var endTime = this.endTimePicker.Time;
            var startTime = this.startTimePicker.Time;
            if (endDate < startDate)
            {
                Application.Current.MainPage.DisplayAlert(" ", "End date should be greater than start date", "OK");
            }
            else if (endDate == startDate && endTime < startTime)
            {
                Application.Current.MainPage.DisplayAlert(" ", "End time should be greater than start time", "OK");
            }
            else
            {
                this.UpdateAppointmentDetails();
                if ((!string.IsNullOrEmpty(this.selectedAppointment?.RecurrenceRule) && this.IsRecursiveAppointment && this.deleteButton.IsVisible) || (this.selectedAppointment?.RecurrenceId != null && this.selectedAppointment.Id != null))
                {
                    this.isDeleteRecurrenceAppointment = false;
                    this.recurrenceTextBlock.Text = "Save recurring event";
                    this.recurringEditorOptionViewOkButton.Text = "Save";
                    this.SingleRadioButton.IsChecked = true;
#if __IOS__ || __MACCATALYST__
                    this.SeriesRadioButton.IsEnabled = false;
                    this.SeriesRadioButton.IsChecked = false;
#endif
                    this.recurrenceEditingOptionView.IsVisible = true;
                }
                else
                {
                    var args = new AppointmentEditorClosedEventArgs();
                    args.Appointment = this.selectedAppointment;
                    args.IsEdit = true;
                    args.IsDelete = false;
                    (this.appointmenEditorView.BindingContext as SchedulerDataBindingViewModel).RaiseAppointmentEditorClosedEvent(args);
                    this.appointmenEditorView.IsVisible = false;
                    this.selectedAppointment = null;
                }
            }
        }

        /// <summary>
        /// Method to update the appointment details.
        /// </summary>
        private void UpdateAppointmentDetails()
        {
            if (this.selectedAppointment == null)
            {
                this.selectedAppointment = new Meeting();

                Brush background;
                AppointmentHelper.BackgroundCollection.TryGetValue(this.colorPicker.SelectedItem.ToString(), out background);
                this.selectedAppointment.Background = background;
            }

            if (this.subjectText.Text != null)
            {
                this.selectedAppointment.EventName = this.subjectText.Text.ToString();
            }

            if (string.IsNullOrEmpty(this.selectedAppointment.EventName))
            {
                this.selectedAppointment.EventName = "No title";
            }

            if (this.descriptionText.Text != null)
            {
                this.selectedAppointment.Notes = this.descriptionText.Text.ToString();
            }

            this.selectedAppointment.From = this.startDatePicker.Date.Add(this.startTimePicker.Time);
            this.selectedAppointment.To = this.endDatePicker.Date.Add(this.endTimePicker.Time);
            this.selectedAppointment.IsAllDay = this.switchAllDay.IsChecked;
            TimeZoneInfo timeZone = this.TimeZonePicker.SelectedItem as string == "Default" ? TimeZoneInfo.Local : TimeZoneInfo.FindSystemTimeZoneById(this.TimeZonePicker.SelectedItem.ToString());
            if (timeZone != null)
            {
                this.selectedAppointment.StartTimeZone = timeZone;
                this.selectedAppointment.EndTimeZone = timeZone;
            }

            if (this.rRulePicker.SelectedIndex > 0)
            {
                SchedulerRecurrenceInfo recurrenceInfo = AppointmentHelper.GetRecurrenceInfo(this.rRulePicker.SelectedIndex, this.selectedDate);
                recurrenceInfo.StartDate = this.startDatePicker.Date;
                this.selectedAppointment.RecurrenceRule = SchedulerRecurrenceManager.GenerateRRule(recurrenceInfo, this.startDatePicker.Date, this.endDatePicker.Date);
            }
        }

#endregion
    }
}
