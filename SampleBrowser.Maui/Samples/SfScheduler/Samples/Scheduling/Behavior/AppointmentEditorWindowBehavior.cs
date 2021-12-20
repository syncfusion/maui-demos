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
    internal class AppointmentEditorWindowBehavior : Behavior<StackLayout>
    {
        #region Fields

        /// <summary>
        /// Save appointment button.
        /// </summary>
        private Button saveAppointmentButton;

        /// <summary>
        /// Cancel appointment button.
        /// </summary>
        private Button cancelAppointmentButton;

        /// <summary>
        /// Editor close button. 
        /// </summary>
        private Button headerCancelIconButton;

        /// <summary>
        /// Title label.
        /// </summary>
        private Label titleLabel;

        /// <summary>
        /// Location label.
        /// </summary>
        private Label locationLabel;

        /// <summary>
        /// Start time label.
        /// </summary>
        private Label startLabel;

        /// <summary>
        /// End time label
        /// </summary>
        private Label endLabel;

        /// <summary>
        /// Recurrence label.
        /// </summary>
        private Label repeatLabel;

        /// <summary>
        /// The appointment editor frame.
        /// </summary>
        private Frame colorFrame;

        /// <summary>
        /// The color picker.
        /// </summary>
        private Picker colorPicker;

        /// <summary>
        /// Gets or sets a value indicating whether header title label value.
        /// </summary>
        private Label headerTitleLabel;

        /// <summary>
        /// Gets or sets a value indicating whether subject textbox value.
        /// </summary>
        private Entry subjectTextBox;

        /// <summary>
        /// Gets or sets a value indicating whether location textbox value.
        /// </summary>
        private Entry locationTextBox;

        /// <summary>
        /// Gets or sets a value indicating whether start DatePicker value.
        /// </summary>
        private DatePicker startDatePicker;

        /// <summary>
        /// Gets or sets a value indicating whether end DatePicker value.
        /// </summary>
        private DatePicker endDatePicker;

        /// <summary>
        /// Gets or sets a value indicating whether start TimePicker value.
        /// </summary>
        private TimePicker startTimePicker;

        /// <summary>
        /// Gets or sets a value indicating whether end TimePicker value.
        /// </summary>
        private TimePicker endTimePicker;

        /// <summary>
        /// Gets or sets a value indicating whether allday checkbox value.
        /// </summary>
        private CheckBox allDayCheckBox;

        /// <summary>
        /// Gets or sets a value indicating whether start timezone combobox value.
        /// </summary>
        private Picker timeZonePicker;

        /// <summary>
        /// Gets or sets a value indicating whether recurrence type combobox value.
        /// </summary>
        private Picker recurrenceTypesPicker;

        /// <summary>
        /// Gets or sets a value indicating whether NotesTextBox value.
        /// </summary>
        private Entry notesTextBox;

        /// <summary>
        /// Gets or sets a value indicating whether DeleteAppointmentButton value.
        /// </summary>
        private Button deleteAppointmentButton;

        /// <summary>
        /// Editor window instance.
        /// </summary>
        private StackLayout appointmentEditorWindow;

        /// <summary>
        /// The selected date.
        /// </summary>
        private DateTime selectedDate;

        /// <summary>
        ///The selected appointment.
        /// </summary>
        private Meeting selectedAppointment;

        /// <summary>
        /// The recurrence text block
        /// </summary>
        private Label recurrenceTextBlock;

        #endregion

        #region Methods 

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnAttachedTo(StackLayout bindable)
        {
            base.OnAttachedTo(bindable);
            this.appointmentEditorWindow = bindable;

            this.headerTitleLabel = bindable.FindByName<Label>("HeaderTitleLabel");
            this.headerCancelIconButton = bindable.FindByName<Button>("HeaderCancelIconButton");
            this.titleLabel = bindable.FindByName<Label>("TitleLabel");
            this.locationLabel = bindable.FindByName<Label>("LocationLabel");
            this.subjectTextBox = bindable.FindByName<Entry>("SubjectTextBox");
            this.locationTextBox = bindable.FindByName<Entry>("LocationTextBox");
            this.startLabel = bindable.FindByName<Label>("StartLabel");
            this.endLabel = bindable.FindByName<Label>("EndLabel");
            this.startDatePicker = bindable.FindByName<DatePicker>("Start_DatePicker");
            this.endDatePicker = bindable.FindByName<DatePicker>("End_DatePicker");
            this.startTimePicker = bindable.FindByName<TimePicker>("Start_TimePicker");
            this.endTimePicker = bindable.FindByName<TimePicker>("End_TimePicker");
            this.allDayCheckBox = bindable.FindByName<CheckBox>("AllDayCheckBox");
            this.timeZonePicker = bindable.FindByName<Picker>("TimeZonePicker");
            this.repeatLabel = bindable.FindByName<Label>("RepeatLabel");
            this.recurrenceTypesPicker = bindable.FindByName<Picker>("RecurrenceTypesPicker");
            this.notesTextBox = bindable.FindByName<Entry>("NotesTextBox");
            this.deleteAppointmentButton = bindable.FindByName<Button>("DeleteAppointmentButton");
            this.saveAppointmentButton = bindable.FindByName<Button>("SaveAppointmentButton");
            this.cancelAppointmentButton = bindable.FindByName<Button>("CancelAppointmentButton");
            this.colorFrame = bindable.FindByName<Frame>("frameColor");
            this.colorPicker = bindable.FindByName<Picker>("Color_Picker");

            this.UpdateEditorsContent();
            this.colorPicker.SelectedIndex = 0;
            this.recurrenceTypesPicker.SelectedIndex = 0;
            this.WireEvents();
        }

        /// <summary>
        /// Method to Wire editor events.
        /// </summary>
        private void WireEvents()
        {
            if (this.saveAppointmentButton != null)
            {
                this.saveAppointmentButton.Clicked += this.OnSaveAppointmentButtonClick;
            }

            if (this.deleteAppointmentButton != null)
            {
                this.deleteAppointmentButton.Clicked += this.OnDeleteAppointmentButtonClick; ;
            }

            if (this.headerCancelIconButton != null)
            {
                this.headerCancelIconButton.Clicked += this.OnHeaderCancelIconButtonClick;
            }

            if (this.cancelAppointmentButton != null)
            {
                this.cancelAppointmentButton.Clicked += this.OnCancelAppointmentButtonClick;
            }

            if (this.colorPicker != null)
            {
                this.colorPicker.SelectedIndexChanged += this.OnColorPickerSelectedIndexChanged;
            }
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
            this.colorFrame.Background = background;
        }

        private void OnCancelAppointmentButtonClick(object sender, EventArgs e)
        {
            this.appointmentEditorWindow.IsVisible = false;
            this.selectedAppointment = null;
        }

        private void OnHeaderCancelIconButtonClick(object sender, EventArgs e)
        {
            this.appointmentEditorWindow.IsVisible = false;
            this.selectedAppointment = null;
        }

        private void OnDeleteAppointmentButtonClick(object sender, EventArgs e)
        {
            var args = new AppointmentEditorClosedEventArgs();
            args.Appointment = this.selectedAppointment;
            args.IsEdit = false;
            args.IsDelete = true;
            (this.appointmentEditorWindow.BindingContext as SchedulerDataBindingViewModel).RaiseAppointmentEditorClosedEvent(args);
            this.appointmentEditorWindow.IsVisible = false;
            this.selectedAppointment = null;
        }

        private void OnSaveAppointmentButtonClick(object sender, EventArgs e)
        {
            var startDate = this.startDatePicker.Date;
            var startTime = this.startTimePicker.Time;
            var endDate = this.endDatePicker.Date;
            var endTime = this.endTimePicker.Time;
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
                var args = new AppointmentEditorClosedEventArgs();
                args.Appointment = this.selectedAppointment;
                args.IsEdit = true;
                args.IsDelete = false;
                (this.appointmentEditorWindow.BindingContext as SchedulerDataBindingViewModel)?.RaiseAppointmentEditorClosedEvent(args);
                this.appointmentEditorWindow.IsVisible = false;
                this.selectedAppointment = null;
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
        /// Method to set the new appointment editor properties.
        /// </summary>
        /// <param name="isAllDay">The All day.</param>
        private void SetNewAppointmentEditorProperties(bool isAllDay)
        {
            this.headerTitleLabel.Text = "New Appointment";
            this.subjectTextBox.Placeholder = "Add title";
            this.notesTextBox.Placeholder = "Add description";
            this.subjectTextBox.PlaceholderColor = Colors.LightGray;
            this.notesTextBox.PlaceholderColor = Colors.LightGray;
            this.deleteAppointmentButton.IsVisible = false;
            this.subjectTextBox.Text = string.Empty;
            this.locationTextBox.Text = string.Empty;
            this.notesTextBox.Text = string.Empty;
            this.allDayCheckBox.IsChecked = isAllDay;

            this.startDatePicker.Date = this.selectedDate;
            this.startTimePicker.Time = new TimeSpan(this.selectedDate.Hour, this.selectedDate.Minute, this.selectedDate.Second);
            this.colorFrame.Background = Brush.Blue;

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
            this.recurrenceTypesPicker.SelectedIndex = 0;
            this.timeZonePicker.SelectedIndex = 0;
            this.appointmentEditorWindow.IsVisible = true;
        }

        /// <summary>
        /// Method to edit an selected appointment.
        /// </summary>
        /// <param name="app">The selected appointment.</param>
        internal void EditAppointment(Meeting app)
        {
            this.selectedAppointment = app;
            this.UpdateAppointmentEditorProperties();
            this.headerTitleLabel.Text = "Edit Appointment";
        }

        /// <summary>
        /// Gets or sets editor contents.
        /// </summary>
        private void UpdateEditorsContent()
        {
            this.titleLabel.Text = "Title";
            this.locationLabel.Text = "Location";
            this.startLabel.Text = "Start";
            this.endLabel.Text = "End";
            this.repeatLabel.Text = "Repeat";
            this.timeZonePicker.ItemsSource = AppointmentHelper.TimeZoneList;
            this.timeZonePicker.SelectedIndex = 0;

            this.recurrenceTypesPicker.ItemsSource = AppointmentHelper.RecurrenceTypesCollection;
            this.recurrenceTypesPicker.SelectedIndex = 0;
            foreach (string colorName in AppointmentHelper.BackgroundCollection.Keys)
            {
                this.colorPicker.Items.Add(colorName);
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
            }

            if (this.subjectTextBox.Text != null)
            {
                this.selectedAppointment.EventName = this.subjectTextBox.Text;
            }

            if (string.IsNullOrEmpty(this.selectedAppointment.EventName))
            {
                this.selectedAppointment.EventName = "No title";
            }

            if (this.notesTextBox.Text != null)
            {
                this.selectedAppointment.Notes = this.notesTextBox.Text;
            }

            if (this.locationTextBox.Text != null)
            {
                this.selectedAppointment.Location = this.locationTextBox.Text;
            }

            this.selectedAppointment.Background = this.colorFrame.Background;
            this.selectedAppointment.From = this.startDatePicker.Date.Add(this.startTimePicker.Time);
            this.selectedAppointment.To = this.endDatePicker.Date.Add(this.endTimePicker.Time);
            this.selectedAppointment.IsAllDay = this.allDayCheckBox.IsChecked;

            TimeZoneInfo timeZone = this.timeZonePicker.SelectedItem as string == "Default" ? TimeZoneInfo.Local : TimeZoneInfo.FindSystemTimeZoneById(this.timeZonePicker.SelectedItem.ToString());
            if (timeZone != null)
            {
                this.selectedAppointment.StartTimeZone = timeZone;
                this.selectedAppointment.EndTimeZone = timeZone;
            }

            if (this.recurrenceTypesPicker.SelectedIndex > 0)
            {
                SchedulerRecurrenceInfo recurrenceInfo = AppointmentHelper.GetRecurrenceInfo(this.recurrenceTypesPicker.SelectedIndex, this.selectedDate);
                recurrenceInfo.StartDate = this.startDatePicker.Date;
                this.selectedAppointment.RecurrenceRule = SchedulerRecurrenceManager.GenerateRRule(recurrenceInfo, this.startDatePicker.Date, this.endDatePicker.Date);
            }
        }

        private void UpdateAppointmentEditorProperties()
        {
            if (this.selectedAppointment != null)
            {
                this.subjectTextBox.Text = this.selectedAppointment.EventName;
                this.locationTextBox.Text = this.selectedAppointment.Location;
                this.notesTextBox.Text = this.selectedAppointment.Notes;
                this.startDatePicker.Date = this.selectedAppointment.From;
                this.endDatePicker.Date = this.selectedAppointment.To;
                this.timeZonePicker.SelectedIndex = AppointmentHelper.GetTimeZoneIndex(this.selectedAppointment.StartTimeZone);
                this.colorFrame.Background = this.selectedAppointment.Background;
                this.colorPicker.SelectedIndex = AppointmentHelper.GetColorIndex(this.selectedAppointment.Background);
                this.deleteAppointmentButton.IsVisible = true;
                this.recurrenceTypesPicker.SelectedIndex = this.GetRecurrenceIndex(this.selectedAppointment);
                if (!this.selectedAppointment.IsAllDay)
                {
                    this.startTimePicker.Time = new TimeSpan(this.selectedAppointment.From.Hour, this.selectedAppointment.From.Minute, this.selectedAppointment.From.Second);
                    this.endTimePicker.Time = new TimeSpan(this.selectedAppointment.To.Hour, this.selectedAppointment.To.Minute, this.selectedAppointment.To.Second);
                    this.allDayCheckBox.IsChecked = false;
                }
                else
                {
                    this.startTimePicker.Time = new TimeSpan(12, 0, 0);
                    this.endTimePicker.Time = new TimeSpan(12, 0, 0);
                    this.allDayCheckBox.IsChecked = true;
                }
            }

            this.appointmentEditorWindow.IsVisible = true;
        }

        private int GetRecurrenceIndex(Meeting appointment)
        {
            if (string.IsNullOrEmpty(appointment.RecurrenceRule))
            {
                return 0;
            }
            else if (appointment.RecurrenceRule.Contains("DAILY"))
            {
                return 1;
            }
            else if (appointment.RecurrenceRule.Contains("WEEKLY"))
            {
                return 2;
            }
            else if (appointment.RecurrenceRule.Contains("MONTHLY"))
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);
            this.UnwireEvents();

            if (this.saveAppointmentButton != null)
            {
                this.saveAppointmentButton = null;
            }

            if (this.cancelAppointmentButton != null)
            {
                this.cancelAppointmentButton = null;
            }

            if (this.headerCancelIconButton != null)
            {
                this.headerCancelIconButton = null;
            }

            if (this.titleLabel != null)
            {
                this.titleLabel = null;
            }

            if (this.locationLabel != null)
            {
                this.locationLabel = null;
            }

            if (this.startLabel != null)
            {
                this.startLabel = null;
            }

            if (this.endLabel != null)
            {
                this.endLabel = null;
            }

            if (this.repeatLabel != null)
            {
                this.repeatLabel = null;
            }

            if (this.colorFrame != null)
            {
                this.colorFrame = null;
            }

            if (this.colorPicker != null)
            {
                this.colorPicker = null;
            }

            if (this.headerTitleLabel != null)
            {
                this.headerTitleLabel = null;
            }

            if (this.subjectTextBox != null)
            {
                this.subjectTextBox = null;
            }

            if (this.locationTextBox != null)
            {
                this.locationTextBox = null;
            }

            if (this.startDatePicker != null)
            {
                this.startDatePicker = null;
            }

            if (this.endDatePicker != null)
            {
                this.endDatePicker = null;
            }

            if (this.startTimePicker != null)
            {
                this.startTimePicker = null;
            }

            if (this.endTimePicker != null)
            {
                this.endTimePicker = null;
            }

            if (this.allDayCheckBox != null)
            {
                this.allDayCheckBox = null;
            }

            if (this.timeZonePicker != null)
            {
                this.timeZonePicker = null;
            }

            if (this.recurrenceTypesPicker != null)
            {
                this.recurrenceTypesPicker = null;
            }

            if (this.notesTextBox != null)
            {
                this.notesTextBox = null;
            }

            if (this.deleteAppointmentButton != null)
            {
                this.deleteAppointmentButton = null;
            }

            if (this.appointmentEditorWindow != null)
            {
                this.appointmentEditorWindow = null;
            }

            if (this.selectedAppointment != null)
            {
                this.selectedAppointment = null;
            }

            if (this.recurrenceTextBlock != null)
            {
                this.recurrenceTextBlock = null;
            }
        }

        /// <summary>
        /// Method to unwire editor events.
        /// </summary>
        private void UnwireEvents()
        {
            if (this.saveAppointmentButton != null)
            {
                this.saveAppointmentButton.Clicked -= this.OnSaveAppointmentButtonClick;
            }

            if (this.deleteAppointmentButton != null)
            {
                this.deleteAppointmentButton.Clicked -= this.OnDeleteAppointmentButtonClick; ;
            }

            if (this.headerCancelIconButton != null)
            {
                this.headerCancelIconButton.Clicked -= this.OnHeaderCancelIconButtonClick;
            }

            if (this.cancelAppointmentButton != null)
            {
                this.cancelAppointmentButton.Clicked -= this.OnCancelAppointmentButtonClick;
            }

            if (this.colorPicker != null)
            {
                this.colorPicker.SelectedIndexChanged -= this.OnColorPickerSelectedIndexChanged;
            }
        }
        #endregion
    }
}
