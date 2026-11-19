namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    using Microsoft.Maui.Controls;
    using Syncfusion.Maui.Scheduler;
    using Scheduler = Syncfusion.Maui.Scheduler.SfScheduler;

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
            if (e.OldView == SchedulerView.Agenda || e.OldView == SchedulerView.Month)
            {
                if (scheduler != null)
                    scheduler.DisplayDate = scheduler.DisplayDate.Date.AddHours(8).AddMinutes(50);
            }
        }

        protected override void OnDetachingFrom(Scheduler scheduler)
        {
            base.OnDetachingFrom(scheduler);
            scheduler.ViewChanged -= this.OnSchedulerViewChanged;
        }
    }
}