#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Azure.AI.OpenAI;
using Azure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.AI;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    public class ComboBoxAzureAIService : AzureBaseService
    {
        /// <summary>
        /// Gets a completion response from the AzureAI service based on the provided prompt.
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> GetCompletion(string prompt, CancellationToken cancellationToken)
        {
            ChatHistory = string.Empty;
            if (ChatHistory != null && Client != null)
            {
                ChatHistory = ChatHistory + "You are a filtering assistant.";
                // Add the user message to the options
                ChatHistory = ChatHistory + prompt;
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var chatresponse = await Client.CompleteAsync(ChatHistory);
                    cancellationToken.ThrowIfCancellationRequested();
                    return chatresponse.ToString();
                }
                catch (RequestFailedException ex)
                {
                    // Log the error message and rethrow the exception or handle it appropriately
                    Debug.WriteLine($"Request failed: {ex.Message}");
                    throw;
                }
                catch (Exception ex)
                {
                    // Handle other potential exceptions
                    Debug.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
            return "";
        }
    }
}
