#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Maps;

public class CountryTimeInGMT
{
    public String CountryName { get; set; }

    public String GmtTime { get; set; }

    public CountryTimeInGMT(string countryName, string gmtTime)
    {
        this.CountryName = countryName;
        this.GmtTime = gmtTime;
    }

}