#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using System.Drawing;
using System.Globalization;
using Syncfusion.Maui.Popup;
using Color = Microsoft.Maui.Graphics.Color;

namespace SampleBrowser.Maui.Calendar.SfCalendar
{
    /// <summary>
    /// Interaction logic for GettingStarted.xaml
    /// </summary>
    public partial class AppointmentBooking : SampleView
    {
        /// <summary>
        /// The time slot string is used to handle while book an appointment. While select the time slot then time slot variable value will be updates with respective tapped time slot.
        /// It is used to reset the time slot.
        /// </summary>
        private string timeSlot = string.Empty;

        /// <summary>
        /// Check the application theme is light or dark.
        /// </summary>
        private bool isLightTheme = Application.Current?.RequestedTheme == AppTheme.Light;

        /// <summary>
        /// Initializes a new instance of the <see cref="Year" /> class.
        /// </summary>
        public AppointmentBooking()
        {
            InitializeComponent();

            if (this.popUp != null)
            {
                popUp.FooterTemplate = this.GetFooterTemplate(popUp);
                popUp.ContentTemplate = this.GetContentTemplate(popUp);
            }

#if MACCATALYST
            this.border.IsVisible = true;
            this.border.Stroke = isLightTheme ? Color.FromArgb("#CAC4D0") : Color.FromArgb("#49454F");
            this.InitializeCalendar(this.appointmentBooking1, this.deskTop);
#elif !ANDROID && !IOS
            this.frame.IsVisible = true;
            this.frame.BorderColor = isLightTheme ? Color.FromArgb("#CAC4D0") : Color.FromArgb("#49454F");
            this.InitializeCalendar(this.appointmentBooking, this.deskTop);
#else
            if (DeviceInfo.Idiom == DeviceIdiom.Tablet)
            {
                this.border.IsVisible = true;
                this.border.Stroke = isLightTheme ? Color.FromArgb("#CAC4D0") : Color.FromArgb("#49454F");
                this.InitializeCalendar(this.appointmentBooking1, this.deskTop);
            }
            else
            {
                this.InitializeCalendar(this.mobileAppointmentBooking, this.mobile);
            }
#endif
        }

        /// <summary>
        /// Method to get the dynamic color.
        /// </summary>
        /// <param name="resourceName">The resource name.</param>
        /// <returns>The color.</returns>
        private Color GetDynamicColor(string? resourceName = null)
        {
            if (resourceName != null && App.Current != null && App.Current.Resources.TryGetValue(resourceName, out var colorValue) && colorValue is Color color)
            {
                return color;
            }
            else
            {
                if (App.Current != null && App.Current.RequestedTheme == AppTheme.Light)
                {
                    return Color.FromRgb(0xFF, 0xFF, 0xFF);
                }
                else if (App.Current != null && App.Current.RequestedTheme == AppTheme.Dark)
                {
                    return Color.FromRgb(0x38, 0x1E, 0x72);
                }
            }

            return Colors.Transparent;
        }

