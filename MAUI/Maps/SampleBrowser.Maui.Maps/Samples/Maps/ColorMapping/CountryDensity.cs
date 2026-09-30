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
