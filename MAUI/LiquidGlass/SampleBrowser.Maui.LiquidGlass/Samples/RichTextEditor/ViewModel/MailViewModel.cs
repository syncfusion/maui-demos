#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Popup;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SampleBrowser.Maui.LiquidGlass.SfRichTextEditor
{
    public class MailViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MailModel> _mailAddresses = new();
        private string _mailContent = string.Empty;
        private bool _sendButtonVisibility = false;

        private ObservableCollection<MailModel> _selectedFromAddresses = new();
        private ObservableCollection<MailModel> _selectedToAddresses = new();

        private readonly Command _sendButtonCommand;
        private readonly Command _discardButtonCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailViewModel"/> class.
        /// This constructor initializes the list of email addresses and selected addresses.
        /// </summary>
        public MailViewModel(Syncfusion.Maui.Popup.SfPopup? popup = null)
        {
            if (popup != null)
                _popup = popup;
            // Sample list of 10 email addresses
            MailAddresses = new()
            {
                new("alice@example.com", "people_circle13.png"),
                new("bob@example.com", "people_circle1.png"),
                new("carol@example.com", "people_circle14.png"),
                new("dan@example.com", "people_circle19.png"),
                new("eve@example.com", "people_circle2.png"),
                new("frank@example.com", "people_circle22.png"),
                new("grace@example.com", "people_circle23.png"),
                new("henry@example.com", "people_circle25.png"),
                new("ivy@example.com", "people_circle27.png"),
                new("jack@example.com", "people_circle3.png")
            };

            SelectedToAddresses.Add(MailAddresses[5]);
            SelectedFromAddresses.Add(MailAddresses[8]);

            // Initialize commands
            _sendButtonCommand = new Command(ExecuteSendCommand);
            _discardButtonCommand = new Command(ExecuteDiscardCommand);

            MailContent = @"<p><strong>Hello Frank,</strong></p><p>I hope you're doing well. I'm writing to follow up on our recent conversation regarding <strong>MAUI control development</strong>.</p><p>Here’s a quick summary of the agenda:</p><ul><li>✅ Kickoff completed successfully</li><li>📌 Deliverables in progress</li><li>💬 Weekly sync scheduled</li></ul><p><strong>Detailed Task Overview:</strong></p><table border='1' cellpadding='5' cellspacing='0' style='border-collapse:collapse;'><tr><th>Task</th><th>Owner</th><th>Due Date</th><th>Status</th></tr><tr><td>Kickoff Meeting</td><td>Project Lead</td><td>August 19</td><td>✅ Completed</td></tr><tr><td>Deliverables Review</td><td>Dev Team</td><td>September 30</td><td>🔄 In Progress</td></tr><tr><td>Weekly Sync</td><td>All Members</td><td>Every Tuesday</td><td>📅 Scheduled</td></tr></table><p style='margin-top:20px; margin-bottom:20px;'>If you need more details, feel free to reach out. I’ll be happy to provide any additional information you need.</p><p>Best regards,<br/><strong>Ivy</strong><br/>Technical Project Coordinator</p>";
        }

        /// <summary>
        /// Gets the command to execute when the send button is clicked.
        /// </summary>
        public ICommand SendButtonCommand => _sendButtonCommand;

        /// <summary>
        /// Gets the command to execute when the discard button is clicked.
        /// </summary>
        public ICommand DiscardButtonCommand => _discardButtonCommand;

        /// <summary>
        /// Executes the send command.
        /// </summary>
        private void ExecuteSendCommand()
        {
            // Add your send mail logic here
            ShowPopup("Success", "Mail sent successfully!");
        }
        private readonly Syncfusion.Maui.Popup.SfPopup? _popup;
        private void ShowPopup(string title, string message)
        {
            if (_popup != null)
            {
                _popup.HeaderTitle = title;
                _popup.Message = message;
                _popup.AcceptButtonText = "Ok";
                _popup.ShowFooter = true;
                _popup.AppearanceMode = PopupButtonAppearanceMode.OneButton;
                _popup.EnableLiquidGlassEffect = true;
                _popup.Show();
            }
        }

        /// <summary>
        /// Executes the discard command.
        /// </summary>
        private void ExecuteDiscardCommand()
        {
            // Clear the mail content
            MailContent = string.Empty;

            // Reset recipients
            SelectedToAddresses.Clear();
            SelectedFromAddresses.Clear();

            // Reset to default selections
            SelectedToAddresses.Add(MailAddresses[5]);
            SelectedFromAddresses.Add(MailAddresses[8]);
        }

        /// <summary>
        /// Gets or sets the collection of mail addresses.
        /// </summary>
        public ObservableCollection<MailModel> MailAddresses
        {
            get => _mailAddresses;
            set
            {
                _mailAddresses = value;
                OnPropertyChanged(nameof(MailAddresses));
            }
        }

        /// <summary>
        /// Gets or sets the content of the mail.
        /// </summary>
        public string MailContent
        {
            get => _mailContent;
            set
            {
                _mailContent = value;
                SendButtonVisibility = !string.IsNullOrEmpty(_mailContent);
                OnPropertyChanged(nameof(MailContent));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the send button is visible.
        /// </summary>
        public bool SendButtonVisibility
        {
            get => _sendButtonVisibility;
            set
            {
                _sendButtonVisibility = value;
                OnPropertyChanged(nameof(SendButtonVisibility));
            }
        }

        /// <summary>
        /// Gets or sets the selected 'To' addresses.
        /// </summary>
        public ObservableCollection<MailModel> SelectedToAddresses
        {
            get => _selectedToAddresses;
            set
            {
                _selectedToAddresses = value;
                OnPropertyChanged(nameof(SelectedToAddresses));
            }
        }

        /// <summary>
        /// Gets or sets the selected 'From' addresses.
        /// </summary>
        public ObservableCollection<MailModel> SelectedFromAddresses
        {
            get => _selectedFromAddresses;
            set
            {
                _selectedFromAddresses = value;
                OnPropertyChanged(nameof(SelectedFromAddresses));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Triggers the PropertyChanged event.
        /// </summary>
        /// <param name="name">The property name that changed.</param>
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}