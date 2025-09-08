#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Calendar.SfCalendar
{
    using SampleBrowser.Maui.Base;
    using Syncfusion.Maui.Calendar;
    using Syncfusion.Maui.Buttons;

    internal class CalendarIdentifierBehavior : Behavior<SampleView>
    {
        /// <summary>
        /// Calendar view 
        /// </summary>
        private SfCalendar? calendar;

        /// <summary>
        /// The radio buttons collection.
        /// </summary>
        private IEnumerable<SfRadioButton>? radioButtons;

        /// <summary>
        /// Begins when the behavior attached to the view 
        /// </summary>
        /// <param name="bindable">bindable value</param>
        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
#if MACCATALYST
            HorizontalStackLayout desktopLayout = bindable.Content.FindByName<HorizontalStackLayout>("desktopCalendar");
            desktopLayout.IsVisible = true;
            Border border = bindable.Content.FindByName<Border>("desktopBorder");
            border.IsVisible = true;
            this.calendar = bindable.Content.FindByName<SfCalendar>("desktopIdentifier1");
            VerticalStackLayout optionView = bindable.Content.FindByName<VerticalStackLayout>("desktopOptionView");
            this.radioButtons = optionView.Children.OfType<SfRadioButton>();
#elif IOS
            Grid mobileLayout = bindable.Content.FindByName<Grid>("mobileCalendar");
            mobileLayout.IsVisible = true;
            Border border = bindable.Content.FindByName<Border>("mobileBorder");
            border.IsVisible = true;
            this.calendar = bindable.Content.FindByName<SfCalendar>("mobileIdentifier1");
            Border optionBorder = bindable.Content.FindByName<Border>("mobileOptionBorder");
            optionBorder.IsVisible = true;
            HorizontalStackLayout optionView = bindable.Content.FindByName<HorizontalStackLayout>("mobileOptionBorderView");
            this.radioButtons = optionView.Children.OfType<SfRadioButton>();
#elif ANDROID
            Grid mobileLayout = bindable.Content.FindByName<Grid>("mobileCalendar");
            mobileLayout.IsVisible = true;
            Border frame = bindable.Content.FindByName<Border>("mobileFrame");
            frame.IsVisible = true;
            this.calendar = bindable.Content.FindByName<SfCalendar>("mobileIdentifier");
            Border optionFrame = bindable.Content.FindByName<Border>("mobileOptionFrame");
            optionFrame.IsVisible = true;
            HorizontalStackLayout optionView = bindable.Content.FindByName<HorizontalStackLayout>("mobileOptionFrameView");
            this.radioButtons = optionView.Children.OfType<SfRadioButton>();
#else
            HorizontalStackLayout desktopLayout = bindable.Content.FindByName<HorizontalStackLayout>("desktopCalendar");
            desktopLayout.IsVisible = true;
            Border frame = bindable.Content.FindByName<Border>("desktopFrame");
            frame.IsVisible = true;
            this.calendar = bindable.Content.FindByName<SfCalendar>("desktopIdentifier");
            VerticalStackLayout optionView = bindable.Content.FindByName<VerticalStackLayout>("desktopOptionView");
            this.radioButtons = optionView.Children.OfType<SfRadioButton>();
#endif

            if (this.radioButtons != null)
            {
                foreach (var item in this.radioButtons)
                {
                    string? radioButtonText = item.Text.ToString();
                    //// Handle the is checked on xaml does not rendered correctly on windows.
                    if (this.calendar != null && string.Equals(radioButtonText, "Hijri"))
                    {
                        item.IsChecked = true;
                    }

                    item.StateChanged += OnRadioButton_StateChanged;
                }
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
                    item.StateChanged -= OnRadioButton_StateChanged;
                }

                this.radioButtons = null;
            }
        }

        /// <summary>
        /// Method for Radio button selection type changed.
        /// </summary>
        /// <param name="sender">Return the object</param>
        /// <param name="e">Event Arguments</param>
        private void OnRadioButton_StateChanged(object? sender, StateChangedEventArgs e)
        {
            if (e.IsChecked == false)
            {
                return;
            }

            string? radioButtonText = (sender as SfRadioButton)?.Text.ToString();
            if (calendar == null)
            {
                return;
            }

            this.calendar.MonthView.HeaderView.TextFormat = "ddddd";
            if (string.Equals(radioButtonText, "Gregorian"))
            {
                this.calendar.Identifier = Syncfusion.Maui.Calendar.CalendarIdentifier.Gregorian;
            }
            else if (string.Equals(radioButtonText, "Hijri"))
            {
                this.calendar.Identifier = Syncfusion.Maui.Calendar.CalendarIdentifier.Hijri;
            }
            else if (string.Equals(radioButtonText, "Korean"))
            {
                this.calendar.Identifier = Syncfusion.Maui.Calendar.CalendarIdentifier.Korean;
            }
            else if (string.Equals(radioButtonText, "Persian"))
            {
                this.calendar.Identifier = Syncfusion.Maui.Calendar.CalendarIdentifier.Persian;
            }
            else if (string.Equals(radioButtonText, "Taiwan"))
            {
                this.calendar.Identifier = Syncfusion.Maui.Calendar.CalendarIdentifier.Taiwan;
            }
            else if (string.Equals(radioButtonText, "Thai Buddhist"))
            {
                this.calendar.Identifier = Syncfusion.Maui.Calendar.CalendarIdentifier.ThaiBuddhist;
                this.calendar.MonthView.HeaderView.TextFormat = "ddd";
            }
            else if (string.Equals(radioButtonText, "UmAlQura"))
            {
                this.calendar.Identifier = Syncfusion.Maui.Calendar.CalendarIdentifier.UmAlQura;
            }
        }
    }
}