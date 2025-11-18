#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
//using Markdig;
using Syncfusion.Maui.AIAssistView;
using Syncfusion.Maui.Chat;
using Syncfusion.Maui.Core.Carousel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

#nullable disable
namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    public class MarkdownViewModel : INotifyPropertyChanged
    {
        #region Field
        private ObservableCollection<IAssistItem> messages;
        private ObservableCollection<MarkdownModel> headerInfoCollection;
        private List<List<string>> suggestionlist = new List<List<string>>();
        private ObservableCollection<Syncfusion.Maui.AIAssistView.ISuggestion> _suggestions;
        private AzureAIService azureAIService;
        private Thickness headerPadding;
        private bool cancelResponse;
        private IAssistItem requestItem;
        private bool canAddResponse = true;
        public List<string> contentLines = new List<string>();
        public double totalHeight = 0;
        #endregion

        #region Constructor
        public MarkdownViewModel()
        {
            azureAIService = new AzureAIService();
            this.GetHeaderInfo();
            this.GenerateSuggestions();
            this.messages = new ObservableCollection<IAssistItem>();
            this.AssistViewRequestCommand = new Command<object>(ExecuteRequestCommand);
            this.HeaderItemTappedCommand = new Command(HeaderItemTapCommand);
            this.StopRespondingCommand = new Command(ExecuteStopResponding);
        }

        #endregion

        #region Private Properties

        private ObservableCollection<string> HeaderMessages { get; set; } = new ObservableCollection<string>
        {
            "What features are included in AI AssistView?",
            "What is Markdown, and how is it used?",
        };

        #endregion

        #region Commands

        public ICommand AssistViewRequestCommand { get; set; }
        public ICommand HeaderItemTappedCommand { get; set; }
        public ICommand StopRespondingCommand { get; set; }
        #endregion

        #region Public Properties

        public ObservableCollection<MarkdownModel> HeaderInfoCollection
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

        public Thickness HeaderPadding
        {
            get { return headerPadding; }
            set { headerPadding = value; RaisePropertyChanged(nameof(HeaderPadding)); }
        }

        public double TotalHeight
        {
            get { return totalHeight; }
            set { totalHeight = value; RaisePropertyChanged(nameof(TotalHeight)); }
        }


        public ObservableCollection<Syncfusion.Maui.AIAssistView.ISuggestion> Suggestions
        {
            get { return this._suggestions; }
            set
            {
                this._suggestions = value;
                RaisePropertyChanged(nameof(Suggestions));
            }
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
                var response = await azureAIService!.GetResultsFromAIForMarkdown(request.Text, userAIPrompt).ConfigureAwait(true);
                var suggestion = this.GetSuggestion(request.Text);

                if (!CancelResponse && this.canAddResponse)
                {
                    AssistItem responseItem = new AssistItem();
                    responseItem.ShowAssistItemFooter = false;
                    this.Messages.Add(responseItem);
                    responseItem.Text = response;
                    responseItem.RequestItem = inputQuery;
                    await Task.Delay(3000).ConfigureAwait(true);
                    responseItem.Suggestion = suggestion;
                }
            }

            this.CancelResponse = false;
        }
        #endregion

        #region Private Methods

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

        private void ExecuteStopResponding()
        {
            this.CancelResponse = true;
            AssistItem responseItem = new AssistItem() { Text = "You canceled the response" };
            responseItem.ShowAssistItemFooter = false;
            this.Messages.Add(responseItem);
        }

        private void GetHeaderInfo()
        {
            this.headerInfoCollection = new ObservableCollection<MarkdownModel>
            {
                new MarkdownModel { HeaderMessage = this.HeaderMessages[0] },
                new MarkdownModel { HeaderMessage = this.HeaderMessages[1] }
            };
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
                var response = await azureAIService!.GetResultsFromAIForMarkdown(request.Text, userAIPrompt).ConfigureAwait(true);
                var suggestion = this.GetSuggestion(request.Text);
                if (!CancelResponse)
                {
                    AssistItem responseItem = new AssistItem() { ShowAssistItemFooter = false ,Text = response};
                    responseItem.RequestItem = inputQuery;

                    this.Messages.Add(responseItem);
                    await Task.Delay(1000).ConfigureAwait(true);
                    responseItem.Suggestion = suggestion;
                }
            }

            this.CancelResponse = false;
        }

        private void GenerateSuggestions()
        {
             List<string> firstHeaderSuggestion = new List<string> { "How can I customize the appearance of AI AssistView?", "What are the benefits of using Markdown?" };
             List<string> secondHeaderSuggestion = new List<string> { "What customization options are available in AI AssistView?",};
             List<string> thirdHeaderSuggestion = new List<string> { "What are the benefits of using Markdown?", "What customization options are available in AI AssistView?" };

             suggestionlist.Add(firstHeaderSuggestion);
             suggestionlist.Add(secondHeaderSuggestion);
             suggestionlist.Add(thirdHeaderSuggestion);
        }

        private AssistItemSuggestion GetSuggestion(string prompt)
        {
            var promptSuggestions = new AssistItemSuggestion();

            for (int i = 0; i < azureAIService.promptRequestForMarkdown.Count() - 1; i++)
            {
                if (azureAIService.promptRequestForMarkdown[i].Contains(prompt.ToLower()))
                {
                    _suggestions = new ObservableCollection<Syncfusion.Maui.AIAssistView.ISuggestion>();
                    if (suggestionlist.Count > i)
                    {
                        foreach (var items in suggestionlist[i])
                        {
                            _suggestions.Add(new AssistSuggestion() { Text = items });
                        }

                        promptSuggestions.Items = _suggestions;
                        promptSuggestions.Orientation = Syncfusion.Maui.AIAssistView.SuggestionsOrientation.Vertical;
                        return promptSuggestions;
                    }
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
