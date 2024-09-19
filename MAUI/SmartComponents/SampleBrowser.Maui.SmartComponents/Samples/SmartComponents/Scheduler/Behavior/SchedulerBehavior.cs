#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.SmartComponents.SmartComponents
{
    using SampleBrowser.Maui.Base;
    using System;
    using Syncfusion.Maui.Scheduler;
    using System.Collections.ObjectModel;
    using Syncfusion.Maui.Buttons;
    using Syncfusion.Maui.AIAssistView;

    /// <summary>
    /// Represents Class to access the scheduler control.
    /// </summary>
    public class SchedulerBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Holds the scheduler instance.
        /// </summary>
        private SfScheduler? scheduler;

        /// <summary>
        /// Holds the ai assistview instance.
        /// </summary>
        private SfAIAssistView? sfAIAssistView;

        /// <summary>
        /// Holds the button instance.
        /// </summary>
        private SfButton? aiButton;

        /// <summary>
        /// Animation for AI button.
        /// </summary>
        private Animation? animation;

        /// <summary>
        /// Holds the header view.
        /// </summary>
        private Border? headerView { get; set; }

        /// <summary>
        /// The scheduler behavior.
        /// </summary>
        public SchedulerBehavior()
        {
            animation = new Animation();
        }

        /// <summary>
        /// Begins when the behavior attached to the view.
        /// </summary>
        /// <param name="bindable">The bindable element.</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.scheduler = bindable.Content.FindByName<SfScheduler>("scheduler");
            this.sfAIAssistView = bindable.Content.FindByName<SfAIAssistView>("aiAssistView");
            this.aiButton = bindable.Content.FindByName<SfButton>("aibutton");
            this.headerView = bindable.Content.FindByName<Border>("headerView");
            this.aiButton.Clicked += OnClickToShowAssistView!;
            this.aiButton.Loaded += AiButton_Loaded;
            InitialAppointmentBooking();
        }

        /// <summary>
        /// The ai button clicked event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args</param>
        private void AiButton_Loaded(object? sender, EventArgs e)
        {
            this.StartAnimation();
        }

        /// <summary>
        /// Method to start animation for ai button.
        /// </summary>
        private void StartAnimation()
        {
            if (this.aiButton != null && this.animation != null)
            {
                var bubbleEffect = new Animation(v => this.aiButton.Scale = v, 1, 1.15, Easing.CubicInOut);
                var fadeEffect = new Animation(v => this.aiButton.Opacity = v, 1, 0.5, Easing.CubicInOut);

                animation.Add(0, 0.5, bubbleEffect);
                animation.Add(0, 0.5, fadeEffect);
                animation.Add(0.5, 1, new Animation(v => this.aiButton.Scale = v, 1.15, 1, Easing.CubicInOut));
                animation.Add(0.5, 1, new Animation(v => this.aiButton.Opacity = v, 0.5, 1, Easing.CubicInOut));

                animation.Commit(this.aiButton, "BubbleEffect", length: 1500, easing: Easing.CubicInOut, repeat: () => true);
            }
        }

        /// <summary>
        /// Method to stop animation for ai button.
        /// </summary>
        private void StopAnimation()
        {
            if (this.aiButton != null)
            {
                this.aiButton.AbortAnimation("BubbleEffect");
                this.aiButton.Scale = 1;
                this.aiButton.Opacity = 1;
            }
        }

        /// <summary>
        /// Method to get the initial appointments.
        /// </summary>
        /// <returns></returns>
        private void InitialAppointmentBooking()
        {
            var appointments = new ObservableCollection<SchedulerAppointment>();

            appointments.Add(new SchedulerAppointment()
            {
                StartTime = DateTime.Today.AddHours(15),
                EndTime = DateTime.Today.AddHours(15).AddMinutes(30),
                Subject = "General Check-Up",
                Location = "ABC hospital",
                Background = new SolidColorBrush(Color.FromArgb("#36B37B")),
                ResourceIds = new ObservableCollection<object>() { "1000" }
            });

            appointments.Add(new SchedulerAppointment()
            {
                StartTime = DateTime.Today.AddHours(10),
                EndTime = DateTime.Today.AddHours(10).AddMinutes(30),
                Subject = "Vaccinations",
                Location = "ABC hospital",
                Background = new SolidColorBrush(Color.FromArgb("#36B37B")),
                ResourceIds = new ObservableCollection<object>() { "1000" }
            });

            appointments.Add(new SchedulerAppointment()
            {
                StartTime = DateTime.Today.AddHours(9),
                EndTime = DateTime.Today.AddHours(9).AddMinutes(30),
                Subject = "Diagnostic report",
                Location = "ABC hospital",
                Background = new SolidColorBrush(Color.FromArgb("#8B1FA9")),
                ResourceIds = new ObservableCollection<object>() { "1001" }
            });

            appointments.Add(new SchedulerAppointment()
            {
                StartTime = DateTime.Today.AddHours(16),
                EndTime = DateTime.Today.AddHours(16).AddMinutes(30),
                Subject = "Diabetes",
                Location = "ABC hospital",
                Background = new SolidColorBrush(Color.FromArgb("#8B1FA9")),
                ResourceIds = new ObservableCollection<object>() { "1001" }
            });

            this.scheduler!.AppointmentsSource = appointments;
        }

        /// <summary>
        /// Method to open the popup view.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void OnClickToShowAssistView(object sender, EventArgs e)
        {
            this.StopAnimation();
            if (sfAIAssistView != null)
            {
                bool isVisible = !sfAIAssistView.IsVisible;
                sfAIAssistView.IsVisible = isVisible;
                this.headerView!.IsVisible = isVisible;
            }
        }

        /// <summary>
        /// On Detached method.
        /// </summary>
        /// <param name="bindable">The bindable element.</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);

            if (this.aiButton != null)
            {
                this.aiButton.Clicked -= OnClickToShowAssistView!;
                this.aiButton.Loaded -= AiButton_Loaded;
            }

            this.animation = null;
        }
    }
}