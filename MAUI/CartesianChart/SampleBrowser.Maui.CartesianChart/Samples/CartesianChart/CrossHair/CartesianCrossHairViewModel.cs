#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public class CartesianCrossHairViewModel : BaseViewModel
    {
        public ObservableCollection<ChartDataModel> StockPrices { get; private set; } = new ObservableCollection<ChartDataModel>();
        public ObservableCollection<ChartDataModel> VolumeUp { get; private set; } = new ObservableCollection<ChartDataModel>();
        public ObservableCollection<ChartDataModel> VolumeDown { get; private set; } = new ObservableCollection<ChartDataModel>();
        public DateTime MinDate { get; private set; }
        public DateTime MaxDate { get; private set; }
        private const string EmbeddedCsvResource = "SampleBrowser.Maui.CartesianChart.Resources.Raw.amazon.csv";

        public CartesianCrossHairViewModel()
        {
            var data = LoadCsvFromResource(EmbeddedCsvResource);

            var ordered = data
                .GroupBy(d => d.Date.Date)
                .Select(g => g.First())
                .OrderBy(d => d.Date)
                .ToList();

            StockPrices = new ObservableCollection<ChartDataModel>(ordered);
            var up = new List<ChartDataModel>(ordered.Count);
            var down = new List<ChartDataModel>(ordered.Count);
            foreach (var it in ordered)
            {
                bool isUp = it.Size >= it.Value;
                var upVol = isUp ? it.Volume : 0d;
                var dnVol = isUp ? 0d : it.Volume;

                up.Add(new ChartDataModel(it.Date, it.Value, it.High, it.Low, it.Size, upVol));
                down.Add(new ChartDataModel(it.Date, it.Value, it.High, it.Low, it.Size, dnVol));
            }

            VolumeUp = new ObservableCollection<ChartDataModel>(up);
            VolumeDown = new ObservableCollection<ChartDataModel>(down);

            if (ordered.Count > 0)
            {
                MinDate = ordered.First().Date.Date;
                MaxDate = ordered.Last().Date.Date;
            }
        }


        private static List<ChartDataModel> LoadCsvFromResource(string resourceName)
        {
            var items = new List<ChartDataModel>();
            var assembly = typeof(CartesianCrossHairViewModel).GetTypeInfo().Assembly;

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null) return items;

            using var reader = new StreamReader(stream);
            string? line;
            var formats = new[] { "M/d/yyyy", "MM/dd/yyyy"};

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(',');
                if (parts.Length < 6) continue;
                if (parts[0].Trim().Equals("date", StringComparison.OrdinalIgnoreCase)) continue;
                if (!DateTime.TryParseExact(parts[0].Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date)) continue;

#if ANDROID || IOS
                if (!(date.Month >= 10 && date.Month <= 12))
                    continue;
#endif

                if (!double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var low)) continue;
                if (!double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var high)) continue;
                if (!double.TryParse(parts[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var open)) continue;
                if (!double.TryParse(parts[4], NumberStyles.Any, CultureInfo.InvariantCulture, out var close)) continue;
                if (!double.TryParse(parts[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var vol)) continue;
                items.Add(new ChartDataModel(date.Date, open, high, low, close, vol / 150_000d));
            }

            return items;
        }
    }
}
