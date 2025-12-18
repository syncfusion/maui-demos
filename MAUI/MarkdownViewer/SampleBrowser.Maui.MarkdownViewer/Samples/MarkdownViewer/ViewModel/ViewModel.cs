#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.MarkdownViewer.SfMarkdownViewer
{
    public class ViewModel : BaseViewModel
    {
        #region Properties

        public Command ToggleModeCommand { get; private set; }

        private string weatherData = "";
        public string WeatherData
        {
            get { return weatherData; }
            set
            {
                weatherData = value;
                OnPropertyChanged(nameof(WeatherData));
            }
        }

        private string editableSource = "";
        public string EditableSource
        {
            get { return editableSource; }
            set
            {
                editableSource = value;
                OnPropertyChanged(nameof(EditableSource));
                //UpdateStatistics();
            }
        }

        private bool isEditMode = false;
        public bool IsEditMode
        {
            get { return isEditMode; }
            set
            {
                isEditMode = value;
                OnPropertyChanged(nameof(IsEditMode));
                OnPropertyChanged(nameof(IsViewMode));
                OnPropertyChanged(nameof(FloatingButtonIcon));
            }
        }

        public bool IsViewMode => !IsEditMode;

        public string FloatingButtonIcon => IsEditMode ? "\uE726": "\uE73D";

        private bool isLoading = false;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private string cssStyleRules;

        public string CssStyleRules
        {
            get => cssStyleRules;
            set
            {
                if (cssStyleRules != value)
                {
                    cssStyleRules = value;
                    OnPropertyChanged(nameof(CssStyleRules));
                }
            }
        }

        #endregion

        #region Constructor

        public ViewModel()
        {
            EditableSource = WeatherData;
            ToggleModeCommand = new Command(ToggleEditMode);
            GenerateCurrentWeatherData();
            // Load the default CSS from your ResourceDictionary
            Application.Current!.Resources.TryGetValue(Application.Current.RequestedTheme == AppTheme.Dark ? "WeatherDarkStyle" : "WeatherLightStyle", out var style);
            cssStyleRules = (style as string) ?? string.Empty;
        }

        #endregion

        #region Methods

        private void ToggleEditMode()
        {
            if (IsEditMode)
            {
                // Save mode - update the markdown viewer source
                SaveMarkdownContent();
            }
            else
            {
                // Edit mode - switch to editor
                EnterEditMode();
            }
        }

        private void EnterEditMode()
        {
            // Copy current source to editable source
            EditableSource = WeatherData;
            IsEditMode = true;
        }

        private async void SaveMarkdownContent()
        {
            IsLoading = true;

            try
            {
                // Update the main source with edited content
                WeatherData = EditableSource;

                // Simulate a brief save operation
                await Task.Delay(300);

                IsEditMode = false;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void GenerateCurrentWeatherData()
        {
            var today = DateTime.Today;
            var updateTime = DateTime.Now;

            // Calculate dates
            var todayStr = today.ToString("dddd");
            var tomorrowStr = today.AddDays(1).ToString("dddd");
            var dayAfterStr = today.AddDays(2).ToString("dddd");

            // Alert end date (2 days from now)
            var alertEndDate = today.AddDays(2).ToString("dddd, MMMM dd");

            WeatherData = $@"# Weather Report
## Seattle, Washington
###### *Updated: {updateTime:dddd, MMMM dd, yyyy} - {updateTime:h:mm tt} PDT*

## Current Summary
- **Temperature:** 76°F (feels like 78°F)
- **Condition:** Partly Cloudy
- **Humidity:** 54%

### ⚠️ Air Quality Alert
**Moderately poor air quality due to wildfire smoke from eastern Washington.**  
Sensitive groups should limit prolonged outdoor activities.  
Alert in effect until **{alertEndDate} at 5 PM PDT**

## Current Conditions
| Wind      | Visibility | Pressure     | Dew Point | UV Index | Sunrise | Sunset | Moon Phase    | Precipitation |
|-----------|------------|-------------|-----------|----------|---------|--------|--------------|---------------|
| 8 mph NW  | 9 miles    | 30.05 inHg  | 58°F      | 6 (High) | 5:38 AM | 8:54 PM| First Quarter | 10% chance    |

## Hourly Forecast
| 4 PM | 5 PM | 6 PM | 7 PM | 8 PM | 9 PM | 10 PM | 11 PM |
|------|------|------|------|------|------|-------|-------|
| 77°F | 76°F | 75°F | 73°F | 70°F | 67°F | 65°F  | 64°F  |
| Partly Cloudy | Partly Cloudy | Mostly Sunny | Mostly Sunny | Clear | Clear | Clear | Clear |
| 10% ☂️ | 10% ☂️ | 5% ☂️ | 5% ☂️ | 0% ☂️ | 0% ☂️ | 0% ☂️ | 0% ☂️ |

## 5-Day Forecast
| {todayStr} | {tomorrowStr} | {dayAfterStr} | {today.AddDays(3):ddd} | {today.AddDays(4):ddd} | {today.AddDays(5):ddd} |
|-------|------|------|------------|--------------|--------------|
| 76°F  | 79°F | 83°F | 84°F       | 81°F         | 78°F         |
| Partly Cloudy | Sunny | Sunny | Mostly Sunny | Partly Cloudy | Partly Cloudy |
| 10% ☂️ | 0% ☂️ | 0% ☂️ | 10% ☂️ | 20% ☂️ | 30% ☂️ |

## Weather Discussion
A high-pressure system continues to dominate the Pacific Northwest, bringing warm and dry conditions to the Seattle area through the end of the week. Temperatures will gradually rise into the low to mid-80s by {dayAfterStr}—about 5–7°F above normal for late {today:MMMM}.

Morning marine clouds are possible near the coast but should burn off by late morning. Air quality remains a concern due to wildfire smoke from eastern Washington and British Columbia. The smoke layer is expected to stay mostly aloft but may occasionally mix down, especially in the morning.

The weekend forecast shows a slight cooling trend with more cloud cover as a weak system approaches from the northwest. This may bring a small chance of showers by {today.AddDays(5):dddd}, though confidence in precipitation is still low.

### Marine Forecast
Puget Sound and Strait of Juan de Fuca:
- {todayStr}: NW winds 5–10 knots. Waves under 1 foot.
- {tomorrowStr}: Variable becoming W 5–15 knots afternoon. Waves 1–2 feet.
- {dayAfterStr}: NW winds 10–15 knots. Waves 1–3 feet.

### Mountain Forecast
Cascade Mountains:
- Freezing Level: 14,000 feet  
- Summit: Sunny, excellent visibility above smoke  
- {todayStr}'s Highs: Paradise (Mt. Rainier): 75°F; Stevens Pass: 78°F

### ☀️ Heat Safety Reminder
- Stay hydrated
- Limit strenuous activities 2–6 PM
- **Never leave children or pets in parked vehicles**

`This weather content is provided for demonstration and showcase purposes only. The weather data, forecasts, and safety information displayed are simulated examples.`

For the most recent updates, check the [live weather in Seattle, Washington](https://weather-reporter.com/station?station=12).
Data provided by National Weather Service | Seattle Weather Forecast Office
Coordinates: 47.6062° N, 122.3321° W | Elevation: 175 ft
© {today.Year} Regional Weather Authority";
        }

        #endregion
    }
}