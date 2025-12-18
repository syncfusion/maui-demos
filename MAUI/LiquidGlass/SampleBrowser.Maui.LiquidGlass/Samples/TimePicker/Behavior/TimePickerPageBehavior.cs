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

namespace SampleBrowser.Maui.LiquidGlass.SfTimePicker
{
    internal class TimePickerPageBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Picker view 
        /// </summary>
        private Syncfusion.Maui.Picker.SfTimePicker? timePicker;

        /// <summary>
        /// The show header switch
        /// </summary>
        private Syncfusion.Maui.Buttons.SfSwitch? showHeaderSwitch, showColumnHeaderSwitch, showFooterSwitch, showEnableLoopingSwitch;

        /// <summary>
        /// The date format combo box.
        /// </summary>
        private Syncfusion.Maui.Inputs.SfComboBox? formatComboBox;

        /// <summary>
        /// The time format to set the combo box item source.
        /// </summary>
        private ObservableCollection<object>? formats;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.timePicker = bindable.Content.FindByName<Syncfusion.Maui.Picker.SfTimePicker>("TimePicker");
            this.timePicker.HeaderView.Height = 50;
            this.timePicker.HeaderView.Text = "Select a Time";
            this.timePicker.BlackoutTimes = new ObservableCollection<TimeSpan>()
            {
                DateTime.Now.AddHours(1).AddMinutes(2).TimeOfDay,
                DateTime.Now.AddHours(1).AddMinutes(4).TimeOfDay,
                DateTime.Now.AddHours(1).AddMinutes(5).TimeOfDay,
                DateTime.Now.AddHours(1).AddMinutes(-1).TimeOfDay,
                DateTime.Now.AddHours(1).AddMinutes(-3).TimeOfDay,
                DateTime.Now.AddHours(1).AddMinutes(-9).TimeOfDay,
                DateTime.Now.AddHours(1).AddMinutes(1).TimeOfDay,
                DateTime.Now.AddHours(-1).AddMinutes(2).TimeOfDay,
                DateTime.Now.AddHours(-1).AddMinutes(6).TimeOfDay,
                DateTime.Now.AddHours(-1).AddMinutes(-2).TimeOfDay,
                DateTime.Now.AddHours(-1).AddMinutes(-9).TimeOfDay,
                DateTime.Now.AddHours(-1).AddMinutes(-7).TimeOfDay,
                DateTime.Now.AddMinutes(1).TimeOfDay,
                DateTime.Now.AddMinutes(2).TimeOfDay,
                DateTime.Now.AddMinutes(4).TimeOfDay,
                DateTime.Now.AddMinutes(-8).TimeOfDay,
                DateTime.Now.AddMinutes(-4).TimeOfDay,
                DateTime.Now.AddMinutes(-1).TimeOfDay,
            };
            this.showHeaderSwitch = bindable.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("showHeaderSwitch");
            this.showColumnHeaderSwitch = bindable.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("showColumnHeaderSwitch");
            this.showFooterSwitch = bindable.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("showFooterSwitch");
            this.showEnableLoopingSwitch = bindable.Content.FindByName<Syncfusion.Maui.Buttons.SfSwitch>("enableLoopingSwitch");

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

            formats = new ObservableCollection<object>()
            {
                "H mm", "H mm ss", "h mm ss tt", "h mm tt", "HH mm", "HH mm ss", "HH mm ss fff", "hh mm ss tt", "hh mm ss fff tt", "hh mm tt", "hh tt", "ss fff", "mm ss", "mm ss fff", "Default"
            };

            this.formatComboBox = bindable.Content.FindByName<Syncfusion.Maui.Inputs.SfComboBox>("formatComboBox");
            this.formatComboBox.ItemsSource = formats;
            this.formatComboBox.SelectedIndex = 1;
            this.formatComboBox.SelectionChanged += FormatComboBox_SelectionChanged;
        }

        /// <summary>
        /// Method for show header switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowHeaderSwitch_Toggled(object? sender, SwitchStateChangedEventArgs e)
        {
            if (this.timePicker != null && e.NewValue.HasValue)
            {
                if (e.NewValue.Value == true)
                {
                    this.timePicker.HeaderView.Height = 50;
                    this.timePicker.HeaderView.Text = "Select a time";
                }
                else if (e.NewValue.Value == false)
                {
                    this.timePicker.HeaderView.Height = 0;
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
            if (this.timePicker != null && e.NewValue.HasValue)
            {
                if (e.NewValue.Value == true)
                {
                    TimePickerColumnHeaderView timePickerColumnHeaderView = new TimePickerColumnHeaderView()
                    {
                        Height = 40,
                    };
#if MACCATALYST || IOS
                    timePickerColumnHeaderView.Background = new SolidColorBrush(Colors.Transparent);
#endif
                    this.timePicker.ColumnHeaderView = timePickerColumnHeaderView;
                }
                if (e.NewValue.Value == false)
                {
                    this.timePicker.ColumnHeaderView = new TimePickerColumnHeaderView()
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
            if (this.timePicker != null && e.NewValue.HasValue)
            {
                if (e.NewValue.Value == true)
                {
                    this.timePicker.FooterView.Height = 40;
                }
                else if (e.NewValue.Value == false)
                {
                    this.timePicker.FooterView.Height = 0;
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
            if (this.timePicker != null && e.NewValue.HasValue)
            {
                if (e.NewValue.Value == true)
                {
                    this.timePicker.EnableLooping = true;
                }
                else if (e.NewValue.Value == false)
                {
                    this.timePicker.EnableLooping = false;
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
            if (this.timePicker == null || e.AddedItems == null || this.formatComboBox == null)
            {
                return;
            }

            string? format = e.AddedItems[0].ToString();
            switch (format)
            {
                case "H mm":
                    this.timePicker.Format = PickerTimeFormat.H_mm;
                    break;

                case "H mm ss":
                    this.timePicker.Format = PickerTimeFormat.H_mm_ss;
                    break;

                case "h mm ss tt":
                    this.timePicker.Format = PickerTimeFormat.h_mm_ss_tt;
                    break;

                case "h mm tt":
                    this.timePicker.Format = PickerTimeFormat.h_mm_tt;
                    break;

                case "HH mm":
                    this.timePicker.Format = PickerTimeFormat.HH_mm;
                    break;

                case "HH mm ss":
                    this.timePicker.Format = PickerTimeFormat.HH_mm_ss;
                    break;

                case "HH mm ss fff":
                    this.timePicker.Format = PickerTimeFormat.HH_mm_ss_fff;
                    break;

                case "hh mm ss tt":
                    this.timePicker.Format = PickerTimeFormat.hh_mm_ss_tt;
                    break;

                case "hh mm ss fff tt":
                    this.timePicker.Format = PickerTimeFormat.hh_mm_ss_fff_tt;
                    break;

                case "hh mm tt":
                    this.timePicker.Format = PickerTimeFormat.hh_mm_tt;
                    break;

                case "hh tt":
                    this.timePicker.Format = PickerTimeFormat.hh_tt;
                    break;

                case "ss fff":
                    this.timePicker.Format = PickerTimeFormat.ss_fff;
                    break;

                case "mm ss":
                    this.timePicker.Format = PickerTimeFormat.mm_ss;
                    break;

                case "mm ss fff":
                    this.timePicker.Format = PickerTimeFormat.mm_ss_fff;
                    break;

                case "Default":
                    this.timePicker.Format = PickerTimeFormat.Default;
                    break;
            }
        }

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);
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
