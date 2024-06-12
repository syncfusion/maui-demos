#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Picker.SfPicker
{
    using SampleBrowser.Maui.Base;
    using System.Globalization;
    using Syncfusion.Maui.Popup;
    using Microsoft.Maui.Controls.Internals;

    /// <summary>
    /// The Flight booking sample.
    /// </summary>
    public partial class FlightBooking : SampleView
    {
        private DateTime from;

        private DateTime to;

        private List<string> fromList;

        private List<string> toList;

        /// <summary>
        /// Check the application theme is light or dark.
        /// </summary>
        private bool isLightTheme = Application.Current?.RequestedTheme == AppTheme.Light;

        private static List<string> countries = new List<string>() { "UK", "USA", "India", "UAE", "Germany" };

        private static List<string> ukCities = new List<string>() { "London", "Manchester", "Cambridge", "Edinburgh", "Glasgow", "Birmingham" };

        private static List<string> usaCities = new List<string>() { "New York", "Seattle", "Washington", "Chicago", "Boston", "Los Angles" };

        private static List<string> indiaCities = new List<string>() { "Mumbai", "Bengaluru", "Chennai", "Pune", "Jaipur", "Delhi" };

        private static List<string> uaeCities = new List<string>() { "Dubai", "Abu Dhabi", "Fujairah", "Sharjah", "Ajman", "AL Ain" };

        private static List<string> germanyCities = new List<string>() { "Berlin", "Munich", "Frankfurt", "Hamburg", "Cologne", "Bonn" };

        public FlightBooking()
        {
            InitializeComponent();

            if (this.popup != null)
            {
                popup.FooterTemplate = this.GetFooterTemplate(popup);
                popup.ContentTemplate = this.GetContentTemplate(popup);
            }

            from = DateTime.Now.Date;
            to = DateTime.Now.Date;
            fromList = new List<string>() { "India", "Chennai" };
            toList = new List<string>() { "USA", "Boston" };
            Syncfusion.Maui.Picker.PickerColumn countyColumn = new Syncfusion.Maui.Picker.PickerColumn() { HeaderText = "Country", SelectedIndex = 2, ItemsSource = countries, Width = 150 };
            Syncfusion.Maui.Picker.PickerColumn countyColumn1 = new Syncfusion.Maui.Picker.PickerColumn() { HeaderText = "Country", SelectedIndex = 1, ItemsSource = countries, Width = 150 };
            Syncfusion.Maui.Picker.PickerColumn cityColumn = new Syncfusion.Maui.Picker.PickerColumn() { HeaderText = "City", SelectedIndex = 2, ItemsSource = indiaCities, Width = 150 };
            Syncfusion.Maui.Picker.PickerColumn cityColumn1 = new Syncfusion.Maui.Picker.PickerColumn() { HeaderText = "City", SelectedIndex = 4, ItemsSource = usaCities, Width = 150 };
            //// To initialize the picker based on the platform.
#if ANDROID || IOS
            mobileFromPicker.Columns = new System.Collections.ObjectModel.ObservableCollection<Syncfusion.Maui.Picker.PickerColumn>() { countyColumn, cityColumn };
            mobileToPicker.Columns = new System.Collections.ObjectModel.ObservableCollection<Syncfusion.Maui.Picker.PickerColumn>() { countyColumn1, cityColumn1 };
            this.InitializePicker(this.mobileGrid);
#else
            fromPicker.Columns = new System.Collections.ObjectModel.ObservableCollection<Syncfusion.Maui.Picker.PickerColumn>() { countyColumn, cityColumn };
            toPicker.Columns = new System.Collections.ObjectModel.ObservableCollection<Syncfusion.Maui.Picker.PickerColumn>() { countyColumn1, cityColumn1 };
            this.InitializePicker(this.grid);
#endif
            string str = DateTime.Now.Day.ToString();
            string fromString= str + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month).ToString() + "," + " " + DateTime.Now.Year.ToString();

#if ANDROID || IOS

            mobileDepartureDatePicker.OkButtonClicked += DepartureDatePicker_OkButtonClicked;
            mobileReturnDatePicker.OkButtonClicked += ReturnDatePicker_OkButtonClicked;

            mobileDepartureDatePicker.CancelButtonClicked += DepartureDatePicker_CancelButtonClicked;
            mobileReturnDatePicker.CancelButtonClicked += ReturnDatePicker_CancelButtonClicked;

            mobileDepartureDatePicker.Opened += DepartureDatePicker_OnPopUpOpened;
            mobileReturnDatePicker.Opened += ReturnDatePicker_OnPopUpOpened;

            mobileDepartureDateLabel.Text = fromString;
            mobileReturnDateLabel.Text = fromString;
            if (mobileReturnDatePicker.SelectedDate != null)
            {
                mobileReturnDatePicker.MinimumDate = mobileReturnDatePicker.SelectedDate.Value.Date;
            }

            mobileFromPicker.HeaderView.Height = 40;
            mobileFromPicker.HeaderView.Text = "FROM";
            mobileFromPicker.FooterView.Height = 40;

            mobileToPicker.HeaderView.Height = 40;
            mobileToPicker.HeaderView.Text = "TO";
            mobileToPicker.FooterView.Height = 40;

            mobileDepartureDatePicker.HeaderView.Height = 40;
            mobileDepartureDatePicker.HeaderView.Text = "Select a date";
            mobileDepartureDatePicker.FooterView.Height = 40;

            mobileReturnDatePicker.HeaderView.Height = 40;
            mobileReturnDatePicker.HeaderView.Text = "Select a date";
            mobileReturnDatePicker.FooterView.Height = 40;
#else
            departureDatePicker.OkButtonClicked += DepartureDatePicker_OkButtonClicked;
            returnDatePicker.OkButtonClicked += ReturnDatePicker_OkButtonClicked;

            departureDatePicker.CancelButtonClicked += DepartureDatePicker_CancelButtonClicked;
            returnDatePicker.CancelButtonClicked += ReturnDatePicker_CancelButtonClicked;

            departureDatePicker.Opened += DepartureDatePicker_OnPopUpOpened;
            returnDatePicker.Opened += ReturnDatePicker_OnPopUpOpened;

            departureDateLabel.Text = fromString;
            returnDateLabel.Text = fromString;
            if (returnDatePicker.SelectedDate != null)
            {
                returnDatePicker.MinimumDate = returnDatePicker.SelectedDate.Value.Date;
            }

            fromPicker.HeaderView.Height = 40;
            fromPicker.HeaderView.Text = "FROM";
            fromPicker.FooterView.Height = 40;

            toPicker.HeaderView.Height = 40;
            toPicker.HeaderView.Text = "TO";
            toPicker.FooterView.Height = 40;

            departureDatePicker.HeaderView.Height = 40;
            departureDatePicker.HeaderView.Text = "Select a date";
            departureDatePicker.FooterView.Height = 40;

            returnDatePicker.HeaderView.Height = 40;
            returnDatePicker.HeaderView.Text = "Select a date";
            returnDatePicker.FooterView.Height = 40;
#endif
        }

        /// <summary>
        /// Method to initialize the picker based on the platform.
        /// </summary>
        /// <param name="parent">The parent grid.</param>
        private void InitializePicker(Grid parent)
        {
            parent.IsVisible = true;
        }

        /// <summary>
        /// Method to handle the PopUpOpened event of the departure date picker.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event args.</param>
        private void DepartureDatePicker_OnPopUpOpened(object? sender, EventArgs e)
        {
#if ANDROID || IOS
            mobileDepartureDatePicker.IsOpen = true;
            mobileDepartureDatePicker.SelectedDate = from;
#else
            departureDatePicker.IsOpen = true;
            departureDatePicker.SelectedDate = from;
#endif
        }

        /// <summary>
        /// Method to handle the PopUpOpened event of the return date picker.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event args.</param>
        private void ReturnDatePicker_OnPopUpOpened(object? sender, EventArgs e)
        {
#if ANDROID || IOS
            mobileReturnDatePicker.IsOpen = true;
            mobileReturnDatePicker.MinimumDate = from;
            mobileReturnDatePicker.SelectedDate = to;
#else
            returnDatePicker.IsOpen = true;
            returnDatePicker.MinimumDate = from;
            returnDatePicker.SelectedDate = to;
#endif
        }

        /// <summary>
        /// Method to handle the ok button clicked event of the departure date picker.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event args.</param>
        private void DepartureDatePicker_OkButtonClicked(object? sender, EventArgs e)
        {
#if ANDROID || IOS
            if (mobileDepartureDatePicker.SelectedDate != null)
            {
                from = mobileDepartureDatePicker.SelectedDate.Value.Date;
            }
            mobileDepartureDateLabel.Text = from.Day.ToString() + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(from.Month).ToString() + "," + " " + from.Year.ToString();
            if (from.Date > to.Date)
            {
                to = from;
                mobileReturnDateLabel.Text = to.Day.ToString() + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(to.Month).ToString() + "," + " " + to.Year.ToString();
            }
            
            mobileDepartureDatePicker.IsOpen = false;
#else
            if (departureDatePicker.SelectedDate != null)
            {
               from = departureDatePicker.SelectedDate.Value.Date;
            }
            departureDateLabel.Text = from.Day.ToString() + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(from.Month).ToString() + "," + " " + from.Year.ToString();
            if (from.Date > to.Date)
            {
                to = from;
                returnDateLabel.Text = to.Day.ToString() + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(to.Month).ToString() + "," + " " + to.Year.ToString();
            }

            departureDatePicker.IsOpen = false;
#endif
        }

        /// <summary>
        /// Method to handle the ok button clicked event of the return date picker.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event args.</param>
        private void ReturnDatePicker_OkButtonClicked(object? sender, EventArgs e)
        {
#if ANDROID || IOS
            if (mobileReturnDatePicker.SelectedDate != null)
            {
                to = mobileReturnDatePicker.SelectedDate.Value.Date;
            }
            mobileReturnDateLabel.Text = to.Day.ToString() + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(to.Month).ToString() + "," + " " + to.Year.ToString();
            mobileReturnDatePicker.IsOpen = false;
#else
            if (returnDatePicker.SelectedDate != null)
            {
                to = returnDatePicker.SelectedDate.Value.Date;
            }
            returnDateLabel.Text = to.Day.ToString() + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(to.Month).ToString() + "," + " " + to.Year.ToString();
            returnDatePicker.IsOpen = false;
#endif
        }

        /// <summary>
        /// Method to handle the Cancel button clicked event of the departure date picker.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event args.</param>
        private void DepartureDatePicker_CancelButtonClicked(object? sender, EventArgs e)
        {
#if ANDROID || IOS
            mobileDepartureDatePicker.IsOpen = false;
#else
            departureDatePicker.IsOpen = false;
#endif
        }

        /// <summary>
        /// Method to handle the Cancel button clicked event of the return date picker.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event args.</param>
        private void ReturnDatePicker_CancelButtonClicked(object? sender, EventArgs e)
        {
#if ANDROID || IOS
            mobileReturnDatePicker.IsOpen = false;
#else
            returnDatePicker.IsOpen = false;
#endif
        }

        private void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
#if ANDROID || IOS
            mobileFromPicker.IsOpen = true;
            if (mobileFromPicker.Columns[0].ItemsSource is List<string> country)
            {
                int selectedIndex = country.IndexOf(fromList[0]);
                List<string> cities = this.GetCityList(countries[selectedIndex]);
                int citySelectedIndex = cities.IndexOf(fromList[1]);
                Syncfusion.Maui.Picker.PickerColumn cityColumn = new Syncfusion.Maui.Picker.PickerColumn() { HeaderText = "City", SelectedIndex = citySelectedIndex, ItemsSource = cities, Width = 150 };
                if (mobileFromPicker.Columns[1].ItemsSource != cities)
                {
                    mobileFromPicker.Columns[1] = cityColumn;
                }
                else
                {
                    mobileFromPicker.Columns[1].SelectedIndex = citySelectedIndex;
                }

                mobileFromPicker.Columns[0].SelectedIndex = selectedIndex;
            }

#else
            fromPicker.IsOpen = true;
            if (fromPicker.Columns[0].ItemsSource is List<string> country)
            {
                int selectedIndex = country.IndexOf(fromList[0]);
                List<string> cities = this.GetCityList(countries[selectedIndex]);
                int citySelectedIndex = cities.IndexOf(fromList[1]);
                Syncfusion.Maui.Picker.PickerColumn cityColumn = new Syncfusion.Maui.Picker.PickerColumn() { HeaderText = "City", SelectedIndex = citySelectedIndex, ItemsSource = cities, Width = 150 };
                if (fromPicker.Columns[1].ItemsSource != cities)
                {
                    fromPicker.Columns[1] = cityColumn;
                }
                else
                {
                    fromPicker.Columns[1].SelectedIndex = citySelectedIndex;
                }

                fromPicker.Columns[0].SelectedIndex = selectedIndex;
            }
#endif
        }

        void TapGestureRecognizer_Tapped_1(System.Object sender, System.EventArgs e)
        {
#if ANDROID || IOS
            mobileToPicker.IsOpen = true;
            if (mobileToPicker.Columns[0].ItemsSource is List<string> country)
            {
                int selectedIndex = country.IndexOf(toList[0]);
                List<string> cities = this.GetCityList(countries[selectedIndex]);
                int citySelectedIndex = cities.IndexOf(toList[1]);
                Syncfusion.Maui.Picker.PickerColumn cityColumn = new Syncfusion.Maui.Picker.PickerColumn() { HeaderText = "City", SelectedIndex = citySelectedIndex, ItemsSource = cities, Width = 150 };
                if (mobileToPicker.Columns[1].ItemsSource != cities)
                {
                    mobileToPicker.Columns[1] = cityColumn;
                }
                else
                {
                    mobileToPicker.Columns[1].SelectedIndex = citySelectedIndex;
                }

                mobileToPicker.Columns[0].SelectedIndex = selectedIndex;
            }
#else
            toPicker.IsOpen = true;
            if (toPicker.Columns[0].ItemsSource is List<string> country)
            {
                int selectedIndex = country.IndexOf(toList[0]);
                List<string> cities = this.GetCityList(countries[selectedIndex]);
                int citySelectedIndex = cities.IndexOf(toList[1]);
                Syncfusion.Maui.Picker.PickerColumn cityColumn = new Syncfusion.Maui.Picker.PickerColumn() { HeaderText = "City", SelectedIndex = citySelectedIndex, ItemsSource = cities, Width = 150 };
                if (toPicker.Columns[1].ItemsSource != cities)
                {
                    toPicker.Columns[1] = cityColumn;
                }
                else
                {
                    toPicker.Columns[1].SelectedIndex = citySelectedIndex;
                }

                toPicker.Columns[0].SelectedIndex = selectedIndex;
            }
#endif
        }

        private void FromPicker_SelectionChanged(System.Object sender, Syncfusion.Maui.Picker.PickerSelectionChangedEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                return;
            }

            string country = countries[e.NewValue];
            List<string> cities = this.GetCityList(country);
            Syncfusion.Maui.Picker.PickerColumn cityColumn = new Syncfusion.Maui.Picker.PickerColumn() { HeaderText = "City", SelectedIndex = 0, ItemsSource = cities, Width = 150 };

