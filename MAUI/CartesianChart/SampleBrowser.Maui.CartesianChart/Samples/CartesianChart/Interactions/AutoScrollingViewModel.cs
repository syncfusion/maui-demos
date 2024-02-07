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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
    public class AutoScrollingViewModel : BaseViewModel
    {
        private DateTime Date;
        private int count;
        readonly Random random = new();
        private bool canStopTimer;
        
        public ObservableCollection<ChartDataModel> LiveChartData { get; set; }

        public AutoScrollingViewModel()
        {
            LiveChartData = new ObservableCollection<ChartDataModel>();
            
        }

        private bool UpdateVerticalData()
        {
            if (canStopTimer) return false;
            
            Date = Date.Add(TimeSpan.FromSeconds(1));
            LiveChartData.Add(new ChartDataModel(Date, random.Next(5, 48)));
            count = count + 1;
            return true;
        }

        public void StopTimer()
        {
            canStopTimer = true;
            count = 1;
        }

        public void StartTimer()
        {
            LiveChartData.Clear();
             Date = new DateTime(2019, 1, 1, 01, 00, 00);
            LiveChartData.Add(new ChartDataModel(Date, random.Next(5, 48)));
            LiveChartData.Add(new ChartDataModel(Date.Add(TimeSpan.FromSeconds(1)), random.Next(5, 48)));
            LiveChartData.Add(new ChartDataModel(Date.Add(TimeSpan.FromSeconds(2)), random.Next(5, 48)));
            Date = Date.Add(TimeSpan.FromSeconds(2));
            canStopTimer = false;
            count = 1;
            if (Application.Current != null)
                Application.Current.Dispatcher.StartTimer(new TimeSpan(0, 0, 0, 0,500), UpdateVerticalData);
        }
    }
}
