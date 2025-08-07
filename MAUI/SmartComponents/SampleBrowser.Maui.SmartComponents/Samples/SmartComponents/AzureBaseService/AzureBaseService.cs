#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    public abstract class AzureBaseService
    {
        #region Fields
        /// <summary>
        /// The EndPoint
        /// </summary>
        internal const string Endpoint = "YOUR_END_POINT_NAME";

        /// <summary>
        /// The Deployment name
        /// </summary>
        internal const string DeploymentName = "DEPLOYMENT_NAME";

        /// <summary>
        /// The Image Deployment name
        /// </summary>
        internal const string ImageDeploymentName = "IMAGE_DEPOLYMENT_NAME";

        /// <summary>
        /// The API key
        /// </summary>
        internal const string Key = "API_KEY";

        /// <summary>
        /// The already credential validated field
        /// </summary>
        private static bool isAlreadyValidated = false;

        /// <summary>
        /// Indicating whether an credentials are valid or not
        /// </summary>
        private static bool _isCredentialValid;

        #endregion

        public AzureBaseService()
        {
            ValidateCredential();
        }

        #region Properties

        /// <summary>
        /// The kernal
        /// </summary>
        internal IChatClient? Client { get; set; }

        /// <summary>
        /// The chat histroy
        /// </summary>
        internal string? ChatHistory { get; set; }

        /// <summary>
        /// Gets or Set a value indicating whether an credentials are valid or not.
        /// </summary>
        internal static bool IsCredentialValid
        {
            get => _isCredentialValid;
            set
            {
                if (_isCredentialValid != value)
                {
                    _isCredentialValid = value;
                    OnCredentialChanged?.Invoke(value);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validate Azure Credentials
        /// </summary>
        private async void ValidateCredential()
        {
            this.GetAzureOpenAIKernal();

            if (isAlreadyValidated)
            {
                return;
            }

            try
            {
                if (Client != null)
                {
                    await Client!.CompleteAsync("Hello, Test Check");
                    ChatHistory = string.Empty;
                    IsCredentialValid = true;
                    isAlreadyValidated = true;
                }
                else
                {
                    ShowAlertAsync();
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// To get the Azure open ai kernal method
        /// </summary>
        private void GetAzureOpenAIKernal()
        {
            try
            {
                var client = new AzureOpenAIClient(new Uri(Endpoint), new AzureKeyCredential(Key)).AsChatClient(modelId: DeploymentName);
                this.Client = client;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Show Alert Popup
        /// </summary>
        private async void ShowAlertAsync()
        {
            var page = Application.Current?.Windows[0].Page;
            if (page != null && !IsCredentialValid)
            {
                isAlreadyValidated = true;
                await page.DisplayAlert("Alert", "The Azure API key or endpoint is missing or incorrect. Please verify your credentials. You can also continue with the offline data.", "OK");
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Represents the method that will handle IsCredentialValid change event.
        /// </summary>
        internal delegate void CredentialValidated(bool isCredentialValid);

        /// <summary>
        /// Represents the event when IsCredentialValid value is changed.
        /// </summary>
        internal static event CredentialValidated? OnCredentialChanged;

        #endregion

    }
}