        /// <summary>
        /// Method to get the Ok button style.
        /// </summary>
        /// <returns>The button style.</returns>
        private Style GetOkButtonStyle()
        {
            return new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter { Property = Button.CornerRadiusProperty, Value = 15 },
                    new Setter { Property = Button.BorderColorProperty, Value = Color.FromArgb("#6750A4") },
                    new Setter { Property = Button.BorderWidthProperty, Value = 1 },
                    new Setter { Property = Button.BackgroundColorProperty, Value = this.GetDynamicColor("SfCalendarTodayHighlightColor") },
                    new Setter { Property = Button.TextColorProperty, Value = this.GetDynamicColor() },
                    new Setter { Property = Button.FontSizeProperty, Value = 14 },
                }
            };
        }

        /// <summary>
        /// Method to get the footer template.
        /// </summary>
        /// <param name="popup">The pop up.</param>
        /// <returns>The data template.</returns>
        private DataTemplate GetFooterTemplate(SfPopup popup)
        {
            var footerTemplate = new DataTemplate(() =>
            {
                var grid = new Grid
                {
                    ColumnSpacing = 12,
                    Padding = new Thickness(24)
                };
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                var oKButton = new Button
                {
                    Text = "OK",
                    Style = this.GetOkButtonStyle(),
                    WidthRequest = 96,
                    HeightRequest = 40
                };
                oKButton.Clicked += (sender, args) =>
                {
                    popup.Dismiss();
                };
                grid.Children.Add(oKButton);
                Grid.SetColumn(oKButton, 1);
                return grid;
            });

            return footerTemplate;
        }

        /// <summary>
        /// Method to get the content template.
        /// </summary>
        /// <param name="popup">The pop up.</param>
        /// <returns>The data template.</returns>
        private DataTemplate GetContentTemplate(SfPopup popup)
        {
            var contentTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.1, GridUnitType.Star) });
                var label = new Label
                {
                    LineBreakMode = LineBreakMode.WordWrap,
                    Padding = new Thickness(20, 0, 0, 0),
                    FontSize = 16,
                    HorizontalOptions = LayoutOptions.Start,
                    HorizontalTextAlignment = TextAlignment.Start
                };

                label.BindingContext = popup;
                label.SetBinding(Label.TextProperty, "Message");
                var stackLayout = new StackLayout
                {
                    Margin = new Thickness(0, 2, 0, 0),
                    HeightRequest = 1,
                };

                stackLayout.BackgroundColor = isLightTheme ? Color.FromArgb("#611c1b1f") : Color.FromArgb("#61e6e1e5");
                grid.Children.Add(label);
                grid.Children.Add(stackLayout);
                Grid.SetRow(label, 0);
                Grid.SetRow(stackLayout, 1);
                return grid;
            });

            return contentTemplate;
        }

        /// <summary>
        /// Initialize the calendar.
        /// </summary>
        /// <param name="calendar">Calendar instance.</param>
        /// <param name="parent">Parent view of calendar control.</param>
        private void InitializeCalendar(Syncfusion.Maui.Calendar.SfCalendar calendar, Grid parent)
        {
            parent.IsVisible = true;
            calendar.MaximumDate = DateTime.Now.Date.AddMonths(3);
            calendar.SelectedDate = DateTime.Now.Date;
        }

        /// <summary>
        /// Method to update the selected date changed.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="e">Event arguments.</param>
        private void AppointmentBooking_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
#if MACCATALYST
            this.UpdateDateSelection(this.appointmentBooking1, this.label1, this.flexLayout1);
#elif !ANDROID && !IOS
            this.UpdateDateSelection(this.appointmentBooking, this.label, this.flexLayout);
#else
            if (DeviceInfo.Idiom == DeviceIdiom.Tablet)
            {
                this.UpdateDateSelection(this.appointmentBooking1, this.label1, this.flexLayout1);
            }
            else
            {
                this.UpdateDateSelection(this.mobileAppointmentBooking, this.mobileLabel, this.mobileFlexLayout);
            }
