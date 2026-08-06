using Azure.AI.OpenAI;
using Azure;
using Microsoft.Extensions.AI;

namespace SampleBrowser.Maui.PdfViewer.SfPdfViewer
{
    public class AIHelper : AzureBaseService
    {
        /// <summary>
        /// Retrieves an answer from the GPT-4 model using the provided user prompt.
        /// </summary>
        /// <param name="userPrompt">The prompt input from the user for generating a response.</param>
        /// <returns>A string containing the response from OpenAI or an empty string if an error occurs.</returns>
        public async Task<string> GetAnswerFromGPT(string userPrompt)
        {
            try
            {
                if (IsCredentialValid && Client != null)
                {
                    ChatHistory = string.Empty;
                    ChatHistory = ChatHistory + userPrompt;
                    var response = await Client.GetResponseAsync(ChatHistory);
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