#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.BusyIndicator.SfBusyIndicator
{
    public class ColumnSeriesViewModel : INotifyPropertyChanged
    {
        public List<Stock> data = new List<Stock>();

        public List<Stock> Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
                NotifyPropertyChanged();
            }
        }

        private bool isRunning;

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
                NotifyPropertyChanged();
            }
        }

        private bool canAutoUpdate = false;

        public bool CanAutoUpdate
        {
            get
            {
                return canAutoUpdate;
            }
            set
            {
                canAutoUpdate = value;
                if(!canAutoUpdate)
                    CaptionText = "Auto update disabled";
                NotifyPropertyChanged();
            }
        }

        private string? captionText;

        public string? CaptionText
        {
            get
            {
                return captionText;
            }
            set
            {
                captionText = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Brush> PaletteBrushes { get; set; }

        Random random = new Random();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ColumnSeriesViewModel()
        {
            LoadData();

            PaletteBrushes = new ObservableCollection<Brush>()
            {
                 new SolidColorBrush(Color.FromArgb("#512BD4")),
                 new SolidColorBrush(Color.FromArgb("#5E498C")),
                 new SolidColorBrush(Color.FromArgb("#74BD6F")),
                 new SolidColorBrush(Color.FromArgb("#597FCA"))
            };
        }


        

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void LoadData()
        {
            CaptionText = "Fetching Data";
            this.IsRunning = true;
            await Task.Delay(5000);
            Data = new List<Stock>()
        {
            new Stock { Name = "Stock 1", Value = 170 },
            new Stock { Name = "Stock 2", Value = 96 },
            new Stock { Name = "Stock 3", Value = 65 },
            new Stock { Name = "Stock 4", Value = 182 },
            new Stock { Name = "Stock 5", Value = 134 }
        };
            this.IsRunning = false;
            this.CanAutoUpdate = true;
            RefreshData();
        }

        private  async void RefreshData()
        {
            while (true)
            {
                if(!this.CanAutoUpdate)
                {
                    await Task.Delay(1000);
                    continue;
                }

                int waitTime = 3;
                for (int i = 0; i < waitTime; i++)
                {
                    CaptionText = "Auto update in " +(waitTime - i)+ " Second";
                    if ((waitTime - i) > 1)
                        CaptionText += "s";
                    await Task.Delay(1000);
                    if (!canAutoUpdate)
                    {
                        break;
                    }
                }
                if (CaptionText == "Auto update disabled")
                    continue;

                CaptionText = "Fetching Data";
                this.IsRunning = true;
                await Task.Delay(3000);
                Data = new List<Stock>()
                 {
                      new Stock { Name = "Stock 1", Value =random.Next(55,300) },
                      new Stock { Name = "Stock 2", Value =random.Next(55,300) },
                      new Stock { Name = "Stock 3", Value =random.Next(55,300)  },
                      new Stock { Name = "Stock 4", Value =random.Next(55,300) },
                      new Stock { Name = "Stock 5", Value =random.Next(55,300)  }
                  };
                this.IsRunning = false;
            }
        }
    }


    public class Stock
    {
        public string? Name { get; set; }
        public double Value { get; set; }
    }
}
