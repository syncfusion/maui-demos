#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
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
        private SfSwitch? showHeaderSwitch, showColumnHeaderSwitch, showFooterSwitch, clearSelectionSwitch;

        /// <summary>
        /// The date format combo box.
        /// </summary>
        private SfComboBox? dateFormatComboBox, timeFormatComboBox, textDisplayComboBox;

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
        /// Check the application theme is light or dark.
        /// </summary>
        private bool isLightTheme = Application.Current?.RequestedTheme == AppTheme.Light;

        /// <summary>
        /// Get the previous selected date and time from date time picker.
        /// </summary>
        private DateTime? previousDate;

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
            this.dateTimePicker.SelectionChanged += DateTimePicker_SelectionChanged;
            this.showHeaderSwitch = sampleView.Content.FindByName<SfSwitch>("showHeaderSwitch");
            this.showColumnHeaderSwitch = sampleView.Content.FindByName<SfSwitch>("showColumnHeaderSwitch");
            this.showFooterSwitch = sampleView.Content.FindByName<SfSwitch>("showFooterSwitch");
            this.clearSelectionSwitch = sampleView.Content.FindByName<SfSwitch>("clearSelectionSwitch");
            this.dateTimePicker.SelectedTextStyle.TextColor = isLightTheme ? Color.FromArgb("#FFFFFF") : Color.FromArgb("#381E72");

            dateFormat = new ObservableCollection<object>()
            {
                 "dd MM", "dd MM yyyy", "dd MMM yyyy", "M d yyyy", "MM dd yyyy", "MM yyyy", "MMM yyyy", "yyyy MM dd", "Default"
            };

            timeFormat = new ObservableCollection<object>()
            {
                "H mm", "H mm ss", "h mm ss tt", "h mm tt", "HH mm", "HH mm ss", "hh mm ss tt", "hh mm tt", "hh tt", "Default"
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

                case "hh mm ss tt":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.hh_mm_ss_tt;
                    break;

                case "hh mm tt":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.hh_mm_tt;
                    break;

                case "hh tt":
                    this.dateTimePicker.TimeFormat = PickerTimeFormat.hh_tt;
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
        }

        public GettingStartedBehavior()
        {
        }
    }
}