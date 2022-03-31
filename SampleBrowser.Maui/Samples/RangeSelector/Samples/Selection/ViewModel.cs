#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.RangeSelector
{
    public class SelectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private DateTime rangeStart = new(2019, 04, 06);
        private DateTime rangeEnd = new(2019, 04, 15);
        private ObservableCollection<Brush> brushes = new();
        private double totalDataUsage;

        public ObservableCollection<ChartDataModel> Items { get; set; }

        public ObservableCollection<Brush> Brushes
        {
            get { return brushes; }
            set
            {
                brushes = value;
                OnPropertyChanged(nameof(Brushes));
            }
        }

        public DateTime RangeStart
        {
            get { return rangeStart; }
            set
            {
                if (rangeStart != value)
                {
                    rangeStart = value;
                    UpdatePaletteBrushes();
                    OnPropertyChanged(nameof(RangeStart));
                }
            }
        }

        public DateTime RangeEnd
        {
            get { return rangeEnd; }
            set
            {
                if (rangeEnd != value)
                {
                    rangeEnd = value;
                    UpdatePaletteBrushes();
                    OnPropertyChanged(nameof(RangeEnd));
                }
            }
        }

        public double TotalDataUsage
        {
            get { return totalDataUsage; }
            set
            {
                if (totalDataUsage != value)
                {
                    totalDataUsage = value;
                    OnPropertyChanged(nameof(TotalDataUsage));
                }
            }
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void UpdatePaletteBrushes()
        {
            ObservableCollection<Brush> PaletteBrushes = new();

            foreach (ChartDataModel item in Items)
            {
                if (item.Date.ToOADate() >= rangeStart.ToOADate()
                    && item.Date.ToOADate() <= rangeEnd.Date.ToOADate())
                {
                    PaletteBrushes.Add(new SolidColorBrush(Color.FromArgb("#00B2CE")));
                }
                else
                {
                    PaletteBrushes.Add(Brush.Transparent);
                }
            }

            Brushes = PaletteBrushes;
            UpdateTotalDataUsage();
        }

        private void UpdateTotalDataUsage()
        {
            double total = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Date > rangeStart.Subtract(new TimeSpan(1, 00, 00)) &&
                    Items[i].Date < rangeEnd.Add(new TimeSpan(1, 00, 00)))
                {
                    total += Items[i].Value;
                }
            }

            TotalDataUsage = total;
        }

        public SelectionViewModel()
        {
            Items = new ObservableCollection<ChartDataModel>()
            {
                new ChartDataModel( new DateTime(2019, 04, 01), 0.2),
                new ChartDataModel( new DateTime(2019, 04, 02), 0.3),
                new ChartDataModel( new DateTime(2019, 04, 03), 0.4),
                new ChartDataModel( new DateTime(2019, 04, 04), 0.6),
                new ChartDataModel( new DateTime(2019, 04, 05), 0.8),
                new ChartDataModel( new DateTime(2019, 04, 06), 1.2),
                new ChartDataModel( new DateTime(2019, 04, 07), 1.6),
                new ChartDataModel( new DateTime(2019, 04, 08), 2.4),
                new ChartDataModel( new DateTime(2019, 04, 09), 3.2),
                new ChartDataModel( new DateTime(2019, 04, 10), 4.8),
                new ChartDataModel( new DateTime(2019, 04, 11), 6.4),
                new ChartDataModel( new DateTime(2019, 04, 12), 9.6),
                new ChartDataModel( new DateTime(2019, 04, 13), 12.8),
                new ChartDataModel( new DateTime(2019, 04, 14), 16.0),
                new ChartDataModel( new DateTime(2019, 04, 15), 22.0),
                new ChartDataModel( new DateTime(2019, 04, 16), 25.6),
                new ChartDataModel( new DateTime(2019, 04, 17), 20.0),
                new ChartDataModel( new DateTime(2019, 04, 18), 14.5),
                new ChartDataModel( new DateTime(2019, 04, 19), 12.8),
                new ChartDataModel( new DateTime(2019, 04, 20), 10.0),
                new ChartDataModel( new DateTime(2019, 04, 21), 6.6),
                new ChartDataModel( new DateTime(2019, 04, 22), 5.0),
                new ChartDataModel( new DateTime(2019, 04, 23), 3.2),
                new ChartDataModel( new DateTime(2019, 04, 24), 3.2),
                new ChartDataModel( new DateTime(2019, 04, 25), 1.6),
                new ChartDataModel( new DateTime(2019, 04, 26), 1.6),
                new ChartDataModel( new DateTime(2019, 04, 27), 0.8),
                new ChartDataModel( new DateTime(2019, 04, 28), 0.8),
                new ChartDataModel( new DateTime(2019, 04, 29), 0.4),
                new ChartDataModel( new DateTime(2019, 04, 30), 0.2)
            };

            UpdatePaletteBrushes();
        }
    }

    public class ChartDataModel
    {
        public DateTime Date { get; set; }

        public double Value { get; set; }

        public ChartDataModel(DateTime date, double value)
        {
            Date = date;
            Value = value;
        }
    }
}



