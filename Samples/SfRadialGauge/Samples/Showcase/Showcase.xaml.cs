using Microsoft.Maui.Controls;
using SampleBrowser.Maui.Core;
using System;
using System.Threading.Tasks;


namespace SampleBrowser.Maui.SfRadialGauge
{
    public partial class Showcase : SampleView
    {
        private double seconds;
        private bool canStopTimer;
        public Showcase()
        {
            InitializeComponent();
            this.seconds = 0;
            StartTimer();
        }

        private void StopTimer()
        {
            canStopTimer = true;
        }

        private async void StartTimer()
        {
            await Task.Delay(500);
            Device.StartTimer(new TimeSpan(0, 0, 0, 1, 0), UpdateTick);
            canStopTimer = false;
        }

        private bool UpdateTick()
        {
            if (canStopTimer) return false;
            this.seconds += 0.2;
            if (this.seconds > 12)
            {
                this.secondsPointer.EnableAnimation = false;
                this.seconds = 0;
                this.secondsPointer.Value = seconds;
                this.secondsPointer.EnableAnimation = true;
                this.seconds += 0.2;
                this.secondsPointer.Value = seconds;
            }
            else
            {
                this.secondsPointer.Value = seconds;
            }
            return true;
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            StopTimer();
        }
         
        public override void OnExpandedViewDisappearing(Microsoft.Maui.Controls.View view)
        {
            base.OnExpandedViewDisappearing(view);
            StopTimer();
        }
    }

    public class ClockViewModel
    {
        private double hour;
        private double minute;

        public ClockViewModel()
        {
            var date = DateTime.Now;
            hour = date.Hour > 12 ? date.Hour - 12 : date.Hour;
            hour = hour + date.Minute / 60d;
            minute = date.Minute / 60d * 12d;
        }

        public double Hour
        {
            get
            {
                return hour;
            }
        }

        public double Minute
        {
            get
            {
                return minute;
            }
        }
    }
}