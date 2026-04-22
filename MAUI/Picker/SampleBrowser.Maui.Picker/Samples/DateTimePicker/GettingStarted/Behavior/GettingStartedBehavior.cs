#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Picker.SfDateTimePicker
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Inputs;
    using Syncfusion.Maui.Picker;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Syncfusion.Maui.Buttons;

    public class GettingStartedBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Picker view 
        /// </summary>
        private SfDateTimePicker? dateTimePicker;

        /// <summary>
        /// The show header switch
        /// </summary>
        private SfSwitch? showHeaderSwitch, showColumnHeaderSwitch, showFooterSwitch, clearSelectionSwitch, showEnableLoopingSwitch;

        /// <summary>
        /// The date format combo box.
        /// </summary>
        private SfComboBox? dateFormatComboBox, timeFormatComboBox, textDisplayComboBox, activeViewComboBox;

        /// <summary>
        /// The date format to set the combo box item source.
        /// </summary>
        private ObservableCollection<object>? dateFormat;

        /// <summary>
        /// The time format to set the combo box item source.
        /// </summary>
        private ObservableCollection<object>? timeFormat;

        /// <summary>
        /// The text display to set the combo box item source.
        /// </summary>
        private ObservableCollection<object>? textDisplay;

        /// <summary>
        /// the active view to set the combo box item source.
        /// </summary>
        private ObservableCollection<object>? activeView;

        /// <summary>
        /// Check the application theme is light or dark.
        /// </summary>
        private bool isLightTheme = Application.Current?.RequestedTheme == AppTheme.Light;

        /// <summary>
        /// Get the previous selected date and time from date time picker.
        /// </summary>
        private DateTime? previousDate;

        /// <summary>
        /// Internal flag to avoid event recursion when syncing ActiveView.
        /// </summary>
        private bool isInternalActiveViewUpdate;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);

#if IOS || MACCATALYST
            Border border = sampleView.Content.FindByName<Border>("border");
            border.IsVisible = true;
            border.Stroke = isLightTheme ? Color.FromArgb("#CAC4D0") : Color.FromArgb("#49454F");
            this.dateTimePicker = sampleView.Content.FindByName<SfDateTimePicker>("DateTimePicker1");
#else
            Border frame = sampleView.Content.FindByName<Border>("frame");
            frame.IsVisible = true;
            frame.Stroke = isLightTheme ? Color.FromArgb("#CAC4D0") : Color.FromArgb("#49454F");
            this.dateTimePicker = sampleView.Content.FindByName<SfDateTimePicker>("DateTimePicker");
