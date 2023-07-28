#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.DataGrid;

public class SalesInfoRepository : ContentPage
{
    private readonly List<string> salesParsonNames = new List<string>()
            {
                "Gary",
                "Maciej",
                "Shelley",
                "Linda",
                "Carla",
                "Carol",
                "Shannon",
                "Jauna",
                "Michael",
                "Terry",
                "John",
                "Gail",
                "Mark",
                "Martha",
                "Julie",
                "Janeth",
                "Twanna",
                  "Brenda",
                "Danielle",
                "Fiona",
                "Howard",
                "Jack",
                "Larry",
                "Holly",
                "Jennifer",
                "Liz",
                "Pete",
            "Steve",
            "Vince",
            "Zeke",
             "Holmes",
            "Jefferson",
            "Landry",
            "Newberry",
            "Perez",
            "Spencer",
            "Vargas",
            "Grimes",
            "Edwards",
            "Stark",
            "Cruise",
            "Fitz",
            "Chief",
            "Blanc",
            "Perry",
            "Stone",
            "Williams",
            "Lane",
            "Jobs"
            };

    /// <summary>
    /// Generates days with given count
    /// </summary>
    /// <param name="days">generates row count</param>
    /// <returns>SalesByDate collection values</returns>
    public ObservableCollection<SalesInfo> GetSalesDetailsByDay(int days)
    {
        var collection = new ObservableCollection<SalesInfo>();
        var r = new Random();
        for (var i = 0; i < days; i++)
        {
            var dt = DateTime.Now;
            var s = new SalesInfo
            {
                Name = (i > 48) ? this.salesParsonNames[r.Next(20)] : this.salesParsonNames[i],
                QS1 = i,
                QS2 = ((i < 30 ? 100 : 900 - i) * (i + 1)) + r.NextDouble(),
                QS3 = r.Next(20, 50),
                QS4 = r.Next(40, 75),
            };
            s.Total = s.QS1 + s.QS2 + s.QS3 + s.QS4;
            s.Date = dt.AddDays(-1 * i);
            collection.Add(s);
        }

        return collection;
    }
}