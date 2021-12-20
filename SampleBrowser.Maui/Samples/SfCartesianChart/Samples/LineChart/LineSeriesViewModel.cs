using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfCartesianChart
{
    public class LineSeriesViewModel : BaseViewModel
    {
        private bool canStopTimer;

        public ObservableCollection<ChartDataModel> LineData1 { get; set; }

        public ObservableCollection<ChartDataModel> LineData2 { get; set; }

        public ObservableCollection<ChartDataModel> DashedLine { get; set; }

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

        public LineSeriesViewModel()
        {
            
            LineData1 = new ObservableCollection<ChartDataModel>
            {
                new ChartDataModel("2005", 21),
                new ChartDataModel("2006", 24),
                new ChartDataModel("2007", 36),
                new ChartDataModel("2008", 38),
                new ChartDataModel("2009", 54),
                new ChartDataModel("2010", 57),
                new ChartDataModel("2011", 70)
            };

            LineData2 = new ObservableCollection<ChartDataModel>
            {
                new ChartDataModel("2005", 28),
                new ChartDataModel("2006", 44),
                new ChartDataModel("2007", 48),
                new ChartDataModel("2008", 50),
                new ChartDataModel("2009", 66),
                new ChartDataModel("2010", 78),
                new ChartDataModel("2011", 84)
            };

            DashedLine = new ObservableCollection<ChartDataModel>()
            {
                new ChartDataModel(2010, 6.6, 9.0, 15.1, 18.8),
                new ChartDataModel(2011, 6.3, 9.3, 15.5, 18.5),
                new ChartDataModel(2012, 6.7, 10.2, 14.5, 17.6),
                new ChartDataModel(2013, 6.7, 10.2, 13.9, 16.1),
                new ChartDataModel(2014, 6.4, 10.9, 13, 17.2),
                new ChartDataModel(2015, 6.8, 9.3, 13.4, 18.9),
                new ChartDataModel(2016, 7.7, 10.1, 14.2, 19.4),
            };

            var r = new Random();
            MotionAnimation = new ObservableCollection<ChartDataModel>();
            for (int i = 0; i < 10; i++)
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
            for (int i = 0; i < 10; i++)
            {
                data.Add(new ChartDataModel(i, r.Next(5, 90)));
            }

            MotionAnimation = data;

            return true;
        }

    }
}
