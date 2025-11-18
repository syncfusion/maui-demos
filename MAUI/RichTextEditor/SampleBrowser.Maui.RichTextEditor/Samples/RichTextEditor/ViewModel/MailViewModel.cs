#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
using SampleBrowser.Maui.Base.Converters;
using Syncfusion.Maui.Popup;

namespace SampleBrowser.Maui.RichTextEditor.RichTextEditor
{
    /// <summary>
    /// ViewModel for managing email information.
    /// </summary>
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
        public MailViewModel(SfPopup? popup = null)
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

            MailContent = @"<p><strong>Hello Frank,</strong></p><p>I hope you're doing well. I'm writing to follow up on our recent conversation regarding <strong>MAUI controls development</strong>.</p><p>Here’s a quick summary of the key points:</p><ul><li>✅ Kickoff scheduled for <em>August 19</em></li><li>📌 Deliverables due by <em>September 30</em></li><li>💬 Weekly sync every <em>Tuesday</em> at <em>10:00 AM (UTC+05:30)</em></li></ul><p>If you need more details, feel free to reach out. You can also refer to our <a href='https://help.syncfusion.com/maui/release-notes'>release notes page</a> for updates.</p><p>Best regards,<br/><strong>Ivy</strong><br/>Technical Project Coordinator</p>";
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
        private readonly SfPopup? _popup;
        private void ShowPopup(string title, string message)
        {
            if(_popup != null)
            {
                _popup.HeaderTitle = title;
                _popup.Message = message;
                _popup.AcceptButtonText = "Ok";
                _popup.ShowFooter = true;
                _popup.AppearanceMode = PopupButtonAppearanceMode.OneButton;
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

            ShowPopup("Discarded", "Mail draft discarded");
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

    /// <summary>
    /// Model for storing email address and profile image.
    /// </summary>
    public class MailModel
    {
        /// <summary>
        /// Gets or sets the mail ID.
        /// </summary>
        public string MailId { get; set; }

        /// <summary>
        /// Gets or sets the profile image source.
        /// </summary>
        public ImageSource ProfileImage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailModel"/> class.
        /// </summary>
        /// <param name="mailId">The mail ID.</param>
        /// <param name="profileImage">The profile image name.</param>
        public MailModel(string mailId, string profileImage)
        {
            MailId = mailId;
            var resourceAssembly = typeof(SfImageResourceExtension).GetTypeInfo().Assembly;
            ProfileImage = ImageSource.FromResource($"SampleBrowser.Maui.Base.Resources.Images.{profileImage}", resourceAssembly);
        }
    }
}