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