#if ANDROID || IOS
            if (mobileFromPicker.Columns[1].ItemsSource != cities)
            {
                mobileFromPicker.Columns[1] = cityColumn;
            }
#else
            if (fromPicker.Columns[1].ItemsSource != cities)
            {
                fromPicker.Columns[1] = cityColumn;
            }
#endif
        }

        private void ToPicker_SelectionChanged(System.Object sender, Syncfusion.Maui.Picker.PickerSelectionChangedEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                return;
            }

            string country = countries[e.NewValue];
            List<string> cities = this.GetCityList(country);
            Syncfusion.Maui.Picker.PickerColumn cityColumn = new Syncfusion.Maui.Picker.PickerColumn() { HeaderText = "City", SelectedIndex = 0, ItemsSource = cities, Width = 150 };

#if ANDROID || IOS
            if (mobileToPicker.Columns[1].ItemsSource != cities)
            {
                mobileToPicker.Columns[1] = cityColumn;
            }
#else
            if (toPicker.Columns[1].ItemsSource != cities)
            {
                toPicker.Columns[1] = cityColumn;
            }
#endif
        }

        private void FromPicker_CancelButtonClicked(System.Object sender, System.EventArgs e)
        {
#if ANDROID || IOS
            mobileFromPicker.IsOpen = false;
#else
            fromPicker.IsOpen = false;
#endif
        }

        private void ToPicker_CancelButtonClicked(System.Object sender, System.EventArgs e)
        {
#if ANDROID || IOS
            mobileToPicker.IsOpen = false;
#else
            toPicker.IsOpen = false;
#endif
        }

        private void FromPicker_OkButtonClicked(System.Object sender, System.EventArgs e)
        {
            if (sender is Syncfusion.Maui.Picker.SfPicker picker)
            {
                int countryColumnIndex = picker.Columns[0].SelectedIndex;
                int cityColumnIndex = picker.Columns[1].SelectedIndex;
                string country = countries[countryColumnIndex];
                List<string> cities = this.GetCityList(country);
                string city = cities[cityColumnIndex];
                fromList = new List<string>() { country, city };
#if ANDROID || IOS
                mobileFromLabel.Text = $"{city}, {country}";
                mobileFromPicker.IsOpen = false;
#else
                fromLabel.Text = $"{city}, {country}";
                fromPicker.IsOpen = false;
#endif
            }
        }

        private void ToPicker_OkButtonClicked(System.Object sender, System.EventArgs e)
        {
            if (sender is Syncfusion.Maui.Picker.SfPicker picker)
            {
                int countryColumnIndex = picker.Columns[0].SelectedIndex;
                int cityColumnIndex = picker.Columns[1].SelectedIndex;
                string country = countries[countryColumnIndex];
                List<string> cities = this.GetCityList(country);
                string city = cities[cityColumnIndex];
                toList = new List<string>() { country, city };
#if ANDROID || IOS
                mobileToLabel.Text = $"{city}, {country}";
                mobileToPicker.IsOpen = false;
#else
                toLabel.Text = $"{city}, {country}";
                toPicker.IsOpen = false;
#endif
            }
        }

        private List<string> GetCityList(string country)
        {
            if (country == "UK")
            {
                return ukCities;
            }
            else if (country == "USA")
            {
                return usaCities;
            }
            else if (country == "India")
            {
                return indiaCities;
            }
            else if (country == "UAE")
            {
                return uaeCities;
            }
            else if (country == "Germany")
            {
                return germanyCities;
            }

            return new List<string>();
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
                    new Setter { Property = Button.CornerRadiusProperty, Value = 20 },
                    new Setter { Property = Button.BorderColorProperty, Value = Color.FromArgb("#6750A4") },
                    new Setter { Property = Button.BorderWidthProperty, Value = 1 },
                    new Setter { Property = Button.BackgroundColorProperty, Value = this.GetDynamicColor("SfPickerSelectionStroke") },
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
            var footerTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.1, GridUnitType.Star) });

                var label = new Label
                {
                    LineBreakMode = LineBreakMode.WordWrap,
                    Padding = new Thickness(24, 24, 0, 0),
                    FontSize = 16,
                    HorizontalOptions = LayoutOptions.Start,
                    HorizontalTextAlignment = TextAlignment.Start
                };

                label.BindingContext = popup;
                label.SetBinding(Label.TextProperty, "Message");

                var stackLayout = new StackLayout
                {
                    Margin = new Thickness(0, 10, 0, 0),
                    HeightRequest = 1,
                };

                stackLayout.BackgroundColor = isLightTheme ? Color.FromArgb("#611c1b1f") : Color.FromArgb("#61e6e1e5");
                grid.Children.Add(label);
                grid.Children.Add(stackLayout);

                Grid.SetRow(label, 0);
                Grid.SetRow(stackLayout, 1);

                return grid;
            });

            return footerTemplate;
        }

        /// <summary>
        /// Method to handle the search button clicked event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event args.</param>
        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            if (this.popup == null)
            {
                return;
            }

            Random randomNumber = new Random();
            int index = randomNumber.Next(0, 50);

#if ANDROID || IOS
            this.popup.Message = index + " Flights are available on that dates to depart from " + mobileFromLabel.Text.ToString();
#else
            this.popup.Message = index + " Flights are available on that dates to depart from " + fromLabel.Text.ToString();
#endif
            popup.Show();
        }
    }
}