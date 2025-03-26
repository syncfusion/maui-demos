#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using SampleBrowser.Maui.Inputs.Samples.Autocomplete.AutocompleteDropdownFeatures.Model;
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Inputs.Samples.Autocomplete.AutocompleteDropdownFeatures.ViewModel
{
    public class DropDownViewModel
    {
        public ObservableCollection<string> Islands { get; set; }

        public ObservableCollection<string> Continents { get; set; }

        public ObservableCollection<string> Wonders { get; set; }

        public ObservableCollection<Countries> Countries { get; set; }
      
        public DropDownViewModel()
        {
            this.Islands = new ObservableCollection<string>();
            this.Islands.Add("Madeira");
            this.Islands.Add("Mílos");
            this.Islands.Add("Folegandros");
            this.Islands.Add("Andaman Islands");
            this.Islands.Add("Sicily");
            this.Islands.Add("Maldives");
            this.Islands.Add("Maui");
            this.Islands.Add("Bali");
            this.Islands.Add("Holbox Island");
            this.Islands.Add("Whitsundays");
            this.Islands.Add("Bora Bora");
            this.Islands.Add("Vanuatu");
            this.Islands.Add("Palawan");
            this.Islands.Add("Santorini");
            this.Islands.Add("Sri Lanka");
            this.Islands.Add("Dominican Republic");
            this.Islands.Add("Lakshadweep");

            this.Continents = new ObservableCollection<string>();
            this.Continents.Add("Africa");
            this.Continents.Add("Antarctica");
            this.Continents.Add("Asia");
            this.Continents.Add("Australia");
            this.Continents.Add("Europe");
            this.Continents.Add("North America");
            this.Continents.Add("South America");

            this.Wonders = new ObservableCollection<string>();
            this.Wonders.Add("Great Wall of China");
            this.Wonders.Add("Petra");
            this.Wonders.Add("Colosseum");
            this.Wonders.Add("Chichen Itza");
            this.Wonders.Add("Machu Picchu");
            this.Wonders.Add("Taj Mahal");
            this.Wonders.Add("Christ the Redeemer");

            this.Countries = new ObservableCollection<Countries>();
            this.Countries.Add(new Countries { CountryName = "Argentina"});
            this.Countries.Add(new Countries { CountryName = "Austria"});
            this.Countries.Add(new Countries { CountryName = "Afghanistan"});
            this.Countries.Add(new Countries { CountryName = "Albania" });
            this.Countries.Add(new Countries { CountryName = "Algeria" });
            this.Countries.Add(new Countries { CountryName = "Andorra" });
            this.Countries.Add(new Countries { CountryName = "Angola" });
            this.Countries.Add(new Countries { CountryName = "Armenia" });
            this.Countries.Add(new Countries { CountryName = "Australia" });
            this.Countries.Add(new Countries { CountryName = "Austria" });
            this.Countries.Add(new Countries { CountryName = "Azerbaijan" });
            this.Countries.Add(new Countries { CountryName = "Brazil" });
            this.Countries.Add(new Countries { CountryName = "Bahamas" });
            this.Countries.Add(new Countries { CountryName = "Bahrain" });
            this.Countries.Add(new Countries { CountryName = "Bangladesh" });
            this.Countries.Add(new Countries { CountryName = "Barbados" });
            this.Countries.Add(new Countries { CountryName = "Belarus" });
            this.Countries.Add(new Countries { CountryName = "Belgium" });
            this.Countries.Add(new Countries { CountryName = "Belize" });
            this.Countries.Add(new Countries { CountryName = "Benin" });
            this.Countries.Add(new Countries { CountryName = "Bhutan" });
            this.Countries.Add(new Countries { CountryName = "Bolivia" });
            this.Countries.Add(new Countries { CountryName = "Botswana" });
            this.Countries.Add(new Countries { CountryName = "Brunei" });
            this.Countries.Add(new Countries { CountryName = "Bulgaria" });
            this.Countries.Add(new Countries { CountryName = "Burundi" });
            this.Countries.Add(new Countries { CountryName = "Canada"});
            this.Countries.Add(new Countries { CountryName = "Colombia" });
            this.Countries.Add(new Countries { CountryName = "Dubai" });
            this.Countries.Add(new Countries { CountryName = "France" });
            this.Countries.Add(new Countries { CountryName = "Germany" });
            this.Countries.Add(new Countries { CountryName = "India" });
            this.Countries.Add(new Countries { CountryName = "Japan" });
            this.Countries.Add(new Countries { CountryName = "Switzerland" });
            this.Countries.Add(new Countries { CountryName = "Sydney" });
            this.Countries.Add(new Countries { CountryName = "Thailand" });
            this.Countries.Add(new Countries { CountryName = "Tanzania" });
            this.Countries.Add(new Countries { CountryName = "Thailand" });
            this.Countries.Add(new Countries { CountryName = "Togo" });
            this.Countries.Add(new Countries { CountryName = "Tonga" });
            this.Countries.Add(new Countries { CountryName = "Tunisia" });
            this.Countries.Add(new Countries { CountryName = "Turkmenistan" });
            this.Countries.Add(new Countries { CountryName = "Tuvalu" });
        }
    }
}
