#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Scheduler;
    using Scheduler = Syncfusion.Maui.Scheduler.SfScheduler;

    internal class DragAndDropBehavior : Behavior<SampleView>
    {
        private Scheduler? scheduler;

        private List<RadioButton>? radioButtons;

        private int selectedIndex = 1;

        /// <inheritdoc/>
        protected override void OnAttachedTo(SampleView sampleView)
        {
            base.OnAttachedTo(sampleView);
            this.scheduler = sampleView.Content.FindByName<SfScheduler>("dragAndDrop");
            this.radioButtons = new List<RadioButton>
            {
                sampleView.Content.FindByName<RadioButton>("option1"),
                sampleView.Content.FindByName<RadioButton>("option2"),
                sampleView.Content.FindByName<RadioButton>("option3")
            };


            if (this.scheduler != null)
            {
                this.scheduler.DaysView.TimeInterval = new TimeSpan(0, 30, 0);  
                scheduler.AppointmentDrop += OnSchedulerAppointmentDrop;
            }

            if (this.radioButtons != null)
            {
                foreach (var item in this.radioButtons)
                {
                    item.CheckedChanged += OnRadioButton_CheckedChanged;
                }
            }
        }

        private void OnRadioButton_CheckedChanged(object? sender, CheckedChangedEventArgs e)
        {
            var radioButtonText = (sender as RadioButton)?.Content.ToString();

            if (scheduler == null)
            {
                return;
            }

            if (string.Equals(radioButtonText, "Default"))
            {
                selectedIndex = 0;
            }
            else if (string.Equals(radioButtonText, "15 minutes"))
            {
                selectedIndex = 1;
            }
            else if (string.Equals(radioButtonText, "30 minutes"))
            {
                selectedIndex = 2;
            }
        }

        private void OnSchedulerAppointmentDrop(object? sender, AppointmentDropEventArgs e)
        {
            if (scheduler != null && scheduler.View != SchedulerView.Month)
            {
                if (selectedIndex == 1)
                {
                    var dropTime = e.DropTime.Minute % 15;
                    e.DropTime = e.DropTime.AddMinutes(-dropTime + (dropTime <= 8 ? 0 : 15));
                }
                else if (selectedIndex == 2)
                {
                    var dropTime = e.DropTime.Minute % 30;
                    e.DropTime = e.DropTime.AddMinutes(-dropTime + (dropTime <= 15 ? 0 : 30));
                } 
            }
        }

        /// <inheritdoc/>
        protected override void OnDetachingFrom(SampleView sampleView)
        {
            base.OnDetachingFrom(sampleView);
            if (this.scheduler != null)
            {
                scheduler.AppointmentDrop -= OnSchedulerAppointmentDrop;
            }

            if (this.radioButtons != null)
            {
                foreach (var item in this.radioButtons)
                {
                    item.CheckedChanged -= OnRadioButton_CheckedChanged;
                }

                this.radioButtons = null;
            }

        }
    }
}