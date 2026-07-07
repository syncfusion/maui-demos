using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Scheduler;
using System;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    /// <summary>
    /// Interaction logic for SpecialTimeRegion.xaml
    /// </summary>
    public partial class SpecialTimeRegion : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialTimeRegion" /> class.
        /// </summary>
        public SpecialTimeRegion()
        {
            InitializeComponent();
            Scheduler.SelectableDayPredicate = (date) =>
            {
                if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                {
                    return false;
                }

                return true;
            };
        }

        private void OnViewChanged(object? sender, SchedulerViewChangedEventArgs args)
        {
            if (args.OldView == args.NewView)
            {
                return;
            }

            if (args.NewView == SchedulerView.TimelineDay)
            {
                Scheduler.TimelineView.TimeIntervalWidth = 150;
            }
            else
            {
                if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.MacCatalyst)
                {
                    this.Scheduler.TimelineView.TimeIntervalWidth = 60;
                }
                else
                {
                    this.Scheduler.TimelineView.TimeIntervalWidth = 50;
                }
            }
        }
    }
}