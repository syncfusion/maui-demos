#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    public abstract class AzureBaseService
    {
        #region Fields

        /// <summary>
        /// The EndPoint
        /// </summary>
        private const string endpoint = "https://YOUR_ACCOUNT.openai.azure.com/";

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
        private const string key = "API key";

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


        private static bool isCredentialValid = false;

        private static bool isAlreadyValidated = false;

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

        /// <summary>
        /// Show Alert Popup
        /// </summary>
        private async void ShowAlertAsync()
        {
            var page = Application.Current?.Windows[0].Page;
            if (page != null && !IsCredentialValid)
            {
                isAlreadyValidated = true;
                await page.DisplayAlert("Alert", "The Azure API key or endpoint is missing or incorrect. Please verify your credentials. You can also continue with the offline data.", "OK");
            }
        }

        #endregion

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
    }

    public class AzureAIService : AzureBaseService
    {
        public AzureAIService()
        {
            InitializeClient();
        }

        public void InitializeClient()
        {
            if (IsCredentialValid && ChatHistory != null)
            {
                ChatHistory.Clear();
                ChatHistory.AddSystemMessage("You are a helpful, intelligent and conversational assistant that can assit with a wide variety of topics.");

            }
        }

        internal async Task<string> GetResultsFromAI(string userPrompt, string userAIPrompt)
        {
            if (ChatCompletions != null && ChatHistory != null)
            {
                try
                {
                    if (ChatHistory.Count > 5)
                    {
                        //Remove the message history to avoid exceeding the token limit
                        ChatHistory.RemoveRange(0, 2);
                    }

                    ChatHistory.AddUserMessage(userAIPrompt);

                    var response = await ChatCompletions.GetChatMessageContentAsync(chatHistory: ChatHistory, kernel: Kernel);
                    return response.ToString();
                }
                catch
                {
                    var response = this.GetSolutionToPrompt(userPrompt);
                    return response;
                }
            }
            else
            {
                var response = this.GetSolutionToPrompt(userPrompt);
                return response;
            }

        }

        #region Offline Data generation
        internal string GetSolutionToPrompt(string prompt)
        {
            if (prompt != null)
            {
                prompt = prompt.ToLower();
                for (int i = 0; i < promptRequest.Count(); i++)
                {
                    if (prompt.Contains(promptRequest[i]))
                    {
                        return promptResponseHtml[i];
                    }
                }
            }
            return "Please connect to your preferred AI service for real-time queries.";
        }
        #endregion

        #region Prompts
        private string[] promptRequest = new string[]
        {
            "ownership",
            "brainstorming",
            "listening",
            "resilience",
            "initiative",
            "responsibility",
            "accountability",
            "different perspective",
            "more ideas",
            "active listening",
            "passive listening",
            "compare online and offline marketing strategies",
            "why should i set achievable goals at work?",
            "write a joke that my coworkers would find funny",
             "why do people fly in their dreams?",
        };

        private string[] promptResponseHtml = new string[]
        {
            "<b>Characteristics of Ownership</b><ol><li>&nbsp;Ownership is about taking initiative.</li><li>&nbsp;It’s an understanding that taking action is your responsibility, not someone else’s.</li><li>&nbsp;It is the fundamental principle that you, as an individual, are accountable for the delivery of an outcome, even though there may be others who have a role to play.</li></ol>",
            "<b>Brainstorming</b><br>When a group of people sit and discuss a problem and try to derive the solution, this is termed brainstorming. Brainstorming helps team members, pool ideas and bounce them off each other, narrowing them down and refining them into a plan. It's useful for gaining different perspectives and more ideas.",
            "<b>Types of Listening</b><br>For a good communication, it is not only enough to convey the information efficiently, but it also needs to include good listening skill. Common types of Listening are Active listening and Passive listening.",
            "<b>Resilience</b><br>Resilience is the ability to bounce back from setbacks, adapt to difficult situations, and keep going despite adversity. It involves having the strength to confront challenges, learn from failures, and continue to grow without becoming overly discouraged. Essentially, resilience is about enduring tough times and emerging stronger.",
            "<b>Initiative</b><br>Taking the first step to start something or to bring new ideas and actions into motion. It means proactively beginning a task or project<ol><li><b>&nbsp;Definition:</b> Taking the first step to start a project, task, or initiative without waiting for direction or instruction.</li><li><b>&nbsp;Importance:</b> Demonstrates proactive behavior and leadership qualities. It shows an individual’s willingness to take charge and bring ideas to life.</li><li><b>&nbsp;Example:</b> An employee noticing a gap in the company's process and developing a plan to address it without being asked.</li></ol>",
            "<b>Responsibility</b><br>Being reliable and dependable in managing tasks or duties. It involves making decisions and completing tasks that one is assigned or has taken on.<ol><li><b>&nbsp;Definition:</b> Ability to be trusted with the duties and tasks associated with a particular role or project.</li><li><b>&nbsp;Importance:</b> Ensures that tasks are assigned and managed efficiently. Responsible individuals are committed to fulfilling their roles and meeting expectations.</li><li><b>&nbsp;Example:</b> A project manager overseeing the progress of a project, ensuring deadlines are met and resources are utilized effectively.</li></ol>",
            "<b>Accountability</b><br>Being answerable for the outcomes of your actions and decisions. It means acknowledging and accepting the consequences, whether they are successes or failures.<ol><li><b>&nbsp;Definition:</b> Being answerable for the outcomes of one's actions and decisions. This involves acknowledging both successes and failures.</li><li><b>&nbsp;Importance:</b> Encourages trust and transparency within a team or organization. It fosters a culture of continuous improvement and ethical behavior.</li><li><b>&nbsp;Example:</b> A team leader accepting responsibility for a project's failure, analyzing what went wrong, and developing strategies to avoid similar issues in the future.</li></ol>",
            "<b>Different Perspective</b><br>Each person has a different way of approaching a problem. It might differ with the way they see it, due to their unique understanding, way of thinking or experience. While brainstorming, participants can show each other different angles of a problem, giving everyone a better view of it.",
            "<b>More Ideas</b><br>When a group brainstorms, they can come up with a lot of ideas for a solution that don't stick to the usual approach. There are better chances of coming up with an innovative solution, as well as plan Bs and Cs. Sometimes, groups can try integrating the positives of each solution and can thus derive an effective solution for that problem.",
             "<b>Active Listening</b><br>Active listening is a two-way communication. The listener puts in active effort to understand the information conveyed by the speaker and react to it. ",
             "<b>Passive Listening</b><br>In passive listening, a person just tries to know what the speaker is trying to say. It is just like one way communication.",
            "<b>Compare online and offline marketing strategies</b><br>Sure, let’s compare online and offline marketing strategies across several key aspects:<br><br><b>Reach and Audience</b><br><br><b>Online Marketing:</b><ol><li> &nbsp;<b>Global Reach: </b>Can target a worldwide audience.</li><li>&nbsp;<b>Specific Targeting:</b> Allows precise targeting based on demographics, interests, and behaviors.</li></ol><br><b>Offline Marketing:</b><ol><li>&nbsp;<b>Local Reach:</b> Often more effective for local or regional audiences.</li><li>&nbsp;<b>Broad Targeting:</b> Reaches a wide audience but with less precision.</li></ol>",
            "<b>Why should I set achievable goals at work?</b><br>Setting achievable goals at work is a great way to stay motivated and productive.",
            "<b>Write a joke that my coworkers would find funny</b><br>Sure, here’s a lighthearted office joke for your coworkers:<br>Why did the scarecrow become an excellent employee?<br>Because he was outstanding in his field! 🌾😄<br>Hope that brings some smiles to the office! If you need more jokes or anything else, just let me know.",
            "<b>Why do people fly in their dreams?</b><br>Flying in dreams is a fascinating and common experience that often symbolizes various aspects of our subconscious mind. People may fly in their dreams for various symbolic and psychological reasons.",

        };

        #endregion
    }
}
