#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    using Newtonsoft.Json.Linq;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Core;
    using Syncfusion.Maui.Inputs;
    using Syncfusion.Maui.Maps;
    using System.Collections.ObjectModel;
    using System.Text.RegularExpressions;
    public class MapsGettingStartedBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Holds the map tile layer.
        /// </summary>
        private MapTileLayer? mapTileLayer;

        /// <summary>
        /// Holds the map zoompanbehaviour instance.
        /// </summary>
        private MapZoomPanBehavior? zoomPanBehavior;

        /// <summary>
        /// Holds the autocomplete instance.
        /// </summary>
        private SfAutocomplete? autoComplete;

        /// <summary>
        /// Holds the button instance.
        /// </summary>
        private Button? button;

        /// <summary>
        /// Holds the busy indicator instance.
        /// </summary>
        private SfBusyIndicator? busyIndicator;

        /// <summary>
        /// Holds the azure ai helper instance.
        /// </summary>
        private AzureAIHelper azureAIHelper = new AzureAIHelper();

        /// <summary>
        /// Holds the maps data helper instance.
        /// </summary>
        private MapsDataHelper dataHelper = new MapsDataHelper();

        /// <summary>
        /// Holds the custom marker view model instance.
        /// </summary>
        private ObservableCollection<CustomMarker>? customMarkers = new ObservableCollection<CustomMarker>();

        /// <summary>
        /// Begins when the behavior attached to the view.
        /// </summary>
        /// <param name="bindable"></param>
        protected async override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.mapTileLayer = bindable.Content.FindByName<MapTileLayer>("layer");
            this.zoomPanBehavior = bindable.Content.FindByName<MapZoomPanBehavior>("zoomPanBehavior");
            this.autoComplete = bindable.Content.FindByName<SfAutocomplete>("autoComplete");
            this.button = bindable.Content.FindByName<Button>("button");
            this.busyIndicator = bindable.Content.FindByName<SfBusyIndicator>("busyIndicator");
            //// Added the delay to check valid credential.
            await Task.Delay(3000);
            if (this.autoComplete != null)
            {
                if (AzureBaseService.IsCredentialValid)
                {
                    this.autoComplete.NoResultsFoundText = string.Empty;
                }
                else
                {
                    this.autoComplete.ItemsSource = new ObservableCollection<string>()
                    {
                        "Hospitals in New York", "Hotels in Denver"
                    };
                }
            }

            if (this.button != null)
            {
                this.button.Clicked += SearchButtonClicked;
            }
        }

        /// <summary>
        /// Method to button click changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SearchButtonClicked(object? sender, EventArgs e)
        {
            if (this.autoComplete == null)
            {
                return;
            }

            await GetRecommendationAsync(this.autoComplete.Text);
            if (this.busyIndicator != null)
            {
                this.busyIndicator.IsVisible = false;
                this.busyIndicator.IsRunning = false;
            }
        }

        /// <summary>
        /// Method to contain AI respose and updates.
        /// </summary>
        /// <param name="userQuery"></param>
        /// <returns></returns>
        private async Task GetRecommendationAsync(string userQuery)
        {
            if (this.autoComplete == null || this.mapTileLayer == null || this.zoomPanBehavior == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(this.autoComplete.Text))
            {
                return;
            }
            if (this.busyIndicator != null)
            {
                this.busyIndicator.IsVisible = true;
                this.busyIndicator.IsRunning = true;
            }

            string prompt = $"Given location name: {userQuery}" +
                $"\nSome conditions need to follow:" +
                $"\nCheck the location name is just a state, city, capital or region, then retrieve the following fields: location name, detail, latitude, longitude, and set address value as null" +
                $"\nOtherwise, retrieve minimum 5 to 6 entries with following fields: location's name, details, latitude, longitude, address." +
                $"\nThe return format should be the following JSON format: markercollections[Name, Details, Latitude, Longitude, Address]" +
                $"\nRemove ```json and remove ``` if it is there in the code." +
                $"\nProvide JSON format details only, No need any explanation.";

            var returnMessage = await azureAIHelper.GetResultsFromAI(prompt);
            returnMessage = returnMessage.Replace("```json", "").Replace("```", "").Trim();

            if (returnMessage.StartsWith("markercollections:"))
            {
                returnMessage = "{\"" + returnMessage + "}";
            }
            else if (returnMessage.StartsWith("markercollections["))
            {
                returnMessage = "{\"markercollections\": " + returnMessage.Substring("markercollections".Length) + "}";
            }

            try
            {
                var jsonObj = new JObject();
                if (returnMessage == string.Empty)
                {
                    if (this.autoComplete.Text == "Hospitals in New York")
                    {
                        jsonObj = JObject.Parse(dataHelper.hospitalsData);
                        this.zoomPanBehavior.ZoomLevel = 10;
                    }
                    else if (this.autoComplete.Text == "Hotels in Denver")
                    {
                        jsonObj = JObject.Parse(dataHelper.hotelsData);
                        this.zoomPanBehavior.ZoomLevel = 12;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    jsonObj = JObject.Parse(returnMessage);
                }

                this.customMarkers?.Clear();
                foreach (var marker in jsonObj["markercollections"]!)
                {
                    CustomMarker customMarker = new CustomMarker();
                    customMarker.Name = marker["Name"]?.ToString();
                    customMarker.Details = marker["Details"]?.ToString();
                    customMarker.Address = marker["Address"]?.ToString();
                    customMarker.Latitude = StringToDoubleConverter(marker["Latitude"]?.ToString());
                    customMarker.Longitude = StringToDoubleConverter(marker["Longitude"]?.ToString());
                    if (AzureBaseService.IsCredentialValid)
                    {
                        await Task.Run(() =>
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                customMarker.Image = await azureAIHelper.GetImageFromAI(customMarker.Name);
                                customMarker.ImageName = string.Empty;
                            });
                        });
                    }
                    else
                    {
                        customMarker.Image = null;
                        customMarker.ImageName = this.UpdateOfflineImage(customMarker.Name);
                    }

                    this.customMarkers?.Add(customMarker);
                }

                this.mapTileLayer.Markers = this.customMarkers!;
                this.mapTileLayer.EnableCenterAnimation = true;
                if (this.customMarkers != null && this.customMarkers.Count > 0)
                {
                    var firstMarker = this.customMarkers[0];
                    this.mapTileLayer.Center = new MapLatLng
                    {
                        Latitude = firstMarker.Latitude,
                        Longitude = firstMarker.Longitude,
                    };

                    if (AzureBaseService.IsCredentialValid)
                    {
                        this.zoomPanBehavior.ZoomLevel = 10;
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Remove the unwanted string value for latitude and longitude, and change its to double.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private double StringToDoubleConverter(string? input)
        {
            if (input == null)
            {
                return 0;
            }

            string cleanedInput = Regex.Replace(input, @"\s*[°NSEW]\s*", string.Empty);
            return double.Parse(cleanedInput);
        }

        /// <summary>
        /// Return the image name based on offline datas.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string UpdateOfflineImage(string? name)
        {
            string imageName = string.Empty;
            if (name == "NewYork-Presbyterian Hospital" || name == "Hospital for Special Surgery" || name == "Kings County Hospital Center")
            {
                imageName = "hospital1.png";
            }
            else if (name == "Mount Sinai Hospital" || name == "Memorial Sloan Kettering Cancer Center")
            {
                imageName = "hospital2.png";
            }
            else if (name == "NYU Langone Health" || name == "New York Eye and Ear Infirmary of Mount Sinai")
            {
                imageName = "hospital3.png";
            }
            else if (name == "Lenox Hill Hospital" || name == "St. Luke's Roosevelt Hospital Center")
            {
                imageName = "hospital4.png";
            }
            else if (name == "Bellevue Hospital Center" || name == "Bronx-Lebanon Hospital Center")
            {
                imageName = "hospital5.png";
            }
            else if (name == "Mount Sinai West" || name == "Jamaica Hospital Medical Center")
            {
                imageName = "hospital6.png";
            }
            else if (name == "Mount Sinai Beth Israel" || name == "Flushing Hospital Medical Center")
            {
                imageName = "hospital7.png";
            }
            else if (name == "The Brown Palace Hotel and Spa" || name == "Le Méridien Denver Downtown" || name == "The Maven Hotel at Dairy Block")
            {
                imageName = "hotel1.png";
            }
            else if (name == "Hyatt Regency Denver at Colorado Convention Center" || name == "JW Marriott Denver Cherry Creek")
            {
                imageName = "hotel2.png";
            }
            else if (name == "The Ritz-Carlton, Denver" || name == "Halcyon, a hotel in Cherry Creek")
            {
                imageName = "hotel3.png";
            }
            else if (name == "Kimpton Hotel Born" || name == "The Art Hotel Denver, Curio Collection by Hilton")
            {
                imageName = "hotel4.png";
            }
            else if (name == "Grand Hyatt Denver" || name == "Kimpton Hotel Monaco Denver")
            {
                imageName = "hotel5.png";
            }
            else if (name == "The Oxford Hotel" || name == "Four Seasons Hotel Denver")
            {
                imageName = "hotel6.png";
            }
            else if (name == "Hotel Teatro" || name == "The Crawford Hotel")
            {
                imageName = "hotel7.png";
            }

            return imageName;
        }

        /// <summary>
        /// Begins when the behavior attached to the view.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.button != null)
            {
                this.button.Clicked -= SearchButtonClicked;
            }
        }
    }
}
