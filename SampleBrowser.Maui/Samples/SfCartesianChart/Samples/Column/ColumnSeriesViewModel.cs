using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfCartesianChart
{
    public class ColumnSeriesViewModel : BaseViewModel
    {
        private bool canStopTimer;
        public ObservableCollection<ChartDataModel> ColumnData1 { get; set; }
        public ObservableCollection<ChartDataModel> RoundedColumnData { get; set; }
        public ObservableCollection<ChartDataModel> OlympicMedals { get; set; }


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

        public ObservableCollection<Brush> OlympicColorModel { get; set; }

        public ColumnSeriesViewModel()
        {
            ColumnData1 = new ObservableCollection<ChartDataModel>
            {
                new ChartDataModel("China", 0.541),
                new ChartDataModel("Egypt", 0.818),
                new ChartDataModel("Bolivia", 1.51),
                new ChartDataModel("Mexico", 1.302),
                new ChartDataModel("Brazil", 2.017)

           };

            RoundedColumnData = new ObservableCollection<ChartDataModel>()
            {
                new ChartDataModel(1, 25),
                new ChartDataModel(2, 49),
                new ChartDataModel(3, 28),
                new ChartDataModel(4, 14),
                new ChartDataModel(5, 32),
                new ChartDataModel(6, 51),
                new ChartDataModel(7, 45),
                new ChartDataModel(8, 60),
                //new ChartDataModel(9, 54),
                //new ChartDataModel(10, 18),
                //new ChartDataModel(11, 55),
                //new ChartDataModel(12, 22),
            };

            OlympicMedals = new ObservableCollection<ChartDataModel>()
            {
                new ChartDataModel("Germany", 128, 129, 101),
                new ChartDataModel("Russia", 123, 92,  93),
                new ChartDataModel("Norway", 107, 106,  90),
                new ChartDataModel("USA", 87, 95,  71),
            };

            OlympicColorModel = new ObservableCollection<Brush>()
            {
                new SolidColorBrush(Color.FromArgb("#FFD700")),
                new SolidColorBrush(Color.FromArgb("#C0C0C0")),
                new SolidColorBrush(Color.FromArgb("#CD7F32")),
            };

            var r = new Random();
            MotionAnimation = new ObservableCollection<ChartDataModel>();
            for(int i= 0; i<7;i++)
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
            StopTimer();
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
