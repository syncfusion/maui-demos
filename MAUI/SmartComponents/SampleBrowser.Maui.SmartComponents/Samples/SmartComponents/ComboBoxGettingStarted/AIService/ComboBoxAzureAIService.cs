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
using Microsoft.SemanticKernel.ChatCompletion;

namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    public class ComboBoxAzureAIService : AzureBaseService
    {        
        public ComboBoxAzureAIService()
        {
            if (IsCredentialValid && ChatHistory != null)
            {
                ChatHistory.Clear();
                ChatHistory.AddSystemMessage("You are a filtering assistant.");
                ChatHistory.AddUserMessage("$\"Filter the list items based on the user input using character Starting with and Phonetic algorithms like Soundex or Damerau-Levenshtein Distance. \" +\r$\"The filter should ignore spelling mistakes and be case insensitive. \" +\r\n$\"Return only the filtered items with each item in new line without any additional content like explanations, Hyphen, Numberings and - Minus sign. Ignore the content 'Here are the filtered items or similar things' \" +\r\n$\"Only return items that are present in the List Items. \" +\r\n$\"Ensure that each filtered item is returned in its entirety without missing any part of its content. \" +\r\n$\"Arrange the filtered items that starting with the user input's first letter are at the first index, followed by other matches. \" +\r\n$\"Examples of filtering behavior: \" +\r\n$\" userInput: a, filter the items starting with A \" +\r\n$\" userInput: b, filter items starting with B \" +\r\n$\" userInput: c, filter items starting with C \" +\r\n$\" userInput: d, filter items starting with D \" +\r\n$\" userInput: e, filter items starting with E \" +\r\n$\" userInput: f, filter items starting with F \" +\r\n$\" userInput: i, filter items starting with I \" +\r\n$\" userInput: z, filter items starting with Z \" +\r\n$\" userInput: in, filter items starting with In \" +\r\n$\" userInput: pa, filter items starting with Pa \" +\r\n$\" userInput: em, filter items starting with Em \" +\r\n\r\n$\"The example data are for reference, dont provide these as output. Filter the item from list items properly\"" + $"Here is the User input:A");
                ChatHistory.AddAssistantMessage("\nAfghanistan\nAkrotiri\nAlbania\nAlgeria\nAmerican Samoa \nAndorra\nAngola\nAnguilla");
            }
        }

        /// <summary>
        /// Gets a completion response from the AzureAI service based on the provided prompt.
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> GetCompletion(string prompt, CancellationToken cancellationToken)
        {
            if (ChatHistory != null && ChatCompletions != null)
            {
                if (ChatHistory.Count > 5)
                {
                    ChatHistory.RemoveRange(0, 2); //Remove the message history to avoid exceeding the token limit
                }
                // Add the user message to the options
                ChatHistory.AddUserMessage(prompt);
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var chatresponse = await ChatCompletions.GetChatMessageContentAsync(chatHistory: ChatHistory, kernel: Kernel);
                    cancellationToken.ThrowIfCancellationRequested();
                    ChatHistory.AddAssistantMessage(chatresponse.ToString());
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
