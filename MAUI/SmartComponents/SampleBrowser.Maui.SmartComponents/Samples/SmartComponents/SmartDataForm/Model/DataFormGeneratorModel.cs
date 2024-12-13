#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    using Syncfusion.Maui.AIAssistView;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    /// <summary>
    /// The DataForm generator model class.
    /// </summary>
    public class DataFormGeneratorModel : INotifyPropertyChanged
    {
        private bool showDataForm, showAssistView, showSubmitButton, showInputView, showOfflineLabel;

        /// <summary>
        /// Gets or sets the template title
        /// </summary>
        internal string? FormTitle { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataFormGeneratorModel"/> class.
        /// </summary>
        public DataFormGeneratorModel()
        {
            showInputView = true;
            showOfflineLabel = true;

            // Initialize Templates collection with sample data
            Templates = new ObservableCollection<TemplateItem>
            {
                new TemplateItem { Font="\ue763", Title = "Contact Form", Description = "Create a form to capture user details." },
                new TemplateItem { Font="\ue761", Title = "Employment Details", Description = "Create a form to capture employment details." },
                new TemplateItem { Font="\ue73a", Title = "Feedback Form", Description = "Create a form to receive client feedback." }
            };
        }

        /// <summary>
        /// Gets or sets the collection of templates.
        /// </summary>
        public ObservableCollection<TemplateItem> Templates { get; set; }

        /// <summary>
        /// Gets or sets the collection of messages of a conversation.
        /// </summary>
        public ObservableCollection<IAssistItem> Messages { get; set; } = new ObservableCollection<IAssistItem>();

        /// <summary>
        /// Gets or sets a value indicating whether the data form is visible.
        /// </summary>
        public bool ShowDataForm
        {
            get { return this.showDataForm; }
            set
            {
                this.showDataForm = value;
                RaisePropertyChanged(nameof(this.ShowDataForm));
            }
        }

        public bool ShowAssistView
        {
            get { return this.showAssistView; }
            set
            {
                this.showAssistView = value;
                RaisePropertyChanged(nameof(this.ShowAssistView));
            }
        }

        public bool ShowInputView
        {
            get { return this.showInputView; }
            set
            {
                this.showInputView = value;
                RaisePropertyChanged(nameof(this.ShowInputView));
            }
        }

        public bool ShowSubmitButton
        {
            get { return this.showSubmitButton; }
            set
            {
                this.showSubmitButton = value;
                RaisePropertyChanged(nameof(this.ShowSubmitButton));
            }
        }

        public bool ShowOfflineLabel
        {
            get { return this.showOfflineLabel; }
            set
            {
                this.showOfflineLabel = value;
                RaisePropertyChanged(nameof(this.ShowOfflineLabel));
            }
        }

        /// <summary>
        /// Property changed handler.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Occurs when a property is changed.
        /// </summary>
        /// <param name="propName">The name of the changed property.</param>
        public void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }

    /// <summary>
    /// Represents a template item with title and description.
    /// </summary>
    public class TemplateItem
    {
        public string? Font { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
