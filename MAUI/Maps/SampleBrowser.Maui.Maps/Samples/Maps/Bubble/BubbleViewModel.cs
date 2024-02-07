#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Maps;

public class BubbleViewModel 
{
	public ObservableCollection<UserDetails> Details { get; set; }
    public BubbleViewModel()
    {
        Details = new ObservableCollection<UserDetails>()
        {
             new UserDetails("India", 280),
             new UserDetails("United States of America", 190),
             new UserDetails("Indonesia", 130),
             new UserDetails("Brazil", 120),
             new UserDetails("Mexico", 86),
             new UserDetails("Philippines", 72),
             new UserDetails("Vietnam", 63),
             new UserDetails("Thailand", 48),
             new UserDetails("Egypt", 41),
             new UserDetails("Bangladesh", 37),
             new UserDetails("Pakistan", 37),
             new UserDetails("Turkey", 37),
             new UserDetails("United Kingdom", 37),
             new UserDetails("Colombia", 33),
             new UserDetails("France", 32),
        };
    }
}