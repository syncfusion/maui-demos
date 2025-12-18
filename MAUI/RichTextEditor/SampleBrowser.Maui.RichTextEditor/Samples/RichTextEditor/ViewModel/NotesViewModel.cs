#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
#if ANDROID
using Android.Content.Res;
using Microsoft.Maui.Platform;
#endif
using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.RichTextEditor;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace SampleBrowser.Maui.RichTextEditor.RichTextEditor
{
    public class Grouping<TKey, TItem> : ObservableCollection<TItem>
    {
        public TKey Key { get; private set; }

        public Grouping(TKey key, IEnumerable<TItem> items)
        {
            Key = key;
            foreach (var item in items)
            {
                this.Items.Add(item);
            }
        }
    }

    public class NotesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<NoteModel> Notes { get; } = new();

        public ObservableCollection<Grouping<string, NoteModel>> GroupedNotes { get; } = new();


        private bool _isHomePage = true;
        public bool IsHomePage
        {
            get => _isHomePage;
            set => SetField(ref _isHomePage, value);
        }

        private string _pageTitle = "Notes";
        public string PageTitle
        {
            get => _pageTitle;
            set => SetField(ref _pageTitle, value);
        }

        private string _selectedNoteTitle = "";
        public string SelectedNoteTitle
        {
            get => _selectedNoteTitle;
            set => SetField(ref _selectedNoteTitle, value);
        }

        private string _selectedNoteContent = "";
        public string SelectedNoteContent
        {
            get => _selectedNoteContent;
            set => SetField(ref _selectedNoteContent, value);
        }

        private string _selectedHtmlNoteContent = "";
        public string SelectedHtmlNoteContent
        {
            get => _selectedHtmlNoteContent;
            set => SetField(ref _selectedHtmlNoteContent, value);
        }

        private NoteModel? _selectedNote;
        public NoteModel? SelectedNote
        {
            get => _selectedNote;
            set
            {
                SetField(ref _selectedNote, value);
                if (_selectedNote != null && !string.IsNullOrEmpty(_selectedNote.Title) && !string.IsNullOrEmpty(_selectedNote.HtmlText))
                {
                    IsHomePage = false;
                    PageTitle = "Update Note";
                    SelectedNoteTitle = _selectedNote.Title;
                    SelectedNoteContent = _selectedNote.DisplayText;
                    SelectedHtmlNoteContent = _selectedNote.HtmlText;
                }
                else
                {
                    PageTitle = "Notes";
                    SelectedNoteTitle = "";
                    SelectedNoteContent = "";
                    SelectedNoteContent = "";
                    SelectedHtmlNoteContent = "";
                }
            }
        }

        public ICommand ShowAddNotePopupCommand { get; }

        public ICommand AddNoteCommand { get; }

        public ICommand ClosePopupCommand { get; }

        public ICommand StarCommand { get; }

        public NotesViewModel()
        {
            ShowAddNotePopupCommand = new Command(ShowAddNote);
            ClosePopupCommand = new Command(CloseAddNotePage);
            AddNoteCommand = new Command<ButtonCommandParameter>(AddNote);
            StarCommand = new Command(UpdateStar);

            Notes = new ObservableCollection<NoteModel>();
            var model1 = new NoteModel()
            {
                Title = "Birthday Celebration",
                HtmlText = "<p>Plan party, buy cake, send invitations, book venue</p>",
                DisplayText = "Plan party, buy cake, send invitations, book venue",
                ModifiedAt = new DateTime(2025, 1, 7),
                IsPinnded = true,
                StarValue = 1,
            };
            model1.ItemSelectedCommand = new Command<NoteModel>((model) => EditNote(model1));

            var model2 = new NoteModel()
            {
                Title = "Vacation Plan",
                HtmlText = "<p>Book flights, reserve hotel, pack luggage, create itinerary</p>",
                DisplayText = "Book flights, reserve hotel, pack luggage, create itinerary",
                ModifiedAt = new DateTime(2025, 3, 5),
            };
            model2.ItemSelectedCommand = new Command<NoteModel>((model) => EditNote(model2));

            var model3 = new NoteModel()
            {
                Title = "Groceries Items",
                HtmlText = "<p>Milk, Eggs, Bread, Fruits, Vegetables</p>",
                DisplayText = "Milk, Eggs, Bread, Fruits, Vegetables",
                ModifiedAt = new DateTime(2025, 7, 15),
                IsPinnded = true,
                StarValue = 1,
            };
            model3.ItemSelectedCommand = new Command<NoteModel>((model) => EditNote(model3));

            var model4 = new NoteModel()
            {
                Title = "Work Tasks",
                HtmlText = "<p>Finish report, attend meeting, reply to emails, update project plan</p>",
                DisplayText = "Finish report, attend meeting, reply to emails, update project plan",
                ModifiedAt = new DateTime(2025, 8, 25),
            };
            model4.ItemSelectedCommand = new Command<NoteModel>((model) => EditNote(model4));

            Notes.Add(model1);
            Notes.Add(model2);
            Notes.Add(model3);
            Notes.Add(model4);

            UpdateGroupedNotes();
        }

        private string GetItem1()
        {
            return @"
Plan party
Buy cake
Send invitations
Book venue
            ";
        }

        private string GetItem2()
        {
            return @"
Book flights
Reserve hotel
Pack luggage
Create itinerary
            ";
        }

        private string GetItem3()
        {
            return @"
Milk
Eggs
Bread
Fruits
Vegetables
            ";
        }

        private string GetItem4()
        {
            return @"
Finish report
Attend meeting
Reply to emails
Fruits
Update project plan
            ";
        }

        private void UpdateGroupedNotes()
        {
            try
            {
                var sortedNotes = from note in Notes
                                  orderby note.IsPinnded descending, note.ModifiedAt descending
                                  group note by note.IsPinnded ? "Pinned" : "Others" into noteGroup
                                  select new Grouping<string, NoteModel>(noteGroup.Key, noteGroup);

                // Clear the grouped collection
                GroupedNotes.Clear();

                // Add the new groups
                foreach (var group in sortedNotes)
                {
                    GroupedNotes.Add(group);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                System.Diagnostics.Debug.WriteLine($"Error updating grouped notes: {ex.Message}");
            }
        }

        private void UpdateStar(object obj)
        {
            if (SelectedNote != null)
            {
                SelectedNote.IsPinnded = !SelectedNote.IsPinnded;
                if (SelectedNote.IsPinnded)
                {
                    SelectedNote.StarValue = 1;
                }
                else
                {
                    SelectedNote.StarValue = 0;
                }

                // Update the grouped collection to refresh the UI on the main thread
                if (MainThread.IsMainThread)
                {
                    UpdateGroupedNotes();
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() => UpdateGroupedNotes());
                }
            }
        }

        internal void EditNote(NoteModel noteModel)
        {
            SelectedNote = noteModel;
        }

        string GetDisplayContent(string text)
        {
            return Regex.Replace(text, @"\\n", " ");
        }

        private void CloseAddNotePage(object obj)
        {
            IsHomePage = true;
            SelectedNote = null;
            SelectedNoteTitle = "";
            SelectedNoteContent = "";
            PageTitle = "Notes";
        }

        private void ShowAddNote(object obj)
        {
            IsHomePage = false;
            PageTitle = "Create Note";
            SelectedNote = null;
            var tempNote = new NoteModel() { IsNewNote = true };
            SelectedNote = tempNote;
        }

        private void AddNote(ButtonCommandParameter param)
        {
            if (SelectedNote != null && SelectedNote.IsNewNote)
            {
                // Adding new note
                string noteTitle = string.Empty;
                if (param.Reference1 is Entry entry)
                {
                    noteTitle = entry.Text;
                }

                string noteContent = string.Empty;
                string noteHtml = string.Empty;
                if (param.Reference2 is SfRichTextEditor rte)
                {
                    noteContent = rte.Text;
                    noteHtml = rte.HtmlText;
                }
               
                if (!string.IsNullOrEmpty(noteTitle) && !string.IsNullOrEmpty(noteContent))
                {
                    var newNote = new NoteModel()
                    {
                        Title = noteTitle,
                        HtmlText = noteHtml,
                        DisplayText = GetDisplayContent(noteContent),
                        ModifiedAt = DateTime.Now,
                        IsPinnded = SelectedNote.IsPinnded,
                        StarValue = SelectedNote.StarValue
                    };
                    newNote.ItemSelectedCommand = new Command<NoteModel>((model) => EditNote(newNote));
                    SelectedNote.IsNewNote = false;
                    Notes.Add(newNote);
                    UpdateGroupedNotes();
                }
            }
            else if(SelectedNote != null)
            {
                // Updating existing note
                SelectedNote.Title = SelectedNoteTitle;
                if (param.Reference2 is SfRichTextEditor rte)
                {
                    SelectedNote.HtmlText = rte.HtmlText;
                    SelectedNote.DisplayText = GetDisplayContent(rte.Text);
                    SelectedNote.ModifiedAt = DateTime.Now;
                }
            }

            // Navigate back to home and clear editor fields
            IsHomePage = true;
            SelectedNote = null;
            SelectedNoteTitle = "";
            SelectedNoteContent = "";

            // Update the grouped collection to refresh the UI on the main thread
            //APPlication breaks when execute this.
            //if (MainThread.IsMainThread)
            //{
            //    UpdateGroupedNotes();
            //}
            //else
            //{
            //    MainThread.BeginInvokeOnMainThread(() => UpdateGroupedNotes());
            //}
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public class NoteModel : INotifyPropertyChanged
    {
        private string _title = string.Empty;
        private string _htmlText = string.Empty;
        private string _displayText = string.Empty;
        private DateTime _modifiedAt;
        private bool _isPinned;
        private double _starValue = 0d;

        public string Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        public string HtmlText
        {
            get => _htmlText;
            set => SetField(ref _htmlText, value);
        }

        public string DisplayText
        {
            get => _displayText;
            set => SetField(ref _displayText, value);
        }

        public DateTime ModifiedAt
        {
            get => _modifiedAt;
            set => SetField(ref _modifiedAt, value);
        }

        public ICommand? ItemSelectedCommand { get; set; }

        public bool IsPinnded
        {
            get => _isPinned;
            set => SetField(ref _isPinned, value);
        }

         public double StarValue
        {
            get => _starValue;
            set => SetField(ref _starValue, value);
        }

        public bool IsNewNote { get; set; } = false;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public class CustomBorder : Border, ITapGestureListener
    {
        public static readonly BindableProperty TapCommandProperty =
            BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(CustomBorder));


        public ICommand? TapCommand
        {
            get => (ICommand?)GetValue(TapCommandProperty);
            set => SetValue(TapCommandProperty, value);
        }

        public object? TapCommandParameter { get; private set; }

        public CustomBorder()
        {
            this.AddGestureListener(this);
        }

        void ITapGestureListener.OnTap(TapEventArgs e)
        {
            if (TapCommand?.CanExecute(TapCommandParameter) == true)
            {
                TapCommandParameter = this.BindingContext;
                TapCommand.Execute(TapCommandParameter);
            }
        }
    }

    public class BorderlessEntry : Entry
    {
        public BorderlessEntry()
        {

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
                if (v is BorderlessEntry)
                {
#if ANDROID
                    h.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
#elif WINDOWS
                h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
                h.PlatformView.Padding = new Microsoft.UI.Xaml.Thickness(0, 10, 0, 0);
                h.PlatformView.Resources["TextControlBorderThemeThicknessFocused"] = new Microsoft.UI.Xaml.Thickness(0);
#elif MACCATALYST
                h.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
                }
            });
        }
    }

    public class ButtonCommandParameter
    {
        public View? Reference1 { get; set; }
        public View? Reference2 { get; set; }
    }
}