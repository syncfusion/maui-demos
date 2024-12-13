#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Azure.AI.OpenAI;
using Azure;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    public class AIHelper : AzureBaseService
    {

        public AIHelper()
        {
           
        }

        /// <summary>
        /// Retrieves an answer from the GPT-4 model using the provided user prompt.
        /// </summary>
        /// <param name="userPrompt">The prompt input from the user for generating a response.</param>
        /// <returns>A string containing the response from OpenAI or an empty string if an error occurs.</returns>
        public async Task<string> GetAnswerFromGPT(string userPrompt)
        {
            try
            {
                if (IsCredentialValid && ChatCompletions != null && ChatHistory != null)
                {
                    ChatHistory.Clear();
                    ChatHistory.AddUserMessage(userPrompt);
                    var response = await ChatCompletions.GetChatMessageContentAsync(chatHistory: ChatHistory, kernel: Kernel);
                    return response.ToString();
                }
                else
                {
                    return " ";
                }
            }
            catch
            {
                // Return an empty string if an exception occurs
                return " ";
            }
        }

    }
}