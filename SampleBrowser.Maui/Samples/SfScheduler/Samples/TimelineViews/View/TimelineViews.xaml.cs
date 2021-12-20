#region Copyright Syncfusion Inc. 2001-2021.
// Copyright Syncfusion Inc. 2001-2021. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Scheduler;

namespace SampleBrowser.Maui.SfScheduler
{
    /// <summary>
    /// Interaction logic for TimelineViews.xaml
    /// </summary>
    public partial class TimelineViews : SampleView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineViews" /> class.
        /// </summary>
        public TimelineViews()
        {
            InitializeComponent();
        }

        private void OnViewChanged(object sender, SchedulerViewChangedEventArgs args)
        {
            if (args.NewView == SchedulerView.TimelineMonth)
            {
                Scheduler.TimelineViewSettings.TimeIntervalWidth = 150;
            }
            else if (!Scheduler.TimelineViewSettings.TimeIntervalWidth.Equals(double.NaN))
            {
                Scheduler.TimelineViewSettings.TimeIntervalWidth = double.NaN;
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Scheduler.Handler?.DisconnectHandler();
        }
    }
}
