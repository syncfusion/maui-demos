#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Calendar.SfCalendar
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Buttons;
    using Syncfusion.Maui.Calendar;
    using Syncfusion.Maui.Core;
    using Syncfusion.Maui.Popup;

    internal class FlightBookingBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Holds the departure date text input layout.
        /// </summary>
        SfTextInputLayout? views;

        /// <summary>
        /// Holds the return date text input layout.
        /// </summary>
        SfTextInputLayout? textInputLayout;

        /// <summary>
        /// Holds the trailing label for return date text input layout.
        /// </summary>
        Label? trailingLabel;

        /// <summary>
        /// Holds the flight booking frame.
        /// </summary>
        Frame? frame;

        /// <summary>
        /// Holds the return date calendar.
        /// </summary>
        SfCalendar? calendar1;

        /// <summary>
        /// Holds the departure date calendar.
        /// </summary>
        SfCalendar? calendar;

        /// <summary>
        /// Holds the return date stack layout.
        /// </summary>
        StackLayout? returnStackLayout;

        /// <summary>
        /// Holds the radio group.
        /// </summary>
        SfRadioGroup? radioGroup;

        /// <summary>
        /// Holds the radio group is round trip radio button.
        /// </summary>
        SfRadioButton? roundTrip;

        /// <summary>
        /// Holds the departure date label.
        /// </summary>
        Label? fromLabel1;

        /// <summary>
        /// Holds the return date label.
        /// </summary>
        Label? fromLabel;

        /// <summary>
        /// Holds the return date.
        /// </summary>
        Label? returnDate;

        /// <summary>
        /// Holds the selected date.
        /// </summary>
        DateTime? selectedDate;

        /// <summary>
        /// Holds the search button
        /// </summary>
        Button? searchButton;

        /// <summary>
        /// Holds the popup.
        /// </summary>
        SfPopup? popup;

        /// <summary>
        /// Check whether is dark or light theme.
        /// </summary>
        private bool isLightTheme = Application.Current?.RequestedTheme == AppTheme.Light;

        /// <summary>
        /// Begins when the behavior attached to the view.
        /// </summary>
        /// <param name="bindable">The sample view.</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.selectedDate = DateTime.Now.AddDays(1);
            this.radioGroup = bindable.Content.FindByName<SfRadioGroup>("radioGroup");
            this.frame = bindable.Content.FindByName<Frame>("frame");
#if ANDROID
            if (this.frame != null)
            {
                this.frame.BorderColor = Colors.Transparent;
            }
