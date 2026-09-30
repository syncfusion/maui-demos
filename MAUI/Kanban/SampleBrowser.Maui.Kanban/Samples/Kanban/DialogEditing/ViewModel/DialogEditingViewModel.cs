using SampleBrowser.Maui.Base.Converters;
using Syncfusion.Maui.Kanban;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SampleBrowser.Maui.Kanban.SfKanban
{
    /// <summary>
    /// Represents the dialog editing view model for Kanban control.
    /// </summary>
    public class DialogEditingViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The kanban cards.
        /// </summary>
        private ObservableCollection<KanbanModel> cards = new ObservableCollection<KanbanModel>();

        /// <summary>
        /// It holds the editor visibility.
        /// </summary>
        private bool isEditorOpen = false;

        /// <summary>
        /// It holds the edit mode or not.
        /// </summary>
        private bool isEditMode = false;

        /// <summary>
        /// It holds the editor title.
        /// </summary>
        private string editorTitle = "Add New Card";

        /// <summary>
        /// It holds the editing title.
        /// </summary>
        private string editingTitle = string.Empty;

        /// <summary>
        /// It holds the editing status.
        /// </summary>
        private string editingStatus = "Open";

        /// <summary>
        /// It holds the editing tag.
        /// </summary>
        private string editingTag = string.Empty;

        /// <summary>
        /// It holds the editing assignee.
        /// </summary>
        private string editingAssignee = "Andrew Fuller";

        /// <summary>
        /// Gets or sets the editing priority.
        /// </summary>
        private string editingPriority = "Normal";

        /// <summary>
        /// It holds the editing summary.
        /// </summary>
        private string editingSummary = string.Empty;

        /// <summary>
        /// The current card details.
        /// </summary>
        private KanbanModel? currentCard;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogEditingViewModel"/> class.
        /// </summary>
        public DialogEditingViewModel()
        {
            this.Cards = this.GetTaskDetails();
            this.SaveCommand = new Command(this.SaveCard);
            this.DeleteCommand = new Command(this.DeleteCard);
            this.CancelCommand = new Command(this.CancelCard);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the cards.
        /// </summary>
        public ObservableCollection<KanbanModel> Cards
        {
            get => this.cards;
            set
            {
                if (this.cards != value)
                {
                    this.cards = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// It holds the statuses.
        /// </summary>
        public ObservableCollection<string> Statuses { get; } = new()
        {
            "Open",
            "In Progress",
            "Code Review",
            "Done"
        };

        /// <summary>
        /// It holds the assignees.
        /// </summary>
        public ObservableCollection<string> Assignees { get; } = new ObservableCollection<string>()
        {
            "Andrew Fuller",
            "Daniel Williams",
            "Laura Callahan",
            "Stephen Addison",
            "James Williams",
            "Adeline Elena"
        };

        /// <summary>
        /// It holds the priorities.
        /// </summary>
        public ObservableCollection<string> Priorities { get; } = new ObservableCollection<string>()
        {
            "Low",
            "Normal",
            "High"
        };

        /// <summary>
        /// Gets or sets the editor visibility.
        /// </summary>
        public bool IsEditorOpen
        {
            get => this.isEditorOpen;
            set
            {
                if (this.isEditorOpen != value)
                {
                    this.isEditorOpen = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the edit mode or not.
        /// </summary>
        public bool IsEditMode
        {
            get => this.isEditMode;
            set
            {
                if (this.isEditMode != value)
                {
                    this.isEditMode = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the editor title.
        /// </summary>
        public string EditorTitle
        {
            get => this.editorTitle;
            set
            {
                if (this.editorTitle != value)
                {
                    this.editorTitle = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the editing title.
        /// </summary>
        public string EditingTitle
        {
            get => this.editingTitle;
            set
            {
                if (this.editingTitle != value)
                {
                    this.editingTitle = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the editing status.
        /// </summary>
        public string EditingStatus
        {
            get => this.editingStatus;
            set
            {
                if (this.editingStatus != value)
                {
                    this.editingStatus = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the editing tag.
        /// </summary>
        public string EditingTag
        {
            get => this.editingTag;
            set
            {
                if (this.editingTag != value)
                {
                    this.editingTag = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the editing assignee.
        /// </summary>
        public string EditingAssignee
        {
            get => this.editingAssignee;
            set
            {
                if (this.editingAssignee != value)
                {
                    this.editingAssignee = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the editing priority.
        /// </summary>
        public string EditingPriority
        {
            get => this.editingPriority;
            set
            {
                if (this.editingPriority != value)
                {
                    this.editingPriority = value;
                    this.OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the editing summary.
        /// </summary>
        public string EditingSummary
        {
            get => this.editingSummary;
            set
            {
                if (this.editingSummary != value)
                {
                    this.editingSummary = value;
                    this.OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// The save button command.
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// The delete button command.
        /// </summary>
        public ICommand DeleteCommand { get; }

        /// <summary>
        /// The cancel button command.
        /// </summary>
        public ICommand CancelCommand { get; }

        #endregion

        #region internal methods.

        /// <summary>
        /// Method to open the edit popup.
        /// </summary>
        internal void OpenCreateDialog()
        {
            this.currentCard = null;
            this.IsEditMode = false;
            this.EditorTitle = "Add New Card";
            this.EditingTitle = string.Empty;
            this.EditingStatus = this.Statuses.First();
            this.EditingTag = string.Empty;
            this.EditingAssignee = this.Assignees.First();
            this.EditingPriority = this.Priorities.First();
            this.EditingSummary = string.Empty;
            this.IsEditorOpen = true;
        }

        /// <summary>
        /// Method to open the edit popup.
        /// </summary>
        /// <param name="card">The card.</param>
        internal void OpenEditDialog(KanbanModel card)
        {
            this.currentCard = card;
            this.IsEditMode = true;
            this.EditorTitle = "Edit Card";
            this.EditingTitle = card.Title ?? string.Empty;
            this.EditingStatus = card.Category?.ToString() ?? "Open";
            this.EditingTag = card.Tags?.FirstOrDefault() ?? string.Empty;
            this.EditingAssignee = GetAssigneeFromImageUrl(card.ImageURL);
            this.EditingPriority = GetPriorityText(card.IndicatorFill);
            this.EditingSummary = card.Description ?? string.Empty;
            this.IsEditorOpen = true;
        }

        #endregion

        #region Event

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Private methods


        /// <summary>
        /// Returns the image URL string for the given assignee.
        /// </summary>
        /// <param name="assignee">The assignee name</param>
        /// <returns>Image file name for the assignee</returns>
        private static string GetImageUrlForAssignee(string? assignee)
        {
            string assemblyName = typeof(SfImageSourceConverter).GetTypeInfo().Assembly.ToString();
            switch (assignee)
            {
                case "Stephen Addison":
                    return $"{assemblyName}.people_circle1.png";

                case "James Williams":
                    return $"{assemblyName}.people_circle5.png";

                case "Andrew Fuller":
                    return $"{assemblyName}.people_circle6.png";

                case "Daniel Williams":
                    return $"{assemblyName}.people_circle20.png";

                case "Laura Callahan":
                    return $"{assemblyName}.people_circle10.png";

                case "Adeline Elena":
                    return $"{assemblyName}.people_circle12.png";

                default:
                    return $"{assemblyName}.people_circle_default.png";
            }
        }

        /// <summary>
        /// Returns the assignee name for the given image URL.
        /// </summary>
        /// <param name="imageURL">The image file name or path</param>
        /// <returns>Assignee name</returns>
        private static string GetAssigneeFromImageUrl(string? imageURL)
        {
            if (string.IsNullOrWhiteSpace(imageURL))
            {
                return "Adeline Elena";
            }

            switch (imageURL)
            {
                case string s when s.EndsWith("people_circle1.png"):
                    return "Stephen Addison";

                case string s when s.EndsWith("people_circle5.png"):
                    return "James Williams";

                case string s when s.EndsWith("people_circle6.png"):
                    return "Andrew Fuller";

                case string s when s.EndsWith("people_circle20.png"):
                    return "Daniel Williams";

                case string s when s.EndsWith("people_circle10.png"):
                    return "Laura Callahan";

                case string s when s.EndsWith("people_circle12.png"):
                    return "Adeline Elena";

                default:
                    return "Adeline Elena";
            }
        }

        /// <summary>
        /// Returns a SolidColorBrush based on the given priority text.
        /// </summary>
        /// <param name="priorityText">The priority level ("Low", "Normal", "High")</param>
        /// <returns>SolidColorBrush corresponding to the priority</returns>
        private static Brush GetPriorityBrush(string? priorityText)
        {
            switch (priorityText)
            {
                case "Low":
                    return new SolidColorBrush(Colors.Orange);
                case "Normal":
                    return new SolidColorBrush(Colors.Yellow);
                default:
                    return new SolidColorBrush(Colors.Red);
            }
        }


        /// <summary>
        /// Returns the priority text ("Low", "Normal", "High") based on the given Brush.
        /// </summary>
        /// <param name="brush">The Brush to evaluate</param>
        /// <returns>Priority text string</returns>
        private static string GetPriorityText(Brush? brush)
        {
            if (brush is SolidColorBrush solidBrush)
            {
                switch (solidBrush.Color)
                {
                    case var color when color == Colors.Orange:
                        return "Low";

                    case var color when color == Colors.Yellow:
                        return "Normal";
                    default:
                        return "High";

                }
            }

            return "Normal";
        }

        /// <summary>
        /// Method to save the card details with validation.
        /// </summary>
        private void SaveCard()
        {
            if (this.currentCard != null)
            {
                this.currentCard.Title = this.EditingTitle;
                this.currentCard.Category = this.EditingStatus;
                this.currentCard.Description = this.EditingSummary;
                this.currentCard.Tags = string.IsNullOrEmpty(this.EditingTag) ? new List<string>() : new List<string> { this.EditingTag };
                this.currentCard.IndicatorFill = GetPriorityBrush(this.EditingPriority);
                this.currentCard.ImageURL = GetImageUrlForAssignee(this.EditingAssignee);
            }
            else
            {
                this.Cards.Add(new KanbanModel
                {
                    Title = this.EditingTitle,
                    Category = this.EditingStatus,
                    Description = this.EditingSummary,
                    Tags = string.IsNullOrEmpty(this.EditingTag) ? new List<string>() : new List<string> { this.EditingTag },
                    IndicatorFill = GetPriorityBrush(this.EditingPriority),
                    ImageURL = GetImageUrlForAssignee(this.EditingAssignee)
                });
            }

            this.IsEditorOpen = false;
        }

        /// <summary>
        /// Method to delete the current tapped card from the Kanban control.
        /// </summary>
        private void DeleteCard()
        {
            if (this.currentCard != null)
            {
                this.Cards.Remove(this.currentCard);
                this.currentCard = null;
            }

            this.IsEditorOpen = false;
            this.IsEditMode = false;
            this.EditorTitle = string.Empty;
            this.EditingTitle = string.Empty;
            this.EditingStatus = this.Statuses.First();
            this.EditingTag = string.Empty;
            this.EditingAssignee = this.Assignees.First();
            this.EditingPriority = this.Priorities.First();
            this.EditingSummary = string.Empty;
        }

        /// <summary>
        /// Method to cancel the editor dialog popup.
        /// </summary>
        private void CancelCard()
        {
            this.IsEditorOpen = false;
        }

        /// <summary>
        /// Method to get the card details of the kanban control.
        /// </summary>
        /// <returns>It returns the card details.</returns>
        private ObservableCollection<KanbanModel> GetTaskDetails()
        {
            Assembly assemblyName = typeof(SfImageSourceConverter).GetTypeInfo().Assembly;
            string imagePrefix = assemblyName.GetName().Name + ".";
            var cardsDetails = new ObservableCollection<KanbanModel>();

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1001,
                Title = "Application performance",
                ImageURL = imagePrefix + "people_circle1.png",
                Category = "Open",
                Description = "Improve application performance.",
                IndicatorFill = Colors.Red,
                Tags = new List<string> { "Performance" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1002,
                Title = "Customer meeting",
                ImageURL = imagePrefix + "people_circle5.png",
                Category = "Open",
                Description = "Arrange a web meeting with the customer to get new requirements.",
                IndicatorFill = Colors.Orange,
                Tags = new List<string> { "Meeting" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1003,
                Title = "Data in Grid",
                ImageURL = imagePrefix + "people_circle6.png",
                Category = "Open",
                Description = "Show the retrieved data from the server in grid control.",
                IndicatorFill = Colors.Yellow,
                Tags = new List<string> { "Data" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1004,
                Title = "Analysis",
                ImageURL = imagePrefix + "people_circle10.png",
                Category = "In Progress",
                Description = "Analyze SQL Server 2008 connection.",
                IndicatorFill = Colors.Orange,
                Tags = new List<string> { "Analysis" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1005,
                Title = "Edge browser issues",
                ImageURL = imagePrefix + "people_circle12.png",
                Category = "In Progress",
                Description = "Fix the issues reported in the Edge browser.",
                IndicatorFill = Colors.Orange,
                Tags = new List<string> { "Bug" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1006,
                Title = "SQL error",
                ImageURL = imagePrefix + "people_circle1.png",
                Category = "In Progress",
                Description = "Fix cannot open user's default database SQL error.",
                IndicatorFill = Colors.Red,
                Tags = new List<string> { "Bug" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1007,
                Title = "Chrome issue",
                ImageURL = imagePrefix + "people_circle5.png",
                Category = "In Progress",
                Description = "Fix editing issues reported in chrome.",
                IndicatorFill = Colors.Red,
                Tags = new List<string> { "Bug" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1008,
                Title = "Responsive support",
                ImageURL = imagePrefix + "people_circle6.png",
                Category = "In Progress",
                Description = "Add responsive support to application.",
                IndicatorFill = Colors.Red,
                Tags = new List<string> { "Feature" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1009,
                Title = "Review editing changes",
                ImageURL = imagePrefix + "people_circle10.png",
                Category = "Code Review",
                Description = "Review the latest editing changes.",
                IndicatorFill = Colors.Yellow,
                Tags = new List<string> { "Review" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1010,
                Title = "Review filtering changes",
                ImageURL = imagePrefix + "people_circle12.png",
                Category = "Code Review",
                Description = "Review the filtering implementation and confirm quality.",
                IndicatorFill = Colors.Red,
                Tags = new List<string> { "Review" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1011,
                Title = "Customer meeting",
                ImageURL = imagePrefix + "people_circle1.png",
                Category = "Done",
                Description = "Arrange a web meeting with the customer to show filtering demo.",
                IndicatorFill = Colors.Orange,
                Tags = new List<string> { "Meeting" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1012,
                Title = "Filtering issue",
                ImageURL = imagePrefix + "people_circle5.png",
                Category = "Done",
                Description = "Fix the filtering issues reported in Safari.",
                IndicatorFill = Colors.Red,
                Tags = new List<string> { "Bug" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1014,
                Title = "Analysis",
                ImageURL = imagePrefix + "people_circle6.png",
                Category = "Done",
                Description = "Analyze stored procedures.",
                IndicatorFill = Colors.Orange,
                Tags = new List<string> { "Analysis" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1015,
                Title = "Editing feature",
                ImageURL = imagePrefix + "people_circle10.png",
                Category = "Done",
                Description = "Test editing feature in the IE browser.",
                IndicatorFill = Colors.Red,
                Tags = new List<string> { "Testing" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1016,
                Title = "Filtering feature",
                ImageURL = imagePrefix + "people_circle12.png",
                Category = "Done",
                Description = "Test filtering in the IE browser.",
                IndicatorFill = Colors.Yellow,
                Tags = new List<string> { "Testing" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1018,
                Title = "Testing",
                ImageURL = imagePrefix + "people_circle1.png",
                Category = "Done",
                Description = "Test the application in the IE browser.",
                IndicatorFill = Colors.Red,
                Tags = new List<string> { "Testing" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1019,
                Title = "Analysis",
                ImageURL = imagePrefix + "people_circle5.png",
                Category = "Done",
                Description = "Analyze grid control.",
                IndicatorFill = Colors.Yellow,
                Tags = new List<string> { "Analysis" }
            });

            cardsDetails.Add(new KanbanModel()
            {
                ID = 1021,
                Title = "Testing",
                ImageURL = imagePrefix + "people_circle6.png",
                Category = "Done",
                Description = "Check login page validation.",
                IndicatorFill = Colors.Orange,
                Tags = new List<string> { "Testing" }
            });

            return cardsDetails;
        }

        #endregion
    }
}