#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    public abstract class AzureBaseService
    {
        #region Fields

        /// <summary>
        /// The EndPoint
        /// </summary>
        internal const string endpoint = "https://YOUR_ACCOUNT.openai.azure.com/";

        /// <summary>
        /// The Deployment name
        /// </summary>
        internal const string deploymentName = "deployment name";

        /// <summary>
        /// The Image Deployment name
        /// </summary>
        internal const string imageDeploymentName = "IMAGE_MODEL_NAME";

        /// <summary>
        /// The API key
        /// </summary>
        internal const string key = "API key";

        /// <summary>
        /// The chat completion service
        /// </summary>
        private IChatCompletionService? chatCompletions;

        /// <summary>
        /// The kernal
        /// </summary>
        private Kernel? kernel;

        /// <summary>
        /// The chat histroy
        /// </summary>
        private ChatHistory? chatHistory;

        /// <summary>
        /// The credential valid field
        /// </summary>
        private static bool isCredentialValid = false;

        /// <summary>
        /// The already credential validated field
        /// </summary>
        private static bool isAlreadyValidated = false;

        /// <summary>
        /// The uri result field
        /// </summary>
        private Uri? uriResult;

        #endregion

        public AzureBaseService()
        {
            ValidateCredential();
        }

        #region Properties

        /// <summary>
        /// Gets or Set a value indicating whether an credentials are valid or not.
        /// Returns <c>true</c> if the credentials are valid; otherwise, <c>false</c>.
        /// </summary>
        public static bool IsCredentialValid
        {
            get
            {
                return isCredentialValid;
            }
            set
            {
                isCredentialValid = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the chat history object
        /// </summary>
        public ChatHistory? ChatHistory
        {
            get
            {
                return chatHistory;
            }
            set
            {
                chatHistory = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the chat completions object
        /// </summary>
        public IChatCompletionService? ChatCompletions
        {
            get
            {
                return chatCompletions;
            }
            set
            {
                chatCompletions = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the kernal object
        /// </summary>
        public Kernel? Kernel
        {
            get
            {
                return kernel;
            }
            set
            {
                kernel = value;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Validate Azure Credentials
        /// </summary>
        private async void ValidateCredential()
        {
            #region Azure OpenAI
            // Use below method for Azure Open AI
            this.GetAzureOpenAIKernal();
            #endregion

            #region Google Gimini
            // Use below method for Google Gimini
            //this.GetGoogleGiminiAIKernal();
            #endregion

            if (isAlreadyValidated)
            {
                return;
            }
            bool isValidUri = Uri.TryCreate(endpoint, UriKind.Absolute, out uriResult)
                 && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isValidUri || !endpoint.Contains("http") || string.IsNullOrEmpty(key) || key.Contains("API key") || string.IsNullOrEmpty(deploymentName) || deploymentName.Contains("deployment name") || string.IsNullOrEmpty(imageDeploymentName))
            {
                ShowAlertAsync();
                return;
            }
            try
            {
                if (ChatHistory != null && chatCompletions != null)
                {
                    // test the semantic kernal with message.
                    ChatHistory.AddSystemMessage("Hello, Test Check");
                    await chatCompletions.GetChatMessageContentAsync(chatHistory: ChatHistory, kernel: kernel);
                }
            }
            catch (Exception)
            {
                // Handle any exceptions that indicate the credentials or endpoint are invalid.               
                ShowAlertAsync();
                return;
            }
            IsCredentialValid = true;
            isAlreadyValidated = true;
        }

        #region Azure OpenAI
        /// <summary>
        /// To get the Azure open ai kernal method
        /// </summary>
        private void GetAzureOpenAIKernal()
        {
            // Create the chat history
            chatHistory = new ChatHistory();
            var builder = Kernel.CreateBuilder().AddAzureOpenAIChatCompletion(deploymentName, endpoint, key);

            // Get the kernal from build
            kernel = builder.Build();

            //Get the chat completions from kernal
            chatCompletions = kernel.GetRequiredService<IChatCompletionService>();
        }
        #endregion

        #region Goolge Gimini
        /// <summary>
        /// To get the google gimini ai kermal
        /// </summary>
        private void GetGoogleGiminiAIKernal()
        {
            //            //First Add the below package to the application
            //            add package Microsoft.SemanticKernel.Connectors.Google

            //            // Create the chat history
            //            chatHistory = new ChatHistory();
            //            #pragma warning disable SKEXP0070
            //            IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
            //            kernelBuilder.AddGoogleAIGeminiChatCompletion(modelId: "NAME_OF_MODEL", apiKey: key);
            //            Kernel kernel = kernelBuilder.Build();

            //            //Get the chat completions from kernal
            //            chatCompletions = kernel.GetRequiredService<IChatCompletionService>();
        }
        #endregion

        /// <summary>
        /// Show Alert Popup
        /// </summary>
        private async void ShowAlertAsync()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            if (Application.Current?.MainPage != null && !IsCredentialValid)
            {
                isAlreadyValidated = true;
                await Application.Current.MainPage.DisplayAlert("Alert", "The Azure API key or endpoint is missing or incorrect. Please verify your credentials. You can also continue with the offline data.", "OK");
            }
#pragma warning restore CS0618 // Type or member is obsolete
        }

        #endregion
    }
}
