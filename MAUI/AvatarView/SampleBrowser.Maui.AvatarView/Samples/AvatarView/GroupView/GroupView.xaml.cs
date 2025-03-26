#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using SampleBrowser.Maui.Base;
using SampleBrowser.Maui.Base.Converters;
using System.Reflection;
namespace SampleBrowser.Maui.AvatarView.SfAvatarView;

public partial class GroupView : SampleView
{ 
    private ObservableCollection<GroupModel> groupCollection = new ObservableCollection<GroupModel>();

    public ObservableCollection<GroupModel> GroupCollection
    {
        get
        {
            return groupCollection;
        }
        set
        {
            groupCollection = value;
            this.OnPropertyChanged();
        }
    }
   
    public GroupView()
    {
        InitializeComponent();

        this.GroupCollection.Add(new GroupModel(5) { GroupName = "Marketing Managers" });
        this.GroupCollection.Add(new GroupModel(10) { GroupName = "Marketing Representative" });
        this.GroupCollection.Add(new GroupModel(3) { GroupName = "Marketing Heads" });
        this.GroupCollection.Add(new GroupModel(4) { GroupName = "Sales Managers" });
        this.GroupCollection.Add(new GroupModel(9) { GroupName = "Sales Representative" });
        this.GroupCollection.Add(new GroupModel(2) { GroupName = "Sales Heads" });
        this.GroupCollection.Add(new GroupModel(5) { GroupName = "Process Managers" });
        this.GroupCollection.Add(new GroupModel(10) { GroupName = "Process Representative" });
        this.GroupCollection.Add(new GroupModel(2) { GroupName = "Process Heads" });
        this.GroupCollection.Add(new GroupModel(3) { GroupName = "Coordinaters" });
        this.GroupCollection.Add(new GroupModel(3) { GroupName = "Desinger" });
        this.GroupCollection.Add(new GroupModel(2) { GroupName = "Field Managers" });
        this.GroupCollection.Add(new GroupModel(2) { GroupName = "Server Team" });      
        this.BindingContext = this;        
    }  
}

public class GroupModel
{

    public String? GroupName { get; set; }

    public ObservableCollection<People>? PeopleCollection { get; set; }

    private ObservableCollection<People>? TotalPeople { get; set; }

    public String? TotalParticipants { get; set; }

    private Syncfusion.Maui.Core.AvatarShape avatarshape;
    public Syncfusion.Maui.Core.AvatarShape AvatarShape
    {
        get
        {
            return avatarshape;
        }
        set
        {
            avatarshape = value;
            
        }
    }

    public GroupModel(int peopleCount)
    {
        if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.WinUI)
            avatarshape = Syncfusion.Maui.Core.AvatarShape.Circle;
        else 
            avatarshape = Syncfusion.Maui.Core.AvatarShape.Square;

        this.TotalParticipants = peopleCount.ToString() + " Participants";

        this.PopulateAllPeople();

