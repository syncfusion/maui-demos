#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Maps;

public class SelectionViewModel
{
    public ObservableCollection<PopulationDensityDetails> StateWiseElectionResult { get; set; }
    public SelectionViewModel()
    {
        StateWiseElectionResult = new ObservableCollection<PopulationDensityDetails>() {
      new PopulationDensityDetails(
          "Washington",
          "DC",
          22,
          116,
          44.8),
      new PopulationDensityDetails(
          "Oregon",
          "OR",
          39,
          44.1,
          17.0),
      new PopulationDensityDetails(
          "Alabama",
          "AL",
          27,
          99.2,
          38.3),
      new PopulationDensityDetails(
          "Alaska",
          "AK",
          50,
          1.3,
          0.5),
      new PopulationDensityDetails(
          "Arizona",
          "AZ",
          33,
          63.0,
          24.3),
      new PopulationDensityDetails(
          "Arkansas",
          "AR",
          34,
          57.9,
          22.3),
      new PopulationDensityDetails(
          "California",
          "CA",
          11,
          254,
          98.0),
      new PopulationDensityDetails(
          "Colorado",
          "CO",
          37,
          54.3,
          21.5),
      new PopulationDensityDetails(
          "Connecticut",
          "CT",
          4,
          745,
          288),
      new PopulationDensityDetails(
          "Delaware",
          "DE",
          6,
          508,
          196),
      new PopulationDensityDetails(
          "Florida",
          "FL",
          8,
          402,
          155),
      new PopulationDensityDetails(
          "Georgia",
          "GA",
          17,
          186,
          71.9),
      new PopulationDensityDetails(
          "Hawaii",
          "HI",
          13,
          227,
          87.5),
      new PopulationDensityDetails(
          "Idaho",
          "ID",
          44,
          22.3,
          8.6),
      new PopulationDensityDetails(
          "Illinois",
          "IL",
          12,
          231,
          89.1),
      new PopulationDensityDetails(
          "Indiana",
          "IN",
          16,
          189,
          73.1),
      new PopulationDensityDetails(
          "Iowa",
          "IA",
          36,
          57.1,
          22.1),
      new PopulationDensityDetails(
          "Kansas",
          "KS",
          41,
          35.9,
          13.9),
      new PopulationDensityDetails(
          "Kentucky",
          "KY",
          23,
          114,
          44.1),
      new PopulationDensityDetails(
          "Louisiana",
          "LA",
          26,
          108,
          41.6),
      new PopulationDensityDetails(
          "Maine",
          "ME",
          38,
          44.2,
          17.1),
      new PopulationDensityDetails(
          "Maryland",
          "MD",
          5,
          636,
          246),
      new PopulationDensityDetails(
          "Massachusetts",
          "MA",
          3,
          901,
          348),
      new PopulationDensityDetails(
          "Michigan",
          "MI",
          18,
          178,
          68.8),
      new PopulationDensityDetails(
          "Minnesota",
          "MN",
          30,
          71.7,
          27.7),
      new PopulationDensityDetails(
          "Mississippi",
          "MS",
          32,
          63.1,
          24.4),
      new PopulationDensityDetails(
          "Missouri",
          "MO",
          28,
          89.5,
          34.6),
      new PopulationDensityDetails(
          "Montana",
          "MT",
          48,
          7.5,
          2.9),
      new PopulationDensityDetails(
          "Nebraska",
          "NE",
          43,
          25.5,
          9.9),
      new PopulationDensityDetails(
          "Nevada",
          "NV",
          42,
          28.3,
          10.9),
      new PopulationDensityDetails(
          "New Hampshire",
          "NH",
          21,
          154,
          59.4),
      new PopulationDensityDetails(
          "New Jersey",
          "NJ",
          1,
          1263,
          488),
      new PopulationDensityDetails(
          "New Mexico",
          "NM",
          45,
          17.5,
          6.7 ),
      new PopulationDensityDetails(
          "New York",
          "NY",
          7,
          429,
          166),
      new PopulationDensityDetails(
          "North Carolina",
          "NC",
          15,
          215,
          82.9),
      new PopulationDensityDetails(
          "North Dakota",
          "ND",
          47,
          11.3,
          4.4),
      new PopulationDensityDetails(
          "Ohio",
          "OH",
          10,
          289,
          111),
      new PopulationDensityDetails(
          "Oklahoma",
          "OK",
          35,
          57.7,
          22.3),
      new PopulationDensityDetails(
          "Pennsylvania",
          "PA",
          9,
          291,
          112),
      new PopulationDensityDetails(
          "Rhode Island",
          "RI",
          2,
          1061,
          410),
      new PopulationDensityDetails(
          "South Carolina",
          "SC",
          19,
          170,
          65.7),
      new PopulationDensityDetails(
          "South Dakota",
          "SD",
          46,
          11.7,
          4.5),
      new PopulationDensityDetails(
          "Tennessee",
          "TN",
          20,
          168,
          64.7),
      new PopulationDensityDetails(
          "Texas",
          "TX",
          24,
          112,
          43.1),
      new PopulationDensityDetails(
          "Utah",
          "UT",
          40,
          39.8,
          15.4),
      new PopulationDensityDetails(
          "Vermont",
          "VT",
          31,
          69.8,
          26.9),
      new PopulationDensityDetails(
          "Virginia",
          "VA",
          14,
          219,
          84.4),
      new PopulationDensityDetails(
          "West Virginia",
          "WV",
          29,
          74.6,
          28.8),
      new PopulationDensityDetails(
          "Wisconsin",
          "WI",
          25,
          109,
          42.0),
      new PopulationDensityDetails(
          "Wyoming",
          "WY",
          49,
          5.9,
          2.3),
        };
    }
}