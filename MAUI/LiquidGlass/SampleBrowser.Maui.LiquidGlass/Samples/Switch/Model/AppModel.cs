#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace SampleBrowser.Maui.LiquidGlass.SfSwitch;


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
    private SwitchPage? tokensObject;
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
    public AppModel(SwitchPage tokens)
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
