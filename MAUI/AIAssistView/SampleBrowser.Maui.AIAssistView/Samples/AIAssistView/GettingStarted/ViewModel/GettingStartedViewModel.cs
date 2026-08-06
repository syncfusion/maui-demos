using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using SampleBrowser.Maui.Base.Converters;
using Syncfusion.Maui.AIAssistView;

#nullable disable
namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    public class GettingStartedViewModel : INotifyPropertyChanged
    {
        #region Field
        private ObservableCollection<IAssistItem> messages;
        private ObservableCollection<ISuggestion> headerInfoCollection;

        private ObservableCollection<ISuggestion> suggestions;

        private ObservableCollection<AssistConversationItem> history;
        private ObservableCollection<ISuggestion> autoSuggestions;
        private List<List<string>> suggestionlist = new List<List<string>>();
        private List<List<string>> agentSuggestionList = new List<List<string>>();
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
        private bool isListening;
        private AutoSuggestionOverlay autoSuggestionOverlay;
        private bool isEditorVisible;
        private bool isPopupOpen;
        private bool isTemporaryPopup;
        private ObservableCollection<AssistAgent> agentCollection;
        private AssistAgent currentAgent;
        private ObservableCollection<ISuggestion> codingAgentSuggestions;
        private ObservableCollection<ISuggestion> writingAgentSuggestions;
        private ObservableCollection<ISuggestion> dataAgentSuggestions;
        private FontImageSource agentIcon;
        private ObservableCollection<ISuggestion> footerSuggestions;
        private Border attachmentBorder;
        private bool IsStopSuggestion = true;
        private Profile assistProfile;
        #endregion

        #region Constructor
        public GettingStartedViewModel()
        {
            azureAIService = new AzureAIService();
            this.agentIcon = new FontImageSource()
            {
                Glyph = "\ue7e1",
                FontFamily = "MauiSampleFontIcon",
                Size = 16,
                Color = Colors.Blue,
            };
            this.GetHeaderInfo();
            this.GetHeaderSuggestions();
            this.SetRandomHeaderText();
            this.GenerateSuggestions();
            this.GenerateAgentSuggestions();
            this.messages = new ObservableCollection<IAssistItem>();
            this.history = new ObservableCollection<AssistConversationItem>();
            this.autoSuggestions = new ObservableCollection<ISuggestion>();
            this.agentCollection = new ObservableCollection<AssistAgent>();
            this.codingAgentSuggestions = new ObservableCollection<ISuggestion>();
            this.writingAgentSuggestions = new ObservableCollection<ISuggestion>();
            this.dataAgentSuggestions = new ObservableCollection<ISuggestion>();
            this.footerSuggestions = new ObservableCollection<ISuggestion>();
            this.InitializeAutoSuggestions();
            this.InitializeConversationHistory();
            this.InitializeAgentCollection();
            this.InitializeAgentSuggestions();
            this.AutoSuggestionOverlay.AutoSuggestions = this.AutoSuggestions;
            this.NewChatTappedCommand = new Command<object>(ExecuteNewChatTappedCommand);
            this.SendButtonCommand = new Command(ExecuteSendButtonCommand);
            this.ChipCommand = new Command<object>(ExecuteChipCommand);
            this.ReadCommand = new Command<object>(ExecuteReadCommand);
            this.CopyCommand = new Command<object>(ExecuteCopyCommand);
            this.RetryCommand = new Command<object>(ExecuteRetryCommand);
            this.AssistViewRequestCommand = new Command<object>(ExecuteRequestCommand);
            this.HeaderItemTappedCommand = new Command(HeaderItemTapCommand);
            this.StopRespondingCommand = new Command(ExecuteStopResponding);
            this.AttachmentButtonCommand = new Command<object>(ShowAttachmentPopup);
            this.EmptyViewPopupIsOpen = new Command(ExecuteEmptyViewPopup);
            this.TemporaryPopupCommad = new Command(ExecuteTemporaryPopup);
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

        private ObservableCollection<string> HeaderSuggestions { get; set; } = new ObservableCollection<string>
        {
            "Explain the key characteristics of ownership and how it helps individuals take responsibility for outcomes.",
            "Describe the purpose of brainstorming, its benefits, and how it helps teams generate better ideas.",
            "List the main types of listening and explain when each type is most effective.",
            "Define resilience, describe why it matters, and share how people can build it over time.",
        };

        private ObservableCollection<string> ImagesCollection { get; set; } = new ObservableCollection<string>
        {
            "ownership.png",
            "brainstorming.png",
            "listening.png",
            "resilience.png",
        };

        private ObservableCollection<string> HeaderTitleCollection { get; set; } = new ObservableCollection<string>
        {
            "How can I help you?",
            "What would you like to learn today?",
            "Need help exploring new ideas?",
            "Ready to get an answer?",
            "Ask me anything about AI assistance",
        };

        private Random random = new Random();

        #endregion

        #region Commands

        public ICommand ReadCommand { get; set; }
        public ICommand CopyCommand { get; set; }
        public ICommand RetryCommand { get; set; }
        public ICommand AssistViewRequestCommand { get; set; }
        public ICommand HeaderItemTappedCommand { get; set; }
        public ICommand StopRespondingCommand { get; set; }
        public ICommand SendButtonCommand { get; set; }
        public ICommand ChipCommand { get; set; }
        public ICommand NewChatTappedCommand { get; set; }
        public ICommand AttachmentButtonCommand { get; set; }
        public ICommand EmptyViewPopupIsOpen { get; set; }
        public ICommand TemporaryPopupCommad { get; set; }
        #endregion

        #region Public Properties

        public ObservableCollection<ISuggestion> HeaderInfoCollection
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

        public ObservableCollection<ISuggestion> Suggestions
        {
            get
            {
                return suggestions;
            }
            set
            {
                this.suggestions = value;
            }
        }

        private string headerText;
        public string HeaderText
        {
            get { return headerText; }
            set
            {
                headerText = value;
                RaisePropertyChanged(nameof(HeaderText));
            }
        }

        public ObservableCollection<AssistConversationItem> History
        {
            get
            {
                return this.history;
            }
            set
            {
                this.history = value;
                RaisePropertyChanged(nameof(History));
            }
        }

        public ObservableCollection<ISuggestion> AutoSuggestions
        {
            get
            {
                return this.autoSuggestions;
            }
            set
            {
                this.autoSuggestions = value;
                RaisePropertyChanged(nameof(AutoSuggestions));
            }
        }

        public AutoSuggestionOverlay AutoSuggestionOverlay
        {
            get => this.autoSuggestionOverlay;
            set
            {
                this.autoSuggestionOverlay = value;
                RaisePropertyChanged(nameof(autoSuggestionOverlay));
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
                if(value == null)
                {
                    this.messages.CollectionChanged -= this.OnCollectionChanged;
                }
                else
                {
                    this.messages.CollectionChanged += this.OnCollectionChanged;
                }

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
            set
            {
                inputText = value;
                RaisePropertyChanged(nameof(InputText));
            }
        }

        public bool IsListening
        {
            get { return isListening; }
            set
            {
                isListening = value;
                RaisePropertyChanged(nameof(IsListening));
            }
        }

        public bool IsEditorVisible
        {
            get { return isEditorVisible; }
            set { isEditorVisible = value; RaisePropertyChanged(nameof(IsEditorVisible)); }
        }

        public bool IsPopupOpen
        {
            get { return isPopupOpen; }
            set { isPopupOpen  = value; RaisePropertyChanged(nameof(IsPopupOpen)); }
        }
        public bool IsTemporaryPopup
        {
            get { return isTemporaryPopup; }
            set { isTemporaryPopup = value; RaisePropertyChanged(nameof(IsTemporaryPopup));  }
        }

        public ObservableCollection<AssistAgent> AgentCollection
        {
            get { return agentCollection; }
            set { agentCollection = value; RaisePropertyChanged(nameof(AgentCollection)); }
        }

        public Profile AsssistProfile
        {
            get { return assistProfile; }
            set { assistProfile = value; RaisePropertyChanged(nameof(AsssistProfile)); }
        }

        public AssistAgent CurrentAgent
        {
            get { return currentAgent; }
            set
            { 
                currentAgent = value;
                this.AsssistProfile = value == null ? null : new Profile() { Avatar = value.Icon, Name = value.Name };
                RaisePropertyChanged(nameof(CurrentAgent));
            }
        }
        public ObservableCollection<ISuggestion> CodingAgentSuggestions
        {
            get { return codingAgentSuggestions; }
            set { codingAgentSuggestions = value; RaisePropertyChanged(nameof(CodingAgentSuggestions)); }
        }

        public ObservableCollection<ISuggestion> WritingAgentSuggestions
        {
            get { return writingAgentSuggestions; }
            set { writingAgentSuggestions = value; RaisePropertyChanged(nameof(WritingAgentSuggestions)); }
        }

        public ObservableCollection<ISuggestion> DataAgentSuggestions
        {
            get { return dataAgentSuggestions; }
            set { dataAgentSuggestions = value; RaisePropertyChanged(nameof(DataAgentSuggestions)); }
        }

        public ObservableCollection<ISuggestion> FooterSuggestions
        {
            get { return footerSuggestions; }
            set { footerSuggestions = value; RaisePropertyChanged(nameof(FooterSuggestions)); }
        }

        public Border AttachmentBorder
        {
            get { return this.attachmentBorder; }
            set {  this.attachmentBorder = value; RaisePropertyChanged(nameof(AttachmentBorder)); }
        }

        #endregion

        #region Internal Methods

        internal async Task GetResult(object inputQuery)
        {
            await Task.Delay(3000).ConfigureAwait(true);
            AssistItem request = (AssistItem)inputQuery;
            if (request != null)
            {
                string response;
                if (this.CurrentAgent != null)
                {
                    response = await azureAIService.GetResultsFromAIForAgent(request.Text, this.CurrentAgent.Instructions);
                }
                else
                {
                    var userAIPrompt = this.GetUserAIPrompt(request.Text);
                    response = await azureAIService!.GetResultsFromAI(request.Text, userAIPrompt).ConfigureAwait(true);
                }

                response = response.Replace("\n", "<br>");
                if (!CancelResponse && this.canAddResponse)
                {
                    AssistItem responseItem = new AssistItem() { Text = response };
                    if (this.CurrentAgent != null)
                    {
                        responseItem.Profile = new Profile() { Name = this.CurrentAgent.Name, Avatar = this.CurrentAgent.Icon };
                    }

                    responseItem.RequestItem = inputQuery;
                    this.Messages.Add(responseItem);
                    this.enableNewChatIcon = true;
                    this.IsNewChatViewVisible = true;
                    this.EnableSendIcon = !string.IsNullOrEmpty(this.InputText);
                }
            }

            this.CancelResponse = false;
        }

        internal async Task GetImageResult(object inputQuery)
        {
            await Task.Delay(3000).ConfigureAwait(true);
            AssistItem request = (AssistItem)inputQuery;
            if (request != null)
            {
                var response = await this.azureAIService.GetImageResultFromAIForAgent(request.Text, this.CurrentAgent.Instructions, this.CurrentAgent.Description).ConfigureAwait(true);
                AssistImageItem responseItem = new AssistImageItem() { ShowAssistItemFooter = false,};
                if (response == null)
                {
                    responseItem.Text = "Please try again with a different query.";
                }
                else if (response == "bird08.png" || response == "place134.jpg")
                {
                    responseItem.SetBinding(AssistImageItem.SourceProperty, new Binding() { Source = $"{response}", Converter = new SfImageSourceConverter() });
                }
                else if (response.Equals("Image generation failed.", StringComparison.OrdinalIgnoreCase))
                {
                    responseItem.Text = "Unable to process the request. Please connect to your preferred AI service and retry.";
                }
                else
                {
                    responseItem.Source = ImageSource.FromFile(response);
                }

                if (this.CurrentAgent != null)
                {
                    responseItem.Profile = new Profile() { Name = this.CurrentAgent.Name, Avatar = this.CurrentAgent.Icon };
                }
                
                if (!CancelResponse && this.canAddResponse)
                {
                    responseItem.RequestItem = inputQuery;
                    responseItem.Aspect = Aspect.AspectFill;
                    this.Messages.Add(responseItem);
                    this.enableNewChatIcon = true;
                    this.IsNewChatViewVisible = true;
                    this.EnableSendIcon = !string.IsNullOrEmpty(this.InputText);
                }
            }

            this.CancelResponse = false;
        }

        internal void SetFooterSuggestions(string text)
        {
            var promptSuggestions = new ObservableCollection<ISuggestion>();
            if (text.StartsWith("<b>Writing a Professional Email"))
            {
                foreach (var items in agentSuggestionList[0])
                {
                    promptSuggestions.Add(new AssistSuggestion() { Text = items });
                }
            }
            else if (text.StartsWith("<b>Enhancing Writing Skills"))
            {
                foreach (var items in agentSuggestionList[1])
                {
                    promptSuggestions.Add(new AssistSuggestion() { Text = items });
                }
            }
            
            this.FooterSuggestions = promptSuggestions;
        }

        internal void SetRandomHeaderText()
        {
            if (this.HeaderTitleCollection != null && this.HeaderTitleCollection.Count > 0)
            {
                var index = this.random.Next(0, this.HeaderTitleCollection.Count);
                this.HeaderText = this.HeaderTitleCollection[index];
            }
        }
        #endregion

        #region Private Methods

        private void InitializeConversationHistory()
        {
            DateTime baseTime = DateTime.Now;

            string[] topics = new string[]
            {
                "How to Improve Code Quality",
                "Best Practices for API Design",
                "Cloud Migration Strategy",
                "Database Optimization Tips",
                "Security Considerations",
                "Performance Tuning",
                "Testing Strategies",
                "DevOps Fundamentals",
                "Microservices Architecture",
                "Containerization with Docker",
            };

            string[] responses = new string[]
            {
                "<b>How to Improve Code Quality:</b><br>1. Follow SOLID principles (Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion) to create maintainable and scalable code.<br>2. Write comprehensive unit tests with high coverage to catch bugs early and facilitate refactoring with confidence.<br>3. Conduct regular code reviews to share knowledge, identify issues, and maintain consistent coding standards across the team.<br>4. Use static analysis tools like SonarQube or StyleCop to automatically detect code smells, security vulnerabilities, and potential bugs.",
                "<b>Best Practices for API Design:</b><br>1. Use RESTful principles with clear resource models and proper HTTP methods (GET for retrieval, POST for creation, PUT for updates, DELETE for removal).<br>2. Implement API versioning (URL-based or header-based) to maintain backward compatibility while introducing new features.<br>3. Provide comprehensive API documentation using tools like Swagger/OpenAPI with examples, error codes, and authentication details.<br>4. Design consistent error responses, implement rate limiting, and use proper status codes to communicate API behavior clearly.",
                "<b>Cloud Migration Strategy:</b><br>1. Assess current infrastructure by documenting applications, dependencies, performance metrics, and security requirements in detail.<br>2. Choose appropriate cloud services (IaaS, PaaS, SaaS) based on workload requirements, cost analysis, and organizational expertise.<br>3. Create a phased migration plan starting with non-critical systems, conducting thorough testing at each stage, and maintaining rollback procedures.<br>4. Monitor post-migration performance, optimize resource allocation, and continuously review costs to ensure ROI.",
                "<b>Database Optimization Tips:</b><br>1. Implement proper indexing on frequently queried columns, but avoid over-indexing which can slow down write operations.<br>2. Optimize queries by using EXPLAIN plans, eliminating N+1 queries, and using appropriate JOIN strategies for your database system.<br>3. Implement connection pooling to reuse database connections efficiently and reduce overhead from establishing new connections.<br>4. Use caching strategies (Redis, Memcached) for frequently accessed data, and consider database partitioning for very large datasets to improve query performance.",
                "<b>Security Considerations:</b><br>1. Implement robust authentication mechanisms (OAuth 2.0, JWT) and authorization frameworks (RBAC, ABAC) to control access to sensitive resources.<br>2. Encrypt sensitive data both in transit (TLS/SSL) and at rest using industry-standard encryption algorithms like AES-256.<br>3. Validate all user inputs on the server-side to prevent injection attacks, XSS, and other common vulnerabilities.<br>4. Conduct regular security audits, penetration testing, and stay updated with security patches to protect against emerging threats.",
                "<b>Performance Tuning:</b><br>1. Profile your code using tools like dotTrace or profilers to identify bottlenecks and resource-intensive operations accurately.<br>2. Optimize algorithms by choosing appropriate data structures, reducing time complexity, and avoiding unnecessary computations.<br>3. Implement caching layers (distributed caches) and asynchronous processing to handle high loads and improve response times.<br>4. Use load balancing techniques and horizontal scaling to distribute traffic across multiple servers and ensure system reliability.",
                "<b>Testing Strategies:</b><br>1. Unit tests verify individual components in isolation, ensuring each function behaves correctly with various inputs and edge cases.<br>2. Integration tests validate interactions between multiple components, ensuring they work together as expected within the system.<br>3. End-to-end tests simulate real user scenarios to verify complete workflows function properly from UI to database.<br>4. Performance and security tests ensure the application meets non-functional requirements and is protected against common vulnerabilities.",
                "<b>DevOps Fundamentals:</b><br>1. Implement Continuous Integration to automatically build, test, and validate code changes, catching issues early in the development cycle.<br>2. Enable Continuous Deployment to automate the release process, reducing manual errors and enabling faster delivery of features.<br>3. Automate infrastructure provisioning using Infrastructure-as-Code tools like Terraform to ensure consistent, reproducible environments.<br>4. Implement comprehensive monitoring and logging to track application health, performance metrics, and quickly identify and resolve issues.",
                "<b>Microservices Architecture:</b><br>1. Break monolithic applications into smaller, independent services that can be developed, deployed, and scaled independently.<br>2. Use APIs for inter-service communication to maintain loose coupling and enable services to evolve independently.<br>3. Implement service discovery mechanisms to dynamically locate services, handle load balancing, and manage service registration and deregistration.<br>4. Design for failure with circuit breakers, timeouts, and retry mechanisms to ensure system resilience when services fail.",
                "<b>Containerization with Docker:</b><br>1. Package applications with all dependencies into lightweight, portable containers ensuring consistency across development, testing, and production environments.<br>2. Create optimized Dockerfiles using multi-stage builds, minimal base images, and proper layer caching to reduce image size and build time.<br>3. Use orchestration tools like Kubernetes to automate deployment, scaling, and management of containers across clusters.<br>4. Implement container security best practices including scanning for vulnerabilities, using private registries, and running containers with minimal privileges."
            };

            for (int i = 0; i < 10; i++)
            {
                var dateTime = baseTime.AddDays(-i);
                var request = new AssistItem
                {
                    Text = topics[i],
                    IsRequested = true,
                    DateTime = dateTime,
                };

                var response = new AssistItem
                {
                    Text = responses[i],
                    IsRequested = false,
                    DateTime = dateTime,
                    RequestItem = request,
                };

                var title = topics[i];
                var conversationItem = new AssistConversationItem
                {
                    Title = title,
                    DateTime = baseTime.AddDays(-i),
                    AssistItems = new ObservableCollection<IAssistItem>
                    {
                        request,
                        response,
                    }
                };

                this.History.Add(conversationItem);
                this.AutoSuggestions.Add(new AssistSuggestion { Text = title, Category = "History" });
            }

            this.AutoSuggestionOverlay?.AutoSuggestions = this.AutoSuggestions;
        }

        private void InitializeAutoSuggestions()
        {
            this.AutoSuggestions.Add(new AssistSuggestion() { Text = "What is time management", Category = "Recommended" });
            this.AutoSuggestions.Add(new AssistSuggestion() { Text = "How to improve productivity", Category = "Recommended" });
            this.AutoSuggestions.Add(new AssistSuggestion() { Text = "What are communication skills", Category = "Recommended" });
            this.AutoSuggestions.Add(new AssistSuggestion() { Text = "How to build a strong team", Category = "Recommended" });
            this.AutoSuggestions.Add(new AssistSuggestion() { Text = "What is project management", Category = "Recommended" });
            this.AutoSuggestions.Add(new AssistSuggestion() { Text = "How to handle stress", Category = "Recommended" });
            var overlay = new AutoSuggestionOverlay()
            {
                AutoSuggestions = new ObservableCollection<ISuggestion>(),
            };

            overlay.AllowGrouping = true;
            this.AutoSuggestionOverlay = overlay;
        }

        private async void ExecuteNewChatTappedCommand(object obj)
        {
            await Task.Delay(100);
            this.canAddResponse = false;
            this.IsHeaderVisible = true;
            this.IsNewChatViewVisible = false;
            this.enableNewChatIcon = true;
            this.InputText = string.Empty;
            this.Messages.Clear();
            this.SetRandomHeaderText();
        }

        private void ShowAttachmentPopup(object obj)
        {
            if (obj is Border border)
            {
                this.AttachmentBorder = border;
                IsAttachmentPopupOpen = true;
            }
        }

        private async void HeaderItemTapCommand(object obj)
        {
            this.CancelResponse = true;
            var args = obj as SuggestionItemSelectedEventArgs;
            if (args != null && args.SelectedItem is GettingStartedModel suggestion)
            {
                requestItem = new AssistItem() { Text = suggestion.Text, IsRequested = true };
                if (string.IsNullOrEmpty(suggestion.SuggestionDescription))
                {
                    await this.GetResult(requestItem).ConfigureAwait(true);
                }
                else
                {
                    await this.GetResponseWithSuggestion(requestItem).ConfigureAwait(true);
                }
            }

            this.CancelResponse = false;
        }

        private async void ExecuteRequestCommand(object obj)
        {
            requestItem = (obj as Syncfusion.Maui.AIAssistView.RequestEventArgs).RequestItem;
            if (this.CurrentAgent != null && this.CurrentAgent.Name == "Art Assistant")
            {
                await this.GetImageResult(requestItem).ConfigureAwait(true);
            }
            else if (this.CurrentAgent != null)
            {
                await this.GetResult(requestItem).ConfigureAwait(true);
            }
            else
            {
                await this.GetResult(requestItem).ConfigureAwait(true);
            }
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
            var args = obj as SuggestionItemSelectedEventArgs;
            var chipText = (args.SelectedItem as AssistSuggestion).Text;

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

            args.CancelRequest = true;
        }

        private void ExecuteReadCommand(object obj)
        {
            var text = (obj as AssistContextMenuItem).AssistItem.Text;
            text = Regex.Replace(text, "<.*?>|&nbsp;", string.Empty);
            TextToSpeech.Default.SpeakAsync(text);
        }

        private async void ExecuteSendButtonCommand()
        {
            await Task.Delay(100);
            this.enableNewChatIcon = false;
            this.IsEditorVisible = true;
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
            this.IsStopSuggestion = false;
            AssistItem responseItem = new AssistItem() { Text = "You canceled the response" };
            if (this.CurrentAgent != null)
            {
                responseItem.Profile = new Profile() { Name = this.CurrentAgent.Name, Avatar = this.CurrentAgent.Icon };
            }

            responseItem.ShowAssistItemFooter = false;
            this.Messages.Add(responseItem);
            this.IsNewChatViewVisible = true;
            this.enableNewChatIcon = true;
            this.EnableSendIcon = !string.IsNullOrEmpty(this.InputText);
        }

        private void GetHeaderInfo()
        {
            var headerInfo = new ObservableCollection<ISuggestion>();
            for (int i = 0; i < 3; i++)
            {
                var gallery = new GettingStartedModel()
                {
                    Image = this.ImagesCollection[i],
                    SuggestionDescription = this.HeaderMessages[i],
                    Text = this.HeaderSuggestions[i],
                };
                headerInfo.Add(gallery);
            }
            this.HeaderInfoCollection = headerInfo;
        }


        private void GetHeaderSuggestions()
        {
            var headerInfo = new ObservableCollection<ISuggestion>();
            for (int i = 0; i < 4; i++)
            {
                var gallery = new AssistSuggestion()
                {
                    Text = this.HeaderMessages[i],
                };
                headerInfo.Add(gallery);
            }
            this.Suggestions = headerInfo;
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
                if (!CancelResponse && this.IsStopSuggestion)
                {
                    AssistItem responseItem = new AssistItem() { Text = response, Suggestion = suggestion };
                    responseItem.RequestItem = inputQuery;
                    this.Messages.Add(responseItem);
                }
            }

            this.CancelResponse = false;
            this.IsStopSuggestion = true;
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

        private void GenerateAgentSuggestions()
        {
            List<string> writingAgentSuggestion1 = new List<string> { "Can you give me a sample email?", "How can I make my email more polite?" };
            List<string> writingAgentSuggestions2 = new List<string> { "How can I improve my grammar?", "How do I make my writing more clear?" };
            agentSuggestionList.Add(writingAgentSuggestion1);
            agentSuggestionList.Add(writingAgentSuggestions2);
        }

        private AssistItemSuggestion GetSuggestion(string prompt)
        {
            var promptSuggestions = new AssistItemSuggestion();
            if (string.IsNullOrWhiteSpace(prompt))
            {
                return promptSuggestions;
            }
            // Clean HTML and punctuation, normalize to lower-case
            var cleanedPrompt = Regex.Replace(prompt, "<.*?>|&nbsp;", string.Empty);
            string normalizedPrompt = Regex.Replace(cleanedPrompt.ToLowerInvariant(), "[^\\w\\s]", " ");
            string[] promptWords = normalizedPrompt.Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

            // Use sets and ignore very short/common words to reduce accidental matches
            var promptSet = new HashSet<string>(promptWords.Where(w => w.Length > 2));

            for (int i = 0; i < HeaderMessages.Count; i++)
            {
                string normalizedHeader = HeaderMessages[i].ToLowerInvariant();
                string normalizedHeaderSuggestion = i < HeaderSuggestions.Count ? HeaderSuggestions[i].ToLowerInvariant() : string.Empty;

                // Normalize header text and suggestion (remove punctuation)
                string normalizedHeaderClean = Regex.Replace(normalizedHeader, "[^\\w\\s]", " ");
                string normalizedHeaderSuggestionClean = Regex.Replace(normalizedHeaderSuggestion, "[^\\w\\s]", " ");

                string[] headerWords = normalizedHeaderClean.Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                string[] headerSuggestionWords = normalizedHeaderSuggestionClean.Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

                var headerSet = new HashSet<string>(headerWords.Where(w => w.Length > 2));
                var headerSuggestionSet = new HashSet<string>(headerSuggestionWords.Where(w => w.Length > 2));

                // 1) Direct header title appears as whole word(s) in prompt
                bool headerInPrompt = Regex.IsMatch(normalizedPrompt, $"\\b{Regex.Escape(normalizedHeaderClean.Trim())}\\b");
                // 2) Shared significant words between prompt and header title
                bool promptSharesHeaderWords = promptSet.Overlaps(headerSet);
                // 3) Shared significant words between prompt and header suggestion (description)
                bool promptSharesHeaderSuggestionWords = !string.IsNullOrEmpty(normalizedHeaderSuggestion) && promptSet.Overlaps(headerSuggestionSet);

                if (headerInPrompt && promptSharesHeaderWords && promptSharesHeaderSuggestionWords)
                {
                    var suggestions = new ObservableCollection<ISuggestion>();
                    if (i < suggestionlist.Count)
                    {
                        foreach (var item in suggestionlist[i])
                        {
                            suggestions.Add(new AssistSuggestion() { Text = item });
                        }
                    }
                    promptSuggestions.Items = suggestions;
                    promptSuggestions.Orientation = SuggestionsOrientation.Horizontal;
                    return promptSuggestions;
                }
            }
            return promptSuggestions;
        }

        private void ExecuteEmptyViewPopup()
        {
            this.IsPopupOpen = true;
        }

        private void ExecuteTemporaryPopup()
        {
            this.IsTemporaryPopup = true;
        }

        private void InitializeAgentCollection()
        {
            var agent1 = new AssistAgent
            {
                Name = "Writing Assistant",
                Description = "Helps with writing, editing, and brainstorming",
                Instructions = "You are a writing assistant that helps with content creation, editing, brainstorming ideas, and improving written communication. Provide constructive feedback and creative suggestions.",
            };
            agent1.SetBinding(AssistAgent.IconProperty, new Binding() { Source = "richtexteditor.png", Converter = new SfImageSourceConverter() });

            var agent2 = new AssistAgent
            {
                Name = "Art Assistant",
                Description = "Creates images based on user descriptions and ideas",
                Instructions = "You are an image generation assistant that creates visual content from user prompts. Understand the user's description clearly and generate accurate, creative, and high-quality images. If needed, refine the prompt for better results. Keep responses simple and confirm the type of image before generating when require",
            };
            agent2.SetBinding(AssistAgent.IconProperty, new Binding() { Source = "imageeditor.png", Converter = new SfImageSourceConverter() });

            this.AgentCollection.Add(agent1);
            this.AgentCollection.Add(agent2);
        }

        private void InitializeAgentSuggestions()
        {
            this.WritingAgentSuggestions.Add(new AssistSuggestion() { Text = "How can I write a professional email?" });
            this.WritingAgentSuggestions.Add(new AssistSuggestion() { Text = "How do I improve my writing skills?" });

            this.DataAgentSuggestions.Add(new AssistSuggestion() { Text = "Generate a realistic image of a small bird perched on a dry grass stem against a clear blue sky, with natural lighting and sharp details" });
            this.DataAgentSuggestions.Add(new AssistSuggestion() { Text = "Generate a realistic image of a large tree with wide branches near a water body, with a bench placed underneath, surrounded by greenery and soft natural light" });
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    var assistItem = e.NewItems[0] as IAssistItem;
                    if (assistItem != null && !assistItem.IsRequested)
                    {
                        this.SetFooterSuggestions(assistItem.Text);
                    }
                }
            }
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
