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

namespace SampleBrowser.Maui.LiquidGlass.SfDateTimePicker
{
    internal class DateTimePickerPageBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Picker view 
        /// </summary>
        private Syncfusion.Maui.Picker.SfDateTimePicker? dateTimePicker;

        /// <summary>
        /// The show header switch
        /// </summary>
        private Syncfusion.Maui.Buttons.SfSwitch? showHeaderSwitch, showColumnHeaderSwitch, showFooterSwitch, showEnableLoopingSwitch;

        /// <summary>
        /// The date format combo box.
        /// </summary>
        private Syncfusion.Maui.Inputs.SfComboBox? dateFormatComboBox, timeFormatComboBox;

        /// <summary>
        /// The date format to set the combo box item source.
        /// </summary>
        private ObservableCollection<object>? dateFormat;

        /// <summary>
        /// The time format to set the combo box item source.
        /// </summary>
        private ObservableCollection<object>? timeFormat;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);
            this.dateTimePicker = sampleView.Content.FindByName<Syncfusion.Maui.Picker.SfDateTimePicker>("DateTimePicker");
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
            this.showHeaderSwitch = sampleView.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("showHeaderSwitch");
            this.showColumnHeaderSwitch = sampleView.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("showColumnHeaderSwitch");
            this.showFooterSwitch = sampleView.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("showFooterSwitch");
            this.showEnableLoopingSwitch = sampleView.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("enableLoopingSwitch");

            dateFormat = new ObservableCollection<object>()
            {
                 "dd MM", "dd MM yyyy", "dd MMM yyyy", "M d yyyy", "MM dd yyyy", "MM yyyy", "MMM yyyy", "yyyy MM dd", "Default"
            };

            timeFormat = new ObservableCollection<object>()
            {
                "H mm", "H mm ss", "h mm ss tt", "h mm tt", "HH mm", "HH mm ss", "HH mm ss fff", "hh mm ss tt", "hh mm ss fff tt", "hh mm tt", "hh tt", "ss fff", "mm ss", "mm ss fff", "Default"
            };

            this.dateFormatComboBox = sampleView.Content.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("dateFormatComboBox");
            this.dateFormatComboBox.ItemsSource = dateFormat;
            this.dateFormatComboBox.SelectedIndex = 1;
            this.dateFormatComboBox.SelectionChanged += DateFormatComboBox_SelectionChanged;

            this.timeFormatComboBox = sampleView.Content.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("timeFormatComboBox");
            this.timeFormatComboBox.ItemsSource = timeFormat;
            this.timeFormatComboBox.SelectedIndex = 1;
            this.timeFormatComboBox.SelectionChanged += TimeFormatComboBox_SelectionChanged;

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
