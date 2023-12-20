#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
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

    public class GettingStartedBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Picker view 
        /// </summary>
        private SfDateTimePicker? dateTimePicker;

        /// <summary>
        /// The show header switch
        /// </summary>
        private Switch? showHeaderSwitch, showColumnHeaderSwitch, showFooterSwitch;

        /// <summary>
        /// The date format combo box.
        /// </summary>
        private SfComboBox? dateFormatComboBox, timeFormatComboBox;

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

#if IOS || MACCATALYST
            Border border = sampleView.Content.FindByName<Border>("border");
            border.IsVisible = true;
            border.Stroke = Color.FromArgb("#E6E6E6");
            this.dateTimePicker = sampleView.Content.FindByName<SfDateTimePicker>("DateTimePicker1");
#else
            Frame frame = sampleView.Content.FindByName<Frame>("frame");
            frame.IsVisible = true;
            frame.BorderColor = Color.FromArgb("#E6E6E6");
            this.dateTimePicker = sampleView.Content.FindByName<SfDateTimePicker>("DateTimePicker");
#endif

            this.showHeaderSwitch = sampleView.Content.FindByName<Switch>("showHeaderSwitch");
            this.showColumnHeaderSwitch = sampleView.Content.FindByName<Switch>("showColumnHeaderSwitch");
            this.showFooterSwitch = sampleView.Content.FindByName<Switch>("showFooterSwitch");

            dateFormat = new ObservableCollection<object>()
            {
                 "dd MM", "dd MM yyyy", "dd MMM yyyy", "M d yyyy", "MM dd yyyy", "MM yyyy", "MMM yyyy", "yyyy MM dd"
            };

            timeFormat = new ObservableCollection<object>()
            {
                "H mm", "H mm ss", "h mm ss tt", "h mm tt", "HH mm", "HH mm ss", "hh mm ss tt", "hh mm tt", "hh tt"
            };

            this.dateFormatComboBox = sampleView.Content.FindByName<SfComboBox>("dateFormatComboBox");
            this.dateFormatComboBox.ItemsSource = dateFormat;
            this.dateFormatComboBox.SelectedIndex = 1;
            this.dateFormatComboBox.SelectionChanged += DateFormatComboBox_SelectionChanged;

            this.timeFormatComboBox = sampleView.Content.FindByName<SfComboBox>("timeFormatComboBox");
            this.timeFormatComboBox.ItemsSource = timeFormat;
            this.timeFormatComboBox.SelectedIndex = 1;
            this.timeFormatComboBox.SelectionChanged += TimeFormatComboBox_SelectionChanged;

            if (this.showHeaderSwitch != null)
            {
                this.showHeaderSwitch.Toggled += ShowHeaderSwitch_Toggled;
            }

            if (this.showColumnHeaderSwitch != null)
            {
                this.showColumnHeaderSwitch.Toggled += ShowColumnHeaderSwitch_Toggled;
            }

            if (this.showFooterSwitch != null)
            {
                this.showFooterSwitch.Toggled += ShowFooterSwitch_Toggled;
            }
        }

        /// <summary>
        /// Method for show header switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowHeaderSwitch_Toggled(object? sender, ToggledEventArgs e)
        {
            if (this.dateTimePicker != null)
            {
                this.dateTimePicker.HeaderView.Height = e.Value == true ? 50 : 0;
            }
        }

        /// <summary>
        /// Method for show column header switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowColumnHeaderSwitch_Toggled(object? sender, ToggledEventArgs e)
        {
            if (this.dateTimePicker != null)
            {
                this.dateTimePicker.ColumnHeaderView.Height = e.Value == true ? 40 : 0;
            }
        }

        /// <summary>
        /// Method for show footer switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowFooterSwitch_Toggled(object? sender, ToggledEventArgs e)
        {
            if (this.dateTimePicker != null)
            {
                if (e.Value == true)
                {
                    this.dateTimePicker.FooterView = new PickerFooterView()
                    {
                        Height = 40,
                        TextStyle = new PickerTextStyle()
                        {
                            TextColor = Color.FromArgb("#6750A4"),
                            FontSize = 15,
                        },
                    };
                }
                else if (e.Value == false)
                {
                    this.dateTimePicker.FooterView = new PickerFooterView()
                    {
                        Height = 0,
                    };
                }
            }
        }

        /// <summary>
        /// The format combo box selection changed event.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void DateFormatComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
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
            }

        }

        /// <summary>
        /// The format combo box selection changed event.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void TimeFormatComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
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
                this.showHeaderSwitch.Toggled -= ShowHeaderSwitch_Toggled;
                this.showHeaderSwitch = null;
            }

            if (this.showColumnHeaderSwitch != null)
            {
                this.showColumnHeaderSwitch.Toggled -= ShowColumnHeaderSwitch_Toggled;
                this.showColumnHeaderSwitch = null;
            }

            if (this.showFooterSwitch != null)
            {
                this.showFooterSwitch.Toggled -= ShowFooterSwitch_Toggled;
                this.showFooterSwitch = null;
            }
        }

        public GettingStartedBehavior()
        {
        }
    }
}