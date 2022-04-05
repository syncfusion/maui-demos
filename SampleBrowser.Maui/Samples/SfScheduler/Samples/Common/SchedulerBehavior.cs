#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion

using Microsoft.Maui.Controls;
using Syncfusion.Maui.Scheduler;
using Scheduler = Syncfusion.Maui.Scheduler.SfScheduler;

namespace SampleBrowser.Maui.SfScheduler
{
    internal class SchedulerBehavior : Behavior<Scheduler>
    {
        protected override void OnAttachedTo(Scheduler scheduler)
        {
            base.OnAttachedTo(scheduler);
            scheduler.ViewChanged += this.OnSchedulerViewChanged;
        }

        private void OnSchedulerViewChanged(object? sender, SchedulerViewChangedEventArgs e)
        {
            if (e.NewView == e.OldView)
            {
                return;
            }
            var scheduler = sender as Scheduler;
            if ((e.OldView == SchedulerView.Agenda || e.OldView == SchedulerView.Month))
            {
                if (scheduler != null && scheduler.SelectedDate != null)
                    scheduler.SelectedDate = scheduler.SelectedDate.Value.Date.AddHours(8).AddMinutes(50);
            }
        }

        protected override void OnDetachingFrom(Scheduler scheduler)
        {
            base.OnDetachingFrom(scheduler);
            scheduler.ViewChanged -= this.OnSchedulerViewChanged;
        }
    }
}

