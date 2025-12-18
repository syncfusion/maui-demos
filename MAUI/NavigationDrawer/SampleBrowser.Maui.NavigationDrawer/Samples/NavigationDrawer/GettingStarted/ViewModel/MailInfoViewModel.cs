#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.DataSource.Extensions;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.NavigationDrawer.NavigationDrawer;

/// <summary>
/// ViewModel class of NavigationDrawer.
/// </summary>
public class MailInfoViewModel : INotifyPropertyChanged
{
    #region Fields

    private ObservableCollection<MailInfo>? mailInfos;
    private ObservableCollection<MailInfo>? archivedMessages;
    private ObservableCollection<Notification>? notifications;
    private Command? undoCommand;
    private Command? deleteCommand;
    private bool? isDeleted;
    private MailInfo? listViewItem;
    private Command? archiveCommand;
    private string? popUpText;
    private int listViewItemIndex;
    public GroupResult? itemGroup;
    private Random random;
    private MailInfoRepository? listViewRepository;

    #endregion

    #region Interface Member

    /// <summary>
    /// Represents the method that will handle the <see cref="System.ComponentModel.INotifyPropertyChanged.PropertyChanged"></see> event raised when a property is changed on a component
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Triggers when Items Collections Changed.
    /// </summary>
    /// <param name="name">string type parameter represent propertyName as name</param>
    public void OnPropertyChanged(string name)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(name));
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Initiates new intance of ListViewInboxInfoViewModel class.
    /// </summary>
    public MailInfoViewModel()
    {
        this.random = new Random();
        GenerateSource();
        this.listViewRepository = new MailInfoRepository();


        notifications = new ObservableCollection<Notification>
        {
            new Notification { Name = "Clara", Subject = "Goto Meeting", Description = "Join meeting to discuss about daily status, workflow, pending work and improve process", ImageSource = "people_circle1.png",TimeStamp = "12:10 PM" },
            new Notification { Name = "Gabriella", Subject = "FW: Status Update", Description = "Hi, Please find the today's status", ImageSource = "people_circle2.png",TimeStamp = "12:50 PM" },
            new Notification { Name = "Jackson", Subject = "Greetings! Congrats", Description = "Hi, Congrats you have won the raffle", ImageSource = "people_circle26.png" ,TimeStamp = "05:45 PM"},
            new Notification { Name = "Liam", Subject = "Report Monitor", Description = "Do not reply, Please find the attachment. Attachment have the full details of monitor report with timing", ImageSource = "people_circle14.png",TimeStamp = "04:00 PM" },
            new Notification { Name = "Lita", Subject = "News Letter", Description = "Hi, Please find the attached news letter", ImageSource = "people_circle19.png", TimeStamp = "07:10 AM"},
            new Notification { Name = "Briley", Subject = "Conference about Latest", Description = "Hi, We are scheduled a meeting", ImageSource = "people_circle20.png" ,TimeStamp = "12:10 PM"},
            new Notification { Name = "Maci", Subject = "RE: Status Update", Description = "Thanks for the update", ImageSource = "people_circle21.png" ,TimeStamp = "06:40 PM"},
            new Notification { Name = "Peter", Subject = "RE: Status Update", Description = "Thanks for the update", ImageSource = "people_circle23.png" ,TimeStamp = "10:25 AM"},
            new Notification { Name = "Sophia", Subject = "Project Kickoff", Description = "Reminder: Project kickoff meeting scheduled for tomorrow at 10 AM", ImageSource = "people_circle22.png" ,TimeStamp = "2:10 PM"},
            new Notification { Name = "Ethan", Subject = "Weekly Report", Description = "Please review the attached weekly performance report", ImageSource = "people_circle27.png" ,TimeStamp = "02:00 PM"},
            new Notification { Name = "Olivia", Subject = "System Maintenance", Description = "Scheduled maintenance will occur tonight from 12 AM to 3 AM", ImageSource = "people_circle25.png",TimeStamp = "11:00 AM" },
            new Notification { Name = "Noah", Subject = "Policy Update", Description = "New company policy has been released. Please check your email for details", ImageSource = "people_circle24.png" ,TimeStamp = "11:18 AM"},
            new Notification { Name = "Ava", Subject = "Training Session", Description = "Join the mandatory training session on Friday at 2 PM", ImageSource = "people_circle28.png" ,TimeStamp = "12:10 PM"},
            new Notification { Name = "Isabella", Subject = "Reminder: Submit Timesheet", Description = "Please submit your timesheet before 6 PM today to avoid delays in processing.", ImageSource = "people_circle17.png" ,TimeStamp = "12:10 PM"},
        };


    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the MailInfo type of ObservableCollection and notifies user when collection value gets changed.
    /// </summary>
    public ObservableCollection<MailInfo>? MailInfos
    {
        get { return mailInfos; }
        set { mailInfos = value; OnPropertyChanged("MailInfos"); }
    }

    /// <summary>
    /// Gets or sets the MailInfo type of ObservableCollection and stores archived messages on right swipe.
    /// </summary>
    public ObservableCollection<MailInfo>? ArchivedMessages
    {
        get { return archivedMessages; }
        set { archivedMessages = value; OnPropertyChanged("ArchivedMessages"); }
    }

    /// <summary>
    /// Gets or sets the MailInfo type of ObservableCollection and notifies user when collection value gets changed.
    /// </summary>
    public ObservableCollection<Notification>? Notifications
    {
        get { return notifications; }
        set { notifications = value; OnPropertyChanged("Notifications"); }
    }

    /// <summary>
    /// Get or sets the command used to delete the messages.
    /// </summary>
    public Command? DeleteCommand
    {
        get { return deleteCommand; }
        protected set { deleteCommand = value; }
    }

    /// <summary>
    /// Get or sets the command used to undo the archive or delete actions.
    /// </summary>
    public Command? UndoCommand
    {
        get { return undoCommand; }
        protected set { undoCommand = value; }
    }

    /// <summary>
    /// Gets or sets the command used to archive the messages.
    /// </summary>
    public Command? ArchiveCommand
    {
        get { return archiveCommand; }
        protected set { archiveCommand = value; }
    }

    public bool? IsDeleted
    {
        get { return isDeleted; }
        set { isDeleted = value; OnPropertyChanged("IsDeleted"); }
    }

    public string? PopUpText
    {
        get { return popUpText; }
        set { popUpText = value; OnPropertyChanged("PopUpText"); }
    }


    #endregion
    /// <summary>
    /// Used to assign the Collection values to Model Properties while refreshing.
    /// </summary>
    /// <param name="count">Number of items to be added.</param>
    /// <returns>Added mailInfos items</returns>
    internal ObservableCollection<MailInfo> GenerateSource(int count)
    {
        var empInfo = new ObservableCollection<MailInfo>();
        int k = 0;
        for (int i = 0; i < count; i++)
        {

            if (k > 5)
            {
                k = 0;
            }
            var record = new MailInfo()
            {
                ProfileName = listViewRepository!.ProfileList[i],
                Name = listViewRepository!.NameList[i],
                Subject = listViewRepository!.Subject[i],
                Date = DateTime.Now.AddMinutes((i * -3)),
                Description = listViewRepository!.Descriptions[i],
                Image = listViewRepository!.Images[k],
                IsAttached = listViewRepository!.Attachments[i],
                IsImportant = listViewRepository!.Importants[i],
                IsOpened = listViewRepository!.Opens[i],
            };
            empInfo.Add(record);
            k++;
        }

        return empInfo;
    }

    #region Generate Source

    /// <summary>
    /// Initiates Commands, Repository and Collections. Also generates items for the collections.
    /// </summary>
    private void GenerateSource()
    {
        IsDeleted = false;
        MailInfoRepository mailinfo = new MailInfoRepository();
        archivedMessages = new ObservableCollection<MailInfo>();
        mailInfos = mailinfo.GetMailInfo();
        deleteCommand = new Command(OnDelete);
        undoCommand = new Command(OnUndo);
        archiveCommand = new Command(OnArchive);
    }

    /// <summary>
    /// This method helps to delete an message on left swipe.
    /// </summary>
    /// <param name="item">Represents an swiping message.</param>
    private async void OnDelete(object item)
    {
        PopUpText = "Deleted";
        listViewItem = (MailInfo)item;
        listViewItemIndex = mailInfos!.IndexOf(listViewItem);
        mailInfos!.Remove(listViewItem);
        IsDeleted = true;

        // Added Delay in order to maintain the Delete message popUp at screen bottom.
        await Task.Delay(3000);
        IsDeleted = false;
    }

    /// <summary>
    /// This method helps to archive an message on right swipe.
    /// </summary>
    /// <param name="item">Represent an swiping message.</param>
    private async void OnArchive(object item)
    {
        PopUpText = "Archived";
        listViewItem = (MailInfo)item;
        listViewItemIndex = mailInfos!.IndexOf(listViewItem);
        mailInfos!.Remove(listViewItem);
        archivedMessages!.Add(listViewItem);
        IsDeleted = true;

        // Added Delay in order to maintain the Archive message popUp at screen bottom.
        await Task.Delay(3000);
        IsDeleted = false;
    }

    /// <summary>
    /// This method helps to undo the archive or delete action.
    /// </summary>
    private void OnUndo()
    {
        IsDeleted = false;

        if (listViewItem != null)
        {
            mailInfos!.Insert(listViewItemIndex, listViewItem);

            var archivedItem = archivedMessages!.Where(x => x.Name!.Equals(listViewItem.Name));

            if (archivedItem != null)
            {
                foreach (var item in archivedItem)
                {
                    archivedMessages!.Remove(listViewItem);
                    break;
                }
            }
        }

        listViewItemIndex = 0;
        listViewItem = null;
    }

    /// <summary>
    /// This method helps to add messages while refreshing.
    /// </summary>
    /// <param name="count">Represent number of messages to be added.</param>
    public void AddItemsRefresh(int count)
    {
        MailInfoRepository inboxinfo = new MailInfoRepository();

        this.mailInfos = inboxinfo.AddRefreshItems(count);
    }

    #endregion
}

