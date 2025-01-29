#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    using Microsoft.SemanticKernel.ChatCompletion;
    using Microsoft.SemanticKernel.Connectors.OpenAI;

    /// <summary>
    /// Helper class to interact with Azure AI.
    /// </summary>
    public class AzureAIHelper : AzureBaseService
    {

#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0012 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        AzureOpenAITextToImageService? azureOpenAITextToImageService;
#pragma warning restore SKEXP0012 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning restore SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureAIService"/> class.
        /// </summary>
        public AzureAIHelper()
        {
           azureOpenAITextToImageService = new(imageDeploymentName, endpoint, key, null);
        }

        /// <summary>
        /// Retrieves an answer from the deployment name model using the provided user prompt.
        /// </summary>
        /// <param name="userPrompt">The user prompt.</param>
        /// <returns>The AI response.</returns>
        public async Task<string> GetResultsFromAI(string userPrompt)
        {
            if (IsCredentialValid && ChatCompletions != null && ChatHistory != null )
            {
                ChatHistory.Clear();
                // Add the system message and user message to the options
                ChatHistory.AddSystemMessage("You are a predictive analytics assistant.");
                ChatHistory.AddUserMessage(userPrompt);
                try
                {
                    var response = await ChatCompletions.GetChatMessageContentAsync(chatHistory: ChatHistory, kernel: Kernel);
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
            var Prompt = $"Share the {locationName} image. If the image is not available share common image based on the location";

            string imageUrl = await azureOpenAITextToImageService!.GenerateImageAsync(Prompt, 1024, 1024, kernel: Kernel!, cancellationToken: CancellationToken.None);
            return new Uri(imageUrl.ToString());
        }
    }
}
