using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Syncfusion.Maui.Core;

namespace SampleBrowser.Maui.TabView.SfTabView
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ActivityItem> ActivityItems { get; } = new();
        public ObservableCollection<ContactItem> ContactItems { get; } = new();
        public ObservableCollection<ChatItem> ChatItems { get; } = new();
        public ObservableCollection<CallItem> CallItems { get; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        private string? selectedCallDetails = string.Empty;
        public string? SelectedCallDetails
        {
            get => selectedCallDetails;
            set { selectedCallDetails = value; OnPropertyChanged(); }
        }

        public bool IsDesktop => DeviceInfo.Platform == DevicePlatform.WinUI || DeviceInfo.Platform == DevicePlatform.MacCatalyst;

        public MainViewModel()
        {
            ActivityItems.Add(new ActivityItem { Name = "Katherine Will", Action = "mentioned you", Content = "Kindly review this response ", Context1 = "Syncfusion Support", Context2 = "Mobile and Desktop > General Support", Time = "4:53 PM", AvatarColor = Colors.LightPink });
            ActivityItems.Add(new ActivityItem { Name = "Michael Brown", Action = "mentioned you", Content = "The customer installed the MahApps.Metro package si...", Context1 = "DataViz - Mobile and Desktop > MAUI Tools General", Context2 = string.Empty, Time = "4:53 PM", AvatarColor = Colors.LightBlue });
            ActivityItems.Add(new ActivityItem { Name = "Samuel Johnson", Action = "mentioned you", Content = "Kindly check the SB demo with both light and dark themes provided in the sample level.", Context1 = "In chat with you", Context2 = string.Empty, Time = "4:53 PM", AvatarColor = Colors.LightGreen });
            ActivityItems.Add(new ActivityItem { Name = "Victor Smith", Action = "mentioned Everyone", Content = "Team, let's sync up on the latest sprint progress.", Context1 = "General Channel", Context2 = string.Empty, Time = "Yesterday", AvatarColor = Colors.MediumPurple });
            ActivityItems.Add(new ActivityItem { Name = "Training Team", Action = "posted an announcement", Content = "New training materials for MAUI are now available.", Context1 = "Announcements", Context2 = string.Empty, Time = "Yesterday", AvatarColor = Colors.Orange });
            ActivityItems.Add(new ActivityItem { Name = "Jordan", Action = "reacted to your message", Content = "Great work!", Context1 = "Mobile Team Chat", Context2 = string.Empty, Time = "Yesterday", AvatarColor = Colors.Pink });
            ActivityItems.Add(new ActivityItem { Name = "Build Bot", Action = "passed on branch/main", Content = "CI build completed successfully.", Context1 = "DevOps", Context2 = string.Empty, Time = "2 days ago", AvatarColor = Colors.LightGreen });
            ActivityItems.Add(new ActivityItem { Name = "GitHub", Action = "New comment on PR #482", Content = "Looks good to me, but can you add a unit test?", Context1 = "Pull Requests", Context2 = string.Empty, Time = "2 days ago", AvatarColor = Colors.CadetBlue });
            ActivityItems.Add(new ActivityItem { Name = "Deployment Bot", Action = "Release v1.2 deployed", Content = "Successfully deployed to production.", Context1 = "Releases", Context2 = string.Empty, Time = "3 days ago", AvatarColor = Colors.Gold });
            ActivityItems.Add(new ActivityItem { Name = "Calendar", Action = "Reminder: Sprint planning", Content = "Sprint planning meeting starts in 15 minutes.", Context1 = "Team Calendar", Context2 = string.Empty, Time = "3 days ago", AvatarColor = Colors.SteelBlue });

            // Contacts
            ContactItems.Add(new ContactItem { Name = "Alex Miller", Email = "alex.m@syncfusion.com", AvatarCharacter = AvatarCharacter.Avatar4, Category = "General" });
            ContactItems.Add(new ContactItem { Name = "Anna Davis", Email = "anna.d@syncfusion.com",  AvatarCharacter = AvatarCharacter.Avatar14, Category = "Web Contacts" });
            ContactItems.Add(new ContactItem { Name = "Aaron Wilson", Email = "aaron.w@syncfusion.com",  AvatarCharacter = AvatarCharacter.Avatar10, Category = "General" });
            ContactItems.Add(new ContactItem { Name = "Kevin Moore", Email = "kevin.m@syncfusion.com",  AvatarCharacter = AvatarCharacter.Avatar11, Category = "Mobile Contacts" });
            ContactItems.Add(new ContactItem { Name = "Samuel Johnson", Email = "samuel.j@syncfusion.com",  AvatarCharacter = AvatarCharacter.Avatar12, Category = "General" });
            ContactItems.Add(new ContactItem { Name = "Vanessa Taylor", Email = "vanessa.t@syncfusion.com", AvatarCharacter = AvatarCharacter.Avatar13, Category = "Web Contacts" });
            ContactItems.Add(new ContactItem { Name = "Angela Anderson", Email = "angela.a@syncfusion.com", AvatarCharacter = AvatarCharacter.Avatar14, Category = "Mobile Contacts" });
            ContactItems.Add(new ContactItem { Name = "George Thomas", Email = "george.t@syncfusion.com",  AvatarCharacter = AvatarCharacter.Avatar15, Category = "General" });
            ContactItems.Add(new ContactItem { Name = "John", Email = "john.j@syncfusion.com", AvatarCharacter = AvatarCharacter.Avatar16, Category = "General" });
            ContactItems.Add(new ContactItem { Name = "Peter", Email = "peter.p@syncfusion.com",  AvatarCharacter = AvatarCharacter.Avatar17, Category = "General" });

            // Chat
            ChatItems.Add(new ChatItem { Name = "Kevin Moore",  IsUnread = false, IsSelected = false, AvatarColor = Colors.Gold });
            ChatItems.Add(new ChatItem { Name = "Katherine Williams",  IsUnread = true, IsSelected = true, AvatarColor = Colors.LightPink });
            ChatItems.Add(new ChatItem { Name = "Samuel Johnson",  IsUnread = false, IsSelected = false, AvatarColor = Colors.LightGreen });
            ChatItems.Add(new ChatItem { Name = "Ken Jackson",  IsUnread = true, IsSelected = false, AvatarColor = Colors.Gold });
            ChatItems.Add(new ChatItem { Name = "General PO Followup",  IsUnread = false, IsSelected = false, AvatarColor = Colors.LightBlue });
            ChatItems.Add(new ChatItem { Name = "Core Team",  IsUnread = false, IsSelected = false, AvatarColor = Colors.LightBlue });
            ChatItems.Add(new ChatItem { Name = "Alan",  IsUnread = false, IsSelected = false, AvatarColor = Colors.Pink });
            ChatItems.Add(new ChatItem { Name = "Designers",  IsUnread = false, IsSelected = false, AvatarColor = Colors.Lavender});
            ChatItems.Add(new ChatItem { Name = "Ops Oncall",  IsUnread = false, IsSelected = false, AvatarColor = Colors.LightGreen });
            ChatItems.Add(new ChatItem { Name = "Alice",  IsUnread = false, IsSelected = false, AvatarColor = Colors.SkyBlue });

            // Calls
            CallItems.Add(new CallItem { Name = "Samuel Johnson", Status = "Outgoing", Time = "4:59 PM", Duration = "5m 9s",  AvatarImage = AvatarCharacter.Avatar4 });
            CallItems.Add(new CallItem { Name = "Vincent White", Status = "Outgoing", Time = "4:04 PM", Duration = "51m 28s",  AvatarImage = AvatarCharacter.Avatar17 });
            CallItems.Add(new CallItem { Name = "Patricia Harris", Status = "Outgoing", Time = "3:44 PM", Duration = "8m 38s",  AvatarImage = AvatarCharacter.Avatar20 });
            CallItems.Add(new CallItem { Name = "Ken Jackson", Status = "Outgoing", Time = "3:31 PM", Duration = "2m 44s",   AvatarText="KJ" });
            CallItems.Add(new CallItem { Name = "Samuel Johnson", Status = "Outgoing", Time = "3:16 PM", Duration = "4m 43s",  AvatarImage = AvatarCharacter.Avatar4 });
            CallItems.Add(new CallItem { Name = "Jane Martin", Status = "Outgoing", Time = "1:57 PM", Duration = "1m 12s",   AvatarText="JM" });
            CallItems.Add(new CallItem { Name = "Anna Davis", Status = "Incoming", Time = "12:30 PM", Duration = "10m 15s", AvatarImage = AvatarCharacter.Avatar9 });
            CallItems.Add(new CallItem { Name = "Alex Miller", Status = "Missed", Time = "11:15 AM", Duration = "0s",  AvatarImage = AvatarCharacter.Avatar6 });
            CallItems.Add(new CallItem { Name = "Katherine Williams", Status = "Outgoing", Time = "10:05 AM", Duration = "12m 4s", AvatarImage = AvatarCharacter.Avatar7 });
            CallItems.Add(new CallItem { Name = "Training Team", Status = "Incoming", Time = "9:42 AM", Duration = "3m 30s", AvatarImage = AvatarCharacter.Avatar8 });
            CallItems.Add(new CallItem { Name = "QA Lead", Status = "Outgoing", Time = "Yesterday", Duration = "1h 5m", AvatarImage=AvatarCharacter.Avatar17, });

            SelectedCallDetails = "Select a call to see details.";

        }

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }

    public class ActivityItem
    {
        public string? Name { get; set; }
        public string? Action { get; set; }
        public string? Content { get; set; }
        public string? Context1 { get; set; }
        public string? Context2 { get; set; }
        public string? Time { get; set; }
        public Color AvatarColor { get; set; } = Colors.LightGray;
    }

    public class ContactItem
    { 
        public string? Name { get; set; }
        public string? Email { get; set; }
        public AvatarCharacter AvatarCharacter { get; set; }
        public string? Category { get; set; }
        public bool HasCategory => !string.IsNullOrEmpty(Category);
    }

    public class ChatItem
    {
        public string? Name { get; set; }
        public bool IsUnread { get; set; }
        public bool IsSelected { get; set; }
        public string? AvatarText { get; set; }
        public Color AvatarColor { get; set; } = Colors.LightGray;
    }

    public class CallItem
    {
        public string? Name { get; set; }
        public string? Status { get; set; }
        public string? Time { get; set; }
        public string? Duration { get; set; }
        public AvatarCharacter? AvatarImage { get; set; }
        public string? AvatarText { get; set; }
    }

}
