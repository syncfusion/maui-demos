#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SampleBrowser.Maui.SmartComponents.SmartTextEditor
{
    /// <summary>
    /// Provides the data context for the Smart Text Editor sample, including roles and preset responses.
    /// </summary>
    public class SmartTextEditorViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets and store the collection of role and phrases.
        /// </summary>
        private readonly Dictionary<string, Preset> presetMap;

        /// <summary>
        /// Gets the selected role.
        /// </summary>
        private Role? selectedRole;

        /// <summary>
        /// Gets the current preset.
        /// </summary>
        private Preset? currentPreset;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartTextEditorViewModel"/> class.
        /// </summary>
        public SmartTextEditorViewModel()
        {
            Roles = new ObservableCollection<Role>
            {
                new Role { ID = "Role1", Text = "Maintainer of an open-source project replying to GitHub issues" },
                new Role { ID = "Role2", Text = "Employee communicating with internal team" },
                new Role { ID = "Role3", Text = "Customer support representative responding to customer queries" },
                new Role { ID = "Role4", Text = "Sales representative responding to client inquiries" }
            };

            presetMap = new Dictionary<string, Preset>
            {
                ["Role1"] = new Preset
                {
                    UserRole = "Maintainer of an open-source project replying to GitHub issues",
                    UserPhrases = new List<string>
                    {
                        "Thank you for contacting us.",
                        "To investigate, we'll need a repro as a public Git repo.",
                        "Could you please post a screenshot of NEED_INFO?",
                        "This sounds like a usage question. This issue tracker is intended for bugs and feature proposals. Unfortunately, we don't have the capacity to answer general usage questions and would recommend StackOverflow for a faster response.",
                        "We don't accept ZIP files as repros."
                    },
                    PlaceHolder = "Write your response to the GitHub issue..."
                },
                ["Role2"] = new Preset
                {
                    UserRole = "Employee communicating with internal team",
                    UserPhrases = new List<string>
                    {
                        "Please find the attached report.",
                        "Let's schedule a meeting to discuss this further.",
                        "Can you provide an update on this task?",
                        "I appreciate your prompt response.",
                        "Let's collaborate on this project to ensure timely delivery."
                    },
                    PlaceHolder = "Draft your message for the team..."
                },
                ["Role3"] = new Preset
                {
                    UserRole = "Customer support representative responding to customer queries",
                    UserPhrases = new List<string>
                    {
                        "Thank you for reaching out to us.",
                        "Can you please provide your order number?",
                        "We apologize for the inconvenience.",
                        "Our team is looking into this issue and will get back to you shortly.",
                        "For urgent matters, please call our support line."
                    },
                    PlaceHolder = "Enter your reply to the customer query..."
                },
                ["Role4"] = new Preset
                {
                    UserRole = "Sales representative responding to client inquiries",
                    UserPhrases = new List<string>
                    {
                        "Thank you for your interest in our product.",
                        "Can I schedule a demo for you?",
                        "Please find the pricing details attached.",
                        "Our team is excited to work with you.",
                        "Let me know if you have any further questions."
                    },
                    PlaceHolder = "Enter your reply to the client's inquiry..."
                }
            };

            if (Roles.Count > 0)
            {
                selectedRole = Roles[0];
                CurrentPreset = presetMap[selectedRole.ID];
            }
            else
            {
                selectedRole = null;
                CurrentPreset = null;
            }
        }

        /// <summary>
        /// Gets the collection of roles available for selection.
        /// </summary>
        public ObservableCollection<Role> Roles { get; }

        /// <summary>
        /// Gets or sets the role currently selected by the user.
        /// </summary>
        public Role? SelectedRole
        {
            get => selectedRole;
            set
            {
                if (!Equals(selectedRole, value))
                {
                    selectedRole = value;
                    OnPropertyChanged();

                    if (value != null && presetMap.TryGetValue(value.ID, out var preset))
                    {
                        CurrentPreset = preset;
                    }
                    else
                    {
                        CurrentPreset = null;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the preset associated with the selected role.
        /// </summary>
        public Preset? CurrentPreset
        {
            get => currentPreset;
            set
            {
                if (!Equals(currentPreset, value))
                {
                    currentPreset = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event to notify listeners of property updates.
        /// </summary>
        /// <param name="name">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    /// <summary>
    /// Represents a selectable role within the Smart Text Editor.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Gets or sets the unique identifier for the role.
        /// </summary>
        public string ID { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the display text describing the role.
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents a preset containing phrasing and placeholder text for a specific role.
    /// </summary>
    public class Preset : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the user role.
        /// </summary>
        private string? userRole;

        /// <summary>
        /// Gets the user phrases.
        /// </summary>
        private List<string>? userPhrases;

        /// <summary>
        /// Gets the placeholder.
        /// </summary>
        private string? placeHolder;

        /// <summary>
        /// Gets or sets the user role description associated with the preset.
        /// </summary>
        public string? UserRole
        {
            get => userRole;
            set
            {
                if (userRole != value)
                {
                    userRole = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the list of predefined phrases for the role.
        /// </summary>
        public List<string>? UserPhrases
        {
            get => userPhrases;
            set
            {
                if (userPhrases != value)
                {
                    userPhrases = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the placeholder text for the editor input.
        /// </summary>
        public string? PlaceHolder
        {
            get => placeHolder;
            set
            {
                if (placeHolder != value)
                {
                    placeHolder = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event to notify listeners of property updates.
        /// </summary>
        /// <param name="name">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}