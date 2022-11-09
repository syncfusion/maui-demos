#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Core;
using System.Collections.ObjectModel;
using Avatar = Syncfusion.Maui.Core;

namespace SampleBrowser.Maui.AvatarView.SfAvatarView;

public partial class VisualStyleSample : SampleView
{
    private ContentType avatarType = ContentType.Initials;

    public ContentType AvatarType
    {
        get
        {
            return avatarType;
        }
        set
        {
            avatarType = value;

            this.OnPropertyChanged();
        }
    }

    public ObservableCollection<People> TotalPeople { get; set; }

    #region Constructor
    public VisualStyleSample()
    {
        InitializeComponent();

        
        this.TotalPeople = new ObservableCollection<People>();
        this.TotalPeople.Add(new People() { Name = "Michael", Image = "SampleBrowser.Maui.Base.Resources.Images.people.png" });
        this.TotalPeople.Add(new People() { Name = "Kyle", Image = "SampleBrowser.Maui.Base.Resources.Images.people.png" });
        this.TotalPeople.Add(new People() { Name = "Nora" });

        this.BindingContext = this;

    }

    #endregion

    

    public class People
    {
        public String? Name { get; set; }

        public String? Image { get; set; }

        public Color? Backgroundcolor { get; set; }
    }
}

