#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
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

    /// <summary>
    /// The calendar type behavior class
    /// </summary>
    internal class CalendarTypeBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// schedule initialize
        /// </summary>
        private SfScheduler? scheduler;

        /// <summary>
        /// The option view.
        /// </summary>
        private Grid? optionView;

        /// <summary>
        /// The radio buttons collection.
        /// </summary>
        private IEnumerable<RadioButton>? radioButtons;

        /// <summary>
        /// The calendar type view model
        /// </summary>
        private CalendarTypeViewModel? calendarTypeViewModel;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.scheduler = bindable.Content.FindByName<SfScheduler>("Scheduler");
            this.optionView = bindable.Content.FindByName<Grid>("optionView");
            this.calendarTypeViewModel = bindable.Content.FindByName<CalendarTypeViewModel>("calendarTypeViewModel");
            this.radioButtons = this.optionView.Children.OfType<RadioButton>();
            if (this.radioButtons != null)
            {
                foreach (var item in this.radioButtons)
                {
                    item.CheckedChanged += OnRadioButton_CheckedChanged;
                }
            }

            this.scheduler.ViewChanged += Scheduler_ViewChanged;
        }

        /// <summary>
        /// Method for Scheduler view changed.
        /// </summary>
        /// <param name="sender">The sender value.</param>
        /// <param name="args">The args value.</param>
        private void Scheduler_ViewChanged(object? sender, SchedulerViewChangedEventArgs args)
        {
            if (args.OldView == args.NewView || this.scheduler == null)
            {
                return;
            }

            if (args.NewView == SchedulerView.TimelineMonth || args.NewView == SchedulerView.TimelineDay)
            {
                this.scheduler.TimelineView.TimeIntervalWidth = 150;
            }
            else
            {
                if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.MacCatalyst)
                {
                    this.scheduler.TimelineView.TimeIntervalWidth = 80;
                }
                else
                {
                    this.scheduler.TimelineView.TimeIntervalWidth = 50;
                }
            }
        }

        /// <summary>
        /// Method for radio button checked changed.
        /// </summary>
        /// <param name="sender">return the object</param>
        /// <param name="e">Event Args</param>
        private void OnRadioButton_CheckedChanged(object? sender, CheckedChangedEventArgs e)
        {
            var radioButtonText = (sender as RadioButton)?.Content.ToString();

            if (scheduler == null || this.calendarTypeViewModel == null)
            {
                return;
            }

            if (string.Equals(radioButtonText, "Gregorian"))
            {
                this.scheduler.CalendarType = Syncfusion.Maui.Scheduler.CalendarType.Gregorian;
                this.calendarTypeViewModel.GetAppointments(this.calendarTypeViewModel.EnglishSubjects);
            }
            else if (string.Equals(radioButtonText, "Hebrew"))
            {
                this.scheduler.CalendarType = Syncfusion.Maui.Scheduler.CalendarType.Hebrew;
                this.calendarTypeViewModel.GetAppointments(this.calendarTypeViewModel.HebrewSubjects);
            }
            else if (string.Equals(radioButtonText, "Hijri"))
            {
                this.scheduler.CalendarType = Syncfusion.Maui.Scheduler.CalendarType.Hijri;
                this.calendarTypeViewModel.GetAppointments(this.calendarTypeViewModel.HijriSubjects);
            }
            else if (string.Equals(radioButtonText, "Japanese"))
            {
                this.scheduler.CalendarType = Syncfusion.Maui.Scheduler.CalendarType.Japanese;
                this.calendarTypeViewModel.GetAppointments(this.calendarTypeViewModel.JapaneseSubjects);
            }
            else if (string.Equals(radioButtonText, "Korean"))
            {
                this.scheduler.CalendarType = Syncfusion.Maui.Scheduler.CalendarType.Korean;
                this.calendarTypeViewModel.GetAppointments(this.calendarTypeViewModel.KoreanSubjects);
            }
            else if (string.Equals(radioButtonText, "Persian"))
            {
                this.scheduler.CalendarType = Syncfusion.Maui.Scheduler.CalendarType.Persian;
                this.calendarTypeViewModel.GetAppointments(this.calendarTypeViewModel.PersianSubjects);
            }
            else if (string.Equals(radioButtonText, "Taiwan"))
            {
                this.scheduler.CalendarType = Syncfusion.Maui.Scheduler.CalendarType.Taiwan;
                this.calendarTypeViewModel.GetAppointments(this.calendarTypeViewModel.TaiwanSubjects);
            }
            else if (string.Equals(radioButtonText, "ThaiBuddhist"))
            {
                this.scheduler.CalendarType = Syncfusion.Maui.Scheduler.CalendarType.ThaiBuddhist;
                this.calendarTypeViewModel.GetAppointments(this.calendarTypeViewModel.ThaiBudhhishtSubjects);
            }
            else if (string.Equals(radioButtonText, "UmAlQura"))
            {
                this.scheduler.CalendarType = Syncfusion.Maui.Scheduler.CalendarType.UmAlQura;
                this.calendarTypeViewModel.GetAppointments(this.calendarTypeViewModel.UmAlQuaraSubjects);
            }
        }

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);

            if (this.radioButtons != null)
            {
                foreach (var item in this.radioButtons)
                {
                    item.CheckedChanged -= OnRadioButton_CheckedChanged;
                }

                this.radioButtons = null;
            }

            if (this.optionView != null)
            {
                this.optionView = null;
            }

            if (this.calendarTypeViewModel != null)
            {
                this.calendarTypeViewModel = null;
            }

            if (this.scheduler != null)
            {
                this.scheduler.ViewChanged -= Scheduler_ViewChanged;
                this.scheduler = null;
            }
        }
    }
}