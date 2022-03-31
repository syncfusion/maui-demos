#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Dispatching;

namespace SampleBrowser.Maui.SfCartesianChart
{
    internal class DynamicAnimationViewModel : BaseViewModel
    {
        private ObservableCollection<ChartDataModel> motionAnimation = new ObservableCollection<ChartDataModel>();
        public ObservableCollection<ChartDataModel> MotionAnimation
        {
            get { return motionAnimation; }
            set
            {
                motionAnimation = value;
                OnPropertyChanged("MotionAnimation");
            }
        }

        private bool canStopTimer;

        public DynamicAnimationViewModel()
        {
            var r = new Random();
            MotionAnimation = new ObservableCollection<ChartDataModel>();
            for (int i = 0; i < 7; i++)
            {
                MotionAnimation.Add(new ChartDataModel(i, r.Next(5, 90)));
            }
        }

        public void StopTimer()
        {
            canStopTimer = true;
        }

        public async void StartTimer()
        {
            await Task.Delay(500);
            if (Application.Current != null)
                Application.Current.Dispatcher.StartTimer(new TimeSpan(0, 0, 0, 2, 500), UpdateData);

            canStopTimer = false;
        }

        private bool UpdateData()
        {
            if (canStopTimer) return false;

            var r = new Random();
            var data = new ObservableCollection<ChartDataModel>();
            for (int i = 0; i < 7; i++)
            {
                data.Add(new ChartDataModel(i, r.Next(5, 90)));
            }

            MotionAnimation = data;

            return true;
        }
    }
}
