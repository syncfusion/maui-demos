#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Picker.SfDatePicker
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Buttons;
    using Syncfusion.Maui.Inputs;
    using Syncfusion.Maui.Picker;
    using System.Collections.ObjectModel;

    public class GettingStartedBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Picker view 
        /// </summary>
        private SfDatePicker? datePicker;

        /// <summary>
        /// The show header switch
        /// </summary>
        private SfSwitch? showHeaderSwitch, showColumnHeaderSwitch, showFooterSwitch, clearSelectionSwitch;

        /// <summary>
        /// The date format combo box.
        /// </summary>
        private SfComboBox? formatComboBox, textDisplayComboBox;

        /// <summary>
        /// The date format to set the combo box item source.
        /// </summary>
        private ObservableCollection<object>? formats;

        /// <summary>
        /// The text display to set the combo box item source.
        /// </summary>
        private ObservableCollection<object>? textDisplay;

        /// <summary>
        /// Check the application theme is light or dark.
        /// </summary>
        private bool isLightTheme = Application.Current?.RequestedTheme == AppTheme.Light;

        /// <summary>
        /// Get the previous selected date from date picker.
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
            this.datePicker = sampleView.Content.FindByName<SfDatePicker>("DatePicker1");
#else
            Frame frame = sampleView.Content.FindByName<Frame>("frame");
            frame.IsVisible = true;
            frame.BorderColor = isLightTheme ? Color.FromArgb("#CAC4D0") : Color.FromArgb("#49454F");
            this.datePicker = sampleView.Content.FindByName<SfDatePicker>("DatePicker");
#endif

            this.previousDate = this.datePicker.SelectedDate;
            this.datePicker.HeaderView.Height = 50;
            this.datePicker.HeaderView.Text = "Select a Date";
            this.datePicker.SelectionChanged += DatePicker_SelectionChanged;
            this.showHeaderSwitch = sampleView.Content.FindByName<SfSwitch>("showHeaderSwitch");
            this.showColumnHeaderSwitch = sampleView.Content.FindByName<SfSwitch>("showColumnHeaderSwitch");
            this.showFooterSwitch = sampleView.Content.FindByName<SfSwitch>("showFooterSwitch");
            this.clearSelectionSwitch = sampleView.Content.FindByName<SfSwitch>("clearSelectionSwitch");
            this.datePicker.SelectedTextStyle.TextColor = isLightTheme ? Color.FromArgb("#FFFFFF") : Color.FromArgb("#381E72");

            formats = new ObservableCollection<object>()
            {
                 "dd MM", "dd MM yyyy", "dd MMM yyyy", "M d yyyy", "MM dd yyyy", "MM yyyy", "MMM yyyy", "yyyy MM dd", "Default"
            };

            this.formatComboBox = sampleView.Content.FindByName<SfComboBox>("formatComboBox");
            this.formatComboBox.ItemsSource = formats;
            this.formatComboBox.SelectedIndex = 1;
            this.formatComboBox.SelectionChanged += FormatComboBox_SelectionChanged;

            textDisplay = new ObservableCollection<object>()
            {
                "Default", "Fade", "Shrink", "FadeAndShrink"
            };

            this.textDisplayComboBox = sampleView.Content.FindByName<SfComboBox>("textDisplayComboBox");
            this.textDisplayComboBox.ItemsSource = textDisplay;
            this.textDisplayComboBox.SelectedIndex = 0;
            this.textDisplayComboBox.SelectionChanged += TextDisplayComboBox_SelectionChanged;

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

        /// <summary>
        /// Method for date picker selection changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void DatePicker_SelectionChanged(object? sender, DatePickerSelectionChangedEventArgs e)
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
            if (this.datePicker != null && e.NewValue.HasValue)
            {
                if (e.NewValue.Value == true)
                {
                    this.datePicker.HeaderView.Height = 50;
                    this.datePicker.HeaderView.Text = "Select a Date";
                }
                else if (e.NewValue.Value == false)
                {
                    this.datePicker.HeaderView.Height = 0;
                }
            }
        }

        /// <summary>
        /// Method for show column header switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowColumnHeaderSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.datePicker != null && e.NewValue.HasValue)
            {
                if (e.NewValue.Value == true)
                {
                    this.datePicker.ColumnHeaderView = new DatePickerColumnHeaderView()
                    {
                        Height = 40,
                        DayHeaderText = "Day",
                        MonthHeaderText = "Month",
                        YearHeaderText = "Year",
                    };
                }
                if (e.NewValue.Value == false)
                {
                    this.datePicker.ColumnHeaderView = new DatePickerColumnHeaderView()
                    {
                        Height = 0,
                    };
                }
            }
        }

        /// <summary>
        /// Method for show footer switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowFooterSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.datePicker != null && e.NewValue.HasValue)
            {
                if (e.NewValue.Value == true)
                {
                    this.datePicker.FooterView.Height = 40;
                }
                else if (e.NewValue.Value == false)
                {
                    this.datePicker.FooterView.Height = 0;
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
            if (this.datePicker != null && e.NewValue.HasValue)
            {
                if (e.NewValue.Value == true)
                {
                    this.datePicker.SelectedDate = null;
                }
                else if (e.NewValue.Value == false)
                {
                    this.datePicker.SelectedDate = this.previousDate;
                }
            }
        }

        /// <summary>
        /// The format combo box selection changed event.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void FormatComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.datePicker == null || e.AddedItems == null || formatComboBox == null)
            {
                return;
            }

            string? format = e.AddedItems[0].ToString();
            switch (format)
            {
                case "dd MM":
                    this.datePicker.Format = PickerDateFormat.dd_MM;
                    break;

                case "dd MM yyyy":
                    this.datePicker.Format = PickerDateFormat.dd_MM_yyyy;
                    break;

                case "dd MMM yyyy":
                    this.datePicker.Format = PickerDateFormat.dd_MMM_yyyy;
                    break;

                case "M d yyyy":
                    this.datePicker.Format = PickerDateFormat.M_d_yyyy;
                    break;

                case "MM dd yyyy":
                    this.datePicker.Format = PickerDateFormat.MM_dd_yyyy;
                    break;

                case "MM yyyy":
                    this.datePicker.Format = PickerDateFormat.MM_yyyy;
                    break;

                case "MMM yyyy":
                    this.datePicker.Format = PickerDateFormat.MMM_yyyy;
                    break;

                case "yyyy MM dd":
                    this.datePicker.Format = PickerDateFormat.yyyy_MM_dd;
                    break;
                case "Default":
                    this.datePicker.Format = PickerDateFormat.Default;
                    break;
            }
        }

        /// <summary>
        /// The text display combo box selection changed event.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void TextDisplayComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            if (this.datePicker == null || e.AddedItems == null || textDisplayComboBox == null)
            {
                return;
            }

            string? format = e.AddedItems[0].ToString();
            switch (format)
            {
                case "Default":
                    this.datePicker.TextDisplayMode = PickerTextDisplayMode.Default;
                    break;

                case "Fade":
                    this.datePicker.TextDisplayMode = PickerTextDisplayMode.Fade;
                    break;

                case "Shrink":
                    this.datePicker.TextDisplayMode = PickerTextDisplayMode.Shrink;
                    break;

                case "FadeAndShrink":
                    this.datePicker.TextDisplayMode = PickerTextDisplayMode.FadeAndShrink;
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