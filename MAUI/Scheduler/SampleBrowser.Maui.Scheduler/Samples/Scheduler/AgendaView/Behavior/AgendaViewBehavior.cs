#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    using Syncfusion.Maui.Scheduler;
    using Scheduler = Syncfusion.Maui.Scheduler.SfScheduler;

    internal class AgendaViewBehavior : Behavior<Scheduler>
    {
        /// <inheritdoc/>
        protected override void OnAttachedTo(Scheduler scheduler)
        {
            base.OnAttachedTo(scheduler);
            scheduler.ViewChanged += OnSchedulerViewChanged;
        }

        /// <summary>
        /// Invoks on scheduler view change and view swipe.
        /// </summary>
        /// <param name="sender">Ths scheduler.</param>
        /// <param name="e">The view changed event args.</param>
        private void OnSchedulerViewChanged(object? sender, SchedulerViewChangedEventArgs e)
        {
            if (e.NewView != SchedulerView.Agenda && sender is Scheduler scheduler)
            {
                scheduler.View = SchedulerView.Agenda;
            }
        }

        /// <inheritdoc/>
        protected override void OnDetachingFrom(Scheduler scheduler)
        {
            base.OnDetachingFrom(scheduler);
            scheduler.ViewChanged -= this.OnSchedulerViewChanged;
        }
    }
}

