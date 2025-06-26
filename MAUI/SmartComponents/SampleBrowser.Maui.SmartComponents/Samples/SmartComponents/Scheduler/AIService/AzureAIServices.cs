#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    using Microsoft.Extensions.AI;
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
            if (IsCredentialValid && Client != null)
            {
                ChatHistory = string.Empty;
                // Add the system message and user message to the options
                ChatHistory = ChatHistory + "You are a predictive analytics assistant.";
                ChatHistory = ChatHistory + userPrompt;
                try
                {
                    var response = await Client.CompleteAsync(ChatHistory);
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