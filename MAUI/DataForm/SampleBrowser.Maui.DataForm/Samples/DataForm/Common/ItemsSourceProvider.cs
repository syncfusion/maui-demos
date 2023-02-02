#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.DataForm.SfDataForm
{
    using System;
    using Syncfusion.Maui.DataForm;

    /// <summary>
    /// Represents a class to provide items source for picker items.
    /// </summary>
    public class ItemsSourceProvider : IDataFormSourceProvider
    {
        /// <summary>
        /// Gets the items source for respective source name.
        /// </summary>
        /// <param name="sourceName">The porperty name.</param>
        /// <returns>The items source collection.</returns>
        public object GetSource(string sourceName)
        {
            if (sourceName == nameof(ContactFormModel.GroupName))
            {
                List<string> groupNames = new List<string>
                {
                    "Family",
                    "Friends",
                    "Coworkers",
                    "Not assigned",
                    "Emergency contacts",
                };

                return groupNames;
            }
            else if (sourceName == nameof(PaymentFormModel.Month))
            {
                List<string> months = new List<string>
                {
                    "Jan",
                    "Feb",
                    "Mar",
                    "Apr",
                    "May",
                    "Jun",
                    "Jul",
                    "Aug",
                    "Sep",
                    "Oct",
                    "Nov",
                    "Dec"
                };
                return months;
            }
            else if (sourceName == nameof(PaymentFormModel.Year))
            {
                List<string> years = new List<string>
                {
                    "2022",
                    "2023",
                    "2024",
                    "2025",
                    "2026",
                    "2027",
                    "2028",
                    "2029",
                    "2030",
                    "2031",
                    "2032"
                };
                return years;
            }
            else if (sourceName == nameof(SignUpFormModel.Country))
            {
                List<string> countries = new List<string>();
                countries.Add("Afghanistan");
                countries.Add("Albania");
                countries.Add("Algeria");
                countries.Add("American Samoa");
                countries.Add("Andorra");
                countries.Add("Angola");
                countries.Add("Anguilla");
                countries.Add("Antarctica");
                countries.Add("Antigua and Barbuda");
                countries.Add("Argentina");
                countries.Add("Armenia");
                countries.Add("Aruba");
                countries.Add("Australia");
                countries.Add("Austria");
                countries.Add("Azerbaijan");
                countries.Add("Bahamas");
                countries.Add("Bahrain");
                countries.Add("Bangladesh");
                countries.Add("Barbados");
                countries.Add("Belarus");
                countries.Add("Belgium");
                countries.Add("Belize");
                countries.Add("Benin");
                countries.Add("Bermuda");
                countries.Add("Bhutan");
                countries.Add("Bolivia");
                countries.Add("Bosnia and Herzegovina");
                countries.Add("Botswana");
                countries.Add("Brazil");
                countries.Add("Brunei");
                countries.Add("Bulgaria");
                countries.Add("Burma");
                countries.Add("Burundi");
                countries.Add("Cambodia");
                countries.Add("Cameroon");
                countries.Add("Canada");
                countries.Add("Cape Verde");
                countries.Add("Cayman Islands");
                countries.Add("Central African Republic");
                countries.Add("Chad");
                countries.Add("Chile");
                countries.Add("China");
                countries.Add("Christmas Island");
                countries.Add("Cocos (Keeling) Islands");
                countries.Add("Colombia");
                countries.Add("Comoros");
                countries.Add("Congo");
                countries.Add("Cook Islands");
                countries.Add("Costa Rica");
                countries.Add("Cote d'Ivoire");
                countries.Add("Croatia");
                countries.Add("Cuba");
                countries.Add("Cyprus");
                countries.Add("Czech Republic");
                countries.Add("Denmark");
                countries.Add("Djibouti");
                countries.Add("Dominica");
                countries.Add("Dominican Republic");
                countries.Add("Ecuador");
                countries.Add("Egypt");
                countries.Add("El Salvador");
                countries.Add("Equatorial Guinea");
                countries.Add("Eritrea");
                countries.Add("Estonia");
                countries.Add("Ethiopia");
                countries.Add("Europa Island");
                countries.Add("Falkland Islands");
                countries.Add("Faroe Islands");
                countries.Add("Fiji");
                countries.Add("Finland");
                countries.Add("France");
                countries.Add("French Guiana");
                countries.Add("French Polynesia");
                countries.Add("French Southern Territories");
                countries.Add("Gabon");
                countries.Add("The Gambia");
                countries.Add("Gaza Strip");
                countries.Add("Georgia");
                countries.Add("Germany");
                countries.Add("Ghana");
                countries.Add("Gibraltar");
                countries.Add("Greece");
                countries.Add("Greenland");
                countries.Add("Grenada");
                countries.Add("Guadeloupe");
                countries.Add("Guam");
                countries.Add("Guatemala");
                countries.Add("Guernsey");
                countries.Add("Guinea");
                countries.Add("Guinea-Bissau");
                countries.Add("Guyana");
                countries.Add("Haiti");
                countries.Add("Holy See");
                countries.Add("Honduras");
                countries.Add("Hong Kong");
                countries.Add("Hungary");
                countries.Add("Iceland");
                countries.Add("India");
                countries.Add("Indonesia");
                countries.Add("Iran");
                countries.Add("Iraq");
                countries.Add("Ireland");
                countries.Add("Israel");
                countries.Add("Italy");
                countries.Add("Jamaica");
                countries.Add("Japan");
                countries.Add("Jordan");
                countries.Add("Kazakhstan");
                countries.Add("Kenya");
                countries.Add("Kiribati");
                countries.Add("Korea, North");
                countries.Add("Korea, South");
                countries.Add("Kuwait");
                countries.Add("Kyrgyzstan");
                countries.Add("Laos");
                countries.Add("Latvia");
                countries.Add("Lebanon");
                countries.Add("Lesotho");
                countries.Add("Liberia");
                countries.Add("Libya");
                countries.Add("Liechtenstein");
                countries.Add("Lithuania");
                countries.Add("Luxembourg");
                countries.Add("Macau");
                countries.Add("Madagascar");
                countries.Add("Malawi");
                countries.Add("Malaysia");
                countries.Add("Maldives");
                countries.Add("Mali");
                countries.Add("Malta");
                countries.Add("Marshall Islands");
                countries.Add("Martinique");
                countries.Add("Mauritania");
                countries.Add("Mauritius");
                countries.Add("Mayotte");
                countries.Add("Mexico");
                countries.Add("Micronesia");
                countries.Add("Moldova");
                countries.Add("Monaco");
                countries.Add("Mongolia");
                countries.Add("Montserrat");
                countries.Add("Morocco");
                countries.Add("Mozambique");
                countries.Add("Namibia");
                countries.Add("Nauru");
                countries.Add("Nepal");
                countries.Add("Netherlands");
                countries.Add("Netherlands Antilles");
                countries.Add("New Caledonia");
                countries.Add("New Zealand");
                countries.Add("Nicaragua");
                countries.Add("Niger");
                countries.Add("Nigeria");
                countries.Add("Niue");
                countries.Add("Norway");
                countries.Add("Oman");
                countries.Add("Pakistan");
                countries.Add("Palau");
                countries.Add("Panama");
                countries.Add("Papua New Guinea");
                countries.Add("Paracel Islands");
                countries.Add("Paraguay");
                countries.Add("Peru");
                countries.Add("Philippines");
                countries.Add("Pitcairn Islands");
                countries.Add("Poland");
                countries.Add("Portugal");
                countries.Add("Puerto Rico");
                countries.Add("Qatar");
                countries.Add("Romania");
                countries.Add("Russia");
                countries.Add("Rwanda");
                countries.Add("Saint Kitts and Nevis");
                countries.Add("Saint Lucia");
                countries.Add("Saint Vincent");
                countries.Add("Samoa");
                countries.Add("San Marino");
                countries.Add("Saudi Arabia");
                countries.Add("Senegal");
                countries.Add("Serbia");
                countries.Add("Seychelles");
                countries.Add("Sierra Leone");
                countries.Add("Singapore");
                countries.Add("Slovakia");
                countries.Add("Slovenia");
                countries.Add("Solomon Islands");
                countries.Add("Somalia");
                countries.Add("South Africa");
                countries.Add("South Georgia");
                countries.Add("Spain");
                countries.Add("Sri Lanka");
                countries.Add("Sudan");
                countries.Add("Suriname");
                countries.Add("Svalbard");
                countries.Add("Swaziland");
                countries.Add("Sweden");
                countries.Add("Switzerland");
                countries.Add("Syria");
                countries.Add("Taiwan");
                countries.Add("Tajikistan");
                countries.Add("Tanzania");
                countries.Add("Thailand");
                countries.Add("Timor-Leste");
                countries.Add("Togo");
                countries.Add("Tokelau");
                countries.Add("Tonga");
                countries.Add("Trinidad and Tobago");
                countries.Add("Tunisia");
                countries.Add("Turkey");
                countries.Add("Turkmenistan");
                countries.Add("Turks and Caicos Islands");
                countries.Add("Tuvalu");
                countries.Add("Uganda");
                countries.Add("Ukraine");
                countries.Add("United Arab Emirates");
                countries.Add("United Kingdom");
                countries.Add("United States");
                countries.Add("Uruguay");
                countries.Add("Uzbekistan");
                countries.Add("Vanuatu");
                countries.Add("Venezuela");
                countries.Add("Vietnam");
                countries.Add("Virgin Islands");
                countries.Add("Wallis and Futuna");
                countries.Add("West Bank");
                countries.Add("Western Sahara");
                countries.Add("Yemen");
                countries.Add("Zambia");
                countries.Add("Zimbabwe");
                return countries;
            }

            return new List<string>();
        }
    }


}