#endif

            this.previousDate = this.dateTimePicker.SelectedDate;
            this.dateTimePicker.BlackoutDateTimes = new ObservableCollection<DateTime>()
            {
                DateTime.Today.AddDays(2),
                DateTime.Today.AddDays(5),
                DateTime.Today.AddDays(8),
                DateTime.Today.AddDays(-3),
                DateTime.Today.AddDays(-4),
                DateTime.Today.AddDays(-8),
                DateTime.Now.AddHours(1).AddMinutes(2),
                DateTime.Now.AddHours(1).AddMinutes(4),
                DateTime.Now.AddHours(1).AddMinutes(5),
                DateTime.Now.AddHours(1).AddMinutes(-1),
                DateTime.Now.AddHours(1).AddMinutes(-3),
                DateTime.Now.AddHours(1).AddMinutes(-9),
                DateTime.Now.AddHours(1).AddMinutes(1),
                DateTime.Now.AddHours(-1).AddMinutes(2),
                DateTime.Now.AddHours(-1).AddMinutes(6),
                DateTime.Now.AddHours(-1).AddMinutes(-2),
                DateTime.Now.AddHours(-1).AddMinutes(-9),
                DateTime.Now.AddHours(-1).AddMinutes(-7),
                DateTime.Now.AddMinutes(1),
                DateTime.Now.AddMinutes(2),
                DateTime.Now.AddMinutes(4),
                DateTime.Now.AddMinutes(-8),
                DateTime.Now.AddMinutes(-4),
                DateTime.Now.AddMinutes(-1)
            };
            this.dateTimePicker.SelectionChanged += DateTimePicker_SelectionChanged;
            this.dateTimePicker.PropertyChanged += DateTimePicker_PropertyChanged;
            this.showHeaderSwitch = sampleView.Content.FindByName<SfSwitch>("showHeaderSwitch");
            this.showColumnHeaderSwitch = sampleView.Content.FindByName<SfSwitch>("showColumnHeaderSwitch");
            this.showFooterSwitch = sampleView.Content.FindByName<SfSwitch>("showFooterSwitch");
            this.clearSelectionSwitch = sampleView.Content.FindByName<SfSwitch>("clearSelectionSwitch");
            this.showEnableLoopingSwitch = sampleView.Content.FindByName<SfSwitch>("enableLoopingSwitch");
            this.dateTimePicker.SelectedTextStyle.TextColor = isLightTheme ? Color.FromArgb("#FFFFFF") : Color.FromArgb("#381E72");

            dateFormat = new ObservableCollection<object>()
            {
                 "dd MM", "dd MM yyyy", "dd MMM yyyy", "M d yyyy", "MM dd yyyy", "MM yyyy", "MMM yyyy", "yyyy MM dd","MM dd", "MMM dd yyyy", "MMMM dd yyyy", "MMMM yyyy", "yyyy MM", "yyyy MMM", "yyyy MMMM", "yyyy MMM dd", "yyyy MMMM dd", "dd MMM", "dd MMMM", "dd MMMM yyyy", "ddd dd MM YYYY", "Default"
            };

            timeFormat = new ObservableCollection<object>()
            {
                "H mm", "H mm ss", "h mm ss tt", "h mm tt", "HH mm", "HH mm ss", "HH mm ss fff", "hh mm ss tt", "hh mm ss fff tt", "hh mm tt", "hh tt", "ss fff", "mm ss", "mm ss fff", "Default"
            };

            this.dateFormatComboBox = sampleView.Content.FindByName<SfComboBox>("dateFormatComboBox");
            this.dateFormatComboBox.ItemsSource = dateFormat;
            this.dateFormatComboBox.SelectedIndex = 1;
            this.dateFormatComboBox.SelectionChanged += DateFormatComboBox_SelectionChanged;

            this.timeFormatComboBox = sampleView.Content.FindByName<SfComboBox>("timeFormatComboBox");
            this.timeFormatComboBox.ItemsSource = timeFormat;
            this.timeFormatComboBox.SelectedIndex = 1;
            this.timeFormatComboBox.SelectionChanged += TimeFormatComboBox_SelectionChanged;

            textDisplay = new ObservableCollection<object>()
            {
                "Default", "Fade", "Shrink", "FadeAndShrink"
            };

            this.textDisplayComboBox = sampleView.Content.FindByName<SfComboBox>("textDisplayComboBox");
            this.textDisplayComboBox.ItemsSource = textDisplay;
            this.textDisplayComboBox.SelectedIndex = 0;
            this.textDisplayComboBox.SelectionChanged += TextdisplayComboBox_SelectionChanged;

            activeView = new ObservableCollection<object>()
            {
                "Date", "Time"
            };

            this.activeViewComboBox = sampleView.Content.FindByName<SfComboBox>("activeViewComboBox");
            this.activeViewComboBox.ItemsSource = activeView;
            this.activeViewComboBox.SelectedIndex = 0;
            this.activeViewComboBox.SelectionChanged += ActiveViewComboBox_SelectionChanged;

            // Sync initial state from picker to combo box
            SyncActiveViewToComboBox();

            if (this.showHeaderSwitch != null)
            {
                this.showHeaderSwitch.StateChanged += ShowHeaderSwitch_Toggled;
            }

            if (this.showColumnHeaderSwitch != null)
            {
                this.showColumnHeaderSwitch.StateChanged += ShowColumnHeaderSwitch_Toggled;
            }

            if (this.showFooterSwitch != null)
            {
                this.showFooterSwitch.StateChanged += ShowFooterSwitch_Toggled;
            }

            if (this.clearSelectionSwitch != null)
            {
                this.clearSelectionSwitch.StateChanged += ClearSelectionSwitch_Toggled;
            }

            if (this.showEnableLoopingSwitch != null)
            {
                this.showEnableLoopingSwitch.StateChanged += ShowEnableLoopingSwitch_Toggled;
            }
        }

        private void DateTimePicker_SelectionChanged(object? sender, DateTimePickerSelectionChangedEventArgs e)
        {
            if (this.clearSelectionSwitch != null && e.NewValue != null)
            {
                this.previousDate = e.NewValue.Value;
                this.clearSelectionSwitch.IsOn = false;
            }
        }

        /// <summary>
        /// Method for show header switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowHeaderSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.dateTimePicker != null && e.NewValue.HasValue)
            {
                this.dateTimePicker.HeaderView.Height = e.NewValue.Value == true ? 50 : 0;
            }
        }

        /// <summary>
        /// Method for show column header switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowColumnHeaderSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.dateTimePicker != null && e.NewValue.HasValue)
            {
                this.dateTimePicker.ColumnHeaderView.Height = e.NewValue.Value == true ? 40 : 0;
            }
        }

        /// <summary>
        /// Method for show footer switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowFooterSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.dateTimePicker != null && e.NewValue.HasValue)
            {
                if (e.NewValue.Value == true)
                {
                    this.dateTimePicker.FooterView.Height = 40;
                }
                else if (e.NewValue.Value == false)
                {
                    this.dateTimePicker.FooterView.Height = 0;
                }
            }
        }

        /// <summary>
        /// Method for clear selection switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ClearSelectionSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.dateTimePicker != null && e.NewValue.HasValue)
            {
                if (e.NewValue.Value == true)
                {
                    this.dateTimePicker.SelectedDate = null;
                }
                else if (e.NewValue.Value == false)
                {
                    this.dateTimePicker.SelectedDate = this.previousDate;
                }
            }
        }

        /// <summary>
        /// Method for enable looping switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowEnableLoopingSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.dateTimePicker != null && e.NewValue.HasValue)
            {
                if (e.NewValue.Value == true)
                {
                    this.dateTimePicker.EnableLooping = true;
                }
                else if (e.NewValue.Value == false)
                {
                    this.dateTimePicker.EnableLooping = false;
                }
            }
        }

        /// <summary>
        /// The format combo box selection changed event.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void DateFormatComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.dateTimePicker == null || e.AddedItems == null || dateFormatComboBox == null)
            {
                return;
            }

            string? format = e.AddedItems[0].ToString();
            switch (format)
            {
                case "dd MM":
                    this.dateTimePicker.DateFormat = PickerDateFormat.dd_MM;
                    break;

                case "dd MM yyyy":
                    this.dateTimePicker.DateFormat = PickerDateFormat.dd_MM_yyyy;
                    break;

                case "dd MMM yyyy":
                    this.dateTimePicker.DateFormat = PickerDateFormat.dd_MMM_yyyy;
                    break;

                case "M d yyyy":
                    this.dateTimePicker.DateFormat = PickerDateFormat.M_d_yyyy;
                    break;

                case "MM dd yyyy":
                    this.dateTimePicker.DateFormat = PickerDateFormat.MM_dd_yyyy;
                    break;

                case "MM yyyy":
                    this.dateTimePicker.DateFormat = PickerDateFormat.MM_yyyy;
                    break;

                case "MMM yyyy":
                    this.dateTimePicker.DateFormat = PickerDateFormat.MMM_yyyy;
                    break;

                case "yyyy MM dd":
                    this.dateTimePicker.DateFormat = PickerDateFormat.yyyy_MM_dd;
                    break;

                case "MM dd":
                    this.dateTimePicker.DateFormat = PickerDateFormat.MM_dd;
                    break;
                case "MMM dd yyyy":
                    this.dateTimePicker.DateFormat = PickerDateFormat.MMM_dd_yyyy;
                    break;

                case "MMMM dd yyyy":
                    this.dateTimePicker.DateFormat = PickerDateFormat.MMMM_dd_yyyy;
                    break;
                case "MMMM yyyy":
                    this.dateTimePicker.DateFormat = PickerDateFormat.MMMM_yyyy;
                    break;

                case "yyyy MM":
                    this.dateTimePicker.DateFormat = PickerDateFormat.yyyy_MM;
                    break;

                case "yyyy MMM":
                    this.dateTimePicker.DateFormat = PickerDateFormat.yyyy_MMM;
                    break;

                case "yyyy MMMM":
                    this.dateTimePicker.DateFormat = PickerDateFormat.yyyy_MMMM;
                    break;

                case "yyyy MMM dd":
                    this.dateTimePicker.DateFormat = PickerDateFormat.yyyy_MMM_dd;
                    break;

                case "yyyy MMMM dd":
                    this.dateTimePicker.DateFormat = PickerDateFormat.yyyy_MMMM_dd;
                    break;

                case "dd MMM":
                    this.dateTimePicker.DateFormat = PickerDateFormat.dd_MMM;
                    break;

                case "dd MMMM":
                    this.dateTimePicker.DateFormat = PickerDateFormat.dd_MMMM;
                    break;

                case "dd MMMM yyyy":
                    this.dateTimePicker.DateFormat = PickerDateFormat.dd_MMMM_yyyy;
                    break;

                case "ddd dd MM YYYY":
                    this.dateTimePicker.DateFormat = PickerDateFormat.ddd_dd_MM_YYYY;
                    break;

                case "Default":
                    this.dateTimePicker.DateFormat = PickerDateFormat.Default;
                    break;
            }

        }

        /// <summary>
        /// The format combo box selection changed event.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void TimeFormatComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.dateTimePicker == null || e.AddedItems == null || timeFormatComboBox == null)
            {
                return;
            }

            string? format = e.AddedItems[0].ToString();

            switch (format)
            {
                case "H mm":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.H_mm;
                    break;

                case "H mm ss":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.H_mm_ss;
                    break;

                case "h mm ss tt":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.h_mm_ss_tt;
                    break;

                case "h mm tt":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.h_mm_tt;
                    break;

                case "HH mm":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.HH_mm;
                    break;

                case "HH mm ss":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.HH_mm_ss;
                    break;

                case "HH mm ss fff":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.HH_mm_ss_fff;
                    break;

                case "hh mm ss tt":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.hh_mm_ss_tt;
                    break;

                case "hh mm ss fff tt":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.hh_mm_ss_fff_tt;
                    break;

                case "hh mm tt":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.hh_mm_tt;
                    break;

                case "hh tt":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.hh_tt;
                    break;

                case "ss fff":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.ss_fff;
                    break;

                case "mm ss":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.mm_ss;
                    break;

                case "mm ss fff":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.mm_ss_fff;
                    break;

                case "Default":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.Default;
                    break;
            }
        }

        /// <summary>
        /// The text display combo box selection changed event.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void TextdisplayComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.dateTimePicker == null || e.AddedItems == null || textDisplayComboBox == null)
            {
                return;
            }

            string? format = e.AddedItems[0].ToString();
            switch (format)
            {
                case "Default":
                    this.dateTimePicker.TextDisplayMode = PickerTextDisplayMode.Default;
                    break;

                case "Fade":
                    this.dateTimePicker.TextDisplayMode = PickerTextDisplayMode.Fade;
                    break;

                case "Shrink":
                    this.dateTimePicker.TextDisplayMode = PickerTextDisplayMode.Shrink;
                    break;

                case "FadeAndShrink":
                    this.dateTimePicker.TextDisplayMode = PickerTextDisplayMode.FadeAndShrink;
                    break;

            }
        }

        /// <summary>
        /// The Active view combo box selection changed event.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ActiveViewComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.isInternalActiveViewUpdate)
            {
                return;
            }

            if (this.dateTimePicker == null || e.AddedItems == null || activeViewComboBox == null)
            {
                return;
            }

            string? format = e.AddedItems[0].ToString();
            try
            {
                this.isInternalActiveViewUpdate = true;
                switch (format)
                {
                    case "Date":
                        this.dateTimePicker.ActiveView = DateTimePickerView.Date;
                        break;

                    case "Time":
                        this.dateTimePicker.ActiveView = DateTimePickerView.Time;
                        break;
                }
            }
            finally
            {
                this.isInternalActiveViewUpdate = false;
            }
        }

        /// <summary>
        /// Handles property changes on the <see cref="SfDateTimePicker"/>.
        /// When <see cref="SfDateTimePicker.ActiveView"/> changes, this method
        /// synchronizes the value into the UI combo box unless the change was
        /// initiated internally by the behavior.
        /// </summary>
        /// <param name="sender">The <see cref="SfDateTimePicker"/> raising the event.</param>
        /// <param name="e">Property change event arguments.</param>
        private void DateTimePicker_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SfDateTimePicker.ActiveView))
            {
                if (this.isInternalActiveViewUpdate)
                {
                    return;
                }

                SyncActiveViewToComboBox();
            }
        }

        /// <summary>
        /// Updates the `activeViewComboBox` selection to match the
        /// <see cref="SfDateTimePicker.ActiveView"/> value.
        /// Uses an internal guard (<see cref="isInternalActiveViewUpdate"/>) to
        /// avoid recursive events when the selection is set programmatically.
        /// </summary>
        private void SyncActiveViewToComboBox()
        {
            if (this.dateTimePicker == null || this.activeViewComboBox == null)
            {
                return;
            }

            var desired = this.dateTimePicker.ActiveView == DateTimePickerView.Date ? "Date" : "Time";
            if (object.Equals(this.activeViewComboBox.SelectedItem, desired))
            {
                return;
            }

            try
            {
                this.isInternalActiveViewUpdate = true;
                this.activeViewComboBox.SelectedItem = desired;
            }
            finally
            {
                this.isInternalActiveViewUpdate = false;
            }
        }

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnDetachingFrom(SampleView sampleView)
        {
            base.OnDetachingFrom(sampleView);
            if (this.showHeaderSwitch != null)
            {
                this.showHeaderSwitch.StateChanged -= ShowHeaderSwitch_Toggled;
                this.showHeaderSwitch = null;
            }

            if (this.showColumnHeaderSwitch != null)
            {
                this.showColumnHeaderSwitch.StateChanged -= ShowColumnHeaderSwitch_Toggled;
                this.showColumnHeaderSwitch = null;
            }

            if (this.showFooterSwitch != null)
            {
                this.showFooterSwitch.StateChanged -= ShowFooterSwitch_Toggled;
                this.showFooterSwitch = null;
            }

            if (this.clearSelectionSwitch != null)
            {
                this.clearSelectionSwitch.StateChanged -= ClearSelectionSwitch_Toggled;
                this.clearSelectionSwitch = null;
            }

            if (this.showEnableLoopingSwitch != null)
            {
                this.showEnableLoopingSwitch.StateChanged -= ShowEnableLoopingSwitch_Toggled;
                this.showEnableLoopingSwitch = null;
            }

            if (this.activeViewComboBox != null)
            {
                this.activeViewComboBox.SelectionChanged -= ActiveViewComboBox_SelectionChanged;
                this.activeViewComboBox = null;
            }

            if (this.dateTimePicker != null)
            {
                this.dateTimePicker.SelectionChanged -= DateTimePicker_SelectionChanged;
                this.dateTimePicker.PropertyChanged -= DateTimePicker_PropertyChanged;
                this.dateTimePicker = null;
            }
        }

        public GettingStartedBehavior()
        {
        }
    }
}