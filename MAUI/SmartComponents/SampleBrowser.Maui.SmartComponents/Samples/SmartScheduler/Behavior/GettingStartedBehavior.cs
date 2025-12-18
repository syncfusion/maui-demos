#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartScheduler
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.SmartComponents;

    public class GettingStartedBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="sampleView">bindable value</param>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);

            IServiceProvider? serviceProvider = Application.Current?.Handler?.MauiContext?.Services;
            if (serviceProvider == null)
            {
                this.ShowAlertAsync();
                return;
            }

            IChatInferenceService? chatInferenceService = serviceProvider.GetService<IChatInferenceService>();
            if (chatInferenceService == null)
            {
                this.ShowAlertAsync();
                return;
            }
        }

        /// <summary>
        /// Show Alert Popup
        /// </summary>
        private async void ShowAlertAsync()
        {
            var page = Application.Current?.Windows[0].Page;
            if(page == null)
            {
                return;
            }
#if NET10_0
            await page.DisplayAlertAsync("Alert", "The Azure API key or endpoint is missing or incorrect. Please verify your credentials.", "OK");
#else
            await page.DisplayAlert("Alert", "The Azure API key or endpoint is missing or incorrect. Please verify your credentials.", "OK");
#endif
        }

        /// <summary>
        /// Initializes a new instance of the GettingStartedBehavior class.
        /// </summary>
        public GettingStartedBehavior()
        {
        }
    }
}