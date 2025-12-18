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

namespace SampleBrowser.Maui.LiquidGlass.SfSwitch;

public partial class SwitchPage : SampleView
{
    public SwitchPage()
    {
        InitializeComponent();

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
    #endregion
}