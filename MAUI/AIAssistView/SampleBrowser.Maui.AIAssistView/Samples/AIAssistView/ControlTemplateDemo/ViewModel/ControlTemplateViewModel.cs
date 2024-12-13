#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
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
    public class ControlTemplateViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<IAssistItem> assistmessages;
        private ObservableCollection<GettingStartedModel> headerInfoCollection;
        private AzureAIService azureAIService;
        private ObservableCollection<ToneModel> tonesCollection;
        private ObservableCollection<FormatModel> formatModelsCollection;
        private ObservableCollection<LengthModel> lengthModelsCollection;
        private bool isActiveChatView = true;
        private bool isActiveComposeView = false;
        private bool showIndicator = false;
        private bool showHeader = true;
        private object preferredTone;
        private string preferredFormat = "Paragraph";
        private object preferredLength ;
        private int currentIndex = 1;
        private bool leftNavigationEnabled = true;
        private bool rightNavigationEnabled = true;
        private bool isRightNavigationHovered;
        private bool isLeftNavigationHovered;
        private bool cancelResponse;
        private IAssistItem requestItem;

        public ControlTemplateViewModel()
        {
            HeaderInfoCollection = new ObservableCollection<GettingStartedModel>();
            azureAIService = new AzureAIService();
            this.GetHeaderInfo();
            this.assistmessages = new ObservableCollection<IAssistItem>();
            ChatTappedCommand = new Command<object>(ChatTabCommand);
            ComposeTappedCommand = new Command<object>(ComposeTabCommand);
            FormatModelSelectionChangedCommand = new Command<FormatModel>(OnFormatModelSelectionChanged);
            this.AssistViewRequestCommand = new Command<object>(ExecuteRequestCommand);
            this.HeaderItemTappedCommand = new Command(HeaderItemTapCommand);
            this.CopyCommand = new Command<object>(ExecuteCopyCommand);
            this.RetryCommand = new Command<object>(ExecuteRetryCommand);
            this.RefreshCommand = new Command<object>(ExecuteRefreshCommand);
            this.PreviousItemCommand = new Command(ScrollToPreviousItem);
            this.NextItemCommand = new Command(ScrollToNextItemCommand);
            this.LeftNavigationHoveredCommand = new Command<object>(OnLeftNavigationHovered);
            this.RightNavigationHoveredCommand = new Command<object>(OnRightNavigationHovered);
            this.PointerExitCommand = new Command<object>(PointerExitInteraction);
            this.StopRespondingCommand = new Command(ExecuteStopResponding);
            TonesCollection = new ObservableCollection<ToneModel>
            {
                new ToneModel("Professional", true),
                new ToneModel("Casual", false),
                new ToneModel("Enthusiastic", false),
                new ToneModel("Informational", false),
                new ToneModel("Funny", false),
                new ToneModel("Succinct", false)

            };

            FormatModelsCollection = new ObservableCollection<FormatModel>
            {
                new FormatModel("Paragraph", "\ue742", true),
                new FormatModel("Email", "\ue75c", false),
                new FormatModel("Bullet Points", "\ue74e", false),
                new FormatModel("LinkedIn Post", "\ue79c", false),
                new FormatModel("Summary", "\ue75b", false),
                new FormatModel("Report", "\ue7b1", false)
            };

            LengthModelsCollection = new ObservableCollection<LengthModel>
            {
                new LengthModel("Short", false),
                new LengthModel("Medium", true),
                new LengthModel("Long", false)
            };
            this.preferredLength = LengthModelsCollection[1];
            this.preferredTone = TonesCollection[0];
        }

        public ICommand AssistViewRequestCommand { get; set; }
        public ICommand HeaderItemTappedCommand { get; set; }
        public ICommand FormatModelSelectionChangedCommand { get; }
        public ICommand ChatTappedCommand { get; set; }
        public ICommand ComposeTappedCommand { get; set; }
        public ICommand CopyCommand { get; set; }
        public ICommand RetryCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand PreviousItemCommand { get; set; }
        public ICommand NextItemCommand { get; set; }
        public ICommand LeftNavigationHoveredCommand { get; set; }
        public ICommand RightNavigationHoveredCommand { get; set; }
        public ICommand PointerExitCommand { get; set; }
        public ICommand StopRespondingCommand { get; set; }

        private ObservableCollection<string> HeaderMessages { get; set; } = new ObservableCollection<string>
        {
            "Compare online and offline marketing strategies",
            "Why should I set achievable goals at work?",
            "Write a joke that my coworkers would find funny",
             "Why do people fly in their dreams?",
        };

        private ObservableCollection<string> ImagesCollection { get; set; } = new ObservableCollection<string>
        {
             "marketing_analysis.png",
            "goals.png",
            "funny_jokes.png",
            "dream.png",
        };

        public int CurrentIndex
        {
            get { return currentIndex; }
            set { currentIndex = value; RaisePropertyChanged("CurrentIndex"); }
        }

        public bool ShowIndicator
        {
            get
            {
                return this.showIndicator;
            }
            set
            {
                this.showIndicator = value;
                RaisePropertyChanged("ShowIndicator");
            }
        }

        public bool IsRightNavigationHovered
        {
            get
            {
                return this.isRightNavigationHovered;
            }
            set
            {
                this.isRightNavigationHovered = value;
                RaisePropertyChanged("IsRightNavigationHovered");
            }
        }

        public bool IsLeftNavigationHovered
        {
            get
            {
                return this.isLeftNavigationHovered;
            }
            set
            {
                this.isLeftNavigationHovered = value;
                RaisePropertyChanged("IsLeftNavigationHovered");
            }
        }

        public bool LeftNavigationEnabled
        {
            get
            {
                return this.leftNavigationEnabled;
            }
            set
            {
                this.leftNavigationEnabled = value;
                RaisePropertyChanged("LeftNavigationEnabled");
            }
        }

        public bool RightNavigationEnabled
        {
            get
            {
                return this.rightNavigationEnabled;
            }
            set
            {
                this.rightNavigationEnabled = value;
                RaisePropertyChanged("RightNavigationEnabled");
            }
        }

        public bool ShowHeader
        {
            get
            {
                return this.showHeader;
            }
            set
            {
                this.showHeader = value;
                RaisePropertyChanged("ShowHeader");
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
                cancelResponse = value; RaisePropertyChanged("CancelResponse");
            }
        }

        public object PreferredTone
        {
            get
            {
                return this.preferredTone;
            }
            set
            {
                this.preferredTone = value;
                RaisePropertyChanged("PreferredTone");
            }
        }

        public string PreferredFormat
        {
            get
            {
                return this.preferredFormat;
            }
            set
            {
                this.preferredFormat = value;
                RaisePropertyChanged("PreferredFormat");
            }
        }

        public object PreferredLength
        {
            get
            {
                return this.preferredLength;
            }
            set
            {
                this.preferredLength = value;
                RaisePropertyChanged("PreferredLength");
            }
        }

        public bool IsActiveChatView
        {
            get
            {
                return isActiveChatView;
            }
            set
            {
                isActiveChatView = value;
                RaisePropertyChanged(nameof(IsActiveChatView));
            }
        }

        public bool IsActiveComposeView
        {
            get
            {
                return isActiveComposeView;
            }
            set
            {
                isActiveComposeView = value;
                RaisePropertyChanged(nameof(IsActiveComposeView));
            }
        }

        public ObservableCollection<GettingStartedModel> HeaderInfoCollection
        {
            get 
            { 
                return headerInfoCollection;
            }
            set
            {
                this.headerInfoCollection = value;
                RaisePropertyChanged(nameof(HeaderInfoCollection));
            }
        }

        public ObservableCollection<IAssistItem> AssistMessages
        {
            get
            {
                return this.assistmessages;
            }

            set
            {
                this.assistmessages = value;
                RaisePropertyChanged("AssistMessages");
            }
        }

        public ObservableCollection<ToneModel> TonesCollection
        {
            get { return tonesCollection; }
            set
            {
                this.tonesCollection = value;
                RaisePropertyChanged(nameof(TonesCollection));
            }
        }

        public ObservableCollection<FormatModel> FormatModelsCollection
        {
            get { return formatModelsCollection; }
            set
            {
                if (formatModelsCollection != value)
                {
                    formatModelsCollection = value;
                    RaisePropertyChanged(nameof(FormatModelsCollection));
                }
            }
        }

        public ObservableCollection<LengthModel> LengthModelsCollection
        {
            get { return lengthModelsCollection; }
            set
            {
                if (lengthModelsCollection != value)
                {
                    lengthModelsCollection = value;
                    RaisePropertyChanged(nameof(LengthModelsCollection));
                }
            }
        }

        private void ScrollToNextItemCommand()
        {
            if (!LeftNavigationEnabled)
            {
                LeftNavigationEnabled = true;
            }
            var index = this.CurrentIndex;
            int next = index + 1;
            this.CurrentIndex = (this.CurrentIndex == (headerInfoCollection.Count - 1)) ? (headerInfoCollection.Count - 1) : next;
            if (this.CurrentIndex == headerInfoCollection.Count - 1)
            {
                RightNavigationEnabled = false;
            }
        }

        private void ScrollToPreviousItem()
        {
            if (!RightNavigationEnabled)
            {
                RightNavigationEnabled = true;
            }
            var index = this.CurrentIndex;
            int prev = index - 1;
            this.CurrentIndex = (this.CurrentIndex == 0) ? 0 : prev;
            if (this.CurrentIndex == 0)
            {
                LeftNavigationEnabled = false;
            }
        }

        private void PointerExitInteraction(object obj)
        {
            this.IsLeftNavigationHovered = false;
            this.IsRightNavigationHovered = false;
        }

        private void OnLeftNavigationHovered(object obj)
        {
            this.IsLeftNavigationHovered = true;
        }

        private void OnRightNavigationHovered(object obj)
        {
            this.IsRightNavigationHovered = true;
        }

        private async void ExecuteRefreshCommand(object obj)
        {
            this.CurrentIndex = 1;
            LeftNavigationEnabled = true;
            RightNavigationEnabled = true;
            if (this.IsActiveChatView)
            {
                this.ShowIndicator = true;
                ShowHeader = false;
                this.AssistMessages.Clear();
            }
            else
            {
                this.ShowIndicator = true;
                this.IsActiveComposeView = false;
                ShowHeader = false;
                this.AssistMessages.Clear();
                this.IsActiveChatView = true;
            }
            await Task.Delay(3000).ConfigureAwait(true);
            ShowHeader = true;
            this.ShowIndicator = false;
        }

        private void ExecuteCopyCommand(object obj)
        {
            string text = (obj as AssistItem).Text;
            text = Regex.Replace(text, "<.*?>|&nbsp;", string.Empty);
            Clipboard.SetTextAsync(text);
        }

        private async void ExecuteRetryCommand(object obj)
        {
            IAssistItem item = (obj as AssistItem).RequestItem as IAssistItem;
            if (item != null)
            {
                requestItem = item;
            }
            await this.GetResult(requestItem).ConfigureAwait(true);
        }

        private void ExecuteStopResponding()
        {
            this.CancelResponse = true;
            AssistItem responseItem = new AssistItem() { Text = "You canceled the response" };
            responseItem.ShowAssistItemFooter = false;
            this.AssistMessages.Add(responseItem);
        }

        private void ChatTabCommand(object obj)
        {
            var vm = obj as ControlTemplateViewModel;
            if (!vm.IsActiveChatView)
            {
                vm.IsActiveChatView = true;
                vm.IsActiveComposeView = false;
            }
        }

        private void ComposeTabCommand(object obj)
        {
            var vm = obj as ControlTemplateViewModel;
            if (!vm.IsActiveComposeView)
            {
                vm.IsActiveChatView = false;
                vm.IsActiveComposeView = true;
            }
        }

        private async void HeaderItemTapCommand()
        {
            requestItem = new AssistItem() { Text = HeaderMessages[this.CurrentIndex], IsRequested = true };
            this!.AssistMessages.Add(requestItem);
            await this.GetResult(requestItem).ConfigureAwait(true);
        }

        private async void ExecuteRequestCommand(object obj)
        {
            requestItem = (obj as Syncfusion.Maui.AIAssistView.RequestEventArgs).RequestItem;
            await this.GetResult(requestItem).ConfigureAwait(true);
        }

        private async Task GetResult(object inputQuery)
        {
            await Task.Delay(3000).ConfigureAwait(true);
            AssistItem request = (AssistItem)inputQuery;
            if (request != null)
            {
                var userAIPrompt = this.GetUserAIPrompt(request.Text);
                var response = await azureAIService!.GetResultsFromAI(request.Text, userAIPrompt).ConfigureAwait(true);
                response = response.Replace("\n", "<br>");
                if (!CancelResponse)
                {
                    AssistItem responseItem = new AssistItem() { Text = response };
                    responseItem.RequestItem = inputQuery;
                    this.AssistMessages.Add(responseItem);
                }
            }

            this.CancelResponse = false;
        }

        private string GetUserAIPrompt(string userPrompt)
        {
            string userQuery = $"Given User query: {userPrompt}." +
                      $"\nSome conditions need to follow:" +
                      $"\nThe tone should be {(PreferredTone as ToneModel).ToneContent}." +
                      $"\nThe format should be {PreferredFormat}." +
                      $"\nThe length should be {(PreferredLength as LengthModel).LengthContent}."+
                      $"\nGive as string alone"+
                      $"\nRemove ** and remove quotes if it is there in the string.";
            return userQuery;
        }

        private void OnFormatModelSelectionChanged(FormatModel selectedFormatModel)
        {
            if (selectedFormatModel != null)
            {
                // Deselect all other formats in the collection
                foreach (var format in FormatModelsCollection!)
                {
                    if (format != selectedFormatModel && format.IsSelected)
                    {
                        format.IsSelected = false;
                    }
                }

                // Toggle the selected format model
                selectedFormatModel.IsSelected = !selectedFormatModel.IsSelected;
                this.PreferredFormat = selectedFormatModel.Content;

                // Optional: Force update of the entire collection
                RaisePropertyChanged(nameof(FormatModelsCollection));
            }
        }

        internal void GetHeaderInfo()
        {
            var headerInfo = new ObservableCollection<GettingStartedModel>();
            int headerItemsCount = 4;

            for (int i = 0; i < headerItemsCount; i++)
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
