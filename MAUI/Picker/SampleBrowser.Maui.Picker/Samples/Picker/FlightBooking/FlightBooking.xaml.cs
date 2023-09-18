#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Picker.SfPicker
{
    using SampleBrowser.Maui.Base;
    using System.Globalization;

    /// <summary>
    /// The Flight booking sample.
    /// </summary>
    public partial class FlightBooking : SampleView
    {
        private DateTime from;

        private DateTime to;

        private List<string> fromList;

        private List<string> toList;

        private static List<string> countries = new List<string>() { "UK", "USA", "India", "UAE", "Germany" };

        private static List<string> ukCities = new List<string>() { "London", "Manchester", "Cambridge", "Edinburgh", "Glasgow", "Birmingham" };

        private static List<string> usaCities = new List<string>() { "New York", "Seattle", "Washington", "Chicago", "Boston", "Los Angles" };

        private static List<string> indiaCities = new List<string>() { "Mumbai", "Bengaluru", "Chennai", "Pune", "Jaipur", "Delhi" };

        private static List<string> uaeCities = new List<string>() { "Dubai", "Abu Dhabi", "Fujairah", "Sharjah", "Ajman", "AL Ain" };

        private static List<string> germanyCities = new List<string>() { "Berlin", "Munich", "Frankfurt", "Hamburg", "Cologne", "Bonn" };

        public FlightBooking()
        {
            InitializeComponent();

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
            mobileReturnDatePicker.MinimumDate = mobileReturnDatePicker.SelectedDate.Date;
#else
            departureDatePicker.OkButtonClicked += DepartureDatePicker_OkButtonClicked;
            returnDatePicker.OkButtonClicked += ReturnDatePicker_OkButtonClicked;

            departureDatePicker.CancelButtonClicked += DepartureDatePicker_CancelButtonClicked;
            returnDatePicker.CancelButtonClicked += ReturnDatePicker_CancelButtonClicked;

            departureDatePicker.Opened += DepartureDatePicker_OnPopUpOpened;
            returnDatePicker.Opened += ReturnDatePicker_OnPopUpOpened;

            departureDateLabel.Text = fromString;
            returnDateLabel.Text = fromString;
            returnDatePicker.MinimumDate = returnDatePicker.SelectedDate.Date;
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
            from = mobileDepartureDatePicker.SelectedDate.Date;
            mobileDepartureDateLabel.Text = from.Day.ToString() + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(from.Month).ToString() + "," + " " + from.Year.ToString();
            if (from.Date > to.Date)
            {
                to = from;
                mobileReturnDateLabel.Text = to.Day.ToString() + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(to.Month).ToString() + "," + " " + to.Year.ToString();
            }
            
            mobileDepartureDatePicker.IsOpen = false;
#else
            from = departureDatePicker.SelectedDate.Date;
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
            to = mobileReturnDatePicker.SelectedDate.Date;
            mobileReturnDateLabel.Text = to.Day.ToString() + " " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(to.Month).ToString() + "," + " " + to.Year.ToString();
            mobileReturnDatePicker.IsOpen = false;
#else
            to = returnDatePicker.SelectedDate.Date;
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
        /// Method to handle the search button clicked event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event args.</param>
        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            Random randomNumber = new Random();
            int index = randomNumber.Next(0, 50);
#if ANDROID || IOS
            Application.Current?.MainPage?.DisplayAlert("Results", "\n" + index + " Flights are available on that dates to depart from " + mobileFromLabel.Text.ToString(), "OK");
#else
            Application.Current?.MainPage?.DisplayAlert("Results", "\n" + index + " Flights are available on that dates to depart from " + fromLabel.Text.ToString(), "OK");
#endif
        }
    }
}