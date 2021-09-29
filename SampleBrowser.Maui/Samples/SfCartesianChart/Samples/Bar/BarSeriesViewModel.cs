using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfCartesianChart
{
    public class BarSeriesViewModel : BaseViewModel
    {
        public ObservableCollection<ChartDataModel> BarData1 { get; set; }

        public ObservableCollection<ChartDataModel> BarData2 { get; set; }
        public ObservableCollection<ChartDataModel> BarData3 { get; set; }
        public ObservableCollection<ChartDataModel> RundedBarData { get; set; }
        private ObservableCollection<ChartDataModel> motionAnimation;
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

        public BarSeriesViewModel()
        {
            RundedBarData = new ObservableCollection<ChartDataModel>()
            {
                new ChartDataModel("Boat", 9.872),
                new ChartDataModel("Walk", 5.237),
                new ChartDataModel("Plane", 9.0437),
                new ChartDataModel("Bike", 4.11),
                new ChartDataModel("Car", 8.43),
            };

            BarData1 = new ObservableCollection<ChartDataModel>
            {
                new ChartDataModel("Facebook", 4.119),
                new ChartDataModel("FB Messenger", 3.408),
                new ChartDataModel("WhatsApp", 2.979),
                new ChartDataModel("Instagram", 1.843),
                new ChartDataModel("Skype", 1.039),
                new ChartDataModel("Subway Surfers", 1.025),

            };

            BarData2 = new ObservableCollection<ChartDataModel>
            {
                new ChartDataModel("Egg", 2.2),
                new ChartDataModel("Fish", 2.4),
                new ChartDataModel("Misc", 3),
                new ChartDataModel("Tea", 3.1),
            };

            BarData3 = new ObservableCollection<ChartDataModel>
            {
                new ChartDataModel("Egg", 1.2),
                new ChartDataModel("Fish", 1.3),
                new ChartDataModel("Misc", 1.5),
                new ChartDataModel("Tea", 2.2),
            };

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
            Device.StartTimer(new TimeSpan(0, 0, 0, 2, 0), UpdateData);

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
