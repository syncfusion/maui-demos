#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.ComponentModel;

namespace SampleBrowser.Maui.NavigationDrawer.NavigationDrawer;


public class Notification : INotifyPropertyChanged
{
    private string? name;
    private string? subject;
    private string? description;
    private string? imagesource;
    private string? timeStamp;

    public string? Name
    {
        get => name;
        set { name = value; OnPropertyChanged(nameof(Name)); }
    }

    public string? Subject
    {
        get => subject;
        set { subject = value; OnPropertyChanged(nameof(Subject)); }
    }

    public string? Description
    {
        get => description;
        set { description = value; OnPropertyChanged(nameof(Description)); }
    }

    public string? ImageSource
    {
        get => imagesource;
        set { imagesource = value; OnPropertyChanged(nameof(ImageSource)); }
    }
    public string? TimeStamp 
    { 
        get => timeStamp; 
        set { timeStamp = value; OnPropertyChanged(nameof(TimeStamp)); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
