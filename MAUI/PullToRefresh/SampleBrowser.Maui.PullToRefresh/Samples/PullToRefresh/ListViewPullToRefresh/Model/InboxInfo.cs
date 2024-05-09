#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.ComponentModel;

namespace SampleBrowser.Maui.PullToRefresh;

/// <summary>
/// Class contains property and fields for ListViewPullToRefresh.
/// </summary>
public class InboxInfo : INotifyPropertyChanged
{
    #region Fields

    private string? profileName;
    private string? name;
    private string? subject;
    private string? description;
    private DateTime date;
    private string? image;
    private bool isAttached;
    private bool isOpened;
    private bool isImportant;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the Name and notifies user when collection value gets changed.
    /// </summary>
    public string? Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
            OnPropertyChanged("Name");
        }
    }

    /// <summary>
    /// Gets or sets the ProfileName and notifies user when collection value gets changed.
    /// </summary>
    public string? ProfileName
    {
        get { return profileName; }
        set
        {
            profileName = value;
            OnPropertyChanged("ProfileName");
        }
    }

    /// <summary>
    /// Gets or sets the Subject and notifies user when collection value gets changed.
    /// </summary>
    public string? Subject
    {
        get
        {
            return subject;
        }

        set
        {
            subject = value;
            OnPropertyChanged("Subject");
        }
    }

    /// <summary>
    /// Gets or sets the Description and notifies user when collection value gets changed.
    /// </summary>
    public string? Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
            OnPropertyChanged("Description");
        }
    }

    /// <summary>
    /// Gets or sets the Date and notifies user when collection value gets changed.
    /// </summary>
    public DateTime Date
    {
        get
        {
            return date;
        }

        set
        {
            date = value;
            OnPropertyChanged("Date");
        }
    }

    /// <summary>
    /// Gets or sets the Image and notifies user when collection value gets changed.
    /// </summary>
    public string? Image
    {
        get
        {
            return image;
        }

        set
        {
            image = value;
            OnPropertyChanged("Image");
        }
    }

    /// <summary>
    /// Gets or sets the IsAttached and notifies user when collection value gets changed.
    /// </summary>
    public bool IsAttached
    {
        get { return isAttached; }
        set
        {
            isAttached = value;
            OnPropertyChanged("IsAttached");
        }
    }

    /// <summary>
    /// Gets or sets the IsImportant and notifies user when collection value gets changed.
    /// </summary>
    public bool IsImportant
    {
        get { return isImportant; }
        set
        {
            isImportant = value;
            OnPropertyChanged("IsImportant");
        }
    }

    /// <summary>
    /// Gets or sets the IsOpened and notifies user when collection value gets changed.
    /// </summary>
    public bool IsOpened
    {
        get { return isOpened; }
        set
        {
            isOpened = value;
            OnPropertyChanged("IsOpened");
        }
    }

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
}
