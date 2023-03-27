#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.Sliders.SfRangeSelector
{
    public class RangeSelectorDefaultSampleViewModel : INotifyPropertyChanged
    {
        private DateTime rangeStart = new(2005, 01, 01);
        private DateTime rangeEnd = new(2008, 01, 01);
        private double averageInflationRate;

        public event PropertyChangedEventHandler? PropertyChanged;

        public RangeSelectorDefaultSampleViewModel()
        {
            Source = new ObservableCollection<Model>
            {
                new Model(new DateTime(2002, 01, 01), 2.2),
                new Model(new DateTime(2003, 01, 01), 3.4),
                new Model(new DateTime(2004, 01, 01), 2.8),
                new Model(new DateTime(2005, 01, 01), 1.6),
                new Model(new DateTime(2006, 01, 01), 2.3),
                new Model(new DateTime(2007, 01, 01), 2.5),
                new Model(new DateTime(2008, 01, 01), 2.9),
                new Model(new DateTime(2009, 01, 01), 3.8),
                new Model(new DateTime(2010, 01, 01), 1.4),
                new Model(new DateTime(2011, 01, 01), 3.1),
            };

            UpdateAverageInflationRate();
        }

        public ObservableCollection<Model> Source { get; set; }

        public DateTime RangeStart
        {
            get { return rangeStart; }
            set
            {
                if (rangeStart != value)
                {
                    rangeStart = value;
                    UpdateAverageInflationRate();
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
                    UpdateAverageInflationRate();
                    OnPropertyChanged(nameof(RangeEnd));
                }
            }
        }

        public double AverageInflationRate
        {
            get { return averageInflationRate; }
            set
            {
                if (averageInflationRate != value)
                {
                    averageInflationRate = value;
                    OnPropertyChanged(nameof(AverageInflationRate));
                }
            }
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal virtual void UpdateAverageInflationRate()
        {
            double totalInflationRate = 0;
            int dataCount = 0;
            double startRate = 1.6;
            for (int i = 0; i < Source.Count; i++)
            {
                if (Source[i].X.Year == rangeStart.Year)
                {
                    startRate = Source[i].Y;
                }

                if (Source[i].X > rangeStart.Subtract(new TimeSpan(12, 00, 00)) &&
                    Source[i].X < rangeEnd.Add(new TimeSpan(12, 00, 00)))
                {
                    dataCount++;
                    totalInflationRate += Source[i].Y;
                }
            }

            AverageInflationRate = dataCount != 0 ? totalInflationRate / dataCount : startRate;
        }
    }

    public class Model
    {
        public Model(DateTime date, double value)
        {
            X = date;
            Y = value;
        }

        public DateTime X { get; set; }

        public double Y { get; set; }
    }
}
