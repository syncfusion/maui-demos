#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls.Shapes;
using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Buttons;
using Syncfusion.Maui.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.Buttons.Switch;

public partial class GettingStartedDesktop : SampleView
{
	public GettingStartedDesktop()
	{
		InitializeComponent();

#if ANDROID || IOS
        allAppsGrid.Margin = new(15,0,10,0);
#else
        allAppsGrid.Margin = new(15, 0, 5, 0);
#endif


        ListCollection.Add(new AppModel(this) 
        {
            Name = "Twitter", 
            IconColor = Color.FromArgb("#1DA1F2"),
            ImageName = "X.png",
            CanNotify = true
        });
        ListCollection.Add(new AppModel(this) 
        { 
            Name = "YouTube", 
            IconColor = Color.FromArgb("#1DA1F2"),
            ImageName = "Youtube.png",
            CanNotify = true
        });
        ListCollection.Add(new AppModel(this) 
        { 
            Name = "Instagram", 
            IconColor = Color.FromArgb("#1DA1F2"),
            ImageName = "InstagramIcon.png",
            CanNotify = true
        });
        ListCollection.Add(new AppModel(this) 
        { 
            Name = "Gmail", 
            IconColor = Color.FromArgb("#1DA1F2"),
            ImageName = "Gmail.png",
            CanNotify = true
        });
        ListCollection.Add(new AppModel(this) 
        { 
            Name = "LinkedIn", 
            IconColor = Color.FromArgb("#1DA1F2"),
            ImageName = "Linked In.png",
            CanNotify = true
        });
        ListCollection.Add(new AppModel(this)
        {
            Name = "Skype",
            IconColor = Color.FromArgb("#1DA1F2"),
            ImageName = "Skype.png",
            CanNotify = true
        });

        this.BindingContext = this;
    }

    #region Members

    private ObservableCollection<AppModel> listCollection = new ObservableCollection<AppModel>();

    private bool canNotify;

    #endregion

    #region properties

    /// <summary>
    /// Collection which contains the items that will be enabled and disabled by the segment control to display on the main segment control.
    /// </summary>
    public ObservableCollection<AppModel> ListCollection
    {
        get
        {
            return listCollection;
        }
        set
        {
            listCollection = value;
        }
    }

    public bool CanNotify
    {
        get
        {
            return canNotify;
        }
        set
        {
            if (canNotify != value)
            {
                canNotify = value;
                raisePropertyChanged("CanNotify");
            }
        }
    }
    #endregion

    #region PropertyChanged
    public event PropertyChangedEventHandler? propertyChanged;
    private void raisePropertyChanged(string property)
    {
        if (propertyChanged != null)
        {
            propertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
    #endregion

    #region Dispose
    /// <summary>
    /// Dispose method
    /// </summary>
    public void Dispose()
    {
    }
    #endregion

    bool? oldValue = null;

    private async void AllAppSwitch_StateChanged(object sender, SwitchStateChangedEventArgs e)
    {
        if (!setAllAppsSwitchState)
        {
            return;
        }
        else
        {
            if (allAppSwitch.IsOn == null)
            {
                allAppSwitch.IsOn = oldValue;
            }
        }
        await Task.Delay(1);
        if (this.allAppSwitch.IsOn == true)
        {
            foreach (var appModel in this.ListCollection)
            {
                appModel.CanNotify = true;
            }
        }
        else if (this.allAppSwitch.IsOn == false)
        {
            foreach (var appModel in this.ListCollection)
            {
                appModel.CanNotify = false;
            }
        }
        oldValue = this.allAppSwitch.IsOn;
    }

    bool setAllAppsSwitchState = true;
    private async void SfSwitch_StateChanged(object sender, SwitchStateChangedEventArgs e)
    {
        setAllAppsSwitchState = false;
        bool hasTrue = false;

        bool hasFalse = false;

        foreach (var appModel in this.ListCollection)
        {
            if (appModel.CanNotify == true)
            {
                hasTrue = true;
            }
            if (appModel.CanNotify == false)
            {
                hasFalse = true;
            }
        }

        if (hasFalse && hasTrue)
        {
            this.allAppSwitch.AllowIndeterminateState = true;
            this.allAppSwitch.IsOn = null;
        }
        else if (hasFalse && !hasTrue)
        {
            this.allAppSwitch.AllowIndeterminateState = false;
            this.allAppSwitch.IsOn = false;
        }
        else if (!hasFalse && hasTrue)
        {
            this.allAppSwitch.AllowIndeterminateState = false;
            this.allAppSwitch.IsOn = true;
        }
        await Task.Delay(250);
        setAllAppsSwitchState = true;
    }

    private void IconsView_ChildAdded(object sender, ElementEventArgs e)
    {
        
    }
}

/// <summary>
/// Model class for the tokens
/// </summary>
public class AppModel : INotifyPropertyChanged
{
    #region Members
    private bool? canNotify;
    private String? name;
    private String imageName = string.Empty;
    private Color? iconColor;
    private ObservableCollection<SfSegmentItem> dataCollection = new ObservableCollection<SfSegmentItem>();
    private GettingStartedDesktop? tokensObject;
    #endregion

    #region Properties
    /// <summary>
    /// Get or set the name for the items present in the list.
    /// </summary>
    public String Name
    {
        get
        {
            return name!;
        }
        set
        {
            name = value;
            raisePropertyChanged("Name");
        }
    }

    /// <summary>
    /// Get or set the name for the items present in the list.
    /// </summary>
    public String ImageName
    {
        get
        {
            return imageName!;
        }
        set
        {
            imageName = value;
            raisePropertyChanged("ImageName");
        }
    }

    /// <summary>
    /// Get or set the color for the icons used inside the listView.
    /// </summary>
    public Color IconColor
    {
        get
        {
            return iconColor!;
        }
        set
        {
            iconColor = value;
            raisePropertyChanged("IconColor");
        }
    }

    public bool? CanNotify
    {
        get
        {
            return canNotify;
        }
        set
        {
            if (canNotify != value)
            {
                canNotify = value;
                raisePropertyChanged("CanNotify");
            }
        }
    }

    /// <summary>
    /// Data collection for the items used inside the listView
    /// </summary>
    public ObservableCollection<SfSegmentItem> DataCollection
    {
        get
        {
            return dataCollection;
        }
        set
        {
            dataCollection = value;
        }
    }

    #endregion

    #region Constructor
    /// <summary>
    /// Constructor initializing for the App model class
    /// </summary>
    /// <param name="tokens"></param>
    public AppModel(GettingStartedDesktop tokens)
    {
        tokensObject = tokens;
        
    }
    #endregion

    #region PropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    private void raisePropertyChanged(string property)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
    #endregion
}