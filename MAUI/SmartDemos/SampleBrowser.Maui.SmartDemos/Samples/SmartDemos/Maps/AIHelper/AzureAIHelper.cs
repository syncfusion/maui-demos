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
using OpenAI.Images;
namespace SampleBrowser.Maui.SmartDemos.SmartDemos
{

    /// <summary>
    /// Helper class to interact with Azure AI.
    /// </summary>
    public class AzureAIHelper : AzureBaseService
    {
        ImageClient? azureOpenAITextToImageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureAIService"/> class.
        /// </summary>
        public AzureAIHelper()
        {
            InitialImageClient();
        }

        /// <summary>
        /// Retrieves an answer from the deployment name model using the provided user prompt.
        /// </summary>
        /// <param name="userPrompt">The user prompt.</param>
        /// <returns>The AI response.</returns>
        public async Task<string> GetResultsFromAI(string userPrompt)
        {
            if (IsCredentialValid && Client != null)
            {
                ChatHistory = string.Empty;
                // Add the system message and user message to the options
                ChatHistory = ChatHistory + "You are a predictive analytics assistant.";
                ChatHistory = ChatHistory + userPrompt;
                try
                {
                    var response = await Client.GetResponseAsync(ChatHistory);
                    return response.ToString();
                }
                catch
                {
                    return string.Empty;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Retrieves an image from the image deployment name model using the provided location name.
        /// </summary>
        /// <param name="locationName">The location name.</param>
        /// <returns>The AI response.</returns>
        public async Task<Uri> GetImageFromAI(string? locationName)
        {
            if (string.IsNullOrWhiteSpace(locationName))
            {
                locationName = "a beautiful natural landscape";
            }

            var Prompt = $"Generate a realistic image of {locationName}. If specific details about {locationName} are unavailable, generate a common or representative image of a generic location.";

            OpenAI.Images.ImageGenerationOptions options = new OpenAI.Images.ImageGenerationOptions()
            {
                ResponseFormat = GeneratedImageFormat.Uri,
                Size = GeneratedImageSize.W1024xH1024,
                Quality = GeneratedImageQuality.High,
                Style = GeneratedImageStyle.Natural,
            };

            var imageUrl = await azureOpenAITextToImageService!
                .GenerateImageAsync(Prompt, options, cancellationToken: CancellationToken.None);

            if (imageUrl == null || imageUrl.Value?.ImageUri == null)
            {
                throw new Exception("Failed to generate the image. Please check the prompt and service configuration.");
            }

            return imageUrl.Value.ImageUri;
        }

        /// <summary>
        /// Method to get the image client.
        /// </summary>
        private async void InitialImageClient()
        {
            await Task.Delay(2000);
            if (IsCredentialValid && Client != null)
            {
                azureOpenAITextToImageService = new AzureOpenAIClient(new Uri(Endpoint), new AzureKeyCredential(Key)).GetImageClient(ImageDeploymentName);
            }
        }
    }
}