#endif
            if (this.radioGroup != null)
            {
                this.radioGroup.CheckedChanged += RadioGroup_CheckedChanged;
            }

            this.returnStackLayout = bindable.Content.FindByName<StackLayout>("returnStackLayout");
            this.calendar = bindable.Content.FindByName<SfCalendar>("calendar");
            if (this.calendar != null)
            {
                this.calendar.SelectedDate = DateTime.Now;
                this.calendar.ActionButtonClicked += Calendar_ActionButtonClicked;
                this.calendar.ActionButtonCanceled += Calendar_ActionButtonCanceled;
            }

            this.roundTrip = bindable.Content.FindByName<SfRadioButton>("roundTrip");
            if (this.roundTrip != null)
            {
                this.roundTrip.IsChecked = true;
            }

            this.textInputLayout = bindable.Content.FindByName<SfTextInputLayout>("textInputLayout");
            if (this.textInputLayout != null)
            {
                var tap = new TapGestureRecognizer();
                tap.Tapped += OnLabelTapped;
                this.textInputLayout.GestureRecognizers.Add(tap);
            }

            this.fromLabel = bindable.Content.FindByName<Label>("fromLabel");
            if (this.fromLabel  != null)
            {
                this.fromLabel.Text = DateTime.Now.Date.ToString("dd MMM yyyy");
            }

            if (this.fromLabel1 != null)
            {   
                this.fromLabel1.Text = this.selectedDate.Value.Date.ToString("dd MMM yyyy");
            }

            this.searchButton = bindable.Content.FindByName<Button>("searchButton");
            if (this.searchButton != null)
            {
                this.searchButton.Clicked += SearchButton_Clicked;
            }
        }

        /// <summary>
        /// Method to get the search button click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void SearchButton_Clicked(object? sender, EventArgs e)
        {
            this.popup = new SfPopup();
            this.popup.Show();
            this.popup.ShowHeader = false;
            this.popup.ShowFooter = true;
            this.popup.FooterHeight = 80;
            this.popup.HeightRequest = 200;
            this.popup.PopupStyle = new PopupStyle { CornerRadius = 15, MessageFontSize = 16 };
            this.popup.ContentTemplate = this.GetContentTemplate(popup);
            this.popup.FooterTemplate = this.GetFooterTemplate(popup);
            Random randomNumber = new Random();
            int index = randomNumber.Next(0,20);

            this.popup.Message = index + " flights are available on that dates to depart from Cleveland(CLE) to Chicago(CHI) ";
        }

        /// <summary>
        /// Method to get the return date event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void OnLabelTapped(object? sender, TappedEventArgs e)
        {
            if (this.calendar == null)
            {
                return;
            }

            this.calendar.IsVisible = true;
            this.calendar.IsOpen = true;
            this.calendar.Mode = Syncfusion.Maui.Calendar.CalendarMode.Dialog;
        }

        /// <summary>
        /// Method to get the departure date calendar action button cancel event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void Calendar_ActionButtonCanceled(object? sender, EventArgs e)
        {
            if (this.calendar == null)
            {
                return;
            }

            this.calendar.IsOpen = false;
        }

        /// <summary>
        /// Method to get the departure date calendar action button click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void Calendar_ActionButtonClicked(object? sender, CalendarSubmittedEventArgs e)
        {
            if (this.calendar == null)
            {
                return;
            }

            if (this.calendar.SelectedDate != null && this.calendar1 != null && this.fromLabel != null && this.fromLabel1 != null)
            {
                this.fromLabel.Text = this.calendar.SelectedDate.Value.Date.ToString("dd MMM yyyy");
                this.selectedDate = this.calendar.SelectedDate.Value;
                this.calendar1.MinimumDate = this.calendar.SelectedDate.Value;
                this.calendar1.SelectedDate = this.calendar.SelectedDate.Value;
                this.fromLabel1.Text = this.fromLabel.Text;
            }

            this.calendar.IsOpen = false;
        }

        /// <summary>
        /// Method to get the radio button check changed event,=.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void RadioGroup_CheckedChanged(object? sender, CheckedChangedEventArgs e)
        {
            var radioGroup = (SfRadioGroup?)sender;
            var text = radioGroup?.CheckedItem?.Text;

            if (text == "One-way")
            {
                if (returnStackLayout == null)
                {
                    return;
                }
#if WINDOWS || MACCATALYST
                foreach (var child in returnStackLayout.Children.ToList())
                {
                    returnStackLayout.Children.Remove(child);
                }

                // Create and add the returnDate label back to the StackLayout
                returnDate = new Label
                {
                    Text = "Return Date",
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 18,
                    TextColor = Colors.Gray,
                    VerticalTextAlignment = TextAlignment.Center,
#if MACCATALYST
                    Margin = new Thickness(0,20,0,0),
#else
                    Margin = new Thickness(0,0,0,0),
#endif
                };

                if (returnStackLayout != null)
                {
                    returnStackLayout.Children.Add(returnDate);
                    this.returnDate.VerticalTextAlignment = TextAlignment.Center;
                    this.returnDate.VerticalOptions = LayoutOptions.Center;

                    returnStackLayout.HorizontalOptions = LayoutOptions.Center;
                    returnStackLayout.VerticalOptions = LayoutOptions.Center;
                }

#elif ANDROID || IOS
                if (this.views != null)
                {
                    this.views.IsEnabled = false;
                }

                if (this.fromLabel1 != null)
                {
                    this.fromLabel1.IsEnabled = false;
                    this.fromLabel1.TextColor = Colors.Gray;
                }

                if (this.trailingLabel != null)
                {
                    this.trailingLabel.TextColor = Colors.Gray;
                }
#endif
            }
            else if (text == "Round-Trip")
            {
                if (returnStackLayout == null)
                {
                    return;
                }

                //returnStackLayout.Children.Clear();
                foreach (var child in returnStackLayout.Children.ToList())
                {
                     returnStackLayout.Children.Remove(child);
                }

                returnStackLayout.HorizontalOptions = LayoutOptions.Start;
                returnStackLayout.VerticalOptions = LayoutOptions.Start;
                returnDate = new Label
                {
#if ANDROID || IOS
                    Margin = new Thickness(5,0,0,0),
#elif WINDOWS || MACCATALYST
                    Margin = new Thickness(0,0,0,0),
#endif
                    Text = "Return Date:",
                    TextColor = Colors.Gray,
                };

                returnStackLayout.Children.Add(returnDate);

                views = new SfTextInputLayout
                {
#if WINDOWS || MACCATALYST
                    Margin = new Thickness(0,10,0,0),
                    WidthRequest = 150,
#elif ANDROID || IOS
                    WidthRequest = 280,
                    Margin = new Thickness(10, 10, 0, 0),
#endif
                    ShowHint = false,
                    HeightRequest = 65,
                    ContainerType = ContainerType.Outlined,
                    TrailingViewPosition = ViewPosition.Inside,
                    ContainerBackground = Colors.Transparent
                };

                // Add necessary children to the new TextInputLayout
                fromLabel1 = new Label
                {
                    Margin = new Thickness(15, 0, 0, 15),
                    
                    HeightRequest = 40,
                    // Set your text here
                    VerticalTextAlignment = TextAlignment.Center
                };

                if (this.selectedDate != null)
                {
                    this.fromLabel1.Text = this.selectedDate!.Value.Date.ToString("dd MMM yyyy");
                }
#if MACCATALYST
                views.Children.Add(fromLabel1);
#else
                views.Content = fromLabel1;
#endif

                trailingLabel = new Label
                {
                    Text = "\ue757", // Set your text here
                    FontSize = 20,
                    FontFamily = "MauiSampleFontIcon",
                    HeightRequest = 20
                };
                trailingLabel.SetDynamicResource(Label.TextColorProperty, "SfDataFormNormalEditorStroke");
                views.TrailingView = trailingLabel;

                // Add tap gesture recognizer to TextInputLayout
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped1; ;
                views.GestureRecognizers.Add(tapGestureRecognizer);

                // Add the new TextInputLayout to the StackLayout
                returnStackLayout.Children.Add(views);

                calendar1 = new SfCalendar();
                calendar1.IsVisible = false;
                if (this.calendar != null && this.calendar.SelectedDate != null)
                {
                    calendar1.MinimumDate = this.calendar.SelectedDate.Value;
                }

                calendar1.Background = Colors.Transparent;
                calendar1.FooterView.ShowActionButtons = true;
                calendar1.FooterView.ShowTodayButton = true;
                calendar1.ActionButtonClicked += Calendar1_ActionButtonClicked1;
                calendar1.ActionButtonCanceled += Calendar1_ActionButtonCanceled1;
                returnStackLayout.Children.Add(calendar1);
            }
        }

        /// <summary>
        /// Method to get the return date calendar cancel click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calendar1_ActionButtonCanceled1(object? sender, EventArgs e)
        {
            if (this.calendar1 != null)
            {
                this.calendar1.IsOpen = false;
            }
        }

        /// <summary>
        /// Method to get the return date calendar action click event. 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void Calendar1_ActionButtonClicked1(object? sender, CalendarSubmittedEventArgs e)
        {
            if (this.fromLabel1 != null && this.calendar1 != null)
            {
                this.fromLabel1.Text = this.calendar1?.SelectedDate?.Date.ToString("dd MMM yyyy");
                this.selectedDate = this.calendar1?.SelectedDate;
            }

            if (this.calendar1 != null)
            {
                this.calendar1.IsOpen = false;
            }
        }

        /// <summary>
        /// Method to get the label tap gesture recognizer.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void TapGestureRecognizer_Tapped1(object? sender, TappedEventArgs e)
        {
            if (calendar1 == null)
            {
                return;
            }

            this.calendar1.IsVisible = true;
            this.calendar1.SelectedDate = this.selectedDate;
            this.calendar1.Mode = CalendarMode.Dialog;
            this.calendar1.IsOpen = true;
        }

        /// <summary>
        /// Method to get footer template.
        /// </summary>
        /// <param name="popup">The popup.</param>
        /// <returns>Returns the data template.</returns>
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
                    CornerRadius = 20,
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
        /// Method to get the ok button style.
        /// </summary>
        /// <returns></returns>
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
        /// Method the get the dynamic color based on theme.
        /// </summary>
        /// <param name="resourceName">The resource name.</param>
        /// <returns></returns>
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
                    FontFamily = "OpenSansRegular",
                    LineBreakMode = LineBreakMode.WordWrap,
                    Padding = new Thickness(20, 20, 0, 0),
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
        /// Begins when the behavior attached to the view.
        /// </summary>
        /// <param name="bindable">The sample view.</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.radioGroup != null)
            {
                this.radioGroup.CheckedChanged -= RadioGroup_CheckedChanged;
            }

            if (this.calendar != null)
            {
                this.calendar.ActionButtonClicked -= Calendar_ActionButtonClicked;
                this.calendar.ActionButtonCanceled -= Calendar_ActionButtonCanceled;
            }

            if (this.searchButton != null)
            {
                this.searchButton.Clicked -= SearchButton_Clicked;
            }
        }
    }
}
