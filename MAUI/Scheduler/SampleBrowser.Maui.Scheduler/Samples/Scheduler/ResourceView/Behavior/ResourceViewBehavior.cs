#region Copyright Syncfusion Inc. 2001-2022.
// Copyright Syncfusion Inc. 2001-2022. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    using System;
    using Microsoft.Maui.Controls;
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Scheduler;

    internal class ResourceViewBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// the scheduler object.
        /// </summary>
        private SfScheduler? scheduler;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value.</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.scheduler = bindable.Content.FindByName<SfScheduler>("Scheduler");
            this.scheduler.ViewChanged += this.OnSchedulerViewChanged;
        }

        /// <summary>
        /// Invokes on scheduler view type changed.
        /// </summary>
        /// <param name="sender">The scheduler object.</param>
        /// <param name="e">The scheduler view changed event args.</param>
        private void OnSchedulerViewChanged(object? sender, SchedulerViewChangedEventArgs e)
        {
            if (e.OldView == e.NewView || this.scheduler == null)
            {
                return;
            }

            if (e.NewView == SchedulerView.TimelineMonth || e.NewView == SchedulerView.TimelineDay)
            {
                this.scheduler.TimelineView.TimeIntervalWidth = 150;
            }
            else
            {
                this.scheduler.TimelineView.TimeIntervalWidth = 50;
            }
        }

        /// <summary>
        /// Begins when the behavior detaching from the view. 
        /// </summary>
        /// <param name="bindable">bindable value.</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);
            if (this.scheduler != null)
            {
                this.scheduler.ViewChanged -= this.OnSchedulerViewChanged;
                this.scheduler = null;
            }
        }
    }
}
