﻿#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using SampleBrowser.Maui.Core;
using Syncfusion.Maui.Scheduler;
using System;

namespace SampleBrowser.Maui.SfScheduler
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

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Scheduler.ViewChanged -= this.OnViewChanged;
            Scheduler.Handler?.DisconnectHandler();
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
                Scheduler.TimelineView.TimeIntervalWidth = 50;
            }
        }
    }
}