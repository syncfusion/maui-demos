namespace SampleBrowser.Maui.Maps;

public class UserDetails
{
    public string CountryName { get; set; }
    public double UsersCount { get; set; }
    public UserDetails(string countryName, double usersCount)
    {
        this.CountryName = countryName;
        this.UsersCount = usersCount;
    }
}