#endif
            this.timeSlot = string.Empty;
        }

        /// <summary>
        /// Update the UI based on calendar selected date.
        /// </summary>
        /// <param name="calendar">Calendar instance.</param>
        /// <param name="textLabel">Selected date text label.</param>
        /// <param name="buttonLayout">Time slot button layout.</param>
        private void UpdateDateSelection(Syncfusion.Maui.Calendar.SfCalendar calendar, Label textLabel, FlexLayout buttonLayout)
        {
            if (calendar.SelectedDate == null)
            {
                return;
            }

            DateTime dateTime = calendar.SelectedDate.Value;
            string dayText = dateTime.ToString("MMMM" + " " + dateTime.Day.ToString() + ", " + dateTime.ToString("yyyy"), CultureInfo.CurrentUICulture);
            textLabel.Text = dayText;
            //// The time slot is empty then no need to reset the time slot.
            if (this.timeSlot == string.Empty)
            {
                return;
            }

            foreach (Button child in buttonLayout.Children)
            {
                Button button = (Button)child;
                button.TextColor = isLightTheme ? Colors.Black : Colors.White;
                button.Background = isLightTheme ? Colors.White : Color.FromRgba("#1C1B1F");
            }
        }

        /// <summary>
        /// Method to Book an Appointment based on the selected date and selected time slot.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="e">Event arguments.</param>
        private void AppointmentanBooking(object sender, EventArgs e)
        {
            if (this.popUp == null)
            {
                return;
            }

#if MACCATALYST
            this.BookAppointment(this.appointmentBooking1, this.flexLayout1);
#elif !ANDROID && !IOS
            this.BookAppointment(this.appointmentBooking, this.flexLayout);
#else
            if (DeviceInfo.Idiom == DeviceIdiom.Tablet)
            {
                this.BookAppointment(this.appointmentBooking1, this.flexLayout1);
            }
            else
            {
                this.BookAppointment(this.mobileAppointmentBooking, this.mobileFlexLayout);
            }
#endif

            this.popUp.Show();
        }

        /// <summary>
        /// Book the appointment on selected date and time slot.
        /// </summary>
        /// <param name="calendar">Calendar instance.</param>
        /// <param name="buttonLayout">Time slot button layout.</param>
        private void BookAppointment(Syncfusion.Maui.Calendar.SfCalendar calendar, FlexLayout buttonLayout)
        {
            if (this.popUp == null)
            {
                return;
            }

            if (calendar.SelectedDate == null)
            {
                this.popUp.HeaderTitle = "Alert !";
                this.popUp.Message = "Please select a date to book an appointment";
                return;
            }

            if (this.timeSlot == string.Empty)
            {
                this.popUp.HeaderTitle = "Alert !";
                this.popUp.Message = "Please select a time to book an appointment";
                return;
            }

            this.popUp.HeaderTitle = "Confirmation";
            DateTime dateTime = calendar.SelectedDate.Value;
            string dayText = dateTime.ToString("MMMM" + " " + dateTime.Day.ToString() + ", " + dateTime.ToString("yyyy"), CultureInfo.CurrentUICulture);
            this.popUp.Message = "Appointment booked for " + dayText + " " + timeSlot;
            calendar.SelectedDate = DateTime.Now.Date;
            calendar.DisplayDate = DateTime.Now.Date;
            this.timeSlot = string.Empty;
            foreach (Button child in buttonLayout.Children)
            {
                Button button = (Button)child;
                button.TextColor = isLightTheme ? Colors.Black : Colors.White;
                button.Background = isLightTheme ? Colors.White : Color.FromRgba("#1C1B1F");
            }
        }

        /// <summary>
        /// Method to update the slot booking.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="e">Event arguments.</param>
        private void SlotBooking_Changed(object sender, EventArgs e)
        {
#if MACCATALYST
            this.UpdateTimeSlotSelection(this.appointmentBooking1, (Button)sender, this.flexLayout1);
#elif !ANDROID && !IOS
            this.UpdateTimeSlotSelection(this.appointmentBooking, (Button)sender, this.flexLayout);
#else
            if (DeviceInfo.Idiom == DeviceIdiom.Tablet)
            {
                this.UpdateTimeSlotSelection(this.appointmentBooking1, (Button)sender, this.flexLayout1);
            }
            else
            {
                this.UpdateTimeSlotSelection(this.mobileAppointmentBooking, (Button)sender, this.mobileFlexLayout);
            }
#endif
        }

        /// <summary>
        /// Update the UI based on selected time slot.
        /// </summary>
        /// <param name="calendar">Calendar instance.</param>
        /// <param name="selectedButton">Selected time slot button.</param>
        /// <param name="buttonLayout">Time slot button layout.</param>
        private void UpdateTimeSlotSelection(Syncfusion.Maui.Calendar.SfCalendar calendar, Button selectedButton, FlexLayout buttonLayout)
        {
            if (calendar.SelectedDate == null)
            {
                this.popUp.HeaderTitle = "Alert !";
                this.popUp.Message = "Please select a date to book an appointment ";
                this.popUp.Show();
                return;
            }

            foreach (Button child in buttonLayout.Children)
            {
                Button unPressedbutton = (Button)child;
                if (unPressedbutton == selectedButton)
                {
                    selectedButton.TextColor = Colors.White;
                    selectedButton.Background = Color.FromArgb("#6200EE");
                    timeSlot = selectedButton.Text;
                    continue;
                }

                unPressedbutton.TextColor = isLightTheme ? Colors.Black : Colors.White;
                unPressedbutton.Background = isLightTheme ? Colors.White : Color.FromRgba("#1C1B1F");
            }
        }
    }
}