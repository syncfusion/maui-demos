#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Picker.SfDatePicker
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
        private SfDatePicker? datePicker;

        /// <summary>
        /// The show header switch
        /// </summary>
        private Switch? showHeaderSwitch, showColumnHeaderSwitch, showFooterSwitch;

        /// <summary>
        /// The date format combo box.
        /// </summary>
        private SfComboBox? formatComboBox;

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

#if IOS || MACCATALYST
            Border border = sampleView.Content.FindByName<Border>("border");
            border.IsVisible = true;
            border.Stroke = Color.FromArgb("#E6E6E6");
            this.datePicker = sampleView.Content.FindByName<SfDatePicker>("DatePicker1");
#else
            Frame frame = sampleView.Content.FindByName<Frame>("frame");
            frame.IsVisible = true;
            frame.BorderColor = Color.FromArgb("#E6E6E6");
            this.datePicker = sampleView.Content.FindByName<SfDatePicker>("DatePicker");
#endif

            this.showHeaderSwitch = sampleView.Content.FindByName<Switch>("showHeaderSwitch");
            this.showColumnHeaderSwitch = sampleView.Content.FindByName<Switch>("showColumnHeaderSwitch");
            this.showFooterSwitch = sampleView.Content.FindByName<Switch>("showFooterSwitch");

            formats = new ObservableCollection<object>()
            {
                 "dd MM", "dd MM yyyy", "dd MMM yyyy", "M d yyyy", "MM dd yyyy", "MM yyyy", "MMM yyyy", "yyyy MM dd"
            };

            this.formatComboBox = sampleView.Content.FindByName<SfComboBox>("formatComboBox");
            this.formatComboBox.ItemsSource = formats;
            this.formatComboBox.SelectedIndex = 1;
            this.formatComboBox.SelectionChanged += FormatComboBox_SelectionChanged;

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
            if (this.datePicker != null)
            {
                if (e.Value == true)
                {
                    this.datePicker.HeaderView = new PickerHeaderView()
                    {
                        Height = 50,
                        Text = "Select a Date",
                        Background = Color.FromArgb("#6750A4"),
                        TextStyle = new PickerTextStyle()
                        {
                            TextColor = Colors.White,
                            FontSize = 15,
                        },
                    };
                }
                else if (e.Value == false)
                {
                    this.datePicker.HeaderView = new PickerHeaderView()
                    {
                        Height = 0,
                    };
                }
            }
        }

        /// <summary>
        /// Method for show column header switch toggle changed.
        /// </summary>
        /// <param name="sender">Return the object.</param>
        /// <param name="e">The Event Arguments.</param>
        private void ShowColumnHeaderSwitch_Toggled(object? sender, ToggledEventArgs e)
        {
            if (this.datePicker != null)
            {
                if (e.Value == true)
                {
                    this.datePicker.ColumnHeaderView = new DatePickerColumnHeaderView()
                    {
                        Height = 40,
                        DayHeaderText = "Day",
                        MonthHeaderText = "Month",
                        YearHeaderText = "Year",
                    };
                }
                if (e.Value == false)
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
        private void ShowFooterSwitch_Toggled(object? sender, ToggledEventArgs e)
        {
            if (this.datePicker != null)
            {
                if (e.Value == true)
                {
                    this.datePicker.FooterView = new PickerFooterView()
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
                    this.datePicker.FooterView = new PickerFooterView()
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
        private void FormatComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (this.datePicker == null || e.CurrentSelection == null || formatComboBox == null)
            {
                return;
            }

            string? format = e.CurrentSelection[0].ToString();
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