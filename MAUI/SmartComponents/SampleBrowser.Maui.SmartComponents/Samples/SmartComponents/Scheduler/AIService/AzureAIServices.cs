#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    using Azure.AI.OpenAI;
    using Microsoft.SemanticKernel.ChatCompletion;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents Class to interact with Azure AI.
    /// </summary>
    public class AzureAIServices : AzureBaseService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureAIServices"/> class.
        /// </summary>
        public AzureAIServices()
        {
            
        }

        /// <summary>
        /// Retrieves an answer from the deployment name model using the provided user prompt.
        /// </summary>
        /// <param name="userPrompt">The user prompt.</param>
        /// <returns>The AI response.</returns>
        public async Task<string> GetResultsFromAI(string userPrompt)
        {
            if (IsCredentialValid && ChatCompletions != null && ChatHistory != null)
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
    }
}