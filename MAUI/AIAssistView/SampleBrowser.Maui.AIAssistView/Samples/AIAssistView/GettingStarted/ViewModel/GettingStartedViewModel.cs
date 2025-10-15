#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Syncfusion.Maui.AIAssistView;

#nullable disable
namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    public class GettingStartedViewModel : INotifyPropertyChanged
    {
        #region Field
        private ObservableCollection<IAssistItem> messages;
        private ObservableCollection<GettingStartedModel> headerInfoCollection;
        private List<List<string>> suggestionlist = new List<List<string>>();
        private AzureAIService azureAIService;
        private Thickness headerPadding;
        private bool cancelResponse;
        private bool enableSendIcon;
        private bool isHeaderVisible = true;
        internal bool enableNewChatIcon = true;
        private bool isAttachmentPopupOpen;
        private string inputText;
        private IAssistItem requestItem;
        private bool isNewChatViewVisible = false;
        private bool canAddResponse = true;

        #endregion

        #region Constructor
        public GettingStartedViewModel()
        {
            azureAIService = new AzureAIService();
            this.GetHeaderInfo();
            this.GenerateSuggestions();
            this.messages = new ObservableCollection<IAssistItem>();
            this.NewChatTappedCommand = new Command<object>(ExecuteNewChatTappedCommand);
            this.SendButtonCommand = new Command(ExecuteSendButtonCommand);
            this.ChipCommand = new Command<object>(ExecuteChipCommand);
            this.CopyCommand = new Command<object>(ExecuteCopyCommand);
            this.RetryCommand = new Command<object>(ExecuteRetryCommand);
            this.AssistViewRequestCommand = new Command<object>(ExecuteRequestCommand);
            this.HeaderItemTappedCommand = new Command(HeaderItemTapCommand);
            this.StopRespondingCommand = new Command(ExecuteStopResponding);
            this.AttachmentButtonCommand = new Command(ShowAttachmentPopup);
        }

        #endregion

        #region Private Properties

        private ObservableCollection<string> HeaderMessages { get; set; } = new ObservableCollection<string>
        {
            "Ownership",
            "Brainstorming",
            "Listening",
             "Resilience",
        };

        private ObservableCollection<string> ImagesCollection { get; set; } = new ObservableCollection<string>
        {
             "ownership.png",
            "brainstorming.png",
            "listening.png",
            "resilience.png",
        };

        #endregion

        #region Commands

        public ICommand CopyCommand { get; set; }
        public ICommand RetryCommand { get; set; }
        public ICommand AssistViewRequestCommand { get; set; }
        public ICommand HeaderItemTappedCommand { get; set; }
        public ICommand StopRespondingCommand { get; set; }
        public ICommand SendButtonCommand { get; set; }
        public ICommand ChipCommand { get; set; }
        public ICommand NewChatTappedCommand { get; set; }
        public ICommand AttachmentButtonCommand { get; set; }
        #endregion

        #region Public Properties

        public ObservableCollection<GettingStartedModel> HeaderInfoCollection
        {
            get 
            { 
                return headerInfoCollection; 
            }
            set 
            { 
                this.headerInfoCollection = value; 
            }
        }

        public ObservableCollection<IAssistItem> Messages
        {
            get
            {
                return this.messages;
            }

            set
            {
                this.messages = value;
                RaisePropertyChanged(nameof(Messages));
            }
        }

        public bool CancelResponse
        {
            get
            {
                return cancelResponse;
            }
            set
            {
                cancelResponse = value;
                RaisePropertyChanged(nameof(CancelResponse));
            }
        }

        public bool IsHeaderVisible
        {
            get
            {
                return isHeaderVisible;
            }
            set
            {
                isHeaderVisible = value;
                RaisePropertyChanged(nameof(IsHeaderVisible));
            }
        }

        public bool IsNewChatViewVisible
        {
            get
            {
                return isNewChatViewVisible;
            }
            set
            {
                isNewChatViewVisible = value;
                RaisePropertyChanged(nameof(IsNewChatViewVisible));
            }
        }

        public bool EnableSendIcon
        {
            get
            {
                return enableSendIcon;
            }
            set
            {
                enableSendIcon = value; 
                RaisePropertyChanged(nameof(EnableSendIcon));
            }
        }

        public bool IsAttachmentPopupOpen
        {
            get
            {
                return isAttachmentPopupOpen;
            }
            set
            {

                isAttachmentPopupOpen = value;
                RaisePropertyChanged(nameof(IsAttachmentPopupOpen));
            }
        }
        public Thickness HeaderPadding
        {
            get { return headerPadding; }
            set { headerPadding = value; RaisePropertyChanged(nameof(HeaderPadding)); }
        }


        public string InputText
        {
            get { return inputText; }
            set { inputText = value; RaisePropertyChanged(nameof(InputText)); }
        }

        #endregion

        #region Internal Methods

        internal async Task GetResult(object inputQuery)
        {
            await Task.Delay(3000).ConfigureAwait(true);
            AssistItem request = (AssistItem)inputQuery;
            if (request != null)
            {
                var userAIPrompt = this.GetUserAIPrompt(request.Text);
                var response = await azureAIService!.GetResultsFromAI(request.Text, userAIPrompt).ConfigureAwait(true);
                response = response.Replace("\n", "<br>");
                if (!CancelResponse && this.canAddResponse)
                {
                    AssistItem responseItem = new AssistItem() { Text = response };
                    responseItem.RequestItem = inputQuery;
                    this.Messages.Add(responseItem);
                    this.enableNewChatIcon = true;
                    this.IsNewChatViewVisible = true;
                    this.EnableSendIcon = !string.IsNullOrEmpty(this.InputText);
                }
            }

            this.CancelResponse = false;
        }
        #endregion

        #region Private Methods

        private async void ExecuteNewChatTappedCommand(object obj)
        {
            await Task.Delay(100);
            this.canAddResponse = false;
            this.IsHeaderVisible = true;
            this.IsNewChatViewVisible = false;
            this.enableNewChatIcon = true;
            this.InputText = string.Empty;
            this.Messages.Clear();
        }
        private void ShowAttachmentPopup()
        {
            IsAttachmentPopupOpen = true;
        }
        private async void HeaderItemTapCommand(object obj)
        {
            requestItem = new AssistItem() { Text = (obj as Label).Text, IsRequested = true };
            this.Messages.Add(requestItem);
            await this.GetResponseWithSuggestion(requestItem).ConfigureAwait(true);
        }

        private async void ExecuteRequestCommand(object obj)
        {
            requestItem = (obj as Syncfusion.Maui.AIAssistView.RequestEventArgs).RequestItem;
            await this.GetResult(requestItem).ConfigureAwait(true);
        }

        private void ExecuteCopyCommand(object obj)
        {
            string text = (obj as AssistItem).Text;
            text = Regex.Replace(text, "<.*?>|&nbsp;", string.Empty);
            Clipboard.SetTextAsync(text);
        }

        private async void ExecuteRetryCommand(object obj)
        {
            this.enableNewChatIcon = false;
            this.IsNewChatViewVisible = true;
            IAssistItem item = (obj as AssistItem).RequestItem as IAssistItem;
            if (item != null)
            {
                requestItem = item;
            }
            await this.GetResult(requestItem).ConfigureAwait(true);
        }

        private void ExecuteChipCommand(object obj)
        {
            var chipText = obj as string;

            if (chipText == "Ownership")
            {
                this.InputText = "Characteristics of Ownership";
            }
            else if (chipText == "Listening")
            {
                this.InputText = "Types of Listening";
            }
            else
            {
                this.InputText = chipText;
            }
        }

        private async void ExecuteSendButtonCommand()
        {
            await Task.Delay(100);
            this.enableNewChatIcon = false;
            this.canAddResponse = true;
            this.IsNewChatViewVisible = true;
            var text = this.InputText;
            this.IsHeaderVisible = false;
            requestItem = new AssistItem()
            {
                Text = text,
                IsRequested = true
            };

            this.Messages.Add(requestItem);
            this.InputText = string.Empty;

            await this.GetResult(requestItem);
        }

        private void ExecuteStopResponding()
        {
            this.CancelResponse = true;
            AssistItem responseItem = new AssistItem() { Text = "You canceled the response" };
            responseItem.ShowAssistItemFooter = false;
            this.Messages.Add(responseItem);
            this.IsNewChatViewVisible = true;
            this.enableNewChatIcon = true;
            this.EnableSendIcon = !string.IsNullOrEmpty(this.InputText);
        }

        private void GetHeaderInfo()
        {
            var headerInfo = new ObservableCollection<GettingStartedModel>();
            for (int i = 0; i < 4; i++)
            {
                var gallery = new GettingStartedModel()
                {
                    Image = this.ImagesCollection[i],
                    HeaderMessage = this.HeaderMessages[i],
                };
                headerInfo.Add(gallery);
            }
            this.headerInfoCollection = headerInfo;
        }

        private string GetUserAIPrompt(string userPrompt)
        {
            string userQuery = $"Given User query: {userPrompt}." +
                      $"\nSome conditions need to follow:" +
                      $"\nGive heading of the topic and simplified answer in 4 points with numbered format" +
                      $"\nGive as string alone" +
                      $"\nRemove ** and remove quotes if it is there in the string.";
            return userQuery;
        }

        private async Task GetResponseWithSuggestion(object inputQuery)
        {
            await Task.Delay(3000).ConfigureAwait(true);
            AssistItem request = (AssistItem)inputQuery;
            if (request != null)
            {
                var userAIPrompt = this.GetUserAIPrompt(request.Text);
                var response = await azureAIService!.GetResultsFromAI(request.Text, userAIPrompt).ConfigureAwait(true);
                response = response.Replace("\n", "<br>");
                await Task.Delay(1000).ConfigureAwait(true);
                var suggestion = this.GetSuggestion(request.Text);
                await Task.Delay(1000).ConfigureAwait(true);
                if (!CancelResponse)
                {
                    AssistItem responseItem = new AssistItem() { Text = response, Suggestion = suggestion };
                    responseItem.RequestItem = inputQuery;
                    this.Messages.Add(responseItem);
                }
            }

            this.CancelResponse = false;
        }

        private void GenerateSuggestions()
        {
            List<string> firstHeaderSuggestion = new List<string> { "Initiative", "Responsibility", "Accountability" };
            List<string> secondHeaderSuggestion = new List<string> { "Different Perspective", "More Ideas" };
            List<string> thirdHeaderSuggestion = new List<string> { "Active Listening", "Passive Listening" };
            suggestionlist.Add(firstHeaderSuggestion);
            suggestionlist.Add(secondHeaderSuggestion);
            suggestionlist.Add(thirdHeaderSuggestion);
        }

        private AssistItemSuggestion GetSuggestion(string prompt)
        {
            var promptSuggestions = new AssistItemSuggestion();

            for (int i = 0; i < HeaderMessages.Count() - 1; i++)
            {
                if (HeaderMessages[i].Contains(prompt))
                {
                    var suggestions = new ObservableCollection<ISuggestion>();
                    foreach (var items in suggestionlist[i])
                    {
                        suggestions.Add(new AssistSuggestion() { Text = items });
                    }
                    promptSuggestions.Items = suggestions;
                    promptSuggestions.Orientation = SuggestionsOrientation.Horizontal;
                    return promptSuggestions;
                }
            }
            return promptSuggestions;
        }

        #endregion

        #region PropertyChanged

        /// <summary>
        /// Property changed handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when property is changed.
        /// </summary>
        /// <param name="propName">changed property name</param>
        public void RaisePropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion
    }
}
