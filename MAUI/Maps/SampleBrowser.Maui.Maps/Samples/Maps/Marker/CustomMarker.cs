#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Maui.Maps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.Maps;

public class CustomMarker : MapMarker, INotifyPropertyChanged
{
    public new event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private string? name;
    public string? Name
    {
        get
        {
            return name;
        }
        set
        {
            if (name != value)
            {
                name = value;
                NotifyPropertyChanged();
            }
        }
    }

    private string? state;
    public string? State
    {
        get
        {
            return state;
        }
        set
        {
            if (state != value)
            {
                state = value;
                NotifyPropertyChanged();
            }
        }
    }

    private string? country;
    public string? Country
    {
        get
        {
            return country;
        }
        set
        {
            if (country != value)
            {
                country = value;
                NotifyPropertyChanged();
            }
        }
    }

    private string? time;
    public string? Time
    {
        get
        {
            return time;
        }
        set
        {
            if (time != value)
            {
                time = value;
                NotifyPropertyChanged();
            }
        }
    }

}
