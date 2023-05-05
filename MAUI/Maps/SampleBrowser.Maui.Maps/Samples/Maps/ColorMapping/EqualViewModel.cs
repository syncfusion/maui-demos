#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Maps;

public class EqualViewModel
{
    public ObservableCollection<CountryTimeInGMT> TimeZones { get; set; }
    public EqualViewModel()
    {
        TimeZones = new ObservableCollection<CountryTimeInGMT>() {
      new CountryTimeInGMT("Albania", "GMT+2"),
      new CountryTimeInGMT("Aland", "GMT+3"),
      new CountryTimeInGMT("Andorra", "GMT+1"),
      new CountryTimeInGMT("Austria", "GMT+2"),
      new CountryTimeInGMT("Belgium", "GMT+2"),
      new CountryTimeInGMT("Bulgaria", "GMT+3"),
      new CountryTimeInGMT("Bosnia and Herz.", "GMT+2"),
      new CountryTimeInGMT("Belarus", "GMT+3"),
      new CountryTimeInGMT("Switzerland", "GMT+2"),
      new CountryTimeInGMT("Czech Rep.", "GMT+2"),
      new CountryTimeInGMT("Germany", "GMT+2"),
      new CountryTimeInGMT("Denmark", "GMT+2"),
      new CountryTimeInGMT("Spain", "GMT+2"),
      new CountryTimeInGMT("Estonia", "GMT+3"),
      new CountryTimeInGMT("Finland", "GMT+3"),
      new CountryTimeInGMT("France", "GMT+2"),
      new CountryTimeInGMT("Faeroe Is.", "GMT+1"),
      new CountryTimeInGMT("United Kingdom", "GMT+1"),
      new CountryTimeInGMT("Guernsey", "GMT+1"),
      new CountryTimeInGMT("Greece", "GMT+3"),
      new CountryTimeInGMT("Croatia", "GMT+2"),
      new CountryTimeInGMT("Hungary", "GMT+2"),
      new CountryTimeInGMT("Isle of Man", "GMT+1"),
      new CountryTimeInGMT("Ireland", "GMT+1"),
      new CountryTimeInGMT("Iceland", "GMT+0"),
      new CountryTimeInGMT("Italy", "GMT+2"),
      new CountryTimeInGMT("Jersey", "GMT+1"),
      new CountryTimeInGMT("Kosovo", "GMT+2"),
      new CountryTimeInGMT("Liechtenstein", "GMT+2"),
      new CountryTimeInGMT("Lithuania", "GMT+3"),
      new CountryTimeInGMT("Luxembourg", "GMT+2"),
      new CountryTimeInGMT("Latvia", "GMT+3"),
      new CountryTimeInGMT("Monaco", "GMT+2"),
      new CountryTimeInGMT("Moldova", "GMT+3"),
      new CountryTimeInGMT("Macedonia", "GMT+2"),
      new CountryTimeInGMT("Malta", "GMT+2"),
      new CountryTimeInGMT("Montenegro", "GMT+2"),
      new CountryTimeInGMT("Netherlands", "GMT+2"),
      new CountryTimeInGMT("Poland", "GMT+2"),
      new CountryTimeInGMT("Portugal", "GMT+1"),
      new CountryTimeInGMT("Romania", "GMT+3"),
      new CountryTimeInGMT("San Marino", "GMT+2"),
      new CountryTimeInGMT("Serbia", "GMT+2"),
      new CountryTimeInGMT("Slovakia", "GMT+2"),
      new CountryTimeInGMT("Slovenia", "GMT+2"),
      new CountryTimeInGMT("Sweden", "GMT+2"),
      new CountryTimeInGMT("Ukraine", "GMT+3"),
      new CountryTimeInGMT("Vatican", "GMT+1"),
       };
    }
}