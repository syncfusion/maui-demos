#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartDemos.SmartDemos
{
    using Microsoft.Extensions.AI;
    using System.Threading.Tasks;

    internal class DataFormAIService : AzureBaseService
    {
        /// <summary>
        /// Retrieves an answer from the deployment name model using the provided user prompt.
        /// </summary>
        /// <param name="userPrompt">The user prompt.</param>
        /// <returns>The AI response.</returns>
        internal async Task<string> GetAnswerFromGPT(string userPrompt)
        {
            ChatHistory = string.Empty;
            if (IsCredentialValid && Client != null && ChatHistory != null)
            {
                // Add the user's prompt as a user message to the conversation.
                ChatHistory = ChatHistory + "You are a predictive analytics assistant.";
                // Add the user's prompt as a user message to the conversation.
                ChatHistory = ChatHistory + userPrompt;
                try
                {
                    //// Send the chat completion request to the OpenAI API and await the response.
                    var response = await Client.GetResponseAsync(ChatHistory);
                    return response.ToString();
                }
                catch
                {
                    // If an exception occurs (e.g., network issues, API errors), return an empty string.
                    return "";
                }
            }

            return "";
        }
    }
}

