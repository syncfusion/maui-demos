#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Buttons;
using Syncfusion.Maui.Inputs;
using Syncfusion.Maui.Picker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SampleBrowser.Maui.LiquidGlass.SfDatePicker
{
    internal class DatePickerPageBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Picker view 
        /// </summary>
        private Syncfusion.Maui.Picker.SfDatePicker? datePicker;

        /// <summary>
        /// The show header switch
        /// </summary>
        private Syncfusion.Maui.Buttons.SfSwitch? showHeaderSwitch, showColumnHeaderSwitch, showFooterSwitch, showEnableLoopingSwitch;

        /// <summary>
        /// The date format combo box.
        /// </summary>
        private Syncfusion.Maui.Inputs.SfComboBox? formatComboBox;

        /// <summary>
        /// The date format to set the combo box item source.
        /// </summary>
        private ObservableCollection<object>? formats;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);
            this.datePicker = sampleView.Content.FindByName<Syncfusion.Maui.Picker.SfDatePicker>("DatePicker");
            this.datePicker.HeaderView.Height = 50;
            this.datePicker.HeaderView.Text = "Select a Date";
            this.datePicker.BlackoutDates = new ObservableCollection<DateTime>()
            {
                DateTime.Now.AddDays(2),
                DateTime.Now.AddDays(5),
                DateTime.Now.AddDays(8),
                DateTime.Now.AddDays(-3),
                DateTime.Now.AddDays(-4),
                DateTime.Now.AddDays(-8),
                DateTime.Now.AddMonths(1).AddDays(1),
                DateTime.Now.AddMonths(1).AddDays(3),
                DateTime.Now.AddMonths(1).AddDays(6),
                DateTime.Now.AddMonths(1).AddDays(-2),
                DateTime.Now.AddMonths(1).AddDays(-5),
                DateTime.Now.AddMonths(1).AddDays(-6),
                DateTime.Now.AddMonths(-1).AddDays(2),
                DateTime.Now.AddMonths(-1).AddDays(5),
                DateTime.Now.AddMonths(-1).AddDays(1),
                DateTime.Now.AddMonths(-1).AddDays(-7),
                DateTime.Now.AddMonths(-1).AddDays(-1),
                DateTime.Now.AddMonths(-1).AddDays(-6),
            };
            this.showHeaderSwitch = sampleView.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("showHeaderSwitch");
            this.showColumnHeaderSwitch = sampleView.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("showColumnHeaderSwitch");
            this.showFooterSwitch = sampleView.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("showFooterSwitch");
            this.showEnableLoopingSwitch = sampleView.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("enableLoopingSwitch");

            formats = new ObservableCollection<object>()
            {
                 "dd MM", "dd MM yyyy", "dd MMM yyyy", "M d yyyy", "MM dd yyyy", "MM yyyy", "MMM yyyy", "yyyy MM dd", "Default"
            };

            this.formatComboBox = sampleView.Content.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("formatComboBox");
            this.formatComboBox.ItemsSource = formats;
            this.formatComboBox.SelectedIndex = 1;
            this.formatComboBox.SelectionChanged += FormatComboBox_SelectionChanged;

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

            if (this.showEnableLoopingSwitch != null)
            {
                this.showEnableLoopingSwitch.StateChanged += ShowEnableLoopingSwitch_Toggled;
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
                    DatePickerColumnHeaderView datePickerColumnHeaderView = new DatePickerColumnHeaderView()
                    {
                        Height = 40,
                        DayHeaderText = "Day",
                        MonthHeaderText = "Month",
                        YearHeaderText = "Year",
                    };

#if MACCATALYST || IOS
                    datePickerColumnHeaderView.Background = new SolidColorBrush(Colors.Transparent);
#endif
                    this.datePicker.ColumnHeaderView = datePickerColumnHeaderView;
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
        /// Method for enable looping switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowEnableLoopingSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.datePicker != null && e.NewValue.HasValue)
            {
                if (e.NewValue.Value == true)
                {
                    this.datePicker.EnableLooping = true;
                }
                else if (e.NewValue.Value == false)
                {
                    this.datePicker.EnableLooping = false;
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

            if (this.showEnableLoopingSwitch != null)
            {
                this.showEnableLoopingSwitch.StateChanged -= ShowEnableLoopingSwitch_Toggled;
                this.showEnableLoopingSwitch = null;
            }
        }
    }
}