        this.PopulatePeopleBasedOnCount(peopleCount);
    }

    private void PopulateAllPeople()
    {
        this.TotalPeople = new ObservableCollection<People>();
        this.TotalPeople.Add(new People() { Name = "Kyle", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Gina", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Michael", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Oscar", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "William", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Bill",  Backgroundcolor = Color.FromArgb("#BACF82") });
        this.TotalPeople.Add(new People() { Name = "Daniel", Backgroundcolor = Color.FromArgb("#BACF82") });
        this.TotalPeople.Add(new People() { Name = "Frank",  Backgroundcolor = Color.FromArgb("#8AD6B7") });
        this.TotalPeople.Add(new People() { Name = "Howard", Backgroundcolor = Color.FromArgb("#BACF82") });
        this.TotalPeople.Add(new People() { Name = "Jack",  Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Holly",  Backgroundcolor = Color.FromArgb("#8AD6B7") });
        this.TotalPeople.Add(new People() { Name = "Steve", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Vince",  Backgroundcolor = Color.FromArgb("#BACF82") });
        this.TotalPeople.Add(new People() { Name = "Zeke", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Aiden", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Jackson", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Mason",  Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Liam", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Jacob",  Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Jayden", Backgroundcolor = Color.FromArgb("#F5EF9A")  });
        this.TotalPeople.Add(new People() { Name = "Ethan", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Alexander", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Sebastian", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Clara", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Victoriya", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Ellie", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Gabriella", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Arianna", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Sarah", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Kaylee", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Adriana", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Finley",  Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Daleyza", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Leila", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Mckenna", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Jacqueline", Backgroundcolor = Color.FromArgb("#F3B3E4") });
        this.TotalPeople.Add(new People() { Name = "Brynn",  Backgroundcolor = Color.FromArgb("#CFBDFE") });
        this.TotalPeople.Add(new People() { Name = "Sawyer", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Rosalie", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Maci", Backgroundcolor = Color.FromArgb("#8AD6B7") });
        this.TotalPeople.Add(new People() { Name = "Miranda", Backgroundcolor = Color.FromArgb("#8BEAF0") });
        this.TotalPeople.Add(new People() { Name = "Talia", Backgroundcolor = Color.FromArgb("#8AD6B7") });
        this.TotalPeople.Add(new People() { Name = "Shelby", Backgroundcolor = Color.FromArgb("#B1C5FF") });
        this.TotalPeople.Add(new People() { Name = "Haven", Backgroundcolor = Color.FromArgb("#8AD6B7") });
        this.TotalPeople.Add(new People() { Name = "Brynn", Backgroundcolor = Color.FromArgb("#F7BC70") });
        this.TotalPeople.Add(new People() { Name = "Yaretzi", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Zariah", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Karla", Backgroundcolor = Color.FromArgb("#BACF82") });
        this.TotalPeople.Add(new People() { Name = "Cassandra", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Pearl", Backgroundcolor = Color.FromArgb("#FFB2BA") });
        this.TotalPeople.Add(new People() { Name = "Irene", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Zelda", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Wren", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Yamileth",  Backgroundcolor = Color.FromArgb("#F3B3E4") });
        this.TotalPeople.Add(new People() { Name = "Belen", Backgroundcolor = Color.FromArgb("#F3B3E4") });
        this.TotalPeople.Add(new People() { Name = "Briley", Backgroundcolor = Color.FromArgb("#8AD6B7") });
        this.TotalPeople.Add(new People() { Name = "Jada", Backgroundcolor = Color.FromArgb("#F5EF9A") });
        this.TotalPeople.Add(new People() { Name = "Jaden", Backgroundcolor = Color.FromArgb("#8AD6B7") });
        this.TotalPeople.Add(new People() { Name = "George", Backgroundcolor = Color.FromArgb("#CFBDFE") });
        this.TotalPeople.Add(new People() { Name = "Ellanaa", Backgroundcolor = Color.FromArgb("#F3B3E4") });
        this.TotalPeople.Add(new People() { Name = "James", Backgroundcolor = Color.FromArgb("#F5EF9A") });

    }

    //Random random = new Random();
    static int count = 0;
    private void PopulatePeopleBasedOnCount(int peopleCount)
    {
        this.PeopleCollection = new ObservableCollection<People>();
        for (int i = 0; i < peopleCount; i++)
        {
            while (true)
            {
                if(TotalPeople!=null)
                {
                    if (this.TotalPeople.Count <= count)
                        count = 0;

                var person = (this.TotalPeople?[count++]);
                if (person != null && !this.PeopleCollection.Contains(person))
                {
                    this.PeopleCollection.Add(person);
                    break;
                }
                }              
            }
        }
    }
}

public class People
{
    public string? Name { get; set; }

    public ImageSource? Image { get; set; }

    public Color? Backgroundcolor { get; set; }
}