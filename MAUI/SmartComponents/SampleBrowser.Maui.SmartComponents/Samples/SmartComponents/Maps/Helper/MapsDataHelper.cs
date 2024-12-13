#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    /// <summary>
    /// Provides predefined location data for hospitals in New York and hotels in Denver.
    /// This class serves as a data source for map-based applications, offering structured location information that includes names, details, coordinates, and addresses.
    /// </summary>
    internal class MapsDataHelper
    {
        /// <summary>
        /// JSON string containing hospital data.
        /// </summary>
        internal string hospitalsData = "{\r\n  \"markercollections\": [\r\n    {\r\n      \"Name\": \"NewYork-Presbyterian Hospital\",\r\n      \"Details\": \"One of the largest and most comprehensive hospitals in New York.\",\r\n      \"Latitude\": 40.8414,\r\n      \"Longitude\": -73.9423,\r\n      \"Address\": \"622 W 168th St, New York, NY 10032\"\r\n    },\r\n    {\r\n      \"Name\": \"Mount Sinai Hospital\",\r\n      \"Details\": \"A leading hospital in New York, known for its medical research and treatment.\",\r\n      \"Latitude\": 40.7895,\r\n      \"Longitude\": -73.9531,\r\n      \"Address\": \"1468 Madison Ave, New York, NY 10029\"\r\n    },\r\n    {\r\n      \"Name\": \"NYU Langone Health\",\r\n      \"Details\": \"A premier academic medical center in New York.\",\r\n      \"Latitude\": 40.7427,\r\n      \"Longitude\": -73.9741,\r\n      \"Address\": \"550 1st Avenue, New York, NY 10016\"\r\n    },\r\n    {\r\n      \"Name\": \"Lenox Hill Hospital\",\r\n      \"Details\": \"A member of Northwell Health, offering a wide range of medical services.\",\r\n      \"Latitude\": 40.7739,\r\n      \"Longitude\": -73.9602,\r\n      \"Address\": \"100 E 77th St, New York, NY 10075\"\r\n    },\r\n    {\r\n      \"Name\": \"Bellevue Hospital Center\",\r\n      \"Details\": \"The oldest public hospital in the United States.\",\r\n      \"Latitude\": 40.7395,\r\n      \"Longitude\": -73.9750,\r\n      \"Address\": \"462 1st Avenue, New York, NY 10016\"\r\n    },\r\n    {\r\n      \"Name\": \"Mount Sinai West\",\r\n      \"Details\": \"A full-service, 514-bed medical center located in Midtown Manhattan.\",\r\n      \"Latitude\": 40.7690,\r\n      \"Longitude\": -73.9866,\r\n      \"Address\": \"1000 10th Ave, New York, NY 10019\"\r\n    },\r\n    {\r\n      \"Name\": \"Mount Sinai Beth Israel\",\r\n      \"Details\": \"A teaching hospital in Manhattan providing comprehensive health care.\",\r\n      \"Latitude\": 40.7312,\r\n      \"Longitude\": -73.9824,\r\n      \"Address\": \"281 1st Avenue, New York, NY 10003\"\r\n    },\r\n    {\r\n      \"Name\": \"Hospital for Special Surgery\",\r\n      \"Details\": \"A world leader in orthopedics and rheumatology.\",\r\n      \"Latitude\": 40.7641,\r\n      \"Longitude\": -73.9557,\r\n      \"Address\": \"535 E 70th St, New York, NY 10021\"\r\n    },\r\n    {\r\n      \"Name\": \"Memorial Sloan Kettering Cancer Center\",\r\n      \"Details\": \"One of the world's oldest and most comprehensive cancer treatment centers.\",\r\n      \"Latitude\": 40.7641,\r\n      \"Longitude\": -73.9549,\r\n      \"Address\": \"1275 York Ave, New York, NY 10065\"\r\n    },\r\n    {\r\n      \"Name\": \"New York Eye and Ear Infirmary of Mount Sinai\",\r\n      \"Details\": \"Specializes in the treatment of eye, ear, nose, and throat conditions.\",\r\n      \"Latitude\": 40.7323,\r\n      \"Longitude\": -73.9875,\r\n      \"Address\": \"310 E 14th St, New York, NY 10003\"\r\n    },\r\n    {\r\n      \"Name\": \"St. Luke's Roosevelt Hospital Center\",\r\n      \"Details\": \"Part of Mount Sinai Health System, providing a wide range of health services.\",\r\n      \"Latitude\": 40.7690,\r\n      \"Longitude\": -73.9866,\r\n      \"Address\": \"1111 Amsterdam Ave, New York, NY 10025\"\r\n    },\r\n    {\r\n      \"Name\": \"Bronx-Lebanon Hospital Center\",\r\n      \"Details\": \"A major teaching hospital and health care system in the Bronx.\",\r\n      \"Latitude\": 40.8370,\r\n      \"Longitude\": -73.9098,\r\n      \"Address\": \"1650 Grand Concourse, Bronx, NY 10457\"\r\n    },\r\n    {\r\n      \"Name\": \"Jamaica Hospital Medical Center\",\r\n      \"Details\": \"A full-service hospital serving the community of Queens.\",\r\n      \"Latitude\": 40.7024,\r\n      \"Longitude\": -73.8085,\r\n      \"Address\": \"8900 Van Wyck Expy, Jamaica, NY 11418\"\r\n    },\r\n    {\r\n      \"Name\": \"Flushing Hospital Medical Center\",\r\n      \"Details\": \"A 293-bed, not-for-profit teaching hospital in Queens.\",\r\n      \"Latitude\": 40.7490,\r\n      \"Longitude\": -73.8196,\r\n      \"Address\": \"4500 Parsons Blvd, Flushing, NY 11355\"\r\n    },\r\n    {\r\n      \"Name\": \"Kings County Hospital Center\",\r\n      \"Details\": \"A major hospital in Brooklyn providing comprehensive health services.\",\r\n      \"Latitude\": 40.6552,\r\n      \"Longitude\": -73.9445,\r\n      \"Address\": \"451 Clarkson Ave, Brooklyn, NY 11203\"\r\n    }\r\n  ]\r\n}\r\n";

        /// <summary>
        /// JSON string containing hotel data.
        /// </summary>
        internal string hotelsData = "{\r\n  \"markercollections\": [\r\n    {\r\n      \"Name\": \"The Brown Palace Hotel and Spa\",\r\n      \"Details\": \"Historic luxury hotel in downtown Denver\",\r\n      \"Latitude\": \"39.770296\",\r\n      \"Longitude\": \"-104.993514\",\r\n      \"Address\": \"321 17th St, Denver, CO 80202, USA\"\r\n    },\r\n    {\r\n      \"Name\": \"Hyatt Regency Denver at Colorado Convention Center\",\r\n      \"Details\": \"Modern hotel with mountain views near the convention center\",\r\n      \"Latitude\": \"39.7697558\",\r\n      \"Longitude\": \"-105.3429489\",\r\n      \"Address\": \"650 15th St, Denver, CO 80202, USA\"\r\n    },\r\n    {\r\n      \"Name\": \"The Ritz-Carlton, Denver\",\r\n      \"Details\": \"Upscale hotel with elegant rooms and a luxury spa\",\r\n      \"Latitude\": \"39.774337\",\r\n      \"Longitude\": \"-104.965222\",\r\n      \"Address\": \"1881 Curtis Street, Denver, CO 80202, USA\"\r\n    },\r\n    {\r\n      \"Name\": \"Kimpton Hotel Born\",\r\n      \"Details\": \"Chic hotel near Union Station with stylish decor\",\r\n      \"Latitude\": \"39.772375\",\r\n      \"Longitude\": \"-104.979930\",\r\n      \"Address\": \"1600 Wewatta St, Denver, CO 80202, USA\"\r\n    },\r\n    {\r\n      \"Name\": \"Grand Hyatt Denver\",\r\n      \"Details\": \"High-rise hotel with an indoor pool and event space\",\r\n      \"Latitude\": \"39.743458\",\r\n      \"Longitude\": \"-104.961812\",\r\n      \"Address\": \"1750 Welton St, Denver, CO 80202, USA\"\r\n    },\r\n    {\r\n      \"Name\": \"The Oxford Hotel\",\r\n      \"Details\": \"Boutique hotel blending Victorian charm with modern amenities\",\r\n      \"Latitude\": \"39.744162\",\r\n      \"Longitude\": \"-104.987722\",\r\n      \"Address\": \"1600 17th St, Denver, CO 80202, USA\"\r\n    },\r\n    {\r\n      \"Name\": \"Hotel Teatro\",\r\n      \"Details\": \"Luxury hotel in a historic Renaissance Revival building\",\r\n      \"Latitude\": \"39.753789\",\r\n      \"Longitude\": \"-104.998220\",\r\n      \"Address\": \"1100 14th St, Denver, CO 80202, USA\"\r\n    },\r\n    {\r\n      \"Name\": \"Le Méridien Denver Downtown\",\r\n      \"Details\": \"Stylish hotel with a rooftop bar and city views\",\r\n      \"Latitude\": \"39.788041\",\r\n      \"Longitude\": \"-105.024084\",\r\n      \"Address\": \"1475 California St, Denver, CO 80202, USA\"\r\n    },\r\n    {\r\n      \"Name\": \"JW Marriott Denver Cherry Creek\",\r\n      \"Details\": \"Upscale hotel in the Cherry Creek shopping district\",\r\n      \"Latitude\": \"39.743040\",\r\n      \"Longitude\": \"-104.963240\",\r\n      \"Address\": \"150 Clayton Ln, Denver, CO 80206, USA\"\r\n    },\r\n    {\r\n      \"Name\": \"Halcyon, a hotel in Cherry Creek\",\r\n      \"Details\": \"Modern hotel with a rooftop pool and upscale dining\",\r\n      \"Latitude\": \"39.752812\",\r\n      \"Longitude\": \"-104.987565\",\r\n      \"Address\": \"245 Columbine St, Denver, CO 80206, USA\"\r\n    },\r\n    {\r\n      \"Name\": \"The Art Hotel Denver, Curio Collection by Hilton\",\r\n      \"Details\": \"Contemporary hotel with art-themed decor and exhibits\",\r\n      \"Latitude\": \"38.7374\",\r\n      \"Longitude\": \"-104.9876\",\r\n      \"Address\": \"1201 Broadway, Denver, CO 80203, USA\"\r\n    },\r\n    {\r\n      \"Name\": \"Kimpton Hotel Monaco Denver\",\r\n      \"Details\": \"Boutique hotel with Italian decor and fine dining\",\r\n      \"Latitude\": \"39.7078\",\r\n      \"Longitude\": \"-104.2112\",\r\n      \"Address\": \"1717 Champa St, Denver, CO 80202, USA\"\r\n    },\r\n    {\r\n      \"Name\": \"Four Seasons Hotel Denver\",\r\n      \"Details\": \"Luxury hotel with a rooftop pool and city views\",\r\n      \"Latitude\": \"39.7449\",\r\n      \"Longitude\": \"-104.9822\",\r\n      \"Address\": \"1111 14th St, Denver, CO 80202, USA\"\r\n    },\r\n    {\r\n      \"Name\": \"The Crawford Hotel\",\r\n      \"Details\": \"Elegant hotel in Union Station with historic architecture\",\r\n      \"Latitude\": \"39.7527\",\r\n      \"Longitude\": \"-105.0002\",\r\n      \"Address\": \"1701 Wynkoop St, Denver, CO 80202, USA\"\r\n    },\r\n    {\r\n      \"Name\": \"The Maven Hotel at Dairy Block\",\r\n      \"Details\": \"Trendy hotel with industrial-chic decor in a vibrant area\",\r\n      \"Latitude\": \"39.7517\",\r\n      \"Longitude\": \"-105.2204\",\r\n      \"Address\": \"1850 Wazee St, Denver, CO 80202, USA\"\r\n    }\r\n  ]\r\n}\r\n";
    }
}