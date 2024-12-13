#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.ListView.SfListView
{
    #region PlaceInfoRepository
    public class PlaceInfoRepository
    {
        #region Fields
        public ObservableCollection<string[]> Places;
        public ObservableCollection<string[]> PlacesDescription;
        #endregion

        #region Constructor

        public PlaceInfoRepository()
        {
            Places = new ObservableCollection<string[]> { Place1, Place2, Place3, Place4, Place5, Place6, Place7, Place8, Place9, Place10, Place11, Place12, Place13, Place14, Place15 };
            PlacesDescription = new ObservableCollection<string[]> { Place1Description, Place2Description, Place3Description, Place4Description, Place5Description, Place6Description, Place7Description, Place8Description, Place9Description, Place10Description, Place11Description, Place12Description, Place13Description, Place14Description, Place15Description };
        }
        #endregion

        #region Generate methods

        public ObservableCollection<PlaceInfo> GeneratePlaces()
        {
            var index = 1;
            var places = new ObservableCollection<PlaceInfo>();
            for (int i = 0; i < PlaceNames.Count(); i++)
            {
                var place = new PlaceInfo();
                place.Name = PlaceNames[i];
                place.TouristPlaces = GenerateTouristPlaces(i);
                place.Image = "place_" + (index++) + ".jpg";
                places!.Add(place);
            }
            return places;
        }

        public ObservableCollection<PlaceInfo> GenerateTouristPlaces(int index)
        {
            var index1 = index + 1;
            var index2 = 1;
            var list = new ObservableCollection<PlaceInfo>();
            var placeDetails = this.Places[index];
            var placeDescriptionDetails = this.PlacesDescription[index];
            for (int i = 0; i < placeDetails.Count(); i++)
            {
                var place = new PlaceInfo();
                place.Name = placeDetails[i];
                place.Description = placeDescriptionDetails[i];
                place.Image = "place" + index1 + "" + (index2++) + ".jpg";
                list!.Add(place);
            }
            return list;
        }
        #endregion

        #region Collections

        readonly string[] PlaceNames = new string[]
        {
            "New York",
            "San Francisco",
            "Chicago",
            "Yosemite National Park",
            "Washington, D.C",
            "Los Angeles",
            "Boston",
            "Seattle",
            "Miami",
            "Las Vegas",
            "San Diego",
            "Atlanta",
            "New Orleans",
            "Orlando",
            "Denver",
        };

        public readonly string[] Place1 = new string[]
       {
            "Central Park",
            "The Metropolitan Museum of Art",
            "Statue of Liberty",
            "Empire State Building",
            "Solomon R. Guggenheim",
            "Times Square",
            "Brooklyn Bridge",
            "American Museum of Natural History",

       };

        readonly string[] Place1Description = new string[]
      {
            "Central Park is an urban park and the fifth largest park in New York City.",
            "The Metropolitan Museum is the largest art museum in the western hemisphere.",
            "The Statue of Liberty is a colossal neoclassical sculpture on Liberty Island.",
            "The Empire State Building is a 102-story art deco skyscraper in mid-Manhattan.",
            "The Solomon R. Guggenheim Museum is an art museum in Manhattan.",
            "Times Square is a tourist destination in mid-Manhattan.",
            "The Brooklyn Bridge is a hybrid cable-stayed bridge in New York City.",
            "The American Museum of Natural History  sits in the Upper West Side of Manhattan.",

      };
        readonly string[] Place2 = new string[]
      {
            "Golden Gate Bridge",
            "Alcatraz Island",
            "San Francisco Museum of Modern",
            "Golden Gate Park",
            "California Academy of Sciences",
            "Lombard Street",
            "Muir Woods National Monument",

      };

        readonly string[] Place2Description = new string[]
      {
            "This bridge crosses the one-mile-wide strait between San Francisco Bay and the Pacific Ocean.",
            "Alcatraz is a former prison on a small island in the San Francisco Bay.",
            "A modern and contemporary art museum in San Francisco.",
            "The park is a large urban park consisting of 1,017 acres of public grounds.",
            "The California Academy of Sciences is a research institute and natural history museum.",
            "Lombard St. is famous for a steep one-block section with eight hairpin turns.",
            "Muir Woods protects an old-growth redwood forest on Mount Tamalpais.",

      };
        readonly string[] Place3 = new string[]
      {
            "The Art Institute of Chicago",
            "Field Museum",
            "Navy Pier",
            "Millennium Park",
            "Shedd Aquarium",
            "Lincoln Park Zoo",
            "Cloud Gate",
            "Willis Tower",
      };

        readonly string[] Place3Description = new string[]
      {
            "The Art Institute of Chicago is the second-largest art museum in the USA.",
            "The Field Museum is one of the largest natural history museums in the world. ",
            "Navy Pier is a 3,300-foot-long pier and tourist attraction on the shore of Lake Michigan.",
            "Millennium Park is a public park located in the Loop community area of Chicago.",
            "Shedd Aquarium is an indoor public aquarium that opened in 1930.",
            "The Lincoln Park Zoo is a 35-acre zoo opened in 1868 in Chicago.",
            "The Cloud Gate is a public sculpture by Indian-born British artist Anish Kapoor.",
            "At 108 stories, the Willis Tower was the world�s tallest skyscraper for 25 years.",

      };
        readonly string[] Place4 = new string[]
      {
            "Yosemite Falls",
            "Half Dome",
            "Glacier Point",
            "El Capitan",
            "Yosemite Valley",
            "Tunnel view",
            "Bridalveil Fall",
            "The Ahwahnee",
            "Tuolumne Meadows",
      };

        readonly string[] Place4Description = new string[]
      {
            "Yosemite Falls is the highest waterfall in Yosemite National Park.",
            "The Half Dome is a granite dome at the eastern end of Yosemite Valley.",
            "Glacier Point is a viewpoint above Yosemite Valley.",
            "The El Capitan is a vertical rock formation in Yosemite National Park popular with climbers.",
            "The Yosemite Valley is a glacial valley in Yosemite National Park.",
            "The Tunnel View is a scenic viewpoint on California State Route 41.",
            "Fed by Ostrander Lake, Bridalveil Fall is one of the tallest waterfalls in Yosemite Valley.",
            "The Ahwahnee Hotel is a grand hotel in Yosemite National Park.",
            "The Tuolumne Meadows is a gentle, dome- studded, sub-alpine meadow area.",

      };
        readonly string[] Place5 = new string[]
      {
            "The White House",
            "United States Capitol",
            "Smithsonian Institution",
            "Lincoln Memorial",
            "National Gallery of Art",
            "Smithsonian National Museum of Natural History",
            "Washington Monument",
            "Smithsonian National Air and Space Museum",
      };

        readonly string[] Place5Description = new string[]
      {
            "The White House is the official residence of the U.S. president in Washington, D.C.",
            "The Capitol building is the seat of the U.S. Congress.",
            "The Smithsonian is the largest group of museums, education and research centers in the world.",
            "The Lincoln Memorial was built to honor the 16th president of the U.S., Abraham Lincoln.",
            "The National Gallery of Art collects and exhibits Western art in Washington, D.C.",
            "Founded in 1910, this Smithsonian branch contains the largest natural history collection in the world.",
            "The Washington Monument is a 169-meter tall obelisk in the National Mall.",
            "The Air and Space Museum displays pieces like the first airplane and the command module of Apollo 11.",

      };
        readonly string[] Place6 = new string[]
      {
            "Los Angeles County Museum of Art",
            "Disneyland Park",
            "Santa Monica Pier",
            "Griffith Observatory",
            "The Getty",
            "Universal Studios Hollywood",
            "Hollywood Sign",
            "The Huntington Library, Art Museum, and Botanical Gardens",
            "The Broad",

      };

        readonly string[] Place6Description = new string[]
      {
            "A visual arts museum that offers a collection of paintings, photography, etc.",
            "Disneyland is a theme park in Anaheim, California, which opened in 1955.",
            "The Santa Monica Pier is a large, double-jointed pier with an amusement park.",
            "A popular tourist attraction with a close view of the Hollywood Sign.",
            "The campus of the Getty Museum and other programs of the Getty Trust.",
            "Universal Studios Hollywood is a film studio and theme park.",
            "�Hollywood� is famously spelled out in 45-foot-high letters on Mount Lee.",
            "The Huntington exhibits rare American and European books and art and promotes research.",
            "The Broad is a contemporary art museum on Grand Avenue in L.A. ",

      };
        readonly string[] Place7 = new string[]
      {
            "Museum of Fine Arts, Boston",
            "Isabella Stewart Gardner Museum",
            "Museum of Science",
            "Fenway Park",
            "Faneuil Hall Marketplace",
            "Boston Children's Museum",
      };

        readonly string[] Place7Description = new string[]
      {
            "It is the 20th largest art museum in the world, measured by the public gallery area.",
            "The Isabella Stewart Gardner Museum is an art museum in Boston.",
            "The Museum of Science contains 700 exhibits and an indoor zoo in Boston.",
            "Fenway Park is a baseball stadium in Boston, home of the Red Sox team. ",
            "Faneuil Hall is a marketplace and meeting hall from 1742 located near the waterfront.",
            "Boston Children's Museum is the second oldest children's museum in the U.S.",

      };
        readonly string[] Place8 = new string[]
      {
            "Space Needle",
            "Museum of Pop Culture",
            "Chihuly Garden and Glass",
            "Pike Place Market",
            "Seattle Art Museum",
            "Discovery Park",
      };

        readonly string[] Place8Description = new string[]
      {
            "The Space Needle is an observation tower and an icon of Seattle.",
            "MoPOP celebrates the music, films, video games, and fiction writing.",
            "The Chihuly Garden and Glass is an exhibit of glass art in the Seattle Center.",
            "Pike Place Market is a famous public market where Starbucks originated.",
            "The Seattle Art Museum�s collections include African, American, and decorative arts.",
            "At 534 acres, Discovery Park is Seattle�s largest public park.",
      };
        readonly string[] Place9 = new string[]
        {
            "Wynwood Walls",
            "Miami Seaquarium",
            "Fairchild Tropical Botanic Garden",
            "Phillip and Patricia Frost Museum of Science",
      };

        readonly string[] Place9Description = new string[]
      {
            "An outdoor museum with creative street art.",
            "The Miami Seaquarium is a 38-acre oceanarium. It was founded in 1955.",
            "An 83-acre botanic garden known for collections of rare tropical plants.",
            "Interactive exhibits, classes, and shows.",

      };
        readonly string[] Place10 = new string[]
        {
            "Bellagio Hotel & Casino",
            "MGM Grand",
            "Las Vegas Strip",
            "Red Rock Canyon National Conservation Area",
            "The Mirage",
            "The STRAT Hotel, Casino & SkyPod",
            "The Neon Museum",
            "New York-New York Hotel & Casino",

      };

        readonly string[] Place10Description = new string[]
      {
            "The Bellagio Resort and Casino is a hotel on the Las Vegas Strip with an Italian theme.",
            "The MGM Grand is a hotel and casino located on the Las Vegas Strip.",
            "The Las Vegas Strip is the part of Las Vegas Boulevard South lined with famous casinos.",
            "This conservation area is popular for hiking, climbing, and bicycling.",
            "The Mirage is a casino resort on the Las Vegas Strip.",
            "The Strat Tower is a casino in Las Vegas with the tallest observation tower in the U.S.",
            "The Neon Museum features signs from old casinos and other businesses.",
            "The New York-New York is a hotel and casino on the Las Vegas Strip.",
      };
        readonly string[] Place11 = new string[]
      {
            "Balboa Park",
            "SeaWorld San Diego",
            "USS Midway Museum",
            "Cabrillo National Monument",
            "La Jolla Cove",

      };

        readonly string[] Place11Description = new string[]
      {
            "Balboa Park is a 1,200-acre park with theaters, museums, a zoo, and outdoor space.",
            "SeaWorld San Diego is a marine theme park.",
            "The USS Midway Museum is a historical naval aircraft carrier museum.",
            "Cabrillo National Monument commemorates where Europeans first landed on the west coast of the U.S.",
            "La Jolla Cove is a small cove with a beach that is surrounded by cliffs.",

      };
        readonly string[] Place12 = new string[]
      {
            "Georgia Aquarium",
            "Zoo Atlanta",
            "Atlanta Botanical Garden",
            "Piedmont Park",

      };

        readonly string[] Place12Description = new string[]
      {
            "The Georgia aquarium is the largest in the States and is an aquatic education institution.",
            "Zoo Atlanta began in 1889 with the donation of former circus animals.",
            "The Atlanta Botanical Garden is a 30 acres botanical garden.",
            "Piedmont Park is an expanding urban park with playgrounds and athletic facilities.",

      };
        readonly string[] Place13 = new string[]
      {
            "The National WWII Museum",
            "Jackson Square",
            "Bourbon Street",
            "New Orleans City Park",

      };

        readonly string[] Place13Description = new string[]
      {
            "The National WWII Museum is a military history museum.",
            "Jackson Square is a historic park in the French Quarter of New Orleans.",
            "Bourbon Street is party central for the French Quarter, lined with bars and clubs.",
            "City Park is a 1,300-acre public park in northern New Orleans with a botanic garden.",

      };
        readonly string[] Place14 = new string[]
      {
            "Walt Disney World� Resort",
            "Magic Kingdom Park",
            "Epcot",
            "Disney's Hollywood Studios",
            "Disney's Animal Kingdom Theme Park",
            "Universal Studios Florida",
      };

        readonly string[] Place14Description = new string[]
      {
            "The Walt Disney World Resort, is an entertainment resort complex.",
            "The Magic Kingdom is a Disney theme park designed for families with small children.",
            "Epcot is a Disney theme park with an international theme.",
            "Disney's Hollywood Studios is a theme park based on film, TV, and music.",
            "Disney's Animal Kingdom centers around a zoological theme.",
            "Universal Studios Florida is a theme park dedicated to the entertainment industry.",

      };
        readonly string[] Place15 = new string[]
      {
            "Denver Botanic Gardens",
            "Elitch Gardens",
            "Coors Field",
            "Colorado State Capitol",
      };

        readonly string[] Place15Description = new string[]
      {
            "The 23-acre park contains a conservatory and many themed gardens.",
            "Elitch Gardens Theme and Water Park is an amusement park in Denver.",
            "Coors Field is a baseball stadium in downtown Denver.",
            "The Colorado State Capitol Building houses the state�s General Assembly.",
      };

        public readonly string[] Place = new string[]
       {
            "Central Park",
            "The Metropolitan Museum of Art",
            "Statue of Liberty",
            "Empire State Building",
            "Solomon R. Guggenheim",
            "Times Square",
            "Brooklyn Bridge",
            "American Museum of Natural History",
             "Golden Gate Bridge",
            "Alcatraz Island",
            "San Francisco Museum of Modern",
            "Golden Gate Park",
            "California Academy of Sciences",
            "Lombard Street",
            "Muir Woods National Monument",
             "The Art Institute of Chicago",
            "Field Museum",
            "Navy Pier",
            "Millennium Park",
            "Shedd Aquarium",
            "Lincoln Park Zoo",
            "Cloud Gate",
            "Willis Tower",
             "Yosemite Falls",
            "Half Dome",
            "Glacier Point",
            "El Capitan",
            "Yosemite Valley",
            "Tunnel view",
            "Bridalveil Fall",
            "The Ahwahnee",
            "Tuolumne Meadows",
            "The White House",
            "United States Capitol",
            "Smithsonian Institution",
            "Lincoln Memorial",
            "National Gallery of Art",
            "Smithsonian National Museum of Natural History",
            "Washington Monument",
            "Smithsonian National Air and Space Museum",
            "Los Angeles County Museum of Art",
            "Disneyland Park",
            "Santa Monica Pier",
            "Griffith Observatory",
            "The Getty",
            "Universal Studios Hollywood",
            "Hollywood Sign",
            "The Huntington Library, Art Museum, and Botanical Gardens",
            "The Broad",
             "Museum of Fine Arts, Boston",
            "Isabella Stewart Gardner Museum",
            "Museum of Science",
            "Fenway Park",
            "Faneuil Hall Marketplace",
            "Boston Children's Museum",
             "Space Needle",
            "Museum of Pop Culture",
            "Chihuly Garden and Glass",
            "Pike Place Market",
            "Seattle Art Museum",
            "Discovery Park",
            "Wynwood Walls",
            "Miami Seaquarium",
            "Fairchild Tropical Botanic Garden",
            "Phillip and Patricia Frost Museum of Science",
             "Bellagio Hotel & Casino",
            "MGM Grand",
            "Las Vegas Strip",
            "Red Rock Canyon National Conservation Area",
            "The Mirage",
            "The STRAT Hotel, Casino & SkyPod",
            "The Neon Museum",
            "New York-New York Hotel & Casino",
             "Balboa Park",
            "SeaWorld San Diego",
            "USS Midway Museum",
            "Cabrillo National Monument",
            "La Jolla Cove",
             "Georgia Aquarium",
            "Zoo Atlanta",
            "Atlanta Botanical Garden",
            "Piedmont Park",
            "The National WWII Museum",
            "Jackson Square",
            "Bourbon Street",
            "New Orleans City Park",
             "Walt Disney World� Resort",
            "Magic Kingdom Park",
            "Epcot",
            "Disney's Hollywood Studios",
            "Disney's Animal Kingdom Theme Park",
            "Universal Studios Florida",
            "Denver Botanic Gardens",
            "Elitch Gardens",
            "Coors Field",
            "Colorado State Capitol",
       };

        public readonly string[] PlaceDescription = new string[]
      {
            "Central Park is an urban park and the fifth largest park in New York City.",
            "The Metropolitan Museum is the largest art museum in the western hemisphere.",
            "The Statue of Liberty is a colossal neoclassical sculpture on Liberty Island.",
            "The Empire State Building is a 102-story art deco skyscraper in mid-Manhattan.",
            "The Solomon R. Guggenheim Museum is an art museum in Manhattan.",
            "Times Square is a tourist destination in mid-Manhattan.",
            "The Brooklyn Bridge is a hybrid cable-stayed bridge in New York City.",
            "The American Museum of Natural History  sits in the Upper West Side of Manhattan.",
            "This bridge crosses the one-mile-wide strait between San Francisco Bay and the Pacific Ocean.",
            "Alcatraz is a former prison on a small island in the San Francisco Bay.",
            "A modern and contemporary art museum in San Francisco.",
            "The park is a large urban park consisting of 1,017 acres of public grounds.",
            "The California Academy of Sciences is a research institute and natural history museum.",
            "Lombard St. is famous for a steep one-block section with eight hairpin turns.",
            "Muir Woods protects an old-growth redwood forest on Mount Tamalpais.",
             "The Art Institute of Chicago is the second-largest art museum in the USA.",
            "The Field Museum is one of the largest natural history museums in the world. ",
            "Navy Pier is a 3,300-foot-long pier and tourist attraction on the shore of Lake Michigan.",
            "Millennium Park is a public park located in the Loop community area of Chicago.",
            "Shedd Aquarium is an indoor public aquarium that opened in 1930.",
            "The Lincoln Park Zoo is a 35-acre zoo opened in 1868 in Chicago.",
            "The Cloud Gate is a public sculpture by Indian-born British artist Anish Kapoor.",
            "At 108 stories, the Willis Tower was the world�s tallest skyscraper for 25 years.",
            "Yosemite Falls is the highest waterfall in Yosemite National Park.",
            "The Half Dome is a granite dome at the eastern end of Yosemite Valley.",
            "Glacier Point is a viewpoint above Yosemite Valley.",
            "The El Capitan is a vertical rock formation in Yosemite National Park popular with climbers.",
            "The Yosemite Valley is a glacial valley in Yosemite National Park.",
            "The Tunnel View is a scenic viewpoint on California State Route 41.",
            "Fed by Ostrander Lake, Bridalveil Fall is one of the tallest waterfalls in Yosemite Valley.",
            "The Ahwahnee Hotel is a grand hotel in Yosemite National Park.",
            "The Tuolumne Meadows is a gentle, dome- studded, sub-alpine meadow area.",
            "The White House is the official residence of the U.S. president in Washington, D.C.",
            "The Capitol building is the seat of the U.S. Congress.",
            "The Smithsonian is the largest group of museums, education and research centers in the world.",
            "The Lincoln Memorial was built to honor the 16th president of the U.S., Abraham Lincoln.",
            "The National Gallery of Art collects and exhibits Western art in Washington, D.C.",
            "Founded in 1910, this Smithsonian branch contains the largest natural history collection in the world.",
            "The Washington Monument is a 169-meter tall obelisk in the National Mall.",
            "The Air and Space Museum displays pieces like the first airplane and the command module of Apollo 11.",
            "A visual arts museum that offers a collection of paintings, photography, etc.",
            "Disneyland is a theme park in Anaheim, California, which opened in 1955.",
            "The Santa Monica Pier is a large, double-jointed pier with an amusement park.",
            "A popular tourist attraction with a close view of the Hollywood Sign.",
            "The campus of the Getty Museum and other programs of the Getty Trust.",
            "Universal Studios Hollywood is a film studio and theme park.",
            "�Hollywood� is famously spelled out in 45-foot-high letters on Mount Lee.",
            "The Huntington exhibits rare American and European books and art and promotes research.",
            "The Broad is a contemporary art museum on Grand Avenue in L.A. ",
             "It is the 20th largest art museum in the world, measured by the public gallery area.",
            "The Isabella Stewart Gardner Museum is an art museum in Boston.",
            "The Museum of Science contains 700 exhibits and an indoor zoo in Boston.",
            "Fenway Park is a baseball stadium in Boston, home of the Red Sox team. ",
            "Faneuil Hall is a marketplace and meeting hall from 1742 located near the waterfront.",
            "Boston Children's Museum is the second oldest children's museum in the U.S.",
             "The Space Needle is an observation tower and an icon of Seattle.",
            "MoPOP celebrates the music, films, video games, and fiction writing.",
            "The Chihuly Garden and Glass is an exhibit of glass art in the Seattle Center.",
            "Pike Place Market is a famous public market where Starbucks originated.",
            "The Seattle Art Museum�s collections include African, American, and decorative arts.",
            "At 534 acres, Discovery Park is Seattle�s largest public park.",
            "An outdoor museum with creative street art.",
            "The Miami Seaquarium is a 38-acre oceanarium. It was founded in 1955.",
            "An 83-acre botanic garden known for collections of rare tropical plants.",
            "Interactive exhibits, classes, and shows.",
            "The Bellagio Resort and Casino is a hotel on the Las Vegas Strip with an Italian theme.",
            "The MGM Grand is a hotel and casino located on the Las Vegas Strip.",
            "The Las Vegas Strip is the part of Las Vegas Boulevard South lined with famous casinos.",
            "This conservation area is popular for hiking, climbing, and bicycling.",
            "The Mirage is a casino resort on the Las Vegas Strip.",
            "The Strat Tower is a casino in Las Vegas with the tallest observation tower in the U.S.",
            "The Neon Museum features signs from old casinos and other businesses.",
            "The New York-New York is a hotel and casino on the Las Vegas Strip.",
             "Balboa Park is a 1,200-acre park with theaters, museums, a zoo, and outdoor space.",
            "SeaWorld San Diego is a marine theme park.",
            "The USS Midway Museum is a historical naval aircraft carrier museum.",
            "Cabrillo National Monument commemorates where Europeans first landed on the west coast of the U.S.",
            "La Jolla Cove is a small cove with a beach that is surrounded by cliffs.",
            "The Georgia aquarium is the largest in the States and is an aquatic education institution.",
            "Zoo Atlanta began in 1889 with the donation of former circus animals.",
            "The Atlanta Botanical Garden is a 30 acres botanical garden.",
            "Piedmont Park is an expanding urban park with playgrounds and athletic facilities.",
             "The National WWII Museum is a military history museum.",
            "Jackson Square is a historic park in the French Quarter of New Orleans.",
            "Bourbon Street is party central for the French Quarter, lined with bars and clubs.",
            "City Park is a 1,300-acre public park in northern New Orleans with a botanic garden.",
            "The Walt Disney World Resort, is an entertainment resort complex.",
            "The Magic Kingdom is a Disney theme park designed for families with small children.",
            "Epcot is a Disney theme park with an international theme.",
            "Disney's Hollywood Studios is a theme park based on film, TV, and music.",
            "Disney's Animal Kingdom centers around a zoological theme.",
            "Universal Studios Florida is a theme park dedicated to the entertainment industry.",
             "The 23-acre park contains a conservatory and many themed gardens.",
            "Elitch Gardens Theme and Water Park is an amusement park in Denver.",
            "Coors Field is a baseball stadium in downtown Denver.",
            "The Colorado State Capitol Building houses the state�s General Assembly.",
      };

        public readonly string[] Image = new string[]
        {
            "place11.jpg",
            "place12.jpg",
            "place13.jpg",
            "place14.jpg",
            "place15.jpg",
            "place16.jpg",
            "place17.jpg",
            "place18.jpg",
            "place21.jpg",
            "place22.jpg",
            "place23.jpg",
            "place24.jpg",
            "place25.jpg",
            "place26.jpg",
            "place27.jpg",
            "place31.jpg",
            "place32.jpg",
            "place33.jpg",
            "place34.jpg",
            "place35.jpg",
            "place36.jpg",
            "place37.jpg",
            "place38.jpg",
            "place41.jpg",
            "place42.jpg",
            "place43.jpg",
            "place44.jpg",
            "place45.jpg",
            "place46.jpg",
            "place47.jpg",
            "place48.jpg",
            "place49.jpg",
            "place51.jpg",
            "place52.jpg",
            "place53.jpg",
            "place54.jpg",
            "place55.jpg",
            "place56.jpg",
            "place57.jpg",
            "place58.jpg",
            "place61.jpg",
            "place62.jpg",
            "place63.jpg",
            "place64.jpg",
            "place65.jpg",
            "place66.jpg",
            "place67.jpg",
            "place68.jpg",
            "place69.jpg",
            "place71.jpg",
            "place72.jpg",
            "place73.jpg",
            "place74.jpg",
            "place75.jpg",
            "place76.jpg",
            "place81.jpg",
            "place82.jpg",
            "place83.jpg",
            "place84.jpg",
            "place85.jpg",
            "place86.jpg",
            "place91.jpg",
            "place92.jpg",
            "place93.jpg",
            "place94.jpg",
            "place101.jpg",
            "place102.jpg",
            "place103.jpg",
            "place104.jpg",
            "place105.jpg",
            "place106.jpg",
            "place107.jpg",
            "place108.jpg",
            "place111.jpg",
            "place112.jpg",
            "place113.jpg",
            "place114.jpg",
            "place115.jpg",
            "place121.jpg",
            "place122.jpg",
            "place123.jpg",
            "place124.jpg",
            "place131.jpg",
            "place132.jpg",
            "place133.jpg",
            "place134.jpg",
            "place141.jpg",
            "place142.jpg",
            "place143.jpg",
            "place144.jpg",
            "place145.jpg",
            "place146.jpg",
            "place151.jpg",
            "place152.jpg",
            "place153.jpg",
            "place154.jpg",
        };
       
        #endregion
    }

    #endregion
}
