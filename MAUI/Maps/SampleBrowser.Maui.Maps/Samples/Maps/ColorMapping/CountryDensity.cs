#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Maps;

public class CountryDensity
{
    public string CountryName { get; set; }

    public double Density { get; set; }

    public CountryDensity(string countryName, double density)
    {
        this.CountryName = countryName;
        this.Density = density;
    }
}
