#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Maps;

public class PopulationDensityDetails
{
    public string State { get; set; }
    public string StateCode { get; set; }
    public int Rank { get; set; }
    public double SquareMiles { get; set; }
    public double SquareKilometer { get; set; }

    public PopulationDensityDetails(string state, string stateCode, int rank, double squareMiles, double squareKilometer)
    {
        this.State = state;
        this.StateCode = stateCode;
        this.Rank = rank;
        this.SquareMiles = squareMiles;
        this.SquareKilometer = squareKilometer;
    